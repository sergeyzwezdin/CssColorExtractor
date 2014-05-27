//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using CssColorExtractor.Core.ColorDistance;
//using CssColorExtractor.Core.Converter;
//using CssColorExtractor.Core.Map;
//using CssColorExtractor.Core.Optimizer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace CssColorExtractor.Tests.Core
//{
//	[TestClass]
//	public class PaletteOptimizerTests
//	{
//		[TestMethod]
//		public void Optimize()
//		{
//			// Arrange
//			IColorsMap map = new ColorsMap(new ColorsConverter());
//			map.PushColor("#ff7f25");
//			map.PushColor("#ff720a");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff4500");
//			map.PushColor("#ff0084");
//			map.PushColor("#9b6bcc");

//			// Act
//			IPaletteOptimizer optimizer = new PaletteOptimizer(new EuclideanDistance(), 150, null);
//			optimizer.ReducePalette(map);
//			IEnumerable<Color> result = map.GetResultingColors();

//			// Assert
//			CollectionAssert.AreEqual(new[]
//			{
//				ColorTranslator.FromHtml("#ff7f25"),
//				ColorTranslator.FromHtml("#ff0084"),
//				ColorTranslator.FromHtml("#9b6bcc")
//			}, result.ToArray());
//		}

//		[TestMethod]
//		public void Optimize1()
//		{
//			// Arrange
//			IColorsMap map = new ColorsMap(new ColorsConverter());
//			map.PushColor("#ff7f25");
//			map.PushColor("#ff720a");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff4500");
//			map.PushColor("#ff0084");
//			map.PushColor("#9b6bcc");

//			// Act
//			IPaletteOptimizer optimizer = new PaletteOptimizer(new EuclideanDistance(), 150, null);
//			optimizer.ReducePalette(map);
//			IEnumerable<Color> result = map.GetResultingColors();

//			// Assert
//			CollectionAssert.AreEqual(new[]
//			{
//				ColorTranslator.FromHtml("#ff7f25"),
//				ColorTranslator.FromHtml("#ff0084"),
//				ColorTranslator.FromHtml("#9b6bcc")
//			}, result.ToArray());
//		}

//		[TestMethod]
//		public void FreezeWhite()
//		{
//			// Arrange
//			IColorsMap map = new ColorsMap(new ColorsConverter());
//			map.PushColor("#ff7f25");
//			map.PushColor("#ff720a");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff4500");
//			map.PushColor("#ff0084");
//			map.PushColor("#9b6bcc");
//			map.PushColor("#ffffff");

//			// Act
//			IPaletteOptimizer optimizer = new PaletteOptimizer(new EuclideanDistance(), 150, new[]{Color.FromArgb(255,255,255,255)});
//			optimizer.ReducePalette(map);
//			IEnumerable<Color> result = map.GetResultingColors();

//			// Assert
//			CollectionAssert.AreEqual(new[]
//			{
//				ColorTranslator.FromHtml("#ffffff"),
//				ColorTranslator.FromHtml("#ff7f25"),
//				ColorTranslator.FromHtml("#ff0084"),
//				ColorTranslator.FromHtml("#9b6bcc")
//			}, result.ToArray());
//		}


//		[TestMethod]
//		public void FreezeBlack()
//		{
//			// Arrange
//			IColorsMap map = new ColorsMap(new ColorsConverter());
//			map.PushColor("#ff7f25");
//			map.PushColor("#ff720a");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff4500");
//			map.PushColor("#ff0084");
//			map.PushColor("#9b6bcc");
//			map.PushColor("#000000");

//			// Act
//			IPaletteOptimizer optimizer = new PaletteOptimizer(new EuclideanDistance(), 150, new[] { Color.FromArgb(255, 0, 0, 0) });
//			optimizer.ReducePalette(map);
//			IEnumerable<Color> result = map.GetResultingColors();

//			// Assert
//			CollectionAssert.AreEqual(new[]
//			{
//				ColorTranslator.FromHtml("#ff7f25"),
//				ColorTranslator.FromHtml("#ff0084"),
//				ColorTranslator.FromHtml("#9b6bcc"),
//				ColorTranslator.FromHtml("#000000")
//			}, result.ToArray());
//		}

//		[TestMethod]
//		public void FreezeBlackAndWhite()
//		{
//			// Arrange
//			IColorsMap map = new ColorsMap(new ColorsConverter());
//			map.PushColor("#ff7f25");
//			map.PushColor("#ff720a");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff6600");
//			map.PushColor("#ff4500");
//			map.PushColor("#ff0084");
//			map.PushColor("#9b6bcc");
//			map.PushColor("#000000");
//			map.PushColor("#ffffff");

//			// Act
//			IPaletteOptimizer optimizer = new PaletteOptimizer(new EuclideanDistance(), 150, new[] { Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 255, 255, 255) });
//			optimizer.ReducePalette(map);
//			IEnumerable<Color> result = map.GetResultingColors();

//			// Assert
//			CollectionAssert.AreEqual(new[]
//			{
//				ColorTranslator.FromHtml("#ffffff"),
//				ColorTranslator.FromHtml("#ff7f25"),
//				ColorTranslator.FromHtml("#ff0084"),
//				ColorTranslator.FromHtml("#9b6bcc"),
//				ColorTranslator.FromHtml("#000000")
//			}, result.ToArray());
//		}
//	}
//}
