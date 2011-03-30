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

Development
-----------

This solution requires Visual Studio 2010 to open. Maybe it works in Mono, maybe it doesn't. YMMV.

