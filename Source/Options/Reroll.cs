using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option that causes a reroll of a die of a specified value
	/// </summary>
	public record Reroll(decimal Value) : DecimalDiceOption(Value)
	{
		/// <summary>
		/// The dice option string symbol used for this option.
		/// </summary>
		public const char Symbol = 'r';

		/// <inheritdoc cref="DiceOptionBase{T}.Name"/>
		public override string Name => nameof(Reroll);

		/// <inheritdoc cref="DiceOptionBase{T}.Apply"/>
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
						Value = roller.RollDie((int)r.Sides),
					};
				}

				return r;
			});
		}

		/// <inheritdoc cref="DiceOptionBase{T}.BuildOptionString"/>
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
