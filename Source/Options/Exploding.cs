using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Exploding(decimal Value, ExplodingDiceMode Type, ComparisonDiceMode Mode) : ComparisonOptionBase(Value, Mode)
	{
		public override string Name => $"{nameof(Exploding)} ({Type}) {Mode}";

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
					throw new System.NotImplementedException("Exploding dice not implemented yet.");
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

			string explodeString = Type switch
			{
				ExplodingDiceMode.None => throw new InvalidOptionException($"Invalid {nameof(ExplodingDiceMode)}: {Type}"),
				ExplodingDiceMode.Classic => "!",
				ExplodingDiceMode.Compound => "!!",
				ExplodingDiceMode.Penetrating => "!p",
				_ => throw new InvalidOptionException($"Unhandled {nameof(ExplodingDiceMode)}: {Type}"),
			};

			string comparison = ModeOptionString == "=" ? "" : ModeOptionString;

			builder.Append($"{explodeString}{comparison}{Value}");
		}
	}
}
