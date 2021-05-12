using System.Linq;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Operations;

using Superpower;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	// doc
	internal static class DiceExpressionParsers
	{
		private static TokenListParser<DiceExpressionToken, BinaryOperator> Operator(DiceExpressionToken op, BinaryOperator opType)
			=> Token.EqualTo(op).Value(opType);

		private static TokenListParser<DiceExpressionToken, BinaryOperator> Add { get; } =
			Operator(DiceExpressionToken.Add, BinaryOperations.Add);
		private static TokenListParser<DiceExpressionToken, BinaryOperator> Subtract { get; } =
			Operator(DiceExpressionToken.Subtract, BinaryOperations.Subtract);
		private static TokenListParser<DiceExpressionToken, BinaryOperator> Multiply { get; } =
			Operator(DiceExpressionToken.Multiply, BinaryOperations.Multiply);
		private static TokenListParser<DiceExpressionToken, BinaryOperator> Divide { get; } =
			Operator(DiceExpressionToken.Divide, BinaryOperations.Divide);
		private static TokenListParser<DiceExpressionToken, BinaryOperator> Modulo { get; } =
			Operator(DiceExpressionToken.Modulo, BinaryOperations.Modulo);

		private static TokenListParser<DiceExpressionToken, IExpression> SubExpression { get; } =
			from open in Token.EqualTo(DiceExpressionToken.ParenthesisLeft)
			from expr in Parse.Ref(() => Expression ?? throw new ParseException("Expression parser null."))
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

		private static IExpression MakeBinary(BinaryOperator op, IExpression left, IExpression right)
			=> new BinaryOperation(left, right, op);

		private static TokenListParser<DiceExpressionToken, IExpression> MultiplyDivide { get; } =
			Parse.Chain(Multiply.Or(Divide).Or(Modulo), Term, MakeBinary);

		private static TokenListParser<DiceExpressionToken, IExpression> AddSubtract { get; } =
			Parse.Chain(Add.Or(Subtract), MultiplyDivide, MakeBinary);

		internal static TokenListParser<DiceExpressionToken, DiceExpression> Expression { get; } =
			from expr in AddSubtract.AtEnd()
			select new DiceExpression(expr);
	}
}
