using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record NopOption : IDiceOption
	{
		string IDiceOption.Name => string.Empty;
		IEnumerable<DiceExpressionResult> IDiceOption.Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller) => input;
		void IDiceOption.BuildOptionString(StringBuilder builder) { }

		public static readonly NopOption Instance = new();
	}
}
