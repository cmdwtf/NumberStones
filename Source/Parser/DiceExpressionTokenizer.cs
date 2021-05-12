
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
				.Match(Character.EqualTo(MultiplyOperator), DiceExpressionToken.Multiply)
				.Match(Character.EqualTo(DivideOperator), DiceExpressionToken.Divide)
				.Match(Character.EqualTo(AddOperator), DiceExpressionToken.Add)
				.Match(Character.EqualTo(SubtractOperator), DiceExpressionToken.Subtract)
				.Match(Character.EqualTo(ModuloOperator), DiceExpressionToken.Modulo)
				.Match(Character.EqualTo(OpenComment), DiceExpressionToken.Comment)
				.Match(Numerics.Decimal, DiceExpressionToken.Constant, requireDelimiters: true)
				.Match(Dice, DiceExpressionToken.Dice, requireDelimiters: true)
				.Match(Identifier.CStyle, DiceExpressionToken.None, requireDelimiters: true)
				.Build();
	}
}
