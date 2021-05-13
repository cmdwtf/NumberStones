using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.DiceTypes;
using cmdwtf.NumberStones.Options;
using cmdwtf.NumberStones.Parser;

using Superpower.Model;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// A record representing the options and settings for a Die or Dice.
	/// </summary>
	public record DiceSettings(decimal SidesReal, decimal MultiplicityReal = 1m, DiceType Kind = DiceType.Numerical)
	{
		/// <summary>
		/// A string representing no additional dice options.
		/// </summary>
		public const string NoOptions = "";

		/// <summary>
		/// An integer number for the number of sides the dice has.
		/// </summary>
		public int Sides => (int)Math.Truncate(ModeSides);

		/// <summary>
		/// An integer value of how many dice should be rolled.
		/// </summary>
		public int Multiplicity => (int)Math.Truncate(MultiplicityReal);

		/// <summary>
		/// An integer value of how many dice results should be dropped.
		/// </summary>
		public int Drop => (int)_options.OfType<Drop>().Sum(d => d.Value);

		/// <summary>
		/// The number of sides for the dice, specifically adjusted for unique types.
		/// </summary>
		private decimal ModeSides => Kind switch
		{
			DiceType.Coin => 2,
			DiceType.Fudge => 6,
			DiceType.Planar => 6,
			_ => SidesReal,
		};

		/// <summary>
		/// A string representing the number (or type) of sides for the dice.
		/// It may be a non-digit if the dice is a special type.
		/// </summary>
		public string SidesString => Kind switch
		{
			DiceType.Coin => DiceTermTextParsers.CoinTypeSide,
			DiceType.Fudge => DiceTermTextParsers.FudgeTypeSide,
			DiceType.Planar => DiceTermTextParsers.PlanechaseTypeSide,
			_ => SidesReal.ToString(),
		};

		private readonly List<IDiceOption> _options = new();
		/// <summary>
		/// The options for the dice.
		/// </summary>
		public IReadOnlyList<IDiceOption> Options => _options.AsReadOnly();

		internal IDiceOption[] ParsedOptions
		{
			init
			{
				_options = value.ToList();
			}
		}

		/// <summary>
		/// A string representing the options on the dice, in dice syntax.
		/// </summary>
		public string OptionString
		{
			get => BuildOptionString();
			init
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					// nothing to do!
					return;
				}

				Result<IDiceOption[]> result = DiceTermTextParsers.DiceOptions.Invoke(new TextSpan(value));

				if (result.HasValue)
				{
					_options.AddRange(result.Value);
				}
			}
		}

		/// <summary>
		/// Creates a new DiceSettings with the specified sides and mulitiplicity
		/// </summary>
		/// <param name="sides">The number of sides the dice has</param>
		/// <param name="multiplicity">How many of the dice to roll</param>
		public DiceSettings(int sides, int multiplicity)
			: this(Convert.ToDecimal(sides), Convert.ToDecimal(multiplicity))
		{

		}

		/// <summary>
		/// Builds a dice syntax options string based on all the options present.
		/// </summary>
		/// <returns>A string representing all of the options present that can be put in syntax.</returns>
		private string BuildOptionString()
		{
			StringBuilder builder = new();

			foreach (IDiceOption option in _options)
			{
				option.BuildOptionString(builder);
			}

			return builder.ToString();
		}

		/// <summary>
		/// Returns a string representing this DiceSettings
		/// </summary>
		/// <returns>A string representing this DiceSettings</returns>
		public override string ToString() => $"{MultiplicityReal}d{SidesString}{OptionString}";
	}
}
