using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Keep(decimal Amount, HighLowMode Mode = HighLowMode.High) : DecimalDiceOption(Amount)
	{
		public const char Symbol = 'k';
		public const char SymbolHigh = 'h';
		public const char SymbolLow = 'l';

		public override string Name => nameof(Keep);

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			return Mode switch
			{
				HighLowMode.Low => input.OrderBy(r => r.Value).Take((int)Amount),
				HighLowMode.High => input.OrderByDescending(r => r.Value).Take((int)Amount),
				_ => throw new InvalidOptionException("Unhandled high low mode."),
			};
		}

		public override void BuildOptionString(StringBuilder builder)
		{
			if (Amount == 0)
			{
				return;
			}

			char hl = Mode == HighLowMode.High ? SymbolHigh : SymbolLow;
			builder.Append($"{Symbol}{hl}{Amount}");
		}
	}
}
