using System.Linq;

using cmdwtf.NumberStones.Expression;

using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	internal static class DiceExpressionTextParsers
	{
		public const char DiceSeperator = 'd';

		// #creep: penetrating - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-PenetratingDice(B,F)!pCP
		// #creep: matching - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-DiceMatchingmt
		// #creep: reroll - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-RerollingDice(B,F)rCP
		// #creep: sort - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-SortingDice(B,F)sa/sd
		// #creep: group - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-GroupedRolls

		public static string DiceOptionString { get; } =
			$"{Options.Keep.Symbol}"
			+ $"{Options.Drop.Symbol}"
			+ $"{Options.Keep.SymbolHigh}"
			+ $"{Options.Keep.SymbolLow}"
			+ $"{Options.Critical.Symbol}"
			+ $"{Options.Critical.SymbolSuccess}"
			+ $"{Options.Critical.SymbolFailure}"
			+ $"{Options.Reroll.Symbol}"
			+ $"{Options.Twice.Symbol}"
			+ $"{Options.Exploding.Symbol}"
			+ $"{Options.Exploding.SymbolCompound}"
			+ $"{Options.Exploding.SymbolPenetrating}"
			+ $"{Options.ComparisonOptionBase.SymbolEqual}"
			+ $"{Options.ComparisonOptionBase.SymbolGreater}"
			+ $"{Options.ComparisonOptionBase.SymbolLess}"
			+ $"{Options.ComparisonOptionBase.SymbolNot}";

		public static char[] DiceOptionChars { get; } =
			(DiceOptionString + DiceOptionString.ToUpper())
			.AsQueryable().Distinct().ToArray();

		public static TextParser<char> DiceSeperatorCharacter { get; } =
			Character.EqualToIgnoreCase(DiceSeperator);

		public static TextParser<char> DiceOptionCharacter { get; } =
			Character.In(DiceOptionChars);

		public static TextParser<Unit> DiceInlineLabel { get; } =
			from open in Character.EqualTo(DiceExpressionTokenConstants.OpenLabel)
			from content in Character.ExceptIn(DiceExpressionTokenConstants.CloseLabel)
			from close in Character.EqualTo(DiceExpressionTokenConstants.CloseLabel)
			select Unit.Value;


		public static TextParser<IExpression> DiceTerm { get; } =
			from term in DiceTextParsers.DiceTerm
			select term as IExpression;

		public static TextParser<IExpression> ConstantTerm { get; } =
			from value in Span.MatchedBy(Numerics.Decimal)
				.Apply(Numerics.DecimalDecimal)
			select new ConstantTerm(value) as IExpression;
	}
}
