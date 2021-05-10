﻿
using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Operations
{
	public partial class BinaryOperations
	{
		public static ExpressionResult Multiply(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ExpressionResult()
			{
				Value = leftResult.Value * rightResult.Value,
				TermType = $"{leftResult.TermType} * {rightResult.TermType}"
			};
		}
	}
}
