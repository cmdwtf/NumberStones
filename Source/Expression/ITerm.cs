
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// The IDieTerm interface can be implemented to create a new term for a dice expression,
	/// that will expect to have a roller. A base class, <see cref="Term"/> implements this interface
	/// and uses the default roller if another isn't set.
	/// </summary>
	public interface ITerm : IExpression
	{
		/// <summary>
		/// A roller for the term to use to get it's value.
		/// </summary>
		IDieRoller Roller { set; }
	}
}