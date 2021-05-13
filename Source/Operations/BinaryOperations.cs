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
		public static ExpressionResult Add(ExpressionResult leftOperand, ExpressionResult rightOperand)
		{
			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftOperand.Value + rightOperand.Value,
				TermType = $"{leftOperand.TermType} + {rightOperand.TermType}"
			};
		}

		/// <summary>
		/// Subtracts the right operand from the left operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Subtract(ExpressionResult leftOperand, ExpressionResult rightOperand)
		{
			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftOperand.Value - rightOperand.Value,
				TermType = $"{leftOperand.TermType} - {rightOperand.TermType}"
			};
		}

		/// <summary>
		/// Multiplies the left operand by the right operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Multiply(ExpressionResult leftOperand, ExpressionResult rightOperand)
		{
			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftOperand.Value * rightOperand.Value,
				TermType = $"{leftOperand.TermType} * {rightOperand.TermType}"
			};
		}

		/// <summary>
		/// Divides the left operand by the right operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Divide(ExpressionResult leftOperand, ExpressionResult rightOperand)
		{
			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftOperand.Value / rightOperand.Value,
				TermType = $"{leftOperand.TermType} / {rightOperand.TermType}"
			};
		}

		/// <summary>
		/// Multiplies the left operand by the right operand
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Modulo(ExpressionResult leftOperand, ExpressionResult rightOperand)
		{
			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = leftOperand.Value % rightOperand.Value,
				TermType = $"{leftOperand.TermType} % {rightOperand.TermType}"
			};
		}

		/// <summary>
		/// Raises the left operand to the right operand power.
		/// </summary>
		/// <param name="leftOperand">The left operand</param>
		/// <param name="rightOperand">The right operand</param>
		/// <returns>The result of the operation</returns>
		public static ExpressionResult Power(ExpressionResult leftOperand, ExpressionResult rightOperand)
		{
			return new ArithmeticExpressionResult(leftOperand, rightOperand)
			{
				Value = (decimal)Math.Pow((double)leftOperand.Value, (double)rightOperand.Value),
				TermType = $"{leftOperand.TermType}^{rightOperand.TermType}"
			};
		}
	}
}
