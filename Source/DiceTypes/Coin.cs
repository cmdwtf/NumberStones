
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// A class to make and translate rolls for a "coin" style dice.
	/// </summary>
	internal class Coin : IDice
	{
		/// <inheritdoc cref="IDice.Roll(IDieRoller, decimal)"/>
		/// <exception cref="Exceptions.ImpossibleDieException">If the number of sides is not 2</exception>
		public DiceExpressionResult Roll(IDieRoller roller, decimal sides)
		{
			int intSides = (int)sides;

			if (intSides != Constants.CoinSides)
			{
				throw new Exceptions.ImpossibleDieException($"{nameof(Coin)} dice may only have {Constants.CoinSides} sides.");
			}

			int roll = roller.RollDie(intSides);

			CoinResult result = roll == intSides
				? CoinResult.Heads
				: CoinResult.Tails;

			return new CoinExpressionResult(result)
			{
				Value = result == CoinResult.Heads ? 1 : 0,
				Sides = intSides,
				TermType = Constants.DiceCoin
			};
		}

		/// <inheritdoc cref="IDice.Type"/>
		public DiceType Type => DiceType.Coin;
	}
}
