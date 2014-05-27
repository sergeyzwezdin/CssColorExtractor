using System;
using System.Drawing;
using CssColorExtractor.Core.ColorModifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssColorExtractor.Tests.Core.ColorModifier
{
	[TestClass]
	public class ArgbChannelColorModifierTests
	{
		[TestMethod]
		public void MatchColors1()
		{
			// Arrange
			IColorModifier modifier = new ArgbChannelColorModifierMock();

			// Act
			bool result = modifier.Match(Color.FromArgb(128, 255, 255, 255), Color.FromArgb(255, 255, 255, 255));

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void MatchColors2()
		{
			// Arrange
			IColorModifier modifier = new ArgbChannelColorModifierMock();

			// Act
			bool result = modifier.Match(Color.FromArgb(128, 255, 255, 255), Color.FromArgb(140, 255, 255, 255));

			// Assert
			Assert.IsTrue(result);
		}


		[TestMethod]
		public void MatchColors3()
		{
			// Arrange
			IColorModifier modifier = new ArgbChannelColorModifierMock();

			// Act
			bool result = modifier.Match(Color.FromArgb(255, 0, 255, 255), Color.FromArgb(255, 255, 255, 255));

			// Assert
			Assert.IsFalse(result);
		}

		private class ArgbChannelColorModifierMock : ArgbChannelColorModifier
		{
			public override string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
			{
				throw new NotImplementedException();
			}
		}
	}
}
