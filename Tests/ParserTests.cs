
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
			Tools.Write(input, expression, null, expected);
			Assert.AreEqual(expected, parsedExpression);
		}

		[DataTestMethod]
		[DataRow("1d6 *")]
		[DataRow("1d6 + (1 + 1")]
		[DataRow("1d6 + 1 )")]
		[DataRow("1d6 + 1[label")]
		public void DiceTryParseFailsOnPoorlyFormedExpressions(string input)
		{
			bool result = Dice.TryParse(input, out DiceExpression expression);
			Assert.IsNotNull(expression);
			Assert.IsFalse(result);
			Assert.IsTrue(expression.IsEmpty);
		}

		[DataTestMethod]
		[DataRow("1d6 *")]
		[DataRow("1d6 + (1 + 1")]
		[DataRow("1d6 + 1 )")]
		[DataRow("1d6 + 1[label")]
		public void DiceParseThrowsOnPoorlyFormedExpressions(string input)
		{
			Assert.ThrowsException<Exceptions.DiceExpressionParseException>(() =>
				Dice.Parse(input)
			);
		}

		[DataTestMethod]
		[DataRow("1d6cx>14")]
		[DataRow("1d6x")]
		[DataRow("1d6?")]
		[DataRow("1d6cs>x")]
		[DataRow("")]
		[DataRow("foo")]
		public void DiceParseThrowsOnPoorlyFormedDiceOptions(string input)
		{
			Assert.ThrowsException<Exceptions.DiceExpressionParseException>(() =>
				Dice.Parse(input)
			);
		}

		[DataTestMethod]
		[DataRow("[CHA]5")]
		[DataRow("1+[Guidance]2")]
		[DataRow("[Guidance]")]
		public void PoorlyPlacedLabelsFailToParse(string input)
		{
			Assert.ThrowsException<Exceptions.DiceExpressionParseException>(() =>
				Dice.Parse(input)
			);
		}

		[DataTestMethod]
		[DataRow("5[CHA]", "5[CHA]")]
		[DataRow("1+2[Guidance]", "1 + 2[Guidance]")]
		[DataRow("1+2   [Guidance]", "1 + 2[Guidance]")] // spaces after constant shouldn't matter
		[DataRow("1+2   [Guidance is just powerful butt touches]", "1 + 2[Guidance is just powerful butt touches]")] // spaces inside shouldn't matter
		public void ConstantsCanHaveLabels(string input, string expected) => ParseAndCheckExpected(input, expected);

		[TestMethod]
		public void DiceParseHandlesNoOptions() => ParseAndCheckExpected("1d20", "1d20");

		[DataTestMethod]
		[DataRow("8d6cf<5", "8d6cf<5")]
		[DataRow("8d6cf<=5", "8d6cf<=5")]
		public void DiceParseHandlesOptionCriticalFailure(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6cs>5", "8d6cs>5")]
		[DataRow("8d6cs>=5", "8d6cs>=5")]
		public void DiceParseHandlesOptionCriticalSuccess(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6d", "8d6dl1")]
		[DataRow("8d6dl", "8d6dl1")]
		[DataRow("8d6dl2", "8d6dl2")]
		[DataRow("8d6dh", "8d6dh1")]
		[DataRow("8d6dh2", "8d6dh2")]
		public void DiceParseHandlesOptionDrop(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6!", "8d6!")]
		[DataRow("8d6!!", "8d6!!")]
		[DataRow("8d6!p", "8d6!p")]
		[DataRow("8d6!>4", "8d6!>4")]
		[DataRow("8d6!!>4", "8d6!!>4")]
		[DataRow("8d6!p>4", "8d6!p>4")]
		public void DiceParseHandlesOptionExplode(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6k", "8d6kh1")]
		[DataRow("8d6kh", "8d6kh1")]
		[DataRow("8d6kh2", "8d6kh2")]
		[DataRow("8d6kl", "8d6kl1")]
		[DataRow("8d6kl2", "8d6kl2")]
		public void DiceParseHandlesOptionKeep(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6[cha]", "8d6[cha]")]
		[DataRow("8d6[charisma is the only stat that matters, but dex is the only important one]", "8d6[charisma is the only stat that matters, but dex is the only important one]")]
		public void DiceParseHandlesOptionLabel(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6r2", "8d6r2")]
		[DataRow("8d6r2r4", "8d6r2r4")]
		public void DiceParseHandlesOptionReroll(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6", "8d6")]
		public void DiceParseHandlesOptionTarget(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d6t2", "8d6t2")]
		[DataRow("8d6t2t4", "8d6t2t4")]
		public void DiceParseHandlesOptionTwice(string input, string expected) => ParseAndCheckExpected(input, expected);

		[DataTestMethod]
		[DataRow("8d20", "8d20")]
		[DataRow("8d20k", "8d20kh1")]
		[DataRow("8d20kh", "8d20kh1")]
		[DataRow("8d20kh2", "8d20kh2")]
		[DataRow("8d20kh2d", "8d20kh2dl1")]
		[DataRow("8d20kh2dl", "8d20kh2dl1")]
		[DataRow("8d20kh2dl1", "8d20kh2dl1")]
		[DataRow("8d20kh2dl1cs>=5", "8d20kh2dl1cs>=5")]
		[DataRow("8d20kh2dl1cs>=5cf<2", "8d20kh2dl1cs>=5cf<2")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3", "8d20kh2dl1cs>=5cf<2t3")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4", "8d20kh2dl1cs>=5cf<2t3r4")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4=2", "8d20kh2dl1cs>=5cf<2t3r4=2")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4=2[neat]", "8d20kh2dl1cs>=5cf<2t3r4=2[neat]")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4=2[neat]!", "8d20kh2dl1cs>=5cf<2t3r4=2[neat]!")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4=2[neat]!>19", "8d20kh2dl1cs>=5cf<2t3r4=2[neat]!>19")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4=2[neat]!!>19", "8d20kh2dl1cs>=5cf<2t3r4=2[neat]!!>19")]
		[DataRow("8d20kh2dl1cs>=5cf<2t3r4=2[neat]!p>19", "8d20kh2dl1cs>=5cf<2t3r4=2[neat]!p>19")]
		public void DiceParseHandlesComplexOptionStrings(string input, string expected) => ParseAndCheckExpected(input, expected);

		private static void ParseAndCheckExpected(string input, string expected)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			string parsedExpression = expression.ToString();
			DiceResult result = expression.Roll();
			Tools.Write(input, expression, result, expected);
			Assert.AreEqual(expected, parsedExpression);
		}
	}
}
