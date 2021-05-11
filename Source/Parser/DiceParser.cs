using System.Diagnostics.CodeAnalysis;

using Superpower;
using Superpower.Model;

namespace cmdwtf.NumberStones.Parser
{
	// #doc
	internal class DiceParser : IDiceParser
	{
		public static bool TryParse(string expression, out DiceExpression result, [MaybeNullWhen(true)] out string error, out Position errorPosition)
		{
			Result<TokenList<DiceExpressionToken>> tokens = DiceExpressionTokenizer.Instance.TryTokenize(expression);

			if (!tokens.HasValue)
			{
				result = DiceExpression.Empty();
				error = tokens.ToString();
				errorPosition = tokens.ErrorPosition;
				return false;
			}

			TokenListParserResult<DiceExpressionToken, DiceExpression> parsed = DiceExpressionParsers.Expression.TryParse(tokens.Value);

			if (!parsed.HasValue)
			{
				result = DiceExpression.Empty();
				error = parsed.ToString();
				errorPosition = parsed.ErrorPosition;
				return false;
			}

			result = parsed.Value;
			error = null;
			errorPosition = Position.Empty;
			return true;
		}

		public DiceExpression Parse(string expression)
		{
			_ = TryParse(expression, out DiceExpression? result, out string? error, out Position errorPosition);
			return result ?? throw new Exceptions.DiceExpressionParseException($"{errorPosition}: {error}");
		}

		public bool TryParse(string expression, out DiceExpression result)
		{
			if (TryParse(expression, out result, out _, out _))
			{
				return true;
			}

			result = DiceExpression.Empty();

			return false;
		}
	}
}
