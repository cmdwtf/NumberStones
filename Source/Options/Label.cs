using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option representing a piece of arbitrary text attached to a dice expression term
	/// </summary>
	public record Label(string Value) : DiceOptionBase<string>(Value)
	{
		/// <summary>
		/// The symbol to open this option
		/// </summary>
		public const char SymbolOpen = '[';
		/// <summary>
		/// The symbol to close this option
		/// </summary>
		public const char SymbolClose = ']';

		/// <inheritdoc cref="IDiceOption.Name"/>
		public override string Name => nameof(Label);

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
			=> input;

		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public override void BuildOptionString(StringBuilder builder)
		{
			if (string.IsNullOrWhiteSpace(Value))
			{
				return;
			}

			builder.Append($"{SymbolOpen}{Value}{SymbolClose}");
		}

		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
