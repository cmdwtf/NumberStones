using Superpower.Display;

using static cmdwtf.NumberStones.Parser.DiceExpressionTokenConstants;

namespace cmdwtf.NumberStones.Parser
{
	internal enum DiceExpressionToken
	{
		[Token(Category = Categories.Unknown)]
		None = 0,
		[Token(Category = Categories.Term)]
		Constant,
		[Token(Category = Categories.Term)]
		Dice,

		[Token(Category = Categories.Bracket, Example = OpenSubExpressionString)]
		ParenthesisLeft,
		[Token(Category = Categories.Bracket, Example = CloseSubExpressionString)]
		ParenthesisRight,

		// Exponent,	// #exponent

		[Token(Category = Categories.Operator, Example = MultiplyOperatorString)]
		Multiply,
		[Token(Category = Categories.Operator, Example = DivideOperatorString)]
		Divide,
		[Token(Category = Categories.Operator, Example = ModuloOperatorString)]
		Modulo,

		[Token(Category = Categories.Operator, Example = AddOperatorString)]
		Add,
		[Token(Category = Categories.Operator, Example = SubtractOperatorString)]
		Subtract,

		[Token(Category = Categories.Other, Example = OpenCommentString)]
		Comment,
	}
}
