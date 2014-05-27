using System.Collections.Generic;
using System.Drawing;

namespace CssColorExtractor.Core.Map
{
	public interface IColorsMap
	{
		int Count { get; }

		Color this[string index] { get; }

		KeyValuePair<string, Color> this[int index] { get; }

		void Clear();

		void PushColor(string sourceColor);

		IEnumerable<Color> GetResultingColors();

		Color FindAppropriateColor(Color color);

		string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor);
	}
}