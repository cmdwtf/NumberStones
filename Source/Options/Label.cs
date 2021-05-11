using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public record Label(string Value) : IDiceOption<string>
	{
		public string Name => nameof(Label);

		public IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
			=> input;

		public void BuildOptionString(StringBuilder builder)
		{
			if (string.IsNullOrWhiteSpace(Value))
			{
				return;
			}

			builder.Append($"[{Value}]");
		}
	}
}
