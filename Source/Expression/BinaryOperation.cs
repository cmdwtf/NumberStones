﻿using cmdwtf.NumberStones.Operations;

namespace cmdwtf.NumberStones.Expression
{
	// #doc
	public record BinaryOperation(IExpression LeftOperand, IExpression RightOperand, BinaryOperator Operator)
		: IOperation
	{
		public ExpressionResult Evaluate()
		{
			ExpressionResult result = Operator(LeftOperand, RightOperand);
			return result;
		}

		public static BinaryOperation Add(IExpression left, IExpression right) => new(left, right, BinaryOperations.Add);
		public static BinaryOperation Subtract(IExpression left, IExpression right) => new(left, right, BinaryOperations.Subtract);
		public static BinaryOperation Multiply(IExpression left, IExpression right) => new(left, right, BinaryOperations.Multiply);
		public static BinaryOperation Divide(IExpression left, IExpression right) => new(left, right, BinaryOperations.Divide);
		public static BinaryOperation Modulo(IExpression left, IExpression right) => new(left, right, BinaryOperations.Modulo);
	}
}
