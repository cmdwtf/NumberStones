namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// An interface representing any individual, evaluateable expression
	/// </summary>
	public interface IExpression
	{
		/// <summary>
		/// Evaluate the expression and get the results
		/// </summary>
		/// <param name="context">The evaluation context</param>
		/// <returns>The expression's results</returns>
		ExpressionResult Evaluate(EvaluationContext context);
	}
}
