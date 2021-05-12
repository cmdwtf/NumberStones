using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Drop(decimal Amount, HighLowMode Mode = HighLowMode.Low) : DecimalDiceOption(Amount)
	{
		public const char Symbol = 'd';
		public const char SymbolHigh = 'h';
		public const char SymbolLow = 'l';

		public override string Name => nameof(Drop);

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			int count = input.Count();
			int keep = count - (int)Value;

			return Mode switch
			{
				HighLowMode.Low => input.OrderBy(r => r.Value).Take(keep),
				HighLowMode.High => input.OrderByDescending(r => r.Value).Take(keep),
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
