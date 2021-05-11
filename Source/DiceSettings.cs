using System;
using System.Text;

using cmdwtf.NumberStones.Options;

namespace cmdwtf.NumberStones
{
	// #doc
	public record DiceSettings(decimal SidesReal, decimal MultiplicityReal = 1m)
	{
		public const string NoOptions = "";

		public int Sides => (int)Math.Truncate(SidesReal);
		public int Multiplicity => (int)Math.Truncate(MultiplicityReal);

		public string SidesString =>
			Fate ? "F" : SidesReal.ToString();

		public string Options
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
							(ExplodingMode, ExplodingTarget) = ParseExploding(optionChars, ref scan);
							break;
						case 'd':
							// drop
							break;
						case 'k':
							// keep
							break;
						case 'r':
							// reroll
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
			char c = char.ToLowerInvariant(optionChars[scan]);
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

		// options controlled values
		public int Drop { get; private init; } = 0;
		public int Keep { get; private init; } = 0;
		public int DropHigh { get; private init; } = 0;
		public int KeepLow { get; private init; } = 0;
		public string Label { get; private init; } = string.Empty;
		public ExplodingDiceMode ExplodingMode { get; private init; } = ExplodingDiceMode.None;
		public bool Exploding => ExplodingMode != ExplodingDiceMode.None;
		public bool CompoundExploding => ExplodingMode == ExplodingDiceMode.Compound;
		public bool PenetratingExploding => ExplodingMode == ExplodingDiceMode.Penetrating;
		public int ExplodingTarget { get; private init; } = 0;
		public bool Fudge { get; private init; } = false;
		public bool Fate => Fudge;
		public int[] Rerolls { get; private init; } = Array.Empty<int>();
		public int Target { get; private init; } = 0;
		public ComparisonDiceMode TargetMode { get; private init; } = ComparisonDiceMode.None;
		public bool TargetGreaterThan => TargetMode is ComparisonDiceMode.GreaterThan or ComparisonDiceMode.GreaterThanEquals;
		public bool TargetLessThan => TargetMode is ComparisonDiceMode.LessThan or ComparisonDiceMode.LessThanEquals;
		public bool TargetEqualTo => TargetMode is ComparisonDiceMode.Equals or ComparisonDiceMode.GreaterThanEquals or ComparisonDiceMode.LessThanEquals;
		public bool TargetNotEqualTo => TargetMode == ComparisonDiceMode.Not;
		public int[] CriticalSuccessPoints { get; private init; } = Array.Empty<int>();
		public int CriticalSuccessMin { get; private init; } = 0;
		public int[] CriticalFailurePoints { get; private init; } = Array.Empty<int>();
		public int CriticalFailureMax { get; private init; } = 0;
		public int TwicePoint { get; private init; } = 0;

		public static DiceSettings FudgeDice(decimal multiplicity = 1m, string options = NoOptions)
		{
			return new(0, multiplicity)
			{
				Fudge = true,
				Options = options
			};
		}

		public static DiceSettings FateDice(decimal multiplicity = 1m, string options = NoOptions)
			=> FudgeDice(multiplicity, options);

		public DiceSettings(int sides, int multiplicity)
			: this(Convert.ToDecimal(sides), Convert.ToDecimal(multiplicity))
		{

		}

		public DiceSettings(int sides, int multiplicity, int drop)
			: this(Convert.ToDecimal(sides), Convert.ToDecimal(multiplicity))
		{
			Drop = drop;
		}

		private string BuildOptionString()
		{
			StringBuilder result = new();

			if (CompoundExploding)
			{
				result.Append("!!");
			}
			else if (PenetratingExploding)
			{
				result.Append("!p");
			}
			else if (Exploding)
			{
				result.Append('!');
			}

			if (Exploding && ExplodingTarget > 0)
			{
				result.Append($">{ExplodingTarget}");
			}

			if (Drop > 0)
			{
				result.Append($"d{Drop}");
			}

			if (Keep > 0)
			{
				result.Append($"k{Keep}");
			}

			if (DropHigh > 0)
			{
				result.Append($"d{Drop}");
			}

			if (KeepLow > 0)
			{
				result.Append($"k{Keep}");
			}

			foreach (int reroll in Rerolls)
			{
				result.Append($"r{reroll}");
			}

			if (Target > 0 && TargetMode != ComparisonDiceMode.None)
			{
				if (TargetGreaterThan)
				{
					result.Append('>');
				}
				else if (TargetLessThan)
				{
					result.Append('<');
				}

				if (TargetNotEqualTo)
				{
					result.Append('~');
				}

				if (TargetEqualTo)
				{
					result.Append('=');
				}

				result.Append(Target);
			}

			foreach (int csPoint in CriticalSuccessPoints)
			{
				result.Append($"cs{csPoint}");
			}

			if (CriticalSuccessMin > 0)
			{
				result.Append($"cs>{CriticalSuccessMin}");
			}

			foreach (int cfPoint in CriticalFailurePoints)
			{
				result.Append($"cf{cfPoint}");
			}

			if (CriticalFailureMax > 0)
			{
				result.Append($"cf<{CriticalFailureMax}");
			}

			if (TwicePoint > 0)
			{
				result.Append($"t{TwicePoint}");
			}

			if (string.IsNullOrWhiteSpace(Label) == false)
			{
				result.Append($"[{Label}]");
			}

			return result.ToString();
		}

		public override string ToString()
		{
			return $"{MultiplicityReal}d{SidesString}{Options}";
		}
	}
}
