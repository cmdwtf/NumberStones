namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// An expression with no value.
	/// </summary>
	public record EmptyExpression : IExpression
	{
		/// <summary>
		/// Evaluates the empty expression, returning, as you may expect: an empty expression result.
		/// </summary>
		/// <param name="context">The evaluation context</param>
		/// <returns>An empty expression</returns>
		public ExpressionResult Evaluate(EvaluationContext context) => ExpressionResult.Empty;

		/// <summary>
		/// An instance to easily access an empty expression
		/// </summary>
		public static EmptyExpression Instance { get; } = new EmptyExpression();
	}
}
