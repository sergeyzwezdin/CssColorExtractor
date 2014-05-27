using System;
using System.Drawing;
using CssColorExtractor.Core.ColorModifier;

namespace CssColorExtractor.Less.ColorModifier
{
	public class LessHsbColorModifier : HsbColorModifier
	{
		public override string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
		{
			float hue1 = baseColor.GetHue();
			float hue2 = targetColor.GetHue();

			float sat1 = baseColor.GetSaturation();
			float sat2 = targetColor.GetSaturation();


			bool validColor = (Math.Abs(hue1 - hue2) < 2) &&
							  (Math.Abs(sat1 - sat2) < 2);

			if (validColor == true)
			{
				float light1 = baseColor.GetBrightness();
				float light2 = targetColor.GetBrightness();

				int lightDiff = (int) Math.Round((light1 - light2)*100);
				if (lightDiff == 0)
					return variable;
				else if (lightDiff < 0)
					return "lighten(" + variable + ", " + (-lightDiff) + "%)";
				else
					return "darken(" + variable + ", " + lightDiff + "%)";
			}
			else
				throw new ArgumentException("Base color is not same as target color.");
		}
	}
}