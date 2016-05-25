# Excel LaunchPad
Now free and open source! :boom: :tada:

## What is it?
Excel LaunchPad adds a right-click menu option to Excel-compatibile files which opens the file in its own instance of Excel. That's it.  

![Context Menu][3]

## That's it? Why would I want that?
Before Excel 2013, the default behavior was to reuse running Excel instances whenever a new Excel file was opened. But sometimes that's not what you want. Sometimes, you want to open files in a *new* instance of Excel. There are workarounds, of course; you can manually launch a new instance of Excel and then open the desired file. But it's just easier to right-click the file, select `Open in new window`, and be done with it.

However! As of Excel 2013, the default behavior has been changed such that all Excel files are opened in their own instances of Excel - just like Word has done, pretty much forever. So if you're using Excel 2013 or later, Excel LaunchPad is *not* for you. :-)

## How do I get it?

### Prerequisites

1. Windows XP or later
2. Microsoft Excel

### Installation

1. Head on over to the [Releases][1] page and download the latest zip file. (Download the file named: `ExcelLaunchPad.zip`. You do **not** need any of the files that say "Source code.")
2. Extract the zip file to any location you desire on your local computer.
3. Go to the folder where you extracted all the files, find `ExcelLaunchPadInstaller.exe`, and double click it. You'll be presented with a window like the one below.
  - **NOTE:** Admin rights are required. Depending on your account permissions, you may be presented with a UAC dialog (see below) and/or asked to enter an administrator's passowrd.
4. Press `1` to install.
  - **NOTE:** If you do not already have .NET 4.5.2 installed, you will be prompted to download and install it. It's a pretty painless process and shouldn't require a restart.

That's it!

If - after you install - you decide to move Excel LaunchPad files to another location on your computer, no problem, but **remember** you **must** re-run the installer, otherwise LaunchPad will stop working.

**Click Yes Here...**

![UAC Dialog][4]

**Click Yes Here Too...**

![.NET Prompt][5]

**Press 1 To Install Or 2 To Remove...**

![Installer Window][2]

## How to Uninstall
1. Go to the folder where `ExcelLaunchPadInstaller.exe` lives. Double click it.
2. Press `2`.
3. That's it! Excel LaunchPad is uninstalled. You may now delete all the files in the folder.

## Feedback? Bugs?
Please [open an issue](https://github.com/refactorsaurusrex/ExcelLaunchPad/issues)! I'll respond when I can.

[1]: https://github.com/refactorsaurusrex/ExcelLaunchPad/releases
[2]: https://raw.githubusercontent.com/refactorsaurusrex/ExcelLaunchPad/master/Images/InstallerWindow.png
[3]: https://raw.githubusercontent.com/refactorsaurusrex/ExcelLaunchPad/master/Images/ExcelLaunchPadContextMenu.png
[4]: https://raw.githubusercontent.com/refactorsaurusrex/ExcelLaunchPad/master/Images/UACDialog.png
[5]: https://raw.githubusercontent.com/refactorsaurusrex/ExcelLaunchPad/master/Images/DotNetInstallerPrompt.png
