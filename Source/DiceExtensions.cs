
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	public static class DiceExtensions
	{
		private static readonly IDieRoller DieRoller = new DotNetDieRoller();

		public static DiceResult Roll(this DiceExpression diceExpression)
		{
			return diceExpression.Roll(DieRoller);
		}

		public static DiceResult MinRoll(this DiceExpression diceExpression)
		{
			return diceExpression.Roll(new MinDieRoller());
		}

		public static DiceResult MaxRoll(this DiceExpression diceExpression)
		{
			return diceExpression.Roll(new MaxDieRoller());
		}
	}
}