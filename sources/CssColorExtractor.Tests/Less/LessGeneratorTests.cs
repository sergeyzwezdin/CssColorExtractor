using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Core.Converter;
using CssColorExtractor.Core.Generator;
using CssColorExtractor.Core.Map;
using CssColorExtractor.Less;
using CssColorExtractor.Less.ColorModifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssColorExtractor.Tests.Less
{
	[TestClass]
	public class LessGeneratorTests
	{
		[TestMethod]
		public void GenerateColorsList()
		{
			// Arrange
			IColorsMap colorsMap = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new LessArgbChannelColorModifier(), new LessHsbColorModifier() });
			colorsMap.PushColor("#ff0000");
			colorsMap.PushColor("#00ff00");
			colorsMap.PushColor("#0000ff");

			// Act
			IGenerator generator = new LessGenerator("color", 1);
			string result = generator.GenerateColorsList(colorsMap);

			// Assert
			Assert.AreEqual(@"@color1: #ff0000;
@color2: #00ff00;
@color3: #0000ff;
", result);
		}

		[TestMethod]
		public void UpdateStylesheets()
		{
			// Arrange
			IColorsMap colorsMap = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new LessArgbChannelColorModifier(), new LessHsbColorModifier() });
			colorsMap.PushColor("#ff0000");
			colorsMap.PushColor("#00ff00");
			colorsMap.PushColor("#0000ff");

			// Act
			IGenerator generator = new LessGenerator("color", 1);
			string result = generator.UpdateStylesheets(colorsMap, @"p
{
	background-color: #00ff00;
}

a
{
	color: #0000ff;
}

strong
{
	color:#ff0000;
}");

			// Assert
			Assert.AreEqual(@"p
{
	background-color: @color2;
}

a
{
	color: @color3;
}

strong
{
	color:@color1;
}", result);
		}

		[TestMethod]
		public void UpdateStylesheetsWithAlphaChannel()
		{
			// Arrange
			IColorsMap colorsMap = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new LessArgbChannelColorModifier(), new LessHsbColorModifier() });
			colorsMap.PushColor("#ffffff");
			colorsMap.PushColor("rgba(255,255,255,0.4)");

			// Act
			IGenerator generator = new LessGenerator("color", 1);
			string result = generator.UpdateStylesheets(colorsMap, @"p
{
	background-color: #ffffff;
}

strong
{
	color: rgba(255,255,255,0.4);
}");

			// Assert
			Assert.AreEqual(@"p
{
	background-color: @color1;
}

strong
{
	color: fade(@color1, 40%);
}", result);
		}

		[TestMethod]
		public void UpdateStylesheetsWithHsb()
		{
			// Arrange
			IColorsMap colorsMap = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new LessArgbChannelColorModifier(), new LessHsbColorModifier() });
			colorsMap.PushColor("#80e619");
			colorsMap.PushColor("#b3f075");
			colorsMap.PushColor("#335c0a");

			// Act
			IGenerator generator = new LessGenerator("color", 1);
			string result = generator.UpdateStylesheets(colorsMap, @"a {
  color: #80e619;
}
a:link {
	color: #b3f075;
}
a:hover {
	color: #335c0a;
}");

			// Assert
			Assert.AreEqual(@"a {
  color: darken(@color1, 20%);
}
a:link {
	color: @color1;
}
a:hover {
	color: darken(@color1, 50%);
}", result);
		}
	}
}