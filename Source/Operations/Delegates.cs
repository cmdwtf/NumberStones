using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Operations
{
	public delegate ExpressionResult UnaryOperator(ExpressionResult operand);
	public delegate ExpressionResult BinaryOperator(ExpressionResult leftOperand, ExpressionResult rightOperand);
}
