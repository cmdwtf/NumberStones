using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option representing a piece of arbitrary text attached to a dice expression term
	/// </summary>
	public record Label(string Value) : IDiceOption<string>
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
		public string Name => nameof(Label);

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
		public IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
			=> input;

		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public void BuildOptionString(StringBuilder builder)
		{
			if (string.IsNullOrWhiteSpace(Value))
			{
				return;
			}

			builder.Append($"{SymbolOpen}{Value}{SymbolClose}");
		}
	}
}
