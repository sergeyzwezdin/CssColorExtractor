namespace CssColorExtractor.Core.FilesProcessing
{
	public interface IFilesProcessing
	{
		void Process(string path, string[] patterns, string colorsFileName, string[] igonre = null);
	}
}
