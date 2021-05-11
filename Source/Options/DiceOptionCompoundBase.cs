using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	public abstract record DiceOptionCompoundBase : IDiceOption
	{
		protected List<IDiceOption> SubOptions { get; } = new();

		public virtual string Name => string.Join(" ", SubOptions.Select(so => so.Name));

		public IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			IEnumerable<DiceExpressionResult> output = input;
			foreach (IDiceOption so in SubOptions)
			{
				input = output;
				output = so.Apply(input, roller);
			}

			return output;
		}

		public abstract void BuildOptionString(StringBuilder builder);
	}
}
