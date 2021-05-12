using cmdwtf.NumberStones.Parser;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The Dice class is a static class that has convenience methods for parsing and rolling dice.
	/// As well, it contains some shortcuts for starting fleuent dice expressions.
	/// </summary>
	public static class Dice
	{
		/// <summary>
		/// Creates a dice expression with multiple dice
		/// </summary>
		/// <param name="sides">The number of sides the dice have</param>
		/// <param name="numberOfDice">The number of dice to roll</param>
		/// <returns>The expression</returns>
		public static DiceExpression Multiple(int sides, int numberOfDice)
			=> DiceExpression.Dice(sides, numberOfDice);

		/// <summary>
		/// Creates a dice expression for a single die
		/// </summary>
		/// <param name="sides">The number of sides the die has</param>
		/// <returns>The expression</returns>
		public static DiceExpression Single(int sides)
			=> DiceExpression.Dice(sides, 1);

		private static readonly IDiceExpressionParser _diceParser = new DiceExpressionParser();

		/// <summary>
		/// Parse the specified string into a DiceExpression.
		/// </summary>
		/// <param name="expression">The string dice expression to parse. Ex. 3d6+4</param>
		/// <returns>A DiceExpression representing the parsed string</returns>
		/// <exception cref="Exceptions.DiceExpressionParseException">If the dice expression fails to parse</exception>
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