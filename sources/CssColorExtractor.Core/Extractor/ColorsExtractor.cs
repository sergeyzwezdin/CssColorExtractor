using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CssColorExtractor.Core.Extractor
{
	public class ColorsExtractor : IColorsExtractor
	{
		private static readonly Regex[] Templates = new[]
		{
			new Regex(@"(#){1}([0-9a-f]{3,6})",
				RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline),

			new Regex(@"(rgba){1}[ ]*\([ ]*[0-9\.]+[ ]*,[ ]*[0-9\.]+[ ]*,[ ]*[0-9\.]+[ ]*,[ ]*[0-9\.]+[ ]*\)",
				RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline),

			new Regex(@"(rgb){1}[ ]*\([ ]*[0-9\.]+[ ]*,[ ]*[0-9\.]+[ ]*,[ ]*[0-9\.]+[ ]*\)",
				RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline)
		};

		public IEnumerable<string> Extract(string source)
		{
			List<string> result = new List<string>();

			foreach (Regex template in Templates)
			{
				result.AddRange(template
					.Matches(source)
					.OfType<Match>()
					.Select(x => x.Value));
			}

			return result.ToArray();
		}
	}
}