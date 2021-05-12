using cmdwtf.NumberStones.Operations;

namespace cmdwtf.NumberStones.Expression
{
	// #doc
	public record UnaryOperation(IExpression Operand, UnaryOperator Operator, char Symbol)
		: IOperation
	{
		public ExpressionResult Evaluate()
		{
			ExpressionResult result = Operator(Operand);
			return result;
		}

		public override string ToString() => $"{Symbol}{Operand}";

		public static UnaryOperation Negate(IExpression operand) => new(operand, UnaryOperations.Negate, '-');

		public delegate UnaryOperation UnaryOperationCreationDelegate(IExpression operand);
	}
}
