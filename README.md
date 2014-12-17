ConfigureAwaitChecker
=====================

* [Checking for ConfigureAwait(false) automatically](http://blog.cincura.net/233476-checking-for-configureawait-false-automatically)
  was developed originally by _Jiří {x2} Činčura_. The original repo is [here](https://github.com/cincuranet/ConfigureAwaitChecker).
  
* This tool can be called from Visual Studio to check all C# source files found in a directory
  and the generated output is compatible with the VS Output Window format. Clicking on a warning
  will jump to the file and position the cursor on the 'await' keyword.
