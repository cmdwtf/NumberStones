using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option representing looking for specific values on dice rolls, converting them into success or failures
	/// </summary>
	public record Target(decimal Value, ComparisonDiceMode Mode) : ComparisonOptionBase(Value, Mode)
	{
		/// <inheritdoc cref="IDiceOption.Name"/>
		public override string Name => $"{nameof(Target)} {Mode}";

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
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
					return r with
					{
						Success = true
					};
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

			builder.Append($"{ModeOptionString}{Value}");
		}
	}
}
