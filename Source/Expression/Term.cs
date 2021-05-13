namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// An abstract start to a concrete ITerm
	/// </summary>
	public abstract record Term : ITerm
	{
		/// <summary>
		/// Evaluates the term and gets the expression result for the evaluation
		/// </summary>
		/// <param name="context">The evaluation context</param>
		/// <returns>The evaluated result</returns>
		public abstract ExpressionResult Evaluate(EvaluationContext context);

		/// <summary>
		/// Gets a string representing this term. Implementers should override thisl.
		/// </summary>
		/// <returns>A string with the term's class name</returns>
		public override string ToString() => GetType().Name;
	}
}
