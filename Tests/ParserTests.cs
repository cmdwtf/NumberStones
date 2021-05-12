
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class ParserTests
	{
		[DataTestMethod]
		[DataRow("d", "1d0")]
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
		public void SimpleExpressionsParseCorrectly(string input, string expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			string parsedExpression = expression.ToString();
			Assert.AreEqual(expected, parsedExpression);
		}

		[DataTestMethod]
		[DataRow("1d6 *")]
		[DataRow("1d6 + (1 + 1")]
		[DataRow("1d6 + 1 )")]
		[DataRow("1d6 + 1[label")]
		public void ParserFailsOnPoorlyFormedExpressions(string input)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Tools.Write(input, expression);
			Assert.IsTrue(expression.IsEmpty);
		}
	}
}
