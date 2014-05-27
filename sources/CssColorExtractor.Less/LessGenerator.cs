using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CssColorExtractor.Core.Generator;
using CssColorExtractor.Core.Map;

namespace CssColorExtractor.Less
{
	public class LessGenerator : IGenerator
	{
		private readonly string _prefix;

		private readonly int _startingIndex;

		public LessGenerator(string prefix = "color", int startingIndex = 0)
		{
			_prefix = prefix;
			_startingIndex = startingIndex;
		}

		public string GenerateColorsList(IColorsMap colorsMap)
		{
			StringBuilder result = new StringBuilder();

			int index = _startingIndex >= 0 ? _startingIndex : 0;
			foreach (Color color in colorsMap.GetResultingColors())
			{
				result.AppendFormat("@{0}{1}: #{2};", _prefix, index, color.R.ToString("x2") + color.G.ToString("x2") + color.B.ToString("x2")).AppendLine();
				index++;
			}

			return result.ToString();
		}

		public string UpdateStylesheets(IColorsMap colorsMap, string source)
		{
			string result = source;

			IDictionary<Color, string> colorVariables = new Dictionary<Color, string>();

			int index = _startingIndex >= 0 ? _startingIndex : 0;
			foreach (Color color in colorsMap.GetResultingColors())
			{
				colorVariables.Add(color, "@" + _prefix + index);
				index++;
			}

			for (int i = 0; i < colorsMap.Count; i++)
			{
				var colorMap = colorsMap[i];

				string stringToReplace = colorMap.Key;
				Color resultColor = colorsMap.FindAppropriateColor(colorMap.Value);

				string resultModifier = colorsMap.GenerateStylesheetModifier(colorVariables[resultColor], resultColor, colorMap.Value);

				result = result.Replace(stringToReplace, resultModifier);
			}

			return result;
		}
	}
}