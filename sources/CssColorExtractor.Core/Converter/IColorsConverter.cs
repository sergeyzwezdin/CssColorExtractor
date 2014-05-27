using System.Drawing;

namespace CssColorExtractor.Core.Converter
{
	public interface IColorsConverter
	{
		Color ExtractColor(string color);
	}
}