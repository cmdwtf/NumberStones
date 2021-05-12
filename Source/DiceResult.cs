
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The DiceResult class represents the result of rolling a DiceExpression
	/// </summary>
	public record DiceResult(ExpressionResult Results, IDieRoller RollerUsed)
	{
		/// <summary>
		/// The total result of the the roll
		/// </summary>
		public decimal Value => Results.Value;

		public override string ToString() => $"{Results}";
	}
}