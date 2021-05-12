using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Reroll(decimal Value) : DecimalDiceOption(Value)
	{
		public const char Symbol = 'r';

		public override string Name => nameof(Reroll);

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			return input.Select(r =>
			{
				if (Value == r.Value)
				{
					return r with
					{
						Value = Dice.Roll(r.TermType, roller)
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

			builder.Append($"{Symbol}{Value}");
		}
	}
}
