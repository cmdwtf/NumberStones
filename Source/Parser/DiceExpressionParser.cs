using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Superpower;
using Superpower.Model;

namespace cmdwtf.NumberStones.Parser
{
	/// <summary>
	/// A dice expression parser based on the `superpower` library.
	/// </summary>
	internal class DiceExpressionParser : IDiceExpressionParser
	{
		private static bool TryParse(string expression, out DiceExpression result, [MaybeNullWhen(true)] out string error, out Position errorPosition)
		{
			Result<TokenList<DiceExpressionToken>> tokens = DiceExpressionTokenizer.Instance.TryTokenize(expression);

			if (!tokens.HasValue)
			{
				result = DiceExpression.Empty;
				error = tokens.ToString();
				errorPosition = tokens.ErrorPosition;
				Debug.WriteLine($"{nameof(DiceExpressionParser)} Tokenize failed: {errorPosition} {error}");
				return false;
			}

			TokenListParserResult<DiceExpressionToken, DiceExpression> parsed = DiceExpressionParsers.Expression.TryParse(tokens.Value);

			if (!parsed.HasValue)
			{
				result = DiceExpression.Empty;
				error = parsed.ToString();
				errorPosition = parsed.ErrorPosition;
				Debug.WriteLine($"{nameof(DiceExpressionParser)} Parse failed: {errorPosition} {error}");
				return false;
			}

			result = parsed.Value;
			error = null;
			errorPosition = Position.Empty;
			return true;
		}

		/// <summary>
		/// Parses an expression string, and returns a <see cref="DiceExpression"/> if it's valid
		/// </summary>
		/// <param name="expression">The expression string to parse</param>
		/// <returns>The expression in parsed form</returns>
		/// <exception cref="Exceptions.DiceExpressionParseException">When parsing fails</exception>
		public DiceExpression Parse(string expression)
		{
			if (TryParse(expression, out DiceExpression? result, out string? error, out Position errorPosition))
			{
				return result;
			}

			throw new Exceptions.DiceExpressionParseException($"{errorPosition}: {error}");
		}

		/// <summary>
		/// Parses an expression string, and returns a <see cref="DiceExpression"/> if it's valid
		/// </summary>
		/// <param name="expression">The expression string to parse</param>
		/// <param name="result">The resulting expression object</param>
		/// <returns>True, if the expression was successfully parsed</returns>
		public bool TryParse(string expression, out DiceExpression result)
		{
			if (TryParse(expression, out result, out _, out _))
			{
				return true;
			}

			result = DiceExpression.Empty;

			return false;
		}
	}
}
