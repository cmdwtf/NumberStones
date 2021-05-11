using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Keep(int Amount) : DecimalDiceOption(Amount)
	{
		public HighLowMode Mode { get; init; } = HighLowMode.High;

		public override string Name => nameof(Keep);

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			switch (Mode)
			{
				case HighLowMode.Low:
					return input.OrderBy(r => r.Value).Take(Amount);
				case HighLowMode.High:
					return input.OrderByDescending(r => r.Value).Take(Amount);
				default:
					throw new InvalidOptionException("Unhandled high low mode.");
			}
		}

		public override void BuildOptionString(StringBuilder builder)
		{
			if (Amount == 0)
			{
				return;
			}

			string hl = Mode == HighLowMode.High ? "h" : "l";
			builder.Append($"k{hl}{Amount}");
		}
	}
}
