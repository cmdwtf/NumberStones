using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option that sets specific dice values to double their value.
	/// </summary>
	public record Twice(decimal Value) : DecimalDiceOption(Value)
	{
		/// <summary>
		/// The dice expression symbol for this option
		/// </summary>
		public const char Symbol = 't';

		/// <inheritdoc cref="IDiceOption.Name"/>
		public override string Name => nameof(Twice);

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
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
					return r with { Value = r.Value * 2m };
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

			builder.Append($"{Symbol}{Value}");
		}

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
