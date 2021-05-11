using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Fate(bool Enabled) : BoolDiceOption(Enabled)
	{
		public override string Name => nameof(Fate);

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller) => input;
		public override void BuildOptionString(StringBuilder builder) { }
	}
}
