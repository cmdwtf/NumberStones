using cmdwtf.NumberStones.Operations;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record representing a unary operation
	/// </summary>
	public record UnaryOperation(IExpression Operand, UnaryOperator Operator, char Symbol)
		: IOperation
	{
		/// <summary>
		/// Executes the operation on the operand
		/// </summary>
		/// <returns>The result of the operation</returns>
		public ExpressionResult Evaluate()
		{
			ExpressionResult result = Operator(Operand);
			return result;
		}

		/// <summary>
		/// Returns a string representing this operation
		/// </summary>
		/// <returns>A string representing this operation</returns>
		public override string ToString() => $"{Symbol}{Operand}";

		/// <summary>
		/// A shortcut to create a Negate operation
		/// </summary>
		/// <param name="operand">The term to negate</param>
		/// <returns>A new operation representing the action the chosen term</returns>
		public static UnaryOperation Negate(IExpression operand) => new(operand, UnaryOperations.Negate, '-');

		/// <summary>
		/// A delegate for the shortcut creation methods
		/// </summary>
		/// <param name="operand">The operand the operation should act on</param>
		/// <returns>The operation representing the action on the chosen term</returns>
		public delegate UnaryOperation UnaryOperationCreationDelegate(IExpression operand);
	}
}
