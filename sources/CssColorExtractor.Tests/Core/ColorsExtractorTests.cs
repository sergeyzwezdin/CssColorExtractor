using CssColorExtractor.Core.Extractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CssColorExtractor.Tests.Core
{
	[TestClass]
	public class ColorsExtractorTests
	{
		[TestMethod]
		public void NoColors()
		{
			// Arrange
			string source = @"a
			{
			}

			a:focus
			{
			}

			a:hover
			{
			}";

			// Act
			IColorsExtractor extractor = new ColorsExtractor();
			IEnumerable<string> result = extractor.Extract(source);

			// Assert
			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public void Hex()
		{
			// Arrange
			string source = @"a
			{
				color: #e74c3c;
			}

			a:focus
			{
				color: #555;
			}

			a:hover
			{
				color: #ffffff;
			}";

			// Act
			IColorsExtractor extractor = new ColorsExtractor();
			IEnumerable<string> result = extractor.Extract(source);

			// Assert
			Assert.AreEqual(3, result.Count());
			CollectionAssert.AreEqual(new[] { "#e74c3c", "#555", "#ffffff" }, result.ToArray());
		}

		[TestMethod]
		public void Rgb()
		{
			// Arrange
			string source = @"a
			{
				background:rgb(181, 225, 229);
			}

			a:focus
			{
				background:rgb(0, 0, 255);
			}

			a:hover
			{
				background:rgb(255, 0, 0);
			}";

			// Act
			IColorsExtractor extractor = new ColorsExtractor();
			IEnumerable<string> result = extractor.Extract(source);

			// Assert
			Assert.AreEqual(3, result.Count());
			CollectionAssert.AreEqual(new[] { "rgb(181, 225, 229)", "rgb(0, 0, 255)", "rgb(255, 0, 0)" }, result.ToArray());
		}

		[TestMethod]
		public void Rgba()
		{
			// Arrange
			string source = @"a
			{
				background:rgba(181, 225, 229, 0.8);
			}

			a:focus
			{
				background:rgba(181, 225, 229, 0.5);
			}

			a:hover
			{
				background:rgba(181, 225, 229, 0.1);
			}";

			// Act
			IColorsExtractor extractor = new ColorsExtractor();
			IEnumerable<string> result = extractor.Extract(source);

			// Assert
			Assert.AreEqual(3, result.Count());
			CollectionAssert.AreEqual(new[] { "rgba(181, 225, 229, 0.8)", "rgba(181, 225, 229, 0.5)", "rgba(181, 225, 229, 0.1)" }, result.ToArray());
		}

		[TestMethod]
		public void Rgba1()
		{
			// Arrange
			string source = @".carousel-v1 .carousel-caption {
	left: 0;
	right: 0;
	bottom: 0;
	padding: 7px 15px;
	background: rgba(0, 0, 0, 0.7);
}

	.carousel-v1 .carousel-caption p {
		color: #fff;
		margin-bottom: 0;
	}
";

			// Act
			IColorsExtractor extractor = new ColorsExtractor();
			IEnumerable<string> result = extractor.Extract(source);

			// Assert
			Assert.AreEqual(2, result.Count());
			CollectionAssert.AreEqual(new[] { "#fff", "rgba(0, 0, 0, 0.7)" }, result.ToArray());
		}

		[TestMethod]
		public void Mix()
		{
			// Arrange
			string source = @"a
			{
				color: #e74c3c;
			}

			a:focus
			{
				background:#555;
			}

			a:hover
			{
				background:rgba(181, 225, 229, 0.1);
			}";

			// Act
			IColorsExtractor extractor = new ColorsExtractor();
			IEnumerable<string> result = extractor.Extract(source);

			// Assert
			Assert.AreEqual(3, result.Count());
			CollectionAssert.AreEqual(new[] { "#e74c3c", "#555", "rgba(181, 225, 229, 0.1)" }, result.ToArray());
		}
	}
}
