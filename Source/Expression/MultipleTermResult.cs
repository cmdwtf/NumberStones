using System.Collections.Generic;
using System.Linq;

namespace cmdwtf.NumberStones.Expression
{
	public record MultipleTermResult : ExpressionResult
	{
		private readonly List<ExpressionResult> _subResults = new();
		public IReadOnlyList<ExpressionResult> SubResults { get; private init; }

		public MultipleTermResult(IEnumerable<ExpressionResult> subResults)
		{
			_subResults.AddRange(subResults);
			SubResults = _subResults;

			Value = _subResults.Sum(r => r.Value);
		}
	}
}
