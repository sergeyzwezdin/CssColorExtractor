using System.Configuration;
using System.IO;
using System.Linq;
using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Core.Extractor;
using CssColorExtractor.Core.FilesProcessing;
using CssColorExtractor.Core.Map;
using CssColorExtractor.Less;
using CssColorExtractor.Less.ColorModifier;
using System;

namespace CssColorExtractor
{
	class Program
	{
		static void Main(string[] args)
		{
			Welcome();

			if (args.Length < 1)
			{
				Help();
				return;
			}

			string argsPath = args[0].TrimEnd(new[] { '/', '\\', '"' });
			string argsPattern = ConfigurationManager.AppSettings["pattern"];
			string argsIgnore = ConfigurationManager.AppSettings["ignore"];

			if (Directory.Exists(argsPath) == false)
			{
				Console.WriteLine("Directory \"{0}\" doesn't exists.", argsPath);
				return;
			}

			string[] patterns = (argsPattern ?? String.Empty)
				.Split(new[] { ';' })
				.Select(x => x.ToLowerInvariant().Trim())
				.Where(x => String.IsNullOrWhiteSpace(x) == false)
				.Distinct()
				.ToArray();


			if (patterns.Length == 0)
				patterns = new[] { "*.css" };


			string[] igonre = (argsIgnore ?? String.Empty)
				.Split(new[] { ';' })
				.Select(x => x.ToLowerInvariant().Trim())
				.Where(x => String.IsNullOrWhiteSpace(x) == false)
				.Distinct()
				.ToArray();

			Console.WriteLine("Processing \"{0}\" path...", argsPath);
			Console.Write("Files to search: ");
			foreach (string pattern in patterns)
			{
				Console.Write(pattern);
				Console.Write(" ");
			}
			Console.WriteLine();
			if (igonre.Length > 0)
			{
				Console.WriteLine("Files to ignore: ");
				foreach (string file in igonre)
				{
					Console.WriteLine(file);
				}
				Console.WriteLine();
			}


			IColorsMap map = new ColorsMap(new Core.Converter.ColorsConverter(), new IColorModifier[]
			{
				new LessArgbChannelColorModifier { },
				new LessHsbColorModifier { }
			});

			IColorsExtractor extractor = new ColorsExtractor();

			IFilesProcessing processor = new FilesProcessing(map, extractor, new LessGenerator(ConfigurationManager.AppSettings["colorVariablePattern"] ?? "color", 1));
			processor.Process(argsPath, patterns, ConfigurationManager.AppSettings["colorsFileName"] ?? "colors.less", igonre);

			Console.WriteLine("Done.");
			Console.Read();
		}

		private static void Welcome()
		{
			Console.WriteLine("CSS Color Extractor Tool. v.1.0. 27/05/2014");
			Console.WriteLine();
		}

		private static void Help()
		{
			Console.WriteLine("How to use:");
			Console.WriteLine("CssColorExtractor.exe <path>");
			Console.WriteLine();
			Console.WriteLine("\t\t <path> - directory with source CSS files");
			Console.WriteLine();
			Console.WriteLine("\t Example: CssColorExtractor.exe \"D:\\Themes\\Blue\"");
			Console.WriteLine();
		}
	}
}