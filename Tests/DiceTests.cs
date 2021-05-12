
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class DiceTests
	{
		[DataTestMethod]
		[DataRow("1d20", 1, 20)]
		[DataRow("4d6", 4, 24)]
		[DataRow("4d6d1", 3, 18)]
		[DataRow("1d6 + 1", 2, 7)]
		[DataRow("1 + 1d6", 2, 7)]
		[DataRow("1d6 * 3", 3, 18)]
		public void SimpleDiceRollsInRange(string input, int low, int high)
		{
			DiceExpression? expr = Dice.Parse(input);
			Assert.IsNotNull(expr);
			Assert.IsFalse(expr.IsEmpty);
			DiceResult result = expr.Roll();
			Assert.IsTrue(result.Value >= low && result.Value <= high);
		}
	}
}
