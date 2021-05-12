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
		public const char Symbol = '!';
		public const char SymbolCompound = '!';
		public const char SymbolPenetrating = 'p';

		public override string Name => $"{nameof(Exploding)} ({Type}) {Mode}";

		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			return input.SelectMany(r =>
			{
				if (ModeComparison(Value, r.Value))
				{
					return ExplodeRoll(r, roller);
				}

				return new[] { r };
			});
		}

		private IEnumerable<DiceExpressionResult> ExplodeRoll(DiceExpressionResult r, IDieRoller roller)
		{
			List<DiceExpressionResult> explodedResults = new List<DiceExpressionResult>();

			int explodedRoll;
			int explodedTotal = 0;
			int explodedCount = 0;
			int sides = (int)r.Sides;

			do
			{
				explodedRoll = roller.RollDie(sides);
				explodedCount++;
				explodedTotal += explodedRoll;

				// penetrating rolls lose 1 when exploding.
				int actualRollValue = Type == ExplodingDiceMode.Penetrating ? explodedRoll - 1 : explodedRoll;

				if (Type == ExplodingDiceMode.Classic ||
					Type == ExplodingDiceMode.Penetrating)
				{
					yield return new DiceExpressionResult()
					{
						Value = actualRollValue,
						Sides = sides,
						TermType = $"!d{sides}",
					};
				}
			}
			while (explodedRoll == sides);

			if (Type == ExplodingDiceMode.Compound)
			{
				yield return new DiceExpressionResult()
				{
					Value = explodedTotal,
					Sides = sides,
					TermType = $"!{explodedCount}d{sides}",
				};
			}
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
