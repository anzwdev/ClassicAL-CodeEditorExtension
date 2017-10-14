# ClassicAL-CodeEditorExtension

This project adds 2 new features to the standard code editor in classic Microsoft Dynamics Nav Developent Environment:
- code snippets
- xml documentation tags support

To install it, go to the directory where your RTC is installed and copy AnZw.NavCodeEditor.Extensions.dll library into Add-ins\CodeViewer\EditorComponents folder. 
(full path should be C:\Program Files (x86)\Microsoft Dynamics NAV\110\RoleTailored Client\Add-ins\CodeViewer\EditorComponents if you installed nav with default settings)

To insert xml documenation tag, you should be in the first line of function body and that line should be empty. Just type /// and it should insert comment lines with xml nodes for all function parameters

There are 2 keyboard shortcust available while you are inside code editor:
    Ctrl+Shift+E opens extension settings
    Ctrl+Shift+T opens list of available snippets
It is also possible to assign keyboard shortcuts to each snippet in extension settings.

Extension adds available snippets to the second tab on intellisense drop down, because I was not able to merge them with standard dynamics nav list of available functions and variables.

If you want to build extension from source code, remember to remove all references to Microsoft.VisualStudio.*.dll libraries from AnZw.NavCodeEditor.Extensions project and add them again using files from C:\Program Files (x86)\Microsoft Dynamics NAV\110\RoleTailored Client\Add-ins\CodeViewer\CommonComponents. If you don't do it, Visual Studio will use libraries that are installed by Visual Studio, but because Dynamics Nav references older version, compiled extension will crash Nav and it won't be possible to open code editor at all.

Source code contains 2 project:
- AnZw.NavCodeEditor.Extensions - code editor extension
- AnZw.NavCodeEditor.Setup - small executable that references AnZw.NavCodeEditor.Extensions library and opens extension settings window. It is here just in case of some problems, because the only way to open extension settings in Nav is to go to code editor and press Ctrl+Shift+E, so if that shortcut does not work anymore because of broken settings, you can always use that executable to reset them.

 





