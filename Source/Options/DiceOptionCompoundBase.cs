using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An abstract implementation of a dice option that can compose multiple suboptions
	/// </summary>
	internal abstract record DiceOptionCompoundBase : IDiceOption
	{
		protected List<IDiceOption> SubOptions { get; } = new();

		/// <inheritdoc cref="IDiceOption.Name"/>
		public virtual string Name => string.Join(" ", SubOptions.Select(so => so.Name));


		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
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

		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public abstract void BuildOptionString(StringBuilder builder);

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => string.Join(" ", SubOptions.Select(so => so.ToString()));
	}
}
