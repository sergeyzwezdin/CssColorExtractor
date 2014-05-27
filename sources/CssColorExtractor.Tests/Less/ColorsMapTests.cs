using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Core.Converter;
using CssColorExtractor.Core.Map;
using CssColorExtractor.Less.ColorModifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssColorExtractor.Tests.Less
{
	[TestClass]
	public class ColorsMapTests
	{
		[TestMethod]
		public void GenerateStylesheetModifier()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new LessArgbChannelColorModifier(), new LessHsbColorModifier() });

			// Act
			map.PushColor("#ff0000");
			map.PushColor("rgba(255, 0, 0, 0.5)");
			map.PushColor("#0000ff");
			string result = map.GenerateStylesheetModifier("@color12", Color.FromArgb(255, 255, 0, 0),
				Color.FromArgb(128, 255, 0, 0));

			// Assert
			Assert.AreEqual(3, map.Count);
			Assert.AreEqual(2, map.GetResultingColors().Count());
			Assert.AreEqual("fade(@color12, 50%)", result);
		}
	}
}