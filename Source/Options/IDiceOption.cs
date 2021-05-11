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
		string Name { get; }

		IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller);

		void BuildOptionString(StringBuilder builder);
	}

	/// <summary>
	/// A typed dice option.
	/// </summary>
	/// <typeparam name="T">The type the option uses to represent it's state</typeparam>
	public interface IDiceOption<T> : IDiceOption
	{
		T Value { get; }
	}
}
