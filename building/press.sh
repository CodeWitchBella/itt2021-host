#!/bin/zsh

create-dmg --volname "Host" --icon-size 100 --window-pos 200 120 --window-size 800 400 --icon Host.app 200 190 --hide-extension Host.app --background background.png --app-drop-link 600 185 host-$1.dmg host
