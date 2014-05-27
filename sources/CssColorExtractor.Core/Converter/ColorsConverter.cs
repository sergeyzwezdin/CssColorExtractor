using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CssColorExtractor.Core.Converter
{
	public class ColorsConverter : IColorsConverter
	{
		private readonly static Regex RgbTemplate = new Regex(@"(rgb){1}[ ]*\([ ]*(?<R>[0-9\.]+)[ ]*,[ ]*(?<G>[0-9\.]+)[ ]*,[ ]*(?<B>[0-9\.]+)[ ]*\)",
			RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

		private readonly static Regex RgbaTemplate = new Regex(@"(rgba){1}[ ]*\([ ]*(?<R>[0-9\.]+)[ ]*,[ ]*(?<G>[0-9\.]+)[ ]*,[ ]*(?<B>[0-9\.]+)[ ]*,[ ]*(?<A>[0-9\.]+)[ ]*\)",
			RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

		public Color ExtractColor(string color)
		{
			Color? result = null;

			result = ExtractFromHex(color);

			if (result.HasValue == false)
				result = ExtractFromRgb(color);

			if (result.HasValue == false)
				result = ExtractFromRgba(color);

			if (result.HasValue == true)
				return result.Value;
			else
				throw new ArgumentException("Unable to parse color.");
		}

		private static Color? ExtractFromHex(string source)
		{
			Color? result = null;

			try
			{
				result = ColorTranslator.FromHtml(source);
			}
			catch
			{
				result = null;
			}

			return result;
		}

		private static Color? ExtractFromRgb(string source)
		{
			Color? result = null;

			Match match = RgbTemplate.Match(source);
			if (match.Success)
			{
				int r;
				int g;
				int b;

				if (int.TryParse(match.Groups["R"].Value, out r) &&
					int.TryParse(match.Groups["G"].Value, out g) &&
					int.TryParse(match.Groups["B"].Value, out b))
					result = Color.FromArgb(255, r, g, b);
			}

			return result;
		}

		private static Color? ExtractFromRgba(string source)
		{
			Color? result = null;

			Match match = RgbaTemplate.Match(source);
			if (match.Success)
			{
				int r;
				int g;
				int b;
				float a;

				if (int.TryParse(match.Groups["R"].Value, out r) &&
					int.TryParse(match.Groups["G"].Value, out g) &&
					int.TryParse(match.Groups["B"].Value, out b) &&
					float.TryParse(match.Groups["A"].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out a))
					result = Color.FromArgb((int)Math.Round(255 * a), r, g, b);
			}

			return result;
		}
	}
}