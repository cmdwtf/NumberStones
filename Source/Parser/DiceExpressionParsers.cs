﻿using System.Linq;

using cmdwtf.NumberStones.Expression;

using Superpower;
using Superpower.Parsers;

using static cmdwtf.NumberStones.Expression.BinaryOperation;
using static cmdwtf.NumberStones.Expression.UnaryOperation;

namespace cmdwtf.NumberStones.Parser
{
	// doc
	internal static class DiceExpressionParsers
	{
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Operator(DiceExpressionToken op, BinaryOperationCreationDelegate opType)
			=> Token.EqualTo(op).Value(opType);
		private static TokenListParser<DiceExpressionToken, UnaryOperationCreationDelegate> Operator(DiceExpressionToken op, UnaryOperationCreationDelegate opType)
			=> Token.EqualTo(op).Value(opType);

		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Add { get; } =
			Operator(DiceExpressionToken.Add, BinaryOperation.Add);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Subtract { get; } =
			Operator(DiceExpressionToken.Subtract, BinaryOperation.Subtract);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Multiply { get; } =
			Operator(DiceExpressionToken.Multiply, BinaryOperation.Multiply);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> ImplicitMultiplyLeftHandSide { get; } =
			Operator(DiceExpressionToken.ParenthesisRight, BinaryOperation.Multiply);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> ImplicitMultiplyRightHandSide { get; } =
			Operator(DiceExpressionToken.ParenthesisLeft, BinaryOperation.Multiply);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Divide { get; } =
			Operator(DiceExpressionToken.Divide, BinaryOperation.Divide);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> Modulo { get; } =
			Operator(DiceExpressionToken.Modulo, BinaryOperation.Modulo);
		private static TokenListParser<DiceExpressionToken, BinaryOperationCreationDelegate> ToPower { get; } =
			Operator(DiceExpressionToken.Exponent, BinaryOperation.Power);

		private static TokenListParser<DiceExpressionToken, IExpression> Negate { get; } =
			from operation in Operator(DiceExpressionToken.Subtract, UnaryOperation.Negate)
			from term in Term ?? throw new ParseException($"{nameof(Term)} parser null.")
			select operation(term) as IExpression;

		private static TokenListParser<DiceExpressionToken, IExpression> OpenedSubExpression { get; } =
			from subExpression in Parse.Ref(() => AddSubtract ?? throw new ParseException($"{nameof(AddSubtract)} parser null."))
			from implicitMultiply in ImplicitMultiplyLeftHandSide
			from expr in
					(Term ?? throw new ParseException($"{nameof(Term)} parser null.")).Try()
					.Select(nextTerm => MakeOperation(implicitMultiply, subExpression, nextTerm))
					.OptionalOrDefault(subExpression)
					.Named("implicit multiply")
			select expr;

		private static TokenListParser<DiceExpressionToken, IExpression> SubExpression { get; } =
			from open in Token.EqualTo(DiceExpressionToken.ParenthesisLeft)
			from expr in OpenedSubExpression
			select expr;

		private static TokenListParser<DiceExpressionToken, IExpression> Dice { get; } =
			from result in Token.EqualTo(DiceExpressionToken.Dice)
					.Apply(DiceExpressionTextParsers.DiceTerm)
			select result;

		private static TokenListParser<DiceExpressionToken, IExpression> Constant { get; } =
			from result in Token.EqualTo(DiceExpressionToken.Constant)
					.Apply(DiceExpressionTextParsers.ConstantTerm)
			select result;

		private static TokenListParser<DiceExpressionToken, IExpression> Term { get; } =
			from result in Parse.OneOf(
				SubExpression,
				Dice.Try(),
				Negate.Or(Constant)
				)
				.Then(lhs =>
					ImplicitMultiplyRightHandSide.Then(oper =>
						OpenedSubExpression.Select(rhs => MakeOperation(oper, lhs, rhs))
					).OptionalOrDefault(lhs)
					.Named("implicit multiply")
				)
			select result;

		private static IExpression MakeOperation(BinaryOperationCreationDelegate op, IExpression left, IExpression right)
			=> op(left, right);

		private static TokenListParser<DiceExpressionToken, IExpression> Exponent { get; } =
			Parse.Chain(ToPower, Term, MakeOperation);

		private static TokenListParser<DiceExpressionToken, IExpression> MultiplyDivide { get; } =
			Parse.Chain(Multiply.Or(Divide).Or(Modulo), Exponent, MakeOperation);

		private static TokenListParser<DiceExpressionToken, IExpression> AddSubtract { get; } =
			Parse.Chain(Add.Or(Subtract), MultiplyDivide, MakeOperation);

		internal static TokenListParser<DiceExpressionToken, string> ExpressionComment { get; } =
			Token.EqualTo(DiceExpressionToken.Comment)
				.AtEnd().Try()
				.Apply(DiceExpressionTextParsers.Comment);

		/// <summary>
		/// The entry point to the DiceExpressionParser.
		/// </summary>
		internal static TokenListParser<DiceExpressionToken, DiceExpression> Expression { get; } =
			from expression in AddSubtract.Then(expr =>
				from comment in ExpressionComment.AtEnd()
				select new DiceExpression(expr) { Comment = comment }
			)
			.Or(
				AddSubtract.AtEnd()
				.Select(e => new DiceExpression(e))
			)
			select expression;
	}
}
