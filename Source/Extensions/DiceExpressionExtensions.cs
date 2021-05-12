using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// Extensions to quickly and easily get dice rolls with specific rollers.
	/// </summary>
	public static class DiceExpressionExtensions
	{
		/// <summary>
		/// Rolls the expression with the default roller
		/// </summary>
		/// <param name="diceExpression">The expression to roll</param>
		/// <returns>The result of the roll</returns>
		public static DiceResult Roll(this DiceExpression diceExpression) => diceExpression.Roll(Instances.DefaultRoller);

		/// <summary>
		/// Rolls the expression with the minimum value roller
		/// </summary>
		/// <param name="diceExpression">The expression to roll</param>
		/// <returns>The result of the roll</returns>
		public static DiceResult MinRoll(this DiceExpression diceExpression) => diceExpression.Roll(Instances.MinRoller);

		/// <summary>
		/// Rolls the expression with the maximum value roller
		/// </summary>
		/// <param name="diceExpression">The expression to roll</param>
		/// <returns>The result of the roll</returns>
		public static DiceResult MaxRoll(this DiceExpression diceExpression) => diceExpression.Roll(Instances.MaxRoller);
	}
}