using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Core.Converter;

namespace CssColorExtractor.Core.Map
{
	public class ColorsMap : Dictionary<string, Color>, IColorsMap
	{
		private readonly IColorsConverter _colorsConverter;
		private readonly IColorModifier[] _colorModifiers;

		public ColorsMap(IColorsConverter colorsConverter)
			: this(colorsConverter, null)
		{
		}

		public ColorsMap(IColorsConverter colorsConverter, IColorModifier[] colorModifiers)
		{
			_colorsConverter = colorsConverter;
			_colorModifiers = colorModifiers ?? new IColorModifier[] { };
		}

		public KeyValuePair<string, Color> this[int index]
		{
			get
			{
				return this.ToArray()[index];
			}
		}

		public void PushColor(string sourceColor)
		{
			Color targetColor = _colorsConverter.ExtractColor(sourceColor);
			PushColor(sourceColor, targetColor);
		}

		public IEnumerable<Color> GetResultingColors()
		{
			List<Color> result = new List<Color>();

			foreach (Color color in Values.OrderByDescending(x => x.ToArgb()))
			{
				Color targetColor = Color.FromArgb(255, color.R, color.G, color.B);

				bool isColorAlreadyAdded = result.Contains(targetColor);

				if (isColorAlreadyAdded == false)
				{
					foreach (IColorModifier colorModifier in _colorModifiers)
					{
						isColorAlreadyAdded |= result.Any(x => colorModifier.Match(x, color));
					}
				}

				if (isColorAlreadyAdded == false)
					result.Add(targetColor);
			}

			return result;
		}

		public Color FindAppropriateColor(Color color)
		{
			IEnumerable<Color> resultingColors = GetResultingColors();

			return resultingColors.First(x =>
			{
				if (x == color)
					return true;

				return _colorModifiers.Any(c => c.Match(x, color));
			});
		}

		public string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
		{
			if (baseColor == targetColor)
				return variable;

			IColorModifier modifier = _colorModifiers.First(x => x.Match(baseColor, targetColor));
			return modifier.GenerateStylesheetModifier(variable, baseColor, targetColor);
		}

		private void PushColor(string sourceColor, Color targetColor)
		{
			if (ContainsKey(sourceColor) == false)
				Add(sourceColor, targetColor);
			else
				this[sourceColor] = targetColor;
		}
	}
}
