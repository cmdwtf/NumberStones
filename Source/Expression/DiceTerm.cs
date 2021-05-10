using System.Collections.Generic;
using System.Linq;

using cmdwtf.NumberStones.Exceptions;

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
		/// <summary>
		/// The number of dice
		/// </summary>
		public int Multiplicity { get; private init; }

		/// <summary>
		/// The number of sides per die
		/// </summary>
		public int Sides { get; private init; }

		/// <summary>
		/// Sum this many dice with the highest values out of those rolled
		/// </summary>
		protected int Keep { get; private init; }

		/// <summary>
		/// Construct a new instance of the DiceTerm class using the specified values
		/// </summary>
		/// <param name="multiplicity">The number of dice</param>
		/// <param name="sides">The number of sides per die</param>
		/// <param name="scalar">The amount to multiply the final sum of the dice by</param>
		public DiceTerm(int multiplicity, int sides)
		   : this(multiplicity, sides, multiplicity)
		{ }

		/// <summary>
		/// Construct a new instance of the DiceTerm class using the specified values
		/// </summary>
		/// <param name="multiplicity">The number of dice</param>
		/// <param name="sides">The number of sides per die</param>
		/// <param name="choose">Sum this many dice with the highest values out of those rolled</param>
		/// <param name="scalar">The amount to multiply the final sum of the dice by</param>
		public DiceTerm(int multiplicity, int sides, int choose)
		{
			if (sides <= 0)
			{
				throw new ImpossibleDieException($"Cannot construct a die with {sides} sides");
			}
			if (multiplicity < 0)
			{
				throw new InvalidMultiplicityException($"Cannot roll {multiplicity} dice; this quantity is less than 0");
			}
			if (choose < 0)
			{
				throw new InvalidKeepException("Cannot choose {0} of the dice; it is less than 0");
			}
			if (choose > multiplicity)
			{
				throw new InvalidKeepException($"Cannot choose {choose} dice, only {multiplicity} were rolled");
			}

			Sides = sides;
			Multiplicity = multiplicity;
			Keep = choose;
		}

		/// <summary>
		/// Gets the TermResult for this DiceTerm which will include the random value rolled
		/// </summary>
		/// <returns>An IEnumerable of TermResult which will have one item per die rolled</returns>
		public override ExpressionResult Evaluate()
		{
			if (Multiplicity == 1 && Keep == 1)
			{
				return new()
				{
					Value = Roller.RollDie(Sides),
					TermType = $"d{Sides}"
				};
			}

			IEnumerable<ExpressionResult> rolls =
				from i in Enumerable.Range(0, Multiplicity)
				select new ExpressionResult
				{
					Value = Roller.RollDie(Sides),
					TermType = "d" + Sides
				};

			// #keep
			//IEnumerable<TermResult> kept = rolls.OrderByDescending(d => d.Value).Take(Keep);

			return new MultipleTermResult(rolls)
			{
				TermType = $"{Multiplicity}d{Sides}"
			};
		}

		/// <summary>
		/// Returns a string that represents this DiceTerm
		/// </summary>
		/// <returns>A string representing this DiceTerm</returns>
		public override string ToString()
		{
			string keep = Keep == Multiplicity ? "" : "k" + Keep;
			return $"{Multiplicity}d{Sides}{keep}";
		}
	}
}