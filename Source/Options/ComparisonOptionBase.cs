using System;

using cmdwtf.NumberStones.Exceptions;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An abstract implementation of a <see cref="DecimalDiceOption"/> that has
	/// helpers to handle comparison modes for options
	/// </summary>
	public abstract record ComparisonOptionBase(decimal Value, ComparisonDiceMode Mode) : DecimalDiceOption(Value)
	{
		/// <summary>
		/// A dice expression symbol for equality
		/// </summary>
		public const char SymbolEqual = '=';

		/// <summary>
		/// A dice expression symbol for greater than
		/// </summary>
		public const char SymbolGreater = '>';

		/// <summary>
		/// A dice expression symbol for less than
		/// </summary>
		public const char SymbolLess = '<';

		/// <summary>
		/// A dice expression symbol for not
		/// </summary>
		public const char SymbolNot = '~';

		/// <summary>
		/// Returns a string based on the comparison mode option set
		/// </summary>
		protected string ModeOptionString => Mode switch
		{
			ComparisonDiceMode.None => throw new InvalidOptionException($"Invalid {nameof(ComparisonDiceMode)}: {Mode}"),
			ComparisonDiceMode.Equals => $"{SymbolEqual}",
			ComparisonDiceMode.GreaterThan => $"{SymbolGreater}",
			ComparisonDiceMode.LessThan => $"{SymbolLess}",
			ComparisonDiceMode.GreaterThanEquals => $"{SymbolGreater}{SymbolEqual}",
			ComparisonDiceMode.LessThanEquals => $"{SymbolLess}{SymbolEqual}",
			ComparisonDiceMode.Not => $"{SymbolNot}{SymbolEqual}",
			_ => throw new InvalidOptionException($"Unhandled {nameof(ComparisonDiceMode)}: {Mode}"),
		};

		/// <summary>
		/// Returns a comparison function based on the comparison mode option set
		/// </summary>
		protected Func<decimal, decimal, bool> ModeComparison => Mode switch
		{
			ComparisonDiceMode.None => throw new InvalidOptionException($"Invalid {nameof(ComparisonDiceMode)}: {Mode}"),
			ComparisonDiceMode.Equals => ((a, b) => a == b),
			ComparisonDiceMode.GreaterThan => ((a, b) => a > b),
			ComparisonDiceMode.LessThan => ((a, b) => a < b),
			ComparisonDiceMode.GreaterThanEquals => ((a, b) => a >= b),
			ComparisonDiceMode.LessThanEquals => ((a, b) => a <= b),
			ComparisonDiceMode.Not => ((a, b) => a != b),
			_ => throw new InvalidOptionException($"Unhandled {nameof(ComparisonDiceMode)}: {Mode}"),
		};

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
