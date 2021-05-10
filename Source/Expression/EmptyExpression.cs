namespace cmdwtf.NumberStones.Expression
{
	// #doc
	public record EmptyExpression : IExpression
	{
		public ExpressionResult Evaluate() => new() { Value = 0, TermType = "?" };
		public static EmptyExpression Instance { get; } = new EmptyExpression();
	}
}
