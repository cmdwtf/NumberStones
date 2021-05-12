using System;

using cmdwtf.NumberStones.Exceptions;

namespace cmdwtf.NumberStones.Options
{
	public abstract record ComparisonOptionBase(decimal Value, ComparisonDiceMode Mode) : DecimalDiceOption(Value)
	{
		public const char SymbolEqual = '=';
		public const char SymbolGreater = '>';
		public const char SymbolLess = '<';
		public const char SymbolNot = '~';

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
	}
}
