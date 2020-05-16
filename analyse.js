const fs = require("fs");
const path = require("path");

const fname =
  process.argv[2] || path.resolve(__dirname, "examples", "sokoban", "sokoban");
const rom_fname = `${fname}.gb`;
const sym_fname = `${fname}.sym`;

const sym = fs.readFileSync(sym_fname, "utf8");
const rom = fs.readFileSync(rom_fname);

const readByte = offset => rom[offset];

const readBytes = (offset, num) => {
  const res = [];
  for (let i = 0; i < num; i++) {
    res.push(rom[i + offset]);
  }
  return res;
};

const readEnum = (offset, values = {}) => {
  const byte = readByte(offset);
  return byte in values ? values[byte] : byte;
};

const ascii = bytes =>
  bytes
    .filter(n => n !== 0)
    .map(n => String.fromCharCode(n))
    .join("");

const readAscii = (offset, num) => ascii(readBytes(offset, num));

// build map of addr->symbol{name,addr,count}
const sym_lines = sym.split("\n");
const symbols = {};
for (let i = 0; i < sym_lines.length; i++) {
  const line = sym_lines[i].trim();
  if (line[0] === ";" || line === "") continue;
  const [rawAddr, name] = line.split(" ");
  const addr = rawAddr.slice(3).toLowerCase();
  symbols[addr] = { name, addr, count: 0 };
}

// count used space
let used_size = -1;
for (let i = rom.length - 1; i >= 0; i--) {
  const byte = rom[i];
  if (byte !== 0xff && byte !== 0x00) {
    used_size = i;
    break;
  }
}

// count the CALL (0xcd) to symbols
for (let i = 0; i < rom.length; i++) {
  const byte = rom[i];

  if (byte === 0xcd) {
    const lo = rom[i + 1];
    const hi = rom[i + 2];
    const addr = `${hi.toString(16).padStart(2, "0")}${lo
      .toString(16)
      .padStart(2, "0")}`;
    const symbol = symbols[addr];
    if (symbol) {
      symbol.count++;
    }
  }
}

const calls = Object.values(symbols);
calls.sort((a, b) => a.count - b.count);

calls.forEach(call => console.log({ word: call.name, calls: call.count }));

console.log("------------------------------");

// console.log({
//   title: readAscii(0x0134, 15),
//   manufacturer: readAscii(0x13f, 4),
//   maker_code: readAscii(0x0144, 2),
//   color: readEnum(0x0143, {
//     0x00: "<CGB INCOMPATIBLE>",
//     0x80: "<CGB COMPATIBLE>",
//     0xc0: "<CGB EXCLUSIVE>"
//   }),
//   super: readEnum(0x0146, { 0x03: "<SGB ENABLED>", 0x00: "<SGB DISABLED>" }),
//   cart_type: readEnum(0x0147),
//   rom_size: readEnum(0x0148),
//   ram_size: readEnum(0x0149),
//   destination: readEnum(0x014a, { 0: "<JAPAN>", 1: "<OTHER>" }),
//   licensee_code: readEnum(0x014b, { 0x33: "<USE MAKER CODE>" }),
//   rom_version: readByte(0x014c)
// });
//
// console.log("------------------------------");

console.log("title:", readAscii(0x0134, 15));

const kb_rom = Math.floor(rom.length / 102.4) / 10;
const kb_used = Math.floor(used_size / 102.4) / 10;
const kb_pad = Math.floor((rom.length - used_size) / 102.4) / 10;

console.log("used: ", kb_used, "/", kb_rom, "kB");
console.log("free: ", kb_pad, "kB");

/*
aces up:

{ word: 'swap', calls: 22 }
{ word: '!', calls: 24 }        >>>>>>>>>> CORE
{ word: 'size', calls: 25 }
{ word: '@', calls: 35 }        >>>>>>>>>> CORE
{ word: '+', calls: 46 }        >>>>>>>>>> CORE
{ word: 'at-xy', calls: 46 }    >>> LIB
{ word: 'c!', calls: 55 }       >>>>>>>>>> CORE
{ word: 'emit', calls: 73 }     >>> LIB

sokoban:

{ word: 'swap', calls: 20 }
{ word: 'rocks', calls: 20 }
{ word: '@', calls: 20 }        >>>>>>>>>> CORE
{ word: 'over', calls: 24 }     >>>>>>>>>> CORE
{ word: 'drop', calls: 30 }     >>>>>>>>>> CORE
{ word: 'play-rule', calls: 48 }
{ word: 'soko', calls: 51 }
{ word: 'exit', calls: 52 }     >>>>>>>>>> CORE
{ word: 'r@', calls: 59 }       >>>>>>>>>> CORE
{ word: 'r>', calls: 67 }       >>>>>>>>>> CORE
{ word: '+!', calls: 91 }       >>>>>>>>>> CORE

10-print:

{ word: '@', calls: 4 }         >>>>>>>>>> CORE
{ word: 'xor', calls: 4 }       >>>>>>>>>> CORE
{ word: 'c!', calls: 5 }        >>>>>>>>>> CORE
{ word: 'dup', calls: 5 }       >>>>>>>>>> CORE
{ word: '+', calls: 5 }         >>>>>>>>>> CORE
{ word: '!', calls: 6 }         >>>>>>>>>> CORE
{ word: 'swap', calls: 6 }

brainfuck:

{ word: 'c!', calls: 7 }        >>>>>>>>>> CORE
{ word: '@', calls: 8 }         >>>>>>>>>> CORE
{ word: '+', calls: 11 }        >>>>>>>>>> CORE
{ word: 'over', calls: 11 }     >>>>>>>>>> CORE
{ word: '=', calls: 12 }        >>>>>>>>>> CORE
{ word: 'drop', calls: 13 }     >>>>>>>>>> CORE
{ word: '!', calls: 16 }        >>>>>>>>>> CORE

simon:

{ word: 'swap', calls: 14 }
{ word: 'at-xy', calls: 14 }    >>> LIB
{ word: 'over', calls: 15 }     >>>>>>>>>> CORE
{ word: 'c!', calls: 16 }       >>>>>>>>>> CORE
{ word: '=', calls: 16 }        >>>>>>>>>> CORE
{ word: 'drop', calls: 17 }     >>>>>>>>>> CORE

*/
