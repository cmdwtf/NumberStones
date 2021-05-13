using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record PlanechaseExpressionResult(PlanarResult Result) : DiceExpressionResult
	{
		/// <summary>
		/// This result represents a Planechase dice roll.
		/// </summary>
		public override DiceType Type { get; init; } = DiceType.Planar;
	}
}
