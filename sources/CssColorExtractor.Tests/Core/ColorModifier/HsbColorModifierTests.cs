using System;
using System.Drawing;
using CssColorExtractor.Core.ColorModifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssColorExtractor.Tests.Core.ColorModifier
{
	[TestClass]
	public class HsbColorModifierTests
	{
		[TestMethod]
		public void MatchColors1()
		{
			// Arrange
			IColorModifier modifier = new HsbColorModifierMock();

			// Act
			bool result = modifier.Match(ColorTranslator.FromHtml("#00ff00"), ColorTranslator.FromHtml("#006600"));
			// 120-100-100 == 120-100-40

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void MatchColors2()
		{
			// Arrange
			IColorModifier modifier = new HsbColorModifierMock();

			// Act
			bool result = modifier.Match(ColorTranslator.FromHtml("#0000ff"), ColorTranslator.FromHtml("#000033"));
			// 240-100-100 == 240-100-20

			// Assert
			Assert.IsTrue(result);
		}


		[TestMethod]
		public void MatchColors3()
		{
			// Arrange
			IColorModifier modifier = new HsbColorModifierMock();

			// Act
			bool result = modifier.Match(ColorTranslator.FromHtml("#000033"), ColorTranslator.FromHtml("#330033"));
			// 240-100-20 != 300-100-20

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

		private class HsbColorModifierMock : HsbColorModifier
		{
			public override string GenerateStylesheetModifier(string variable, Color baseColor, Color targetColor)
			{
				throw new NotImplementedException();
			}
		}
	}
}
