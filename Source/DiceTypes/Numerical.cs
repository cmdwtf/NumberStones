
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// A class to make and translate rolls for standard numerical dice.
	/// </summary>
	internal class Numerical : IDice
	{
		/// <inheritdoc cref="IDice.Roll(IDieRoller, decimal)"/>
		public DiceExpressionResult Roll(IDieRoller roller, decimal sides)
		{
			int intSides = (int)sides;
			int roll = roller.RollDie(intSides);
			return new()
			{
				Value = roll,
				Sides = intSides,
				TermType = $"d{intSides}",
				CriticalSuccess = roll == intSides,
				CriticalFailure = roll == 1
			};
		}

		/// <inheritdoc cref="IDice.Type"/>
		public DiceType Type => DiceType.Numerical;
	}
}
