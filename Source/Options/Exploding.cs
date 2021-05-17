using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option representing exploding dice that can continue rolling aditional dice, by default on max rolls
	/// </summary>
	public record Exploding(decimal Value, ExplodingDiceMode Type, ComparisonDiceMode Mode) : ComparisonOptionBase(Value, Mode)
	{
		/// <summary>
		/// The dice expression symbol for this option
		/// </summary>
		public const char Symbol = '!';
		/// <summary>
		/// The dice expression symbol for the compound option
		/// </summary>
		public const char SymbolCompound = '!';
		/// <summary>
		/// The dice expression symbol for the penetrating option
		/// </summary>
		public const char SymbolPenetrating = 'p';

		/// <inheritdoc cref="IDiceOption.Name"/>
		public override string Name => $"{nameof(Exploding)} ({Type}) {Mode}";

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			decimal target = Value;

			if (target == 0)
			{
				target = input.First().Sides;
			}

			return input.SelectMany(r =>
			{
				if (ModeComparison(r.Value, target))
				{
					return ExplodeRoll(target, r, roller);
				}

				return new[] { r };
			});
		}

		private IEnumerable<DiceExpressionResult> ExplodeRoll(decimal target, DiceExpressionResult r, IDieRoller roller)
		{
			// check to see if we will be exploding forever.
			// if the roller's range is 0, it will be returning
			// the same value that got us exploding in the first place,
			// and thus we can safely return with a fixed high value.
			if (roller.Range == 0 || r.Sides <= 1)
			{
				yield return new()
				{
					Value = long.MaxValue,
					Sides = r.Sides,
					TermType = $"!d{r.Sides}"
				};

				yield break;
			}

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

				if (Type is ExplodingDiceMode.Classic or
					ExplodingDiceMode.Penetrating)
				{
					yield return new DiceExpressionResult()
					{
						Value = actualRollValue,
						Sides = sides,
						TermType = $"!d{sides}",
					};
				}
			}
			while (ModeComparison(explodedRoll, target));

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


		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public override void BuildOptionString(StringBuilder builder)
		{
			string explodeString = Type switch
			{
				ExplodingDiceMode.None => throw new InvalidOptionException($"Invalid {nameof(ExplodingDiceMode)}: {Type}"),
				ExplodingDiceMode.Classic => "!",
				ExplodingDiceMode.Compound => "!!",
				ExplodingDiceMode.Penetrating => "!p",
				_ => throw new InvalidOptionException($"Unhandled {nameof(ExplodingDiceMode)}: {Type}"),
			};

			builder.Append($"{explodeString}");

			if (Value != 0 && Mode != ComparisonDiceMode.GreaterThanEquals)
			{
				builder.Append($"{ModeOptionString}{Value}");
			}
		}

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
