using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// A class to make and translate rolls for a "fudge" or "fate" style dice.
	/// </summary>
	internal class Fudge : IDice
	{
		/// <inheritdoc cref="IDice.Roll(IDieRoller, decimal)"/>
		/// <exception cref="Exceptions.ImpossibleDieException">If the number of sides is not 6</exception>
		public DiceExpressionResult Roll(IDieRoller roller, decimal sides)
		{
			int intSides = (int)sides;

			if (intSides != Constants.FudgeSides)
			{
				throw new Exceptions.ImpossibleDieException($"{nameof(Fudge)} dice may only have {Constants.FudgeSides} sides.");
			}

			int roll = roller.RollDie(intSides);

			// translate the d6 roll into a fudge result
			FudgeResult result = roll is 1 or 2
				? FudgeResult.Minus
				: roll is 2 or 3
					? FudgeResult.Nothing
					: FudgeResult.Plus;

			// modify the roll based on the actual die faces
			roll = result switch
			{
				FudgeResult.Nothing => 0,
				FudgeResult.Plus => 1,
				FudgeResult.Minus => -1,
				_ => throw new Exceptions.ImpossibleDieException($"{nameof(Fudge)} dice aren't supposed to have this side type"),
			};

			return new FudgeDiceExpressionResult(result)
			{
				Value = roll,
				Sides = intSides,
				TermType = Constants.DiceFudge,
				CriticalFailure = result == FudgeResult.Minus,
				CriticalSuccess = result == FudgeResult.Plus
			};
		}

		/// <inheritdoc cref="IDice.Type"/>
		public DiceType Type => DiceType.Fudge;
	}
}
