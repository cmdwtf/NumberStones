
using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Operations
{
	/// <summary>
	/// Operations that have only a single operand
	/// </summary>
	public static class UnaryOperations
	{
		/// <summary>
		/// Negates the value of the operand
		/// </summary>
		/// <param name="operand">The operand to negate</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Negate(ExpressionResult operand)
		{
			return new ArithmeticExpressionResult(operand)
			{
				Value = -operand.Value,
				TermType = $"-{operand.TermType}"
			};
		}
	}
}
