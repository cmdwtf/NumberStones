using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// A class to make and translate rolls for a "planar" style dice.
	/// </summary>
	internal class Planar : IDice
	{
		/// <inheritdoc cref="IDice.Roll(IDieRoller, decimal)"/>
		/// <exception cref="Exceptions.ImpossibleDieException">If the number of sides is not 6</exception>
		public DiceExpressionResult Roll(IDieRoller roller, decimal sides)
		{
			int intSides = (int)sides;

			if (intSides != Constants.PlanarSides)
			{
				throw new Exceptions.ImpossibleDieException($"{nameof(Planar)} dice may only have {Constants.PlanarSides} sides.");
			}

			int roll = roller.RollDie(intSides);

			PlanarResult result = roll is 1
				? PlanarResult.Planeswalk
				: roll is 6
					? PlanarResult.Chaos
					: PlanarResult.Nothing;

			return new PlanarDiceExpressionResult(result)
			{
				Value = 0, // planar dies have no value
				Sides = intSides,
				TermType = Constants.DicePlanar,
			};
		}

		/// <inheritdoc cref="IDice.Type"/>
		public DiceType Type => DiceType.Planar;
	}
}
