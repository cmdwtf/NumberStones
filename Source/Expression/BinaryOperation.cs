using cmdwtf.NumberStones.Operations;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record representing a binary operation
	/// </summary>
	public record BinaryOperation(IExpression LeftOperand, IExpression RightOperand, BinaryOperator Operator, char Symbol)
		: IOperation
	{
		/// <summary>
		/// Executes the operation on the operands
		/// </summary>
		/// <returns>The result of the operation</returns>
		public ExpressionResult Evaluate()
		{
			ExpressionResult result = Operator(LeftOperand, RightOperand);
			return result;
		}

		/// <summary>
		/// Returns a string representing this operation
		/// </summary>
		/// <returns>A string representing this operation</returns>
		public override string ToString() => $"{LeftOperand} {Symbol} {RightOperand}";

		/// <summary>
		/// A shortcut to create an Add operation
		/// </summary>
		/// <param name="left">The term to the left of the operator</param>
		/// <param name="right">The term to the right of the operator</param>
		/// <returns>A new operation representing the action on the chosen terms</returns>
		public static BinaryOperation Add(IExpression left, IExpression right) => new(left, right, BinaryOperations.Add, '+');

		/// <summary>
		/// A shortcut to create a Subtract operation
		/// </summary>
		/// <param name="left">The term to the left of the operator</param>
		/// <param name="right">The term to the right of the operator</param>
		/// <returns>A new operation representing the action on the chosen terms</returns>
		public static BinaryOperation Subtract(IExpression left, IExpression right) => new(left, right, BinaryOperations.Subtract, '-');

		/// <summary>
		/// A shortcut to create a Multiply operation
		/// </summary>
		/// <param name="left">The term to the left of the operator</param>
		/// <param name="right">The term to the right of the operator</param>
		/// <returns>A new operation representing the action on the chosen terms</returns>
		public static BinaryOperation Multiply(IExpression left, IExpression right) => new(left, right, BinaryOperations.Multiply, '*');

		/// <summary>
		/// A shortcut to create a Divide operation
		/// </summary>
		/// <param name="left">The term to the left of the operator</param>
		/// <param name="right">The term to the right of the operator</param>
		/// <returns>A new operation representing the action on the chosen terms</returns>
		public static BinaryOperation Divide(IExpression left, IExpression right) => new(left, right, BinaryOperations.Divide, '/');

		/// <summary>
		/// A shortcut to create a Modulo operation
		/// </summary>
		/// <param name="left">The term to the left of the operator</param>
		/// <param name="right">The term to the right of the operator</param>
		/// <returns>A new operation representing the action on the chosen terms</returns>
		public static BinaryOperation Modulo(IExpression left, IExpression right) => new(left, right, BinaryOperations.Modulo, '%');

		/// <summary>
		/// A shortcut to create a Power operation
		/// </summary>
		/// <param name="left">The term to the left of the operator</param>
		/// <param name="right">The term to the right of the operator</param>
		/// <returns>A new operation representing the action on the chosen terms</returns>
		public static BinaryOperation Power(IExpression left, IExpression right) => new(left, right, BinaryOperations.Power, '^');

		/// <summary>
		/// A delegate for the shortcut creation methods
		/// </summary>
		/// <param name="left">The operand to the left of the operator the operation should act on</param>
		/// <param name="right">The operand to the right of the operator the operation should act on</param>
		/// <returns>The operation representing the action on the chosen terms</returns>
		public delegate BinaryOperation BinaryOperationCreationDelegate(IExpression left, IExpression right);
	}
}
