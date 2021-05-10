using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Operations
{
	public delegate ExpressionResult UnaryOperator(IExpression operand);
	public delegate ExpressionResult BinaryOperator(IExpression leftOperand, IExpression rightOperand);
}
