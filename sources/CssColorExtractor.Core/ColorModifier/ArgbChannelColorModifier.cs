using System;
using System.Drawing;

namespace CssColorExtractor.Core.ColorModifier
{
	public abstract class ArgbChannelColorModifier : IColorModifier
	{
		public bool Match(Color color1, Color color2)
		{
			return (color1.R == color2.R) &&
				   (color1.G == color2.G) &&
				   (color1.B == color2.B);
		}

		public abstract string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor);
	}
}