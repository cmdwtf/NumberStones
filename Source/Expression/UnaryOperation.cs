using cmdwtf.NumberStones.Operations;

namespace cmdwtf.NumberStones.Expression
{
	// #doc
	public record UnaryOperation(IExpression Operand, UnaryOperator Operator)
		: IOperation
	{
		public ExpressionResult Evaluate()
		{
			ExpressionResult result = Operator(Operand);
			return result;
		}

		public static UnaryOperation Negative(IExpression operand) => new(operand, UnaryOperations.Negative);
	}
}
