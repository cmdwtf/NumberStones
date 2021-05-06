using System.Collections.Generic;

using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Terms
{
	/// <summary>
	/// The IDiceExpressionTerm interface can be implemented to create a new term for a dice expression
	/// </summary>
	public interface IDiceExpressionTerm
	{
		/// <summary>
		/// Gets the TermResults for the implementation
		/// </summary>
		/// <param name="roller">Die Roller used to perform the Roll.</param>
		/// <returns>An IEnumerable of TermResult which will have one item per result</returns>
		IEnumerable<TermResult> GetResults(IDieRoller roller);

		/// <summary>
		/// Gets the TermResults for the implementation
		/// </summary>
		/// <returns>An IEnumerable of TermResult which will have one item per result</returns>
		/// <remarks>Uses the default roller</remarks>
		IEnumerable<TermResult> GetResults();
	}
}