# ShorcutShell

This application provides a customizable shortcut shell from terminal

## System Commands
|System Command|Syntax|Description|
|-------|------|-----------|
|"add"|add CommandName -p "CommandPath" -d "Dettached"|Add new command to the system|
|"remove"|remove "CommandName"|Delete a command from the system|
|"list"|list|List registered commands|
|"close"|close|If any command is in execution, closes it|
|"clear"|clear|Clear the window|

## System arguments

	-p "path" -> Indicate the path of the binary
	-d "true/false" -> Indicate if the command should be attached to shell or not

## Usage Commands
|Command|Syntax|Description|
|-------|------|-----------|
|"CommandName"|"CommandName" "Argumments"|Execute selected command with the command argummens|

## Build Status
||AppVeyor|Code Factor|Codacy|
|--|--------|-----------|------|
|master|[![Build status](https://ci.appveyor.com/api/projects/status/iemq9gog0qg61vp6/branch/master?svg=true)](https://ci.appveyor.com/project/kabestrus/shortcutshell/branch/master)|[![CodeFactor](https://www.codefactor.io/repository/github/jorturfer/shortcutshell/badge)](https://www.codefactor.io/repository/github/jorturfer/shortcutshell)|[![Codacy Badge](https://api.codacy.com/project/badge/Grade/fbbf34a266c04b208e363dec32612a99?branch=master)](https://www.codacy.com/app/JorTurFer/ShortcutShell?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=JorTurFer/ShortcutShell&amp;utm_campaign=Badge_Grade)|
|develop|[![Build status](https://ci.appveyor.com/api/projects/status/iemq9gog0qg61vp6/branch/develop?svg=true)](https://ci.appveyor.com/project/kabestrus/shortcutshell/branch/develop)|[![CodeFactor](https://www.codefactor.io/repository/github/jorturfer/shortcutshell/badge/develop)](https://www.codefactor.io/repository/github/jorturfer/shortcutshell/overview/develop)|[![Codacy Badge](https://api.codacy.com/project/badge/Grade/fbbf34a266c04b208e363dec32612a99?branch=develop)](https://app.codacy.com/project/JorTurFer/ShortcutShell/dashboard?branchId=7684523)|
