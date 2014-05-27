using System.Collections.Generic;
using System.IO;
using System.Linq;
using CssColorExtractor.Core.Extractor;
using CssColorExtractor.Core.Generator;
using CssColorExtractor.Core.Map;

namespace CssColorExtractor.Core.FilesProcessing
{
	public class FilesProcessing : IFilesProcessing
	{
		private readonly IColorsMap _map;
		private readonly IColorsExtractor _extractor;
		private readonly IGenerator _outputGenarator;

		public FilesProcessing(IColorsMap map, IColorsExtractor extractor, IGenerator outputGenarator)
		{
			_map = map;
			_extractor = extractor;
			_outputGenarator = outputGenarator;
		}

		public void Process(string path, string[] patterns, string colorsFileName, string[] igonre = null)
		{
			string colorsFilePath = Path.Combine(path, colorsFileName);
			string[] files = patterns
				.SelectMany(x => Directory.GetFiles(path, x, SearchOption.AllDirectories))
				.Where(x => (igonre == null) || (igonre.Contains(Path.GetFileName(x)) == false))
				.Where(x => x != colorsFilePath)
				.ToArray();

			BuildColorsMap(files);
			ProcessColorsFile(colorsFilePath);
			ProcessStylesheetFiles(files);
		}

		private void BuildColorsMap(IEnumerable<string> files)
		{
			_map.Clear();

			foreach (string file in files)
			{
				foreach (string color in _extractor.Extract(File.ReadAllText(file)))
				{
					_map.PushColor(color);
				}
			}
		}

		private void ProcessColorsFile(string colorsFilePath)
		{
			string colorsFileContent = _outputGenarator.GenerateColorsList(_map);
			File.WriteAllText(colorsFilePath, colorsFileContent);
		}

		private void ProcessStylesheetFiles(IEnumerable<string> files)
		{
			foreach (string file in files)
			{
				string stylesheetFileContent = File.ReadAllText(file);
				string updatedStylesheetFileContent = _outputGenarator.UpdateStylesheets(_map, stylesheetFileContent);
				File.WriteAllText(file, updatedStylesheetFileContent);
			}
		}
	}
}