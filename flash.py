#! /usr/bin/env python3

import sys
import string

import usb.core
import usb.util

Command_Get_Version =   [0x00]
#Command_Get_ROM =       [0x10]
#Command_Set_Bank =      [0x08]
#Command_Flash_ROM =      [0x20]

#ROMsize=0
#RAMsize=0
#ROMbuffer=""
#RAMbuffer=""
#USBbuffer=""
#FlashBlockSize=0

def SDID_Read():
    dev.write(0x01,[0x80])
    USBbuffer = dev.read(0x81,64)
    A=(USBbuffer[0])+ (USBbuffer[1]<<8) + (USBbuffer[2]<<16) + (USBbuffer[3]<<24)
    B=(USBbuffer[4]) + (USBbuffer[5]<<8) + (USBbuffer[6]<<16) + (USBbuffer[7]<<24)
    C=(USBbuffer[8]) + (USBbuffer[9]<<8)+(USBbuffer[10]<<16) + (USBbuffer[11]<<24)
    D=str(hex(A))+","+str(hex(B))+","+str(hex(C))
    return ( D)

def main_CheckVersion():
    dev.write(0x01,Command_Get_Version)
    dat = dev.read(0x81,64)
    sdat=""
    for x in range(5):
        sdat=sdat+chr(dat[x])
    D=(SDID_Read())
    print("Firmware "+sdat+ " Device ID: "+ D)

def main_ELCheapo_Read(Address):
    AddHi=Address>>8
    AddLo=Address&0xFF
    dev.write(0x01,[0x10,0x00,0x00,AddHi,AddLo])
    ROMbuffer= dev.read(0x81,64)
    return (ROMbuffer)

def main_readCartHeader():
    #main_BV_SetBank(0,0)
    #main_ROMBankSwitch(1)
    RAMtypes = [0,2048,8192,32768,(32768*4),(32768*2)]
    global ROMsize
    global RAMsize
    Header=""
    dat = main_ELCheapo_Read(0x0100) #start of logo
    Header=dat
    dat = main_ELCheapo_Read(0x0140)
    Header+=dat
    dat = main_ELCheapo_Read(0x0180)
    Header+=dat #Header contains 0xC0 bytes of header data
    ROMsize=(32768*( 2**(Header[0x48])))
    print("ROM Title: "+str(Header[0x34:0x43],'utf-8'))
    print("ROM Makercode: "+str(Header[0x44:0x46],'utf-8'))
    print("ROM Gamecode: "+str(Header[0x3F:0x43],'utf-8'))
    print("ROM Market: "+str(['Japan','International'][Header[0x4A]]))

    print("ROM Size: "+str (32768*( 2**(Header[0x48]))))
    RAMsize=RAMtypes[Header[0x49]]
    print("RAM Size:"+str(RAMsize))
    print("Mapper: ?????????")

def main_Catskull_erase():
    print ("Erasing...")
    dev.write(0x01,[0x0A,0x01,0x06,0x55,0x55,0xAA,0x2A,0xAA,0x55,0x55,0x55,0x80,0x55,0x55,0xAA,0x2A,0xAA,0x55,0x55,0x55,0x10])
    RAMbuffer= dev.read(0x81,64)
    while main_ELCheapo_Read(0)[0]!=0xFF:
        pass
    print ("Erased")

def main_LoadROM(ROMfileName):
    global ROMsize
    global ROMbuffer
    if ROMfileName:
        ROMfile=open(ROMfileName,'rb')
        ROMbuffer=ROMfile.read()
        ROMsize=len(ROMbuffer)
        ROMfile.close()
        return(1)
    return(0)

def main_Catskull_write(ROMfileName):
    if main_LoadROM(ROMfileName) == 1:
        main_Catskull_erase()
        print('Writing ROM Data')
        print('[' + ' ' * 32 + ']', end='\r[', flush=True)
        ROMpos=0
        for ROMAddress in range (0x0000,0x8000,1):
            AddHi=ROMAddress>>8
            AddLo=ROMAddress&0xFF
            Data1Byte=ROMbuffer[ROMpos:ROMpos+1]
            dev.write(0x01,[0x0A,0x01,0x04,0x55,0x55,0xAA,0x2A,0xAA,0x55,0x55,0x55,0xA0,AddHi,AddLo,Data1Byte[0]])
            ROMpos+=1
            if ROMpos % 1024 == 0:
                print('=', end='', flush=True)
        print('\nOperation Complete','Writing Complete.')
    return


dev = usb.core.find(idVendor=0x046d, idProduct=0x1234)
if dev is None:
    print("USB Error")
    print("I Cant find your hardware! Check the device is plugged in and the USB driver is installed")
    exit()
if dev is not None:
    dev.set_configuration()
    print("Welcome")
    print("Gen3 is a work in progress, please report any bugs or requests to Bennvenn@hotmail.com")
    main_CheckVersion()
    if (len(sys.argv) == 2):
        # print("asd")
        main_Catskull_write(sys.argv[1])
    else:
        main_readCartHeader()
