using System.IO;
using System.Linq;
using ConfigureAwaitChecker.Lib;

namespace ConfigureAwaitChecker
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Count () != 1)
			{
				return ExitCodes.TooFewArguments;
			}

			var path = args[0];

			if (string.IsNullOrWhiteSpace (path))
			{
				return ExitCodes.ArgumentEmpty;
			}

			while (path.EndsWith ("\\"))
			{
				path = path.Substring (0, path.Length-1);
			}

			if (Directory.Exists (path))
			{
				int result = ExitCodes.OK;

				foreach (var file in Directory.GetFiles (path, "*.cs", SearchOption.AllDirectories))
				{
					var error = Program.Process (file);
					if (error != ExitCodes.OK)
					{
						result = error;
					}
				}

				return result;
			}
			else
			{
				return Program.Process (path);
			}
		}
		
		static int Process(string path)
		{
			if (!File.Exists (path))
			{
				return ExitCodes.FileNotFound;
			}

			var result  = ExitCodes.OK;
			var checker = new Checker (path);
			
			foreach (var item in checker.Check ())
			{
				if (!item.HasConfigureAwaitFalse)
				{
					System.Console.WriteLine ("{0}({1},{2},{1},{3}): warning PA1000: Found 'await' without 'ConfigureAwait(false)'", path, item.Line+1, item.Column+1, item.Column+6);
					result = ExitCodes.Error;
				}
			}

			return result;
		}
	}
}