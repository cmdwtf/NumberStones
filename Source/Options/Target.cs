using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Target(decimal Value, ComparisonDiceMode Mode) : ComparisonOptionBase(Value, Mode)
	{
		public override string Name => $"{nameof(Target)} {Mode}";

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			return input.Select(r =>
			{
				if (ModeComparison(Value, r.Value))
				{
					return r with
					{
						Success = true
					};
				}

				return r;
			});
		}

		public override void BuildOptionString(StringBuilder builder)
		{
			if (Value == 0)
			{
				return;
			}

			builder.Append($"{ModeOptionString}{Value}");
		}
	}
}
