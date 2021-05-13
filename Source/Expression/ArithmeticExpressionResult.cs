namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record that holds the result of an arithmetic expression, to investigate their operands later
	/// </summary>
	public record ArithmeticExpressionResult(params ExpressionResult[] Operands) : ExpressionResult
	{
		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="ExpressionResult.ToString"/>
		public override string ToString() => base.ToString();
	}
}
