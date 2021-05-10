using System.Diagnostics.CodeAnalysis;

using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	// #doc
	internal class DiceParser : IDiceParser
	{
		private static TokenListParser<DiceExpressionToken, DiceExpression> Parser { get; } =
			from whatever in Token.EqualTo(DiceExpressionToken.Dice)
			select new DiceExpression();

		public static bool TryParse(string expression, out DiceExpression? value, [MaybeNullWhen(true)] out string error, out Position errorPosition)
		{
			Result<TokenList<DiceExpressionToken>> tokens = DiceExpressionTokenizer.Instance.TryTokenize(expression);

			if (!tokens.HasValue)
			{
				value = null;
				error = tokens.ToString();
				errorPosition = tokens.ErrorPosition;
				return false;
			}

			TokenListParserResult<DiceExpressionToken, DiceExpression> parsed = Parser.TryParse(tokens.Value);
			if (!parsed.HasValue)
			{
				value = null;
				error = parsed.ToString();
				errorPosition = parsed.ErrorPosition;
				return false;
			}

			value = parsed.Value;
			error = null;
			errorPosition = Position.Empty;
			return true;
		}
		public DiceExpression Parse(string expression)
		{
			_ = TryParse(expression, out DiceExpression? result, out _, out _);
			return result ?? new DiceExpression();
		}
	}
}
