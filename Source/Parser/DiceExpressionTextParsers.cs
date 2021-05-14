using System.Linq;

using cmdwtf.NumberStones.Expression;

using Superpower;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	internal static class DiceExpressionTextParsers
	{
		public const char DiceSeperator = 'd';

		// #creep: sort - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-SortingDice(B,F)sa/sd
		// #creep: group - https://help.roll20.net/hc/en-us/articles/360037773133-Dice-Reference#DiceReference-GroupedRolls

		public static string DiceOptionString { get; } =
			$"{Options.ComparisonOptionBase.SymbolEqual}"
			+ $"{Options.ComparisonOptionBase.SymbolGreater}"
			+ $"{Options.ComparisonOptionBase.SymbolLess}"
			+ $"{Options.ComparisonOptionBase.SymbolNot}"
			+ $"{Options.Critical.Symbol}"
			+ $"{Options.Critical.SymbolSuccess}"
			+ $"{Options.Critical.SymbolFailure}"
			+ $"{Options.Drop.Symbol}"
			+ $"{Options.Exploding.Symbol}"
			+ $"{Options.Exploding.SymbolCompound}"
			+ $"{Options.Exploding.SymbolPenetrating}"
			+ $"{Options.Failure.Symbol}"
			+ $"{Options.Keep.Symbol}"
			+ $"{Options.Keep.SymbolHigh}"
			+ $"{Options.Keep.SymbolLow}"
			+ $"{Options.Reroll.Symbol}"
			+ $"{Options.Twice.Symbol}";

		public static char[] DiceOptionChars { get; } =
			(DiceOptionString + DiceOptionString.ToUpper())
			.AsQueryable().Distinct().ToArray();

		public static TextParser<char> DiceSeperatorCharacter { get; } =
			Character.EqualToIgnoreCase(DiceSeperator);

		public static TextParser<char> DiceOptionCharacter { get; } =
			Character.In(DiceOptionChars);

		public static TextParser<string> Comment { get; } =
			from open in Character.EqualTo(DiceExpressionTokenConstants.OpenComment)
			from comment in Character.AnyChar.Many()
			select new string(comment).Trim();


		public static TextParser<IExpression> DiceTerm { get; } =
			from term in DiceTermTextParsers.DiceTerm
			select term as IExpression;

		public static TextParser<IExpression> ConstantTerm { get; } =
			from value in Span.MatchedBy(Numerics.Decimal)
				.Apply(Numerics.DecimalDecimal)
			from _ in Span.WhiteSpace.IgnoreMany().Try()
			from labels in DiceTermTextParsers.LabelOption.Many().Try()
			select new ConstantTerm(value)
			{
				Labels = labels?.Cast<Options.Label>().ToArray()
					?? System.Array.Empty<Options.Label>()
			} as IExpression;
	}
}
