using System;
using System.Drawing;
using CssColorExtractor.Core.ColorModifier;
using CssColorExtractor.Less.ColorModifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssColorExtractor.Tests.Less.ColorModifier
{
	[TestClass]
	public class LessArgbChannelColorModifierTests
	{
		[TestMethod]
		public void OpaqueColor()
		{
			// Arrange
			IColorModifier modifier = new LessArgbChannelColorModifier();

			// Act
			string result = modifier.GenerateStylesheetModifier("@color5", Color.FromArgb(255, 128, 0, 0), Color.FromArgb(255, 128, 0, 0));

			// Assert
			Assert.AreEqual("@color5", result);
		}

		[TestMethod]
		public void Opacity50()
		{
			// Arrange
			IColorModifier modifier = new LessArgbChannelColorModifier();

			// Act
			string result = modifier.GenerateStylesheetModifier("@color5", Color.FromArgb(255, 128, 0, 0), Color.FromArgb(128, 128, 0, 0));

			// Assert
			Assert.AreEqual("fade(@color5, 50%)", result);
		}

		[TestMethod]
		public void Opacity70()
		{
			// Arrange
			IColorModifier modifier = new LessArgbChannelColorModifier();

			// Act
			string result = modifier.GenerateStylesheetModifier("@color5", Color.FromArgb(10, 128, 0, 0), Color.FromArgb(178, 128, 0, 0));

			// Assert
			Assert.AreEqual("fade(@color5, 70%)", result);
		}

		[TestMethod]
		public void InvalidColor()
		{
			// Arrange
			IColorModifier modifier = new LessArgbChannelColorModifier();

			// Act
			Exception expectedException = null;
			try
			{
				string result = modifier.GenerateStylesheetModifier("@color5", Color.FromArgb(255, 128, 0, 0), Color.FromArgb(128, 128, 128, 255));
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
