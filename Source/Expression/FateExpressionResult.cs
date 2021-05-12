using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record FateExpressionResult(FateResult Result) : DiceExpressionResult
	{
		/// <summary>
		/// This result represents a fate expression result.
		/// </summary>
		public override DiceType Type { get; init; } = DiceType.Fate;
	}
}
