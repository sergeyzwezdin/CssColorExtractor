using System.Collections.Generic;

namespace CssColorExtractor.Core.Extractor
{
	public interface IColorsExtractor
	{
		IEnumerable<string> Extract(string source);
	}
}