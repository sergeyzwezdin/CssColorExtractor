using System.Drawing;

namespace CssColorExtractor.Core.ColorModifier
{
	public interface IColorModifier
	{
		bool Match(Color color1, Color color2);

		string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor);
	}
}