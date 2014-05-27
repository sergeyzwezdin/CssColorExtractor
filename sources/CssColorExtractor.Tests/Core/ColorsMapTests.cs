using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Core.Converter;
using CssColorExtractor.Core.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CssColorExtractor.Tests.Core
{
	[TestClass]
	public class ColorsMapTests
	{
		[TestMethod]
		public void PushInitialColor()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter());

			// Act
			map.PushColor("#fff");
			map.PushColor("#0A0A0A");
			map.PushColor("#000");

			// Assert
			Assert.AreEqual(3, map.Count);
			Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), map["#fff"]);
			Assert.AreEqual(Color.FromArgb(255, 10, 10, 10), map["#0A0A0A"]);
			Assert.AreEqual(Color.FromArgb(255, 0, 0, 0), map["#000"]);
		}

		[TestMethod]
		public void PushInitialColor1()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter());

			// Act
			map.PushColor("rgba(0, 0, 0, 0.7)");
			map.PushColor("#fff");

			// Assert
			Assert.AreEqual(2, map.Count);
			Assert.AreEqual(Color.FromArgb(178, 0, 0, 0), map["rgba(0, 0, 0, 0.7)"]);
			Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), map["#fff"]);
		}

		[TestMethod]
		public void GetResultingColors()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter());

			// Act
			map.PushColor("#000");
			map.PushColor("#0A0A0A");
			map.PushColor("#fff");
			IEnumerable<Color> result = map.GetResultingColors();

			// Assert
			Assert.AreEqual(3, result.Count());
			CollectionAssert.AreEqual(new[]
			{
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 10, 10, 10),
				Color.FromArgb(255, 0, 0, 0)
			}, result.ToArray());
		}

		[TestMethod]
		public void GetResultingColorsWithARGB1()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter(), new IColorModifier[]{new ArgbChannelColorModifierMock(), new HsbColorModifierMock()});

			// Act
			map.PushColor("rgba(255,255,255,0.5)"); // A: 50%
			map.PushColor("rgba(255,255,255,0.7)"); // A: 70%
			map.PushColor("rgba(255,255,255,1.0)"); // A: 100%
			IEnumerable<Color> result = map.GetResultingColors();

			// Assert
			Assert.AreEqual(1, result.Count());
			CollectionAssert.AreEqual(new[]
			{
				ColorTranslator.FromHtml("#ffffff")
			}, result.ToArray());
		}

		[TestMethod]
		public void GetResultingColorsWithARGB2()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new ArgbChannelColorModifierMock(), new HsbColorModifierMock() });

			// Act
			map.PushColor("rgba(255,255,255,0.5)"); // A: 50%
			map.PushColor("rgba(0,255,255,0.7)");   // A: 70%
			map.PushColor("rgba(255,255,255,1.0)"); // A: 100%
			map.PushColor("rgba(0,255,255,1.0)");   // A: 100%
			IEnumerable<Color> result = map.GetResultingColors();

			// Assert
			Assert.AreEqual(2, result.Count());
			CollectionAssert.AreEqual(new[]
			{
				ColorTranslator.FromHtml("#ffffff"),
				ColorTranslator.FromHtml("#00ffff")
			}, result.ToArray());
		}


		[TestMethod]
		public void GetResultingColorsWithHSB1()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new ArgbChannelColorModifierMock(), new HsbColorModifierMock() });

			// Act
			map.PushColor("#111a0d"); // HSB: 100-50-10
			map.PushColor("#77b359"); // HSB: 100-50-70
			IEnumerable<Color> result = map.GetResultingColors();

			// Assert
			Assert.AreEqual(1, result.Count());
			CollectionAssert.AreEqual(new[]
			{
				ColorTranslator.FromHtml("#77b359") // HSB: 100-50-70
			}, result.ToArray());
		}

		[TestMethod]
		public void GetResultingColorsWithHSB2()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new ArgbChannelColorModifierMock(), new HsbColorModifierMock() });

			// Act
			map.PushColor("#00ff00"); // HSB: 120-100-100
			map.PushColor("#006600"); // HSB: 120-100-40
			IEnumerable<Color> result = map.GetResultingColors();

			// Assert
			Assert.AreEqual(1, result.Count());
			CollectionAssert.AreEqual(new[]
			{
				ColorTranslator.FromHtml("#00ff00") // HSB: 120-100-100
			}, result.ToArray());
		}

		[TestMethod]
		public void FindAppropriateColor()
		{
			// Arrange
			IColorsMap map = new ColorsMap(new ColorsConverter(), new IColorModifier[] { new ArgbChannelColorModifierMock(), new HsbColorModifierMock() });

			// Act
			map.PushColor("#ff0000");
			map.PushColor("rgba(255, 0, 0, 0.5)");
			map.PushColor("#0000ff");
			Color color = map.FindAppropriateColor(Color.FromArgb(128, 255, 0, 0));

			// Assert
			Assert.AreEqual(3, map.Count);
			Assert.AreEqual(2, map.GetResultingColors().Count());
			Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), color);
		}

		private class ArgbChannelColorModifierMock : ArgbChannelColorModifier
		{
			public override string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
			{
				throw new NotImplementedException();
			}
		}

		private class HsbColorModifierMock : HsbColorModifier
		{
			public override string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
			{
				throw new NotImplementedException();
			}
		}
	}
}
