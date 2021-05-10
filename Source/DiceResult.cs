
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The DiceResult class represents the result of rolling a DiceExpression
	/// </summary>
	public class DiceResult
	{
		/// <summary>
		/// The dice roller used to get this result
		/// </summary>
		public IDieRoller RollerUsed { get; private set; }

		/// <summary>
		/// A Collection of TermResults that represents one result for each DiceTerm in the DiceExpression
		/// </summary>
		public ExpressionResult Results { get; private set; }

		/// <summary>
		/// The total result of the the roll
		/// </summary>
		public decimal Value { get; private set; }

		/// <summary>
		/// Construct a new DiceResult from the specified values
		/// </summary>
		/// <param name="results">An IEnumerable of TermResult that represents one result for each DiceTerm in the DiceExpression</param>
		/// <param name="rollerUsed">The random number generator used to get this result</param>
		public DiceResult(ExpressionResult results, IDieRoller rollerUsed)
		{
			RollerUsed = rollerUsed;
			Results = results;
			Value = results.Value;
		}
	}
}