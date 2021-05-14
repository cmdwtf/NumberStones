using System.Collections.Generic;
using System.Linq;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record representing multiple dice term results as a single result.
	/// </summary>
	public record MultipleDiceTermResult : ExpressionResult
	{
		private readonly List<DiceExpressionResult> _subResults = new();

		/// <summary>
		/// The individual term results that make up this multiple term result
		/// </summary>
		public IReadOnlyList<DiceExpressionResult> SubResults { get; private init; }

		/// <summary>
		/// A botch is no successes and at least one failure. This is based on
		/// recent World of Darkness Botch rules: https://rpg.stackexchange.com/a/95996
		/// </summary>
		public DiceBoolean Botch { get; private init; } = DiceBoolean.Unset;

		/// <summary>
		/// A botch is a collection of rolls with more failures than successes.
		/// This is based on the World of Darkness 1E botch rules.
		/// </summary>
		public DiceBoolean Botch1E { get; private init; } = DiceBoolean.Unset;

		/// <summary>
		/// Creates a new multiple term result
		/// </summary>
		/// <param name="subResults">A list of results to store as the sub results</param>
		public MultipleDiceTermResult(IEnumerable<DiceExpressionResult> subResults)
		{
			_subResults.AddRange(subResults);
			SubResults = _subResults;

			Value = _subResults.Sum(r => r.Value);

			// set botches based on failure and success count
			int successes = _subResults.Count(r => r.Success);
			int failures = _subResults.Count(r => r.Failure);

			if (successes > 0 || failures > 0)
			{
				Botch = (successes == 0 && failures > 1);
				Botch1E = (successes < failures);
			}
		}

		/// <summary>
		/// Returns a string of each of the sub results joined together
		/// </summary>
		/// <returns>A string describing the sub results of this multiple term result</returns>
		public override string ToString() => string.Join(", ", _subResults);
	}
}
