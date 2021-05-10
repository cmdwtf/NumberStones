
using System;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The DiceExpression class can be used to fluently add DiceTerms to a collection and then Roll them.
	/// </summary>
	public class DiceExpression : ITerm
	{
		/// <inheritdoc cref="ITerm.Roller"/>
		public IDieRoller Roller { protected get; set; } = Instances.DefaultRoller;

		private IExpression _expression = EmptyExpression.Instance;

		public bool IsEmpty { get; private set; } = true;

		/// <summary>
		/// Construct a new DiceExpression class with an empty expression.
		/// </summary>
		public DiceExpression() { }

		/// <summary>
		/// Create a DiceExpression with a die that has the specified number of sides
		/// </summary>
		/// <param name="sides">The number of sides on the Die to add to this DiceExpression</param>
		/// <returns>A DiceExpression representing the previous terms in this DiceExpression plus this newly added Die</returns>
		public static DiceExpression Die(int sides) => Dice(sides, 1);

		/// <summary>
		/// Create a DiceExpression with the specified parameters
		/// </summary>
		/// <param name="sides">The number of sides per Die</param>
		/// <param name="multiplicity">The number of Dice</param>
		/// <returns>A DiceExpression representing the previous terms in this DiceExpression plus these newly added Dice</returns>
		public static DiceExpression Dice(int sides, int multiplicity) => Dice(sides, multiplicity, null);

		/// <summary>
		/// Create a DiceExpression with the specified parameters
		/// </summary>
		/// <param name="sides">The number of sides per Die</param>
		/// <param name="multiplicity">The number of Dice</param>
		/// <param name="keep">Optional number of dice to keep out of the total rolled. The highest rolled Dice will be chosen.</param>
		/// <returns>A DiceExpression representing the previous terms in this DiceExpression plus these newly added Dice</returns>
		public static DiceExpression Dice(int sides, int multiplicity, int? keep)
		{
			DiceExpression result = new()
			{
				_expression = new DiceTerm(multiplicity, sides, keep ?? multiplicity),
				IsEmpty = false
			};

			return result;
		}

		/// <summary>
		/// Add a constant to this DiceExpression with the specified integer value
		/// </summary>
		/// <param name="value">An integer constant to add to this DiceExpression</param>
		/// <returns>A DiceExpression representing the previous terms in this DiceExpression plus this newly added Constant</returns>
		public static DiceExpression Constant(int value)
		{
			DiceExpression result = new()
			{
				_expression = new ConstantTerm(value),
				IsEmpty = false
			};

			return result;
		}


		// #doc
		internal DiceExpression Plus(IExpression operand)
			=> AddExpression(BinaryOperation.Add, operand);
		internal DiceExpression Minus(IExpression operand)
			=> AddExpression(BinaryOperation.Subtract, operand);
		internal DiceExpression Times(IExpression operand)
			=> AddExpression(BinaryOperation.Multiply, operand);
		internal DiceExpression DividedBy(IExpression operand)
			=> AddExpression(BinaryOperation.Divide, operand);
		internal DiceExpression Modulo(IExpression operand)
			=> AddExpression(BinaryOperation.Modulo, operand);

		// #doc
		internal DiceExpression AddExpression(Func<IExpression, IExpression, IOperation> binaryOperation, IExpression operand)
		{
			_expression = binaryOperation(_expression, operand);
			return this;
		}

		//internal DiceExpression AddExpression(Func<IExpression, IOperation> unaryOperation, IExpression operand)
		//{
		//	_expression = unaryOperation(_expression, operand);
		//	return this;
		//}

		//internal DiceExpression Positive(IExpression operand) { _expression = UnaryOperation.Positive(operand); return this; }
		//internal DiceExpression Negative(IExpression operand) { _expression = UnaryOperation.Negative(operand); return this; }

		/// <summary>
		/// Roll all of the Dice that are part of this DiceExpression
		/// </summary>
		/// <param name="roller">Dice roller RNG used to perform the Roll.</param>
		/// <returns>A DiceResult representing the results of this Roll</returns>
		public DiceResult Roll(IDieRoller roller)
		{
			Roller = roller;
			ExpressionResult results = (this as IExpression).Evaluate();
			return new DiceResult(results, roller);
		}

		/// <summary>
		/// Roll all of the Dice that are part of this DiceExpression
		/// </summary>
		/// <returns>A DiceResult representing the results of this Roll</returns>
		/// <remarks>Uses default roller as its RNG</remarks>
		public DiceResult Roll() => Roll(Instances.DefaultRoller);

		/// <summary>
		/// Roll all of the Dice that are part of this DiceExpression, but force all of the rolls to be the lowest possible result
		/// </summary>
		/// <returns>A DiceResult representing the results of this Roll. All dice should have rolled their minimum values</returns>
		public DiceResult MinRoll() => Roll(Instances.MinRoller);

		/// <summary>
		/// Roll all of the Dice that are part of this DiceExpression, but force all of the rolls to be the highest possible result
		/// </summary>
		/// <returns>A DiceResult representing the results of this Roll. All dice should have rolled their maximum values</returns>
		public DiceResult MaxRoll() => Roll(Instances.MaxRoller);


		/// <summary>
		/// Evaluates the whole expression and returns it's value as a TermResult.
		/// </summary>
		/// <returns>The evaluated expression's result.</returns>
		ExpressionResult IExpression.Evaluate() => _expression.Evaluate();
	}
}