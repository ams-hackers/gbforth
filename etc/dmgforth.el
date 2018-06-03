;;; dmgforth.el --- Extensions to work with dmg-forth  -*- lexical-binding: t; -*-

;; Keywords: languages, extensions

;; This program is free software; you can redistribute it and/or modify
;; it under the terms of the GNU General Public License as published by
;; the Free Software Foundation, either version 3 of the License, or
;; (at your option) any later version.

;; This program is distributed in the hope that it will be useful,
;; but WITHOUT ANY WARRANTY; without even the implied warranty of
;; MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
;; GNU General Public License for more details.

;; You should have received a copy of the GNU General Public License
;; along with this program.  If not, see <http://www.gnu.org/licenses/>.

;;; Code:

(require 'forth-mode)

(defvar dmgforth-defining-words
  '("simple-instruction" "instruction" "label" "newlabel" "presume" "export"))

(dolist (w dmgforth-defining-words)
  (forth-syntax--define w #'forth-syntax--state-defining-word))

(defvar dmgforth-keywords
  '("begin-dispatch" "end-dispatch" "~~>" "::"
    "end-instruction" "tile" "local" "end-local"
    "begin," "while," "repeat," "until," "if," "else," "then,"))

(dolist (w dmgforth-keywords)
  (forth-syntax--define w #'forth-syntax--state-font-lock-keyword))

(provide 'dmgforth)
;;; dmgforth.el ends here
