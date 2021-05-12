using System.Linq;

using cmdwtf.NumberStones.Expression;

using Superpower;
using Superpower.Parsers;

using static cmdwtf.NumberStones.Expression.BinaryOperation;

namespace cmdwtf.NumberStones.Parser
{
	// doc
	internal static class DiceExpressionParsers
	{
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Operator(DiceExpressionToken op, BinaryOperationCreationDelegate opType)
			=> Token.EqualTo(op).Value(opType);

		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Add { get; } =
			Operator(DiceExpressionToken.Add, BinaryOperation.Add);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Subtract { get; } =
			Operator(DiceExpressionToken.Subtract, BinaryOperation.Subtract);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Multiply { get; } =
			Operator(DiceExpressionToken.Multiply, BinaryOperation.Multiply);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Divide { get; } =
			Operator(DiceExpressionToken.Divide, BinaryOperation.Divide);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Modulo { get; } =
			Operator(DiceExpressionToken.Modulo, BinaryOperation.Modulo);

		private static TokenListParser<DiceExpressionToken, IExpression> SubExpression { get; } =
			from open in Token.EqualTo(DiceExpressionToken.ParenthesisLeft)
			from expr in Parse.Ref(() => AddSubtract ?? throw new ParseException("Expression parser null."))
			from close in Token.EqualTo(DiceExpressionToken.ParenthesisRight)
			select expr as IExpression;

		private static TokenListParser<DiceExpressionToken, IExpression> Term { get; } =
			from result in Parse.OneOf(
				SubExpression,
				Token.EqualTo(DiceExpressionToken.Dice)
					.Apply(DiceExpressionTextParsers.DiceTerm)
					.Try(),
				Token.EqualTo(DiceExpressionToken.Constant)
					.Apply(DiceExpressionTextParsers.ConstantTerm)
				)
			select result;

		private static IExpression MakeBinary(BinaryOperationCreationDelegate op, IExpression left, IExpression right)
			=> op(left, right);

		private static TokenListParser<DiceExpressionToken, IExpression> MultiplyDivide { get; } =
			Parse.Chain(Multiply.Or(Divide).Or(Modulo), Term, MakeBinary);

		private static TokenListParser<DiceExpressionToken, IExpression> AddSubtract { get; } =
			Parse.Chain(Add.Or(Subtract), MultiplyDivide, MakeBinary);

		internal static TokenListParser<DiceExpressionToken, DiceExpression> Expression { get; } =
			from expr in AddSubtract.AtEnd()
			select new DiceExpression(expr);
	}
}
