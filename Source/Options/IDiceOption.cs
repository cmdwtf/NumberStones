using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An interface representing any dice option.
	/// </summary>
	public interface IDiceOption
	{
		/// <summary>
		/// The name of this option
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Applies Dice Exploding to the given dice expression results
		/// </summary>
		/// <param name="input">The input to apply the option to</param>
		/// <param name="roller">The roller used to generate the results</param>
		/// <returns>The results, modified if needed</returns>
		IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller);

		/// <summary>
		/// Builds a dice expression option string based on this option
		/// </summary>
		/// <param name="builder">The string builder to build the string into</param>
		void BuildOptionString(StringBuilder builder);
	}

	/// <summary>
	/// A typed dice option.
	/// </summary>
	/// <typeparam name="T">The type the option uses to represent it's state</typeparam>
	public interface IDiceOption<T> : IDiceOption
	{
		/// <summary>
		/// The value for this typed dice option.
		/// </summary>
		T Value { get; }
	}
}
