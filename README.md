ChurchView2EasyWorship
======================

This is a dinky little program I made because I was in charge of our church's transition from ChurchView to EasyWorship and there is not an existing free software solution for this problem.

Note to owners of the above software: this software was independently developed and I am not aware of any copyright or trademark infringement within the program or this text. However, if you have any concerns, please contact me directly to discuss them.

Input and Output
----------------

ChurchView2EasyWorship takes as input an Access database conforming to the ChurchView database schema (version 3.1) and produces as output a folder of text files named after the songs in the given database. The content of each text file is a CCLI-formatted representation of that song.

This output folder can be imported directly into EasyWorship using the CCLI import function.

Running
-------

The following are required to run this program:

  * .NET Framework 4
  * (Possibly) Microsoft Access

To run it, open a command line and navigate to the folder that holds the .exe file. Execute the following command:

    ChurchView2EasyWorship.exe Database.mdb "Folder Name"

Where the first argument is the song database file name and the second one is the name of the folder you want the files to end up in. Both arguments are optional and default to "Songs.mdb" and "SongsOutput", respectively. There is no option to specify the folder without also specifying the database.

Normalization
-------------

During song transfer, the song lyrics are also put through a case normalization process. The process is as follows for each line of the song:

  1. The line is trimmed of excess white space.
  1. The entire line is converted to lower-case.
  1. The first letter is converted to upper-case.
  1. The following words are capitalized wherever and in whatever form they appear: Jesus, God, King, I, Lord, You, Him, Spirit, Zion, King of Kings, Lord of Lords, Ancient of Days, Jireh, Nissi, Shalom, Rock of Ages, and Emmanuel. This list can be changed in the .config file that comes with the executable.

Development
-----------

This solution requires Visual Studio 2010 to open. Maybe it works in Mono, maybe it doesn't. YMMV.

