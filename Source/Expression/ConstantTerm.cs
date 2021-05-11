using System.Globalization;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// The ConstantTerm class represents a constant value in a DiceExpression
	/// </summary>
	/// <remarks>
	/// In the expression "2d6+5" the integer "5" is a ConstantTerm
	/// </remarks>
	public record ConstantTerm(decimal Constant) : Term
	{
		/// <summary>
		/// Gets the TermResult for this ConstantTerm which will always be a single result with a value of the constant.
		/// </summary>
		/// <returns>A TermResult which will always have a single result with value of the constant</returns>
		public override ExpressionResult Evaluate()
			=> new()
			{
				Value = Constant,
				TermType = "constant"
			};

		/// <summary>
		/// Returns a string that represents this ConstantTerm
		/// </summary>
		/// <returns>A string representing this ConstantTerm</returns>
		public override string ToString() => Constant.ToString(CultureInfo.CurrentCulture);
	}
}