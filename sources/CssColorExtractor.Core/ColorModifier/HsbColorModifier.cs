using System;
using System.Drawing;

namespace CssColorExtractor.Core.ColorModifier
{
	public abstract class HsbColorModifier : IColorModifier
	{
		public bool Match(Color color1, Color color2)
		{
			float hue1 = color1.GetHue();
			float hue2 = color2.GetHue();

			float sat1 = color1.GetSaturation();
			float sat2 = color2.GetSaturation();

			return (Math.Abs(hue1 - hue2) < 2) &&
				   (Math.Abs(sat1 - sat2) < 2);
		}

		public abstract string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor);
	}
}