using cmdwtf.NumberStones.Parser;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The Dice class is a static class that has convenience methods for parsing and rolling dice
	/// </summary>
	public static class Dice
	{
		// #doc
		public static DiceExpression Multiple(int sides, int numberOfDice)
			=> DiceExpression.Dice(sides, numberOfDice);

		// #doc
		public static DiceExpression Single(int sides)
			=> DiceExpression.Dice(sides, 1);

		// #split

		private static readonly IDiceParser _diceParser = new DiceParser();

		/// <summary>
		/// Parse the specified string into a DiceExpression
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <returns>A DiceExpression representing the parsed string</returns>
		public static DiceExpression Parse(string expression) => _diceParser.Parse(expression);

		/// <summary>
		/// A convenience method for parsing a dice expression from a string, rolling the dice, and returning the total.
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <param name="roller">Die Roller RNG used to perform the Roll.</param>
		/// <returns>An integer result of the sum of the dice rolled including constants and scalars in the expression</returns>
		public static decimal Roll(string expression, IDieRoller roller) => Parse(expression).Roll(roller).Value;

		/// <summary>
		/// A convenience method for parsing a dice expression from a string, rolling the dice, and returning the total.
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <returns>An integer result of the sum of the dice rolled including constants and scalars in the expression</returns>
		/// <remarks>Uses DotNetRandom as its RNG</remarks>
		public static decimal Roll(string expression) => Roll(expression, Instances.DefaultRoller);
	}
}