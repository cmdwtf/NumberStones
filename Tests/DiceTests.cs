using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class DiceTests
	{
		[DataTestMethod]
		public void TestPasses()
		{
			decimal result = Dice.Roll("4d6");
			System.Console.WriteLine($"Result: {result}");
		}
	}
}
