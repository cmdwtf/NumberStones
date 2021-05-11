
using System.Linq;

using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using static cmdwtf.NumberStones.Parser.DiceExpressionTextParsers;
using static cmdwtf.NumberStones.Parser.DiceExpressionTokenConstants;

namespace cmdwtf.NumberStones.Parser
{
	// #doc
	internal class DiceExpressionTokenizer
	{
		private static TextParser<Unit> DiceToken { get; } =
			from first in Character.Digit.Or(DiceSeperatorCharacter).IgnoreMany()
			from rest in Character.Digit.Or(DiceOptionCharacter).IgnoreMany()
			select Unit.Value;

		private static TextParser<Unit> DiceNumberToken { get; } =
			from first in Character.Digit
			from rest in Character.Digit.Or(Character.In('.')).IgnoreMany()
			select Unit.Value;

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
				.Match(DiceToken, DiceExpressionToken.Dice, requireDelimiters: true)
				.Match(DiceNumberToken, DiceExpressionToken.Constant, requireDelimiters: true)
				.Match(Identifier.CStyle, DiceExpressionToken.None, requireDelimiters: true)
				.Build();
	}
}
