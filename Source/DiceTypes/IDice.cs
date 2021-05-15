using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// An interface representing a class that can make dice (or similar) rolls
	/// and translate them to approppriate expression result types.
	/// </summary>
	internal interface IDice
	{
		/// <summary>
		/// Rolls the dice of specified sides with the specified roller.
		/// </summary>
		/// <param name="roller">The roller to use</param>
		/// <param name="sides">The number of sides the die has</param>
		/// <returns>The die roll result</returns>
		DiceExpressionResult Roll(IDieRoller roller, decimal sides);

		/// <summary>
		/// The type of dice the <see cref="IDice"/> represents.
		/// </summary>
		DiceType Type { get; }
	}
}
