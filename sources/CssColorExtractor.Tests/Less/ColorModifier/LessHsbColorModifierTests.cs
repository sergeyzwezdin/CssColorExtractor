using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Less.ColorModifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace CssColorExtractor.Tests.Less.ColorModifier
{
	[TestClass]
	public class LessHsbColorModifierTests
	{
		[TestMethod]
		public void SameColor()
		{
			// Arrange
			IColorModifier modifier = new LessHsbColorModifier();

			// Act
			string result = modifier.GenerateStylesheetModifier("@color5",
				ColorTranslator.FromHtml("#336666"), // 180-50-40
				ColorTranslator.FromHtml("#336666")  // 180-50-40
				);

			// Assert
			Assert.AreEqual("@color5", result);
		}

		[TestMethod]
		public void Darker()
		{
			// Arrange
			IColorModifier modifier = new LessHsbColorModifier();

			// Act
			string result = modifier.GenerateStylesheetModifier("@color5",
				ColorTranslator.FromHtml("#336666"), // 180-50-40
				ColorTranslator.FromHtml("#003333")  // 180-50-20
				);

			// Assert
			Assert.AreEqual("darken(@color5, 20%)", result);
		}

		[TestMethod]
		public void Lighten()
		{
			// Arrange
			IColorModifier modifier = new LessHsbColorModifier();

			// Act
			string result = modifier.GenerateStylesheetModifier("@color5",
				ColorTranslator.FromHtml("#80e619"), // 180-50-40
				ColorTranslator.FromHtml("#b3f075")  // 180-50-60
				);

			// Assert
			Assert.AreEqual("lighten(@color5, 20%)", result);
		}

		[TestMethod]
		public void InvalidColor()
		{
			// Arrange
			IColorModifier modifier = new LessHsbColorModifier();

			// Act
			Exception expectedException = null;
			try
			{
				string result = modifier.GenerateStylesheetModifier("@color5",
					ColorTranslator.FromHtml("#cc33cc"), // 300-75-80
					ColorTranslator.FromHtml("#cc3399") // 320-75-80
					);
			}
			catch (Exception ex)
			{
				expectedException = ex;
			}

			// Assert
			Assert.IsNotNull(expectedException);
			Assert.IsInstanceOfType(expectedException, typeof(ArgumentException));
		}
	}
}