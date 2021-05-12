using System;

using cmdwtf.NumberStones.Expression;

namespace cmdwtf.NumberStones.Operations
{
	/// <summary>
	/// Operations that have a left and right operand
	/// </summary>
	public static class BinaryOperations
	{
		/// <summary>
		/// Adds the right operand to the left operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Add(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftResult.Value + rightResult.Value,
				TermType = $"{leftResult.TermType} + {rightResult.TermType}"
			};
		}

		/// <summary>
		/// Subtracts the right operand from the left operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Subtract(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftResult.Value - rightResult.Value,
				TermType = $"{leftResult.TermType} - {rightResult.TermType}"
			};
		}

		/// <summary>
		/// Multiplies the left operand by the right operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Multiply(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftResult.Value * rightResult.Value,
				TermType = $"{leftResult.TermType} * {rightResult.TermType}"
			};
		}

		/// <summary>
		/// Divides the left operand by the right operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Divide(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftResult.Value / rightResult.Value,
				TermType = $"{leftResult.TermType} / {rightResult.TermType}"
			};
		}

		/// <summary>
		/// Multiplies the left operand by the right operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Modulo(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftResult.Value % rightResult.Value,
				TermType = $"{leftResult.TermType} % {rightResult.TermType}"
			};
		}

		/// <summary>
		/// Raises the left operand to the right operand power.
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Power(IExpression leftOperand, IExpression rightOperand)
		{
			ExpressionResult leftResult = leftOperand.Evaluate();
			ExpressionResult rightResult = rightOperand.Evaluate();

			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = (decimal)Math.Pow((double)leftResult.Value, (double)rightResult.Value),
				TermType = $"{leftResult.TermType}^{rightResult.TermType}"
			};
		}
	}
}
