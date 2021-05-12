
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class MathExpressionTests
	{
		[DataTestMethod]
		[DataRow("5+5", 10)]
		[DataRow("5+15", 20)]
		[DataRow("15+5", 20)]
		[DataRow("5+0", 5)]
		[DataRow("0+5", 5)]
		[DataRow("5+-15", -10)]
		public void AdditionCorrect(string input, int expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, expected);
			Assert.IsTrue(result.Value == expected);
		}

		[DataTestMethod]
		[DataRow("5-5", 0)]
		[DataRow("5-15", -10)]
		[DataRow("15-5", 10)]
		[DataRow("5-0", 5)]
		[DataRow("0-5", -5)]
		[DataRow("5--15", 20)]
		public void SubtractionCorrect(string input, int expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, expected);
			Assert.IsTrue(result.Value == expected);
		}

		[DataTestMethod]
		[DataRow("5*5", 25)]
		[DataRow("5*15", 75)]
		[DataRow("15*5", 75)]
		[DataRow("5*0", 0)]
		[DataRow("0*5", 0)]
		[DataRow("5*-15", -75)]
		public void MultiplicationCorrect(string input, int expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, expected);
			Assert.IsTrue(result.Value == expected);
		}

		[DataTestMethod]
		[DataRow("5/5", 1)]
		[DataRow("5/15", 0)]
		[DataRow("15/5", 3)]
		[DataRow("5/-15", -5 / 15)]
		public void IntegerDivisionCorrect(string input, int expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, expected);
			Assert.IsTrue((int)result.Value == expected);
		}

		[DataTestMethod]
		[DataRow("5 + 5 * 6", 35)]
		[DataRow("5 * (5 + 5)", 50)]
		[DataRow("8 - 8 / 4 * 2 + 7", 11)]
		[DataRow("3 + (6 * (11 + 1 - 4)) / 8 * 2", 15)]
		public void OrderOfOperationsCorrect(string input, int expected)
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
