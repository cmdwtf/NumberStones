namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record that holds the result of an arithmetic expression, to investigate their operands later
	/// </summary>
	public record ArithmeticExpressionResult(params ExpressionResult[] Operands) : ExpressionResult
	{

	}
}
