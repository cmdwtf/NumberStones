﻿using System;
using System.Globalization;
using System.Linq;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// The ConstantTerm class represents a constant value in a DiceExpression
	/// </summary>
	/// <remarks>
	/// In the expression "2d6+5" the integer "5" is a ConstantTerm
	/// </remarks>
	public record ConstantTerm(decimal Constant) : Term
	{
		/// <summary>
		/// Labels associated with this constant.
		/// </summary>
		public Options.Label[] Labels { get; init; } = Array.Empty<Options.Label>();

		/// <summary>
		/// Generates a string that would appear in an expression.
		/// </summary>
		protected string LabelString => string.Join("", Labels.Select(l => l.ToString()));

		/// <summary>
		/// Gets the TermResult for this ConstantTerm which will always be a single result with a value of the constant.
		/// </summary>
		/// <param name="context">The evaluation context</param>
		/// <returns>A TermResult which will always have a single result with value of the constant</returns>
		public override ExpressionResult Evaluate(EvaluationContext context)
			=> new()
			{
				Value = Constant,
				TermType = "constant"
			};

		/// <summary>
		/// Returns a string that represents this ConstantTerm
		/// </summary>
		/// <returns>A string representing this ConstantTerm</returns>
		public override string ToString() => $"{Constant.ToString(CultureInfo.CurrentCulture)}{LabelString}";
	}
}