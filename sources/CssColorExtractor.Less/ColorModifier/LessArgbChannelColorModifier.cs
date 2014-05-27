using System;
using System.Drawing;
using CssColorExtractor.Core.ColorModifier;

namespace CssColorExtractor.Less.ColorModifier
{
	public class LessArgbChannelColorModifier : ArgbChannelColorModifier
	{
		public override string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
		{
			bool validColor = (baseColor.R == targetColor.R) && (baseColor.G == targetColor.G) && (baseColor.B == targetColor.B);

			if (validColor == true)
			{
				if (targetColor.A == baseColor.A)
					return variable;
				else
				{
					int opacity = (int)(Math.Round(targetColor.A / 255f * 100));
					return "fade(" + variable + ", " + opacity + "%)";
				}
			}
			else
				throw new ArgumentException("Base color is not same as target color.");
		}
	}
}