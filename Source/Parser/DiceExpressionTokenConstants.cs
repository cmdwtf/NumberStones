namespace cmdwtf.NumberStones.Parser
{
	internal static class DiceExpressionTokenConstants
	{
		public static class Categories
		{
			public const string Unknown = "unknown";
			public const string Term = "term";
			public const string Bracket = "bracket";
			public const string Operator = "operator";
			public const string Other = "other";
		}

		internal const char _openParentheses = '(';
		public const string OpenParentheses = "(";
		internal const char _closeParentheses = ')';
		public const string CloseParentheses = ")";
		internal const char _equals = '=';
		public new const string Equals = "=";
		internal const char _asterisk = '*';
		public const string Asterisk = "*";
		internal const char _slash = '/';
		public const string Slash = "/";
		internal const char _plus = '+';
		public const string Plus = "+";
		internal const char _minus = '-';
		public const string Minus = "-";
		internal const char _percent = '%';
		public const string Percent = "%";
		internal const char _octothorpe = '#';
		public const string Octothorpe = "#";

		// brackets
		public const char OpenSubExpression = _openParentheses;
		public const string OpenSubExpressionString = OpenParentheses;
		public const char CloseSubExpression = _closeParentheses;
		public const string CloseSubExpressionString = CloseParentheses;

		// operators
		public const char MultiplyOperator = _asterisk;
		public const string MultiplyOperatorString = Asterisk;
		public const char DivideOperator = _slash;
		public const string DivideOperatorString = Slash;
		public const char AddOperator = _plus;
		public const string AddOperatorString = Plus;
		public const char SubtractOperator = _minus;
		public const string SubtractOperatorString = Minus;
		public const char ModuloOperator = _percent;
		public const string ModuloOperatorString = Percent;

		// end line comments
		public const char OpenComment = _octothorpe;
		public const string OpenCommentString = Octothorpe;
	}
}
