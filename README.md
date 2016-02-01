# csv-to-txf-converter
Automatically exported from code.google.com/p/csv-to-txf-converter

The original project has the following info:
---------
After attempting to import my equity trades into Intuit’s TurboTax 2010 Home and Business edition, I discovered that  support for importing CSV files has been removed. So, I decided to roll my own program.

You can download the source here or download the Windows executable here. Please note, the .NET 4.0 framework is required. After executing the program, you need to enter in the full file path [directory and file name] in the file text box and the full path for the directory where you want the file to be created. A ‘TXF’ file should be created afterwards in the specified location.

You can import the TXF file in TurboTax 2010 by opening your current tax return and selecting File -> Import -> From Accounting Software. Check the details before finishing the import to make sure everything looks right.

If you want more details about the TXF file format or the CSV reader I used in the program. Please check out the following links.

TXF Format
LumenWorks CSV Reader

 I’ve only tested the TXF files from this program on Turbo Tax 2010 Business and Home edition. This software is provided without warranty or support.

This entry was posted in Uncategorized on February 22, 2011 by Alex.
---------

This is a simple and quick program I used to convert an Excel CSV file from ThinkOrswim (Penson Financial) and Ameritrade into TXF format. The format can then be imported into Turbo Tax
The program is written in C# using the .NET 4.0 framework and LumenWorks.Framework.IO.Csv library to read CSV files.

This program is distributed under the BSD license.
