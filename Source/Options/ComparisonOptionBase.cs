using System;

using cmdwtf.NumberStones.Exceptions;

namespace cmdwtf.NumberStones.Options
{
	public abstract record ComparisonOptionBase(decimal Value, ComparisonDiceMode Mode) : DecimalDiceOption(Value)
	{
		protected string ModeOptionString => Mode switch
		{
			ComparisonDiceMode.None => throw new InvalidOptionException($"Invalid {nameof(ComparisonDiceMode)}: {Mode}"),
			ComparisonDiceMode.Equals => "=",
			ComparisonDiceMode.GreaterThan => ">",
			ComparisonDiceMode.LessThan => "<",
			ComparisonDiceMode.GreaterThanEquals => ">=",
			ComparisonDiceMode.LessThanEquals => "<=",
			ComparisonDiceMode.Not => "~=",
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
