using System.Collections.Generic;
using System.Text;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// A do-nothing option to use as an empty placeholder if needed
	/// IDiceOption is explicitly implemented to prevent accidental use
	/// </summary>
	public record NopOption : IDiceOption
	{
		string IDiceOption.Name => string.Empty;
		IEnumerable<DiceExpressionResult> IDiceOption.Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller) => input;
		void IDiceOption.BuildOptionString(StringBuilder builder) { }

		/// <summary>
		/// Get an instance of the <see cref="NopOption"/>
		/// </summary>
		public static NopOption Instance { get; } = new();

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => string.Empty;
	}
}
