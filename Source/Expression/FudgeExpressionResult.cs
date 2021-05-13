﻿using cmdwtf.NumberStones.DiceTypes;

namespace cmdwtf.NumberStones.Expression
{
	public record FudgeExpressionResult(FudgeResult Result) : DiceExpressionResult
	{
		/// <summary>
		/// This result represents a fate expression result.
		/// </summary>
		public override DiceType Type { get; init; } = DiceType.Fudge;

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceExpressionResult.ToString"/>
		public override string ToString() => base.ToString();
	}
}
