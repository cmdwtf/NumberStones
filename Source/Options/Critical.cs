using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Critical(decimal Value, CriticalTypeMode Type, ComparisonDiceMode Mode) : ComparisonOptionBase(Value, Mode)
	{
		public override string Name => $"{nameof(Critical)} {Type} {Mode}";

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
					switch (Type)
					{
						case CriticalTypeMode.Success:
							return r with { CriticalSuccess = true };
						case CriticalTypeMode.Failure:
							return r with { CriticalFailure = true };
					}
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

			char critModeChar = Type == CriticalTypeMode.Success ? 's' : 'f';

			builder.Append($"c{critModeChar}{ModeOptionString}{Value}");
		}
	}
}
