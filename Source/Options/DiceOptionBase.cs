using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public abstract record DiceOptionBase<T>(T Value) : IDiceOption<T>
	{
		public abstract string Name { get; }

		public abstract IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller);
		public abstract void BuildOptionString(StringBuilder builder);

		public override string ToString()
		{
			StringBuilder builder = new();
			BuildOptionString(builder);
			return builder.ToString();
		}
	}
}
