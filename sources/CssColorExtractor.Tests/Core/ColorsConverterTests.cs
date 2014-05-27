using CssColorExtractor.Core.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace CssColorExtractor.Tests.Core
{
	[TestClass]
	public class ColorsConverterTests
	{
		[TestMethod]
		public void ExtractFromHex()
		{
			// Arrange
			IColorsConverter converter = new ColorsConverter();

			// Act
			Color result = converter.ExtractColor("#ff0000");

			// Assert
			Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), result);
		}

		[TestMethod]
		public void ExtractFromHexShort()
		{
			// Arrange
			IColorsConverter converter = new ColorsConverter();

			// Act
			Color result = converter.ExtractColor("#f00");

			// Assert
			Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), result);
		}

		[TestMethod]
		public void ExtractFromRgb()
		{
			// Arrange
			IColorsConverter converter = new ColorsConverter();

			// Act
			Color result = converter.ExtractColor("rgb(255, 0, 0)");

			// Assert
			Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), result);
		}

		[TestMethod]
		public void ExtractFromRgba()
		{
			// Arrange
			IColorsConverter converter = new ColorsConverter();

			// Act
			Color result = converter.ExtractColor("rgba(255, 0, 0, 0.5)");

			// Assert
			Assert.AreEqual(Color.FromArgb(128, 255, 0, 0), result);
		}
	}
}