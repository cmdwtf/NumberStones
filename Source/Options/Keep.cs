﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An option representing keeping values from a multi-dice roll, and dropping the rest
	/// </summary>
	public record Keep(decimal Amount, HighLowMode Mode = HighLowMode.High) : DecimalDiceOption(Amount)
	{
		/// <summary>
		/// The dice expression symbol for this option
		/// </summary>
		public const char Symbol = 'k';
		/// <summary>
		/// The dice expression symbol for acting on high rolls
		/// </summary>
		public const char SymbolHigh = 'h';
		/// <summary>
		/// The dice expression symbol for acting on low rolls
		/// </summary>
		public const char SymbolLow = 'l';

		/// <inheritdoc cref="IDiceOption.Name"/>
		public override string Name => nameof(Keep);

		/// <inheritdoc cref="IDiceOption.Apply(IEnumerable{DiceExpressionResult}, IDieRoller)"/>
		public override IEnumerable<DiceExpressionResult> Apply(IEnumerable<DiceExpressionResult> input, IDieRoller roller)
		{
			int total = input.Count();
			int amount = (int)Amount;
			int remain = total - amount;

			if (amount >= total)
			{
				return input;
			}

			IOrderedEnumerable<DiceExpressionResult> ordered = Mode switch
			{
				HighLowMode.Low => input.OrderBy(r => r.Value),
				HighLowMode.High => input.OrderByDescending(r => r.Value),
				_ => throw new InvalidOptionException("Unhandled high low mode."),
			};

			return ordered.Take(amount)
				.Concat(
					ordered.Skip(amount)
						.Select(r => r with
						{
							Value = 0,
							Dropped = new DiceDropResult(r.Value, ToString())
						})
				);
		}

		/// <inheritdoc cref="IDiceOption.BuildOptionString(StringBuilder)"/>
		public override void BuildOptionString(StringBuilder builder)
		{
			if (Amount == 0)
			{
				return;
			}

			char hl = Mode == HighLowMode.High ? SymbolHigh : SymbolLow;
			builder.Append($"{Symbol}{hl}{Amount}");
		}

		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
