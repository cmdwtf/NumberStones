using System.Linq;

using Superpower;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	internal class DiceExpressionTextParsers
	{
		public const string DiceOptionKeep = "kK";
		public const string DiceOptionDrop = "dD";
		public const string DiceOptionHigh = "hH";
		public const string DiceOptionLow = "lL";
		public const string DiceOptionCritical = "cC";
		public const string DiceOptionSuccess = "sS";
		public const string DiceOptionFail = "fF";
		public const string DiceOptionFudge = "fF";
		public const string DiceOptionExploding = "!";
		public const string DiceOptionGreaterThan = ">";
		public const string DiceOptionLessThan = "<";
		public const string DiceOptionEqualTo = "=";
		public const string DiceOptionLabelOpen = "[";
		public const string DiceOptionLabelClose = "]";

		// #creep: penetrating - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-PenetratingDice(B,F)!pCP
		// #creep: matching - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-DiceMatchingmt
		// #creep: reroll - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-RerollingDice(B,F)rCP
		// #creep: sort - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-SortingDice(B,F)sa/sd
		// #creep: group - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-GroupedRolls

		public const string DiceOptionString =
			DiceOptionKeep
			+ DiceOptionDrop
			+ DiceOptionHigh
			+ DiceOptionLow
			+ DiceOptionCritical
			+ DiceOptionSuccess
			+ DiceOptionFail
			+ DiceOptionFudge
			+ DiceOptionExploding
			+ DiceOptionGreaterThan
			+ DiceOptionLessThan
			+ DiceOptionEqualTo
			+ DiceOptionLabelOpen
			+ DiceOptionLabelClose;

		public static char[] DiceOptionChars { get; } =
			DiceOptionString.AsQueryable().Distinct().ToArray();

		public static TextParser<char> DiceSeperator { get; } =
			Character.In('d', 'D');

		public static TextParser<char> DiceOptions { get; } =
			Character.In(DiceOptionChars);
	}
}
