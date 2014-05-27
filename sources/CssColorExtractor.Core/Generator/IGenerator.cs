using CssColorExtractor.Core.Map;

namespace CssColorExtractor.Core.Generator
{
	public interface IGenerator
	{
		string GenerateColorsList(IColorsMap colorsMap);

		string UpdateStylesheets(IColorsMap colorsMap, string source);
	}
}