using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cmdwtf.NumberStones.DiceTypes;
using cmdwtf.NumberStones.Options;
using cmdwtf.NumberStones.Parser;

namespace cmdwtf.NumberStones
{
	// #doc
	public record DiceSettings(decimal SidesReal, decimal MultiplicityReal = 1m, DiceType Kind = DiceType.Polyhedron)
	{
		public const string NoOptions = "";

		public int Sides => (int)Math.Truncate(ModeSides);
		public int Multiplicity => (int)Math.Truncate(MultiplicityReal);

		private decimal ModeSides => Kind switch
		{
			DiceType.Coin => 2,
			DiceType.Fate => 6,
			DiceType.Planechase => 6,
			_ => SidesReal,
		};

		public string SidesString => Kind switch
		{
			DiceType.Coin => DiceTextParsers.CoinSide,
			DiceType.Fate => DiceTextParsers.FateSide,
			DiceType.Planechase => DiceTextParsers.PlanechaseSide,
			_ => SidesReal.ToString(),
		};

		private readonly List<IDiceOption> _options = new();
		public IReadOnlyList<IDiceOption> Options => _options.AsReadOnly();

		internal IDiceOption[] ParsedOptions
		{
			init
			{
				_options = value.ToList();
			}
		}

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

				char[] optionChars = value.ToCharArray();

				for (int scan = 0; scan < optionChars.Length; ++scan)
				{
					switch (optionChars[scan])
					{
						case '!':
							ParseExploding(optionChars, ref scan);
							break;
						case 'd':
							// drop
							break;
						case 'k':
							// keep
							break;
						case 'c':
							// crit
							break;
						case 'r':
							// reroll
							break;
						case 't':
							// twice
							break;
						case '=':
						case '>':
						case '<':
						case '~':
							// comparison
							break;
						case '[':
							// label
							break;
						default:
							throw new Exceptions.DiceExpressionOptionParseException($"Unhandled option character: {optionChars[scan]}", value);
					}
				}
			}
		}

		private static int ParseNextInt(char[] chars, ref int scan)
		{
			string subString = new(chars, scan, 32);
			int result = int.Parse(subString);

			while (char.IsDigit(chars[scan]))
			{
				++scan;
				if (scan >= chars.Length)
				{
					break;
				}
			}

			return result;
		}

		private static (ExplodingDiceMode mode, int target) ParseExploding(char[] optionChars, ref int scan)
		{
			char next = (scan + 1 < optionChars.Length) ? char.ToLowerInvariant(optionChars[scan + 1]) : '\0';
			char nextNext = (scan + 2 < optionChars.Length) ? char.ToLowerInvariant(optionChars[scan + 2]) : '\0';

			ExplodingDiceMode mode = ExplodingDiceMode.Classic;
			int target = 0;

			switch (next)
			{
				case '!':
					mode = ExplodingDiceMode.Compound;
					scan++;
					break;
				case 'p':
					mode = ExplodingDiceMode.Penetrating;
					scan++;
					break;
			}

			switch (nextNext)
			{
				case '>':
					++scan;
					target = ParseNextInt(optionChars, ref scan);
					break;
			}

			return (mode, target);
		}

		public DiceSettings(int sides, int multiplicity)
			: this(Convert.ToDecimal(sides), Convert.ToDecimal(multiplicity))
		{

		}

		private string BuildOptionString()
		{
			StringBuilder builder = new();

			foreach (IDiceOption option in _options)
			{
				option.BuildOptionString(builder);
			}

			return builder.ToString();
		}

		public override string ToString()
			=> $"{MultiplicityReal}d{SidesString}{OptionString}";
	}
}
