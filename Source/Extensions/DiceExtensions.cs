using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	// #doc
	public static class DiceExtensions
	{
		public static DiceResult Roll(this DiceExpression diceExpression) => diceExpression.Roll(Instances.DefaultRoller);

		public static DiceResult MinRoll(this DiceExpression diceExpression) => diceExpression.Roll(Instances.MinRoller);

		public static DiceResult MaxRoll(this DiceExpression diceExpression) => diceExpression.Roll(Instances.MaxRoller);
	}
}