using System.Collections.Generic;
using System.Linq;

using cmdwtf.NumberStones.DiceTypes;
using cmdwtf.NumberStones.Exceptions;
using cmdwtf.NumberStones.Options;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// The DiceTerm class represents a single "d" term in a DiceExpression
	/// </summary>
	/// <remarks>
	/// In the expression "2d6+5" the term "2d6" is a DiceTerm
	/// </remarks>
	public record DiceTerm : Term
	{
		public DiceSettings Settings { get; init; }

		/// <summary>
		/// The number of dice
		/// </summary>
		public int Multiplicity => Settings.Multiplicity;

		/// <summary>
		/// The number of sides per die
		/// </summary>
		public int Sides => Settings.Sides;

		/// <summary>
		/// Sum this many dice with the highest values out of those rolled
		/// </summary>
		public int Keep => Settings.Sides - Settings.Drop;

		/// <summary>
		/// Sum all but this many dice with the highest values out of those rolled
		/// </summary>
		public int Drop => Settings.Drop;

		/// <summary>
		/// The type of dice used in this term.
		/// </summary>
		public DiceType Kind => Settings.Kind;

		/// <summary>
		/// Construct a new instance of the DiceTerm class using the specified values
		/// </summary>
		/// <param name="multiplicity">The number of dice</param>
		/// <param name="sides">The number of sides per die</param>
		/// <param name="scalar">The amount to multiply the final sum of the dice by</param>
		public DiceTerm(int multiplicity, int sides)
		   : this(sides, multiplicity, multiplicity)
		{ }

		/// <summary>
		/// Construct a new instance of the DiceTerm class using the specified values
		/// </summary>
		/// <param name="multiplicity">The number of dice</param>
		/// <param name="sides">The number of sides per die</param>
		/// <param name="drop">The number of dice with the lowest values to drop when summing.</param>
		/// <param name="scalar">The amount to multiply the final sum of the dice by</param>
		public DiceTerm(int sides, int multiplicity, int drop)
		{
			if (sides < 0)
			{
				throw new ImpossibleDieException($"Cannot construct a die with {sides} sides");
			}
			if (multiplicity < 0)
			{
				throw new InvalidMultiplicityException($"Cannot roll {multiplicity} dice; this quantity is less than 0");
			}
			if (drop < 0)
			{
				throw new InvalidOptionException("Cannot drop {0} of the dice; it is less than 0");
			}
			if (drop > multiplicity)
			{
				throw new InvalidOptionException($"Cannot choose {drop} dice, only {multiplicity} were rolled");
			}

			Settings = new(sides, multiplicity)
			{
				OptionString = drop > 0 ? $"{Constants.DiceSeperator}{drop}" : ""
			};
		}

		/// <summary>
		/// Construct a new instance of the DiceTerm class using the specified option values
		/// </summary>
		/// <param name="options">The specific options to use</param>
		public DiceTerm(DiceSettings options)
		{
			Settings = options;
		}

		/// <summary>
		/// Gets the TermResult for this DiceTerm which will include the random value rolled
		/// </summary>
		/// <param name="context">The evaluation context</param>
		/// <returns>An IEnumerable of TermResult which will have one item per die rolled</returns>
		public override ExpressionResult Evaluate(EvaluationContext context)
		{
			// a 0 multiplicity die is a bizarre thing.
			// maybe we should be throwing exceptions, but for now
			// we will just give them back a 0.
			if (Multiplicity == 0)
			{
				return new DiceExpressionResult()
				{
					Value = 0,
					Sides = Sides,
					TermType = $"0{Constants.DiceSeperator}{Sides}"
				};
			}

			IEnumerable<DiceExpressionResult> results =
				from i in Enumerable.Range(0, Multiplicity)
				select DiceTypeResolver.Roll(Kind, context.Roller, Sides);

			foreach (IDiceOption option in Settings.Options)
			{
				results = option.Apply(results, context.Roller);
			}

			return new MultipleDiceTermResult(results)
			{
				TermType = $"{Multiplicity}{Constants.DiceSeperator}{Sides}"
			};
		}

		/// <summary>
		/// Returns a string that represents this DiceTerm
		/// </summary>
		/// <returns>A string representing this DiceTerm</returns>
		public override string ToString() => Settings.ToString();
	}
}