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
		/// <exception cref="Exceptions.DiceExpressionParseException">If the parse fails</exception>
		public static DiceExpression Parse(string expression) => _diceParser.Parse(expression);

		/// <summary>
		/// Parse the specified string into a DiceExpression
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <param name="value">A DiceExpression representing the parsed string</param>
		/// <returns>true if the parse was successful, otherwise false</returns>
		public static bool TryParse(string expression, out DiceExpression value) => _diceParser.TryParse(expression, out value);

		/// <summary>
		/// A convenience method for parsing a dice expression from a string, rolling the dice, and returning the total.
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <param name="roller">Die Roller RNG used to perform the Roll.</param>
		/// <returns>An integer result of the sum of the dice rolled including constants and scalars in the expression</returns>
		/// <exception cref="Exceptions.DiceExpressionParseException">If the dice expression fails to parse</exception>
		public static decimal Roll(string expression, IDieRoller roller) => Parse(expression).Roll(roller).Value;

		/// <summary>
		/// A convenience method for parsing a dice expression from a string, rolling the dice, and returning the total.
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <returns>An integer result of the sum of the dice rolled including constants and scalars in the expression</returns>
		/// <remarks>Uses the default roller as its RNG</remarks>
		/// <exception cref="Exceptions.DiceExpressionParseException">If the dice expression fails to parse</exception>
		public static decimal Roll(string expression) => Roll(expression, Instances.DefaultRoller);
	}
}