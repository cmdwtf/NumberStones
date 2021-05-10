
using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Operations
{
	public partial class UnaryOperations
	{
		public static ExpressionResult Negative(IExpression operand)
		{
			ExpressionResult operandResult = operand.Evaluate();

			return new ExpressionResult()
			{
				Value = -operandResult.Value,
				TermType = operandResult.TermType
			};
		}
	}
}
