
using System.Linq;

using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using static cmdwtf.NumberStones.Parser.DiceExpressionTextParsers;
using static cmdwtf.NumberStones.Parser.DiceExpressionTokenConstants;

namespace cmdwtf.NumberStones.Parser
{
	/// <summary>
	/// A class containing the logic to break a dice expression string into tokens to then be parsed.
	/// </summary>
	internal static class DiceExpressionTokenizer
	{
		public static TextParser<Unit> DiceInlineLabel { get; } =
			from open in Character.EqualTo(Options.Label.SymbolOpen)
			from content in Character.ExceptIn(Options.Label.SymbolClose).Many()
			from close in Character.EqualTo(Options.Label.SymbolClose)
			select Unit.Value;
		private static TextParser<Unit> Comment { get; } =
			from open in Character.EqualTo(OpenComment)
			from rest in Character.AnyChar.Many()
			select Unit.Value;

		private static TextParser<Unit> Constant { get; } =
			from val in Numerics.Decimal
			from _ in Span.WhiteSpace.IgnoreMany().Try()
			from labels in DiceInlineLabel.Many().Try()
			select Unit.Value;

		private static TextParser<Unit> Dice { get; } =
			from first in Character.Digit.Or(DiceSeperatorCharacter).AtLeastOnce()
			from rest in Parse.OneOf(
					Character.Digit.AtLeastOnce().Value(Unit.Value),
					DiceInlineLabel,
					DiceOptionCharacter.AtLeastOnce().Value(Unit.Value)
				).IgnoreMany()
			select Unit.Value;

		/// <summary>
		/// The tokenizer instance
		/// </summary>
		public static Tokenizer<DiceExpressionToken> Instance { get; } =
			new TokenizerBuilder<DiceExpressionToken>()
				.Ignore(Span.WhiteSpace)
				.Match(Character.EqualTo(OpenSubExpression), DiceExpressionToken.ParenthesisLeft)
				.Match(Character.EqualTo(CloseSubExpression), DiceExpressionToken.ParenthesisRight)
				.Match(Character.EqualTo(ExponentOperator), DiceExpressionToken.Exponent)
				.Match(Character.EqualTo(MultiplyOperator), DiceExpressionToken.Multiply)
				.Match(Character.EqualTo(DivideOperator), DiceExpressionToken.Divide)
				.Match(Character.EqualTo(AddOperator), DiceExpressionToken.Add)
				.Match(Character.EqualTo(SubtractOperator), DiceExpressionToken.Subtract)
				.Match(Character.EqualTo(ModuloOperator), DiceExpressionToken.Modulo)
				.Match(Comment, DiceExpressionToken.Comment)
				.Match(Constant, DiceExpressionToken.Constant, requireDelimiters: true)
				.Match(Dice, DiceExpressionToken.Dice, requireDelimiters: true)
				.Match(Identifier.CStyle, DiceExpressionToken.None, requireDelimiters: true)
				.Build();
	}
}
