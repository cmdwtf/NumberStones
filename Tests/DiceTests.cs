
using System.Diagnostics;

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
			=> DiceRangeRoll(input, low, high);

		[DataTestMethod]
		[DataRow("dC", 0, 1)]
		[DataRow("1dC", 0, 1)]
		[DataRow("4dC", 0, 4)]
		[DataRow("8dC", 0, 8)]
		[DataRow("8dC", 0, 8)]
		[DataRow("8dC", 0, 8)]
		[DataRow("8dC", 0, 8)]
		public void CoinDiceRollInRange(string input, int low, int high)
			=> DiceRangeRoll(input, low, high);

		[DataTestMethod]
		[DataRow("dF", -1, 1)]
		[DataRow("1dF", -1, 1)]
		[DataRow("4dF", -4, 4)]
		[DataRow("8dF", -8, 8)]
		[DataRow("8dF", -8, 8)]
		[DataRow("8dF", -8, 8)]
		[DataRow("8dF", -8, 8)]
		public void FudgeDiceRollInRange(string input, int low, int high)
			=> DiceRangeRoll(input, low, high);

		[DataTestMethod]
		[DataRow("dP", 0, 0)]
		[DataRow("1dP", 0, 0)]
		[DataRow("4dP", 0, 0)]
		[DataRow("8dP", 0, 0)]
		[DataRow("8dP", 0, 0)]
		[DataRow("8dP", 0, 0)]
		[DataRow("8dP", 0, 0)]
		public void PlanarDiceRollInRange(string input, int low, int high)
			=> DiceRangeRoll(input, low, high);

		private static void DiceRangeRoll(string input, int low, int high)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			DiceResult minResult = expression.Roll(Rollers.Instances.MinRoller);
			DiceResult maxResult = expression.Roll(Rollers.Instances.MaxRoller);
			Debug.WriteLine(minResult);
			Debug.WriteLine(maxResult);
			Tools.Write(input, expression, result, $"{low}<={result}<={high}");
			Assert.IsTrue(low == minResult.Value);
			Assert.IsTrue(high == maxResult.Value);
			Assert.IsTrue(result.Value >= minResult.Value && result.Value <= maxResult.Value);
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

		[DataTestMethod]
		[DataRow("1d8 + 4d6", "1d8 + 4d6")] // Roll one octahedron and four hexahedrons.
		[DataRow("1d20+5 # Grog attacks", "1d20 + 5 # Grog attacks")] // Roll dice with a comment.
		[DataRow("2d6>=5", "2d6>=5")] // Roll two hexahedrons and take only the ones that turned greater or equal to five (aka difficulty check). Prints the number of successes.
		[DataRow("4d6=5", "4d6=5")] // So can this guy roll five?
		[DataRow("3d10>=6f1", "3d10>=6f1")] // oWoD roll: rolling *one* is a failure, rolling more failures than successes is a *botch*.
		[DataRow("1d10>=8f1f2", "1d10>=8f1f2")] // Rolling *one* or *two* is a failure.
		[DataRow("4dF", "4dF")] // [Fudge/Fate dice](http://rpg.stackexchange.com/questions/1765/what-game-circumstance-uses-fudge-dice).
		[DataRow("3d6!", "3d6!")] // Exploding dice.
		[DataRow("1d10!>9", "1d10!>9")] // Explode nine and ten.
		[DataRow("1d20r1", "1d20r1")] // Roll twenty, reroll on one(because halflings are lucky).
		[DataRow("3d10!>=8", "3d10!>=8")] // nWoD roll: tens explode, eights and up are treated like a success.
		[DataRow("1d10t10", "1d10t10")] // If a ten is rolled then count it [twice](https://github.com/ArtemGr/Sidekick/issues/151).
		[DataRow("2d20k1", "2d20k1")] // Roll twice and keep the highest roll (D&D 5e advantage).
		[DataRow("2d20k1 + 2", "2d20k1+2")] // Roll twice and keep the highest roll, with a modifier (D&D 5e advantage).
		[DataRow("2d20kl1", "2d20kl1")] // Roll twice and keep the lowest roll (D&D 5e disadvantage).
		[DataRow("4d6k3", "4d6k3")] // Roll four hexahedrons and keep the highest three (D&D 5e ability roll).
		[DataRow("(2+2)^2", "(2+2)^2")] // Do math.
		[DataRow("4d6^2", "4d6^2")] // Do math with dice.
		public void SidekickBotSuite(string input, string expectedExpression)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult result = expression.Roll();
			DiceResult minResult = expression.Roll(Rollers.Instances.MinRoller);
			DiceResult maxResult = expression.Roll(Rollers.Instances.MaxRoller);
			Debug.WriteLine(minResult);
			Debug.WriteLine(maxResult);
			Tools.Write(input, expression, result, expectedExpression);
			Assert.IsTrue(result.Value >= minResult.Value && result.Value <= maxResult.Value);
		}

		[DataTestMethod]
		[DataRow("1d6!")]
		[DataRow("1d6!!")]
		[DataRow("1d6!p")]
		[DataRow("1d1!")]
		[DataRow("1d1!!")]
		[DataRow("1d1!p")]
		public void ExplodingDiceDontOverflow(string input)
		{
			DiceExpression? expression = Dice.Parse(input);
			Assert.IsNotNull(expression);
			Assert.IsFalse(expression.IsEmpty);
			DiceResult maxRoll = expression.Roll(Rollers.Instances.MaxRoller);
			Tools.Write(input, expression, maxRoll);
		}
	}
}
