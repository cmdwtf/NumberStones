
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
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, $"{low}<={result.Value}{high}");
			Assert.IsTrue(result.Value >= low && result.Value <= high);
		}


		[DataTestMethod]
		[DataRow("d", 0)]
		[DataRow("1d", 0)]
		[DataRow("1d0", 0)]
		[DataRow("5d0", 0)]
		[DataRow("d1", 1)]
		[DataRow("1d1", 1)]
		[DataRow("5d1", 5)]
		public void WeirdRollsAreAsExpected(string input, int expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, expected);
			Assert.IsTrue(result.Value == expected);
		}
	}
}
