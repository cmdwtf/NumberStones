using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// A base class for any dice option that carries a value of a given type with it.
	/// </summary>
	/// <typeparam name="T">The type of the option value</typeparam>
	public abstract record DiceOptionBase<T>(T Value) : IDiceOption<T>
	{
		/// <inheritdoc cref="IDiceOption.Name"/>
		public abstract string Name { get; }

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
		public abstract IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller);

		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public abstract void BuildOptionString(StringBuilder builder);

		/// <summary>
		/// Returns a string based on the option string representation of this dice option
		/// </summary>
		/// <returns>A dice expression option string based on this option</returns>
		public override string ToString()
		{
			StringBuilder builder = new();
			BuildOptionString(builder);
			return builder.ToString();
		}
	}
}
