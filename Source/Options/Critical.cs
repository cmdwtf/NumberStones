using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option representing the ability to set arbitrary goals for what is and
	/// is not considered a critical success or failure based on a dice roll
	/// </summary>
	public record Critical(decimal Value, CriticalTypeMode Type, ComparisonDiceMode Mode) : ComparisonOptionBase(Value, Mode)
	{
		/// <summary>
		/// The dice expression symbol for this option
		/// </summary>
		public const char Symbol = 'c';
		/// <summary>
		/// The dice expression symbol for setting success based on the values
		/// </summary>
		public const char SymbolSuccess = 's';
		/// <summary>
		/// The dice expression symbol for setting failure based on the values
		/// </summary>
		public const char SymbolFailure = 'f';

		/// <inheritdoc cref="IDiceOption.Name"/>
		public override string Name => $"{nameof(Critical)} {Type} {Mode}";

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			if (Value == 0)
			{
				return input;
			}

			return input.Select(r =>
			{
				if (ModeComparison(r.Value, Value))
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

		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public override void BuildOptionString(StringBuilder builder)
		{
			if (Value == 0)
			{
				return;
			}

			char critModeChar = Type == CriticalTypeMode.Success ? SymbolSuccess : SymbolFailure;

			builder.Append($"{Symbol}{critModeChar}{ModeOptionString}{Value}");
		}

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
