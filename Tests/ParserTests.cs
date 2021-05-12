
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class ParserTests
	{
		[DataTestMethod]
		[DataRow("d6", "1d6")]
		[DataRow("4d6", "4d6")]
		[DataRow("4d6d1", "4d6dl1")]
		[DataRow("4d6dl1", "4d6dl1")]
		[DataRow("4d6dl", "4d6dl1")]
		[DataRow("4d6kh3", "4d6kh3")]
		[DataRow("1d6 + 1", "1d6 + 1")]
		[DataRow("1d6+1", "1d6 + 1")]
		[DataRow("1 + 1d6", "1 + 1d6")]
		[DataRow("1+1d6", "1 + 1d6")]
		[DataRow("1d6 * 3", "1d6 * 3")]
		public void SimpleExpressionParse(string expression, string expected)
		{
			DiceExpression? expr = Dice.Parse(expression);
			Assert.IsNotNull(expr);
			Assert.IsFalse(expr.IsEmpty);
			string parsedExpression = expr.ToString();
			Assert.AreEqual(expected, parsedExpression);
		}

		[DataTestMethod]
		[DataRow("1d20", 1, 20)]
		[DataRow("4d6", 4, 24)]
		[DataRow("4d6d1", 3, 18)]
		[DataRow("1d6 + 1", 2, 7)]
		[DataRow("1 + 1d6", 2, 7)]
		[DataRow("1d6 * 3", 3, 18)]
		public void SimpleExpressionRoll(string input, int low, int high)
		{
			DiceExpression? expr = Dice.Parse(input);
			Assert.IsNotNull(expr);
			Assert.IsFalse(expr.IsEmpty);
			DiceResult result = expr.Roll();
			Debug.WriteLine($"Input: {input}");
			Debug.WriteLine($"Parsed: {input}");
			Debug.WriteLine($"Result: {result}");
			Debug.WriteLine($"Expected: {low}<={result}<={high}");
			Assert.IsTrue(result.Value >= low && result.Value <= high);
		}

		[DataTestMethod]
		[DataRow("5+5", 10)]
		[DataRow("5*5", 25)]
		[DataRow("5/5", 1)]
		[DataRow("5 + 5 * 6", 35)]
		[DataRow("5 * 5 + 5", 30)]
		[DataRow("5 * (5 + 5)", 50)]
		public void SimpleMath(string input, int expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			Debug.WriteLine($"Input: {input}");
			Debug.WriteLine($"Parsed: {expression}");
			Debug.WriteLine($"Result: {result}");
			Debug.WriteLine($"Expected: {expected}");
			Assert.IsTrue(result.Value == expected);
		}
	}
}
