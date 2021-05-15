using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record FudgeDiceExpressionResult(FudgeResult Result) : DiceExpressionResult
	{
		/// <summary>
		/// This result represents a fate expression result.
		/// </summary>
		public override DiceType Type { get; init; } = DiceType.Fudge;

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceExpressionResult.ToString"/>
		public override string ToString()
			=> Result switch
			{
				FudgeResult.Nothing => " ",
				FudgeResult.Plus => "+",
				FudgeResult.Minus => "-",
				_ => throw new Exceptions.ImpossibleDieException($"{nameof(FudgeDiceExpressionResult)} dice aren't supposed to have this result type"),
			};
	}
}
