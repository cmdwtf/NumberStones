
using System;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The DiceExpression class can be used to fluently add DiceTerms to a collection and then Roll them.
	/// </summary>
	public sealed class DiceExpression : ITerm
	{
		/// <inheritdoc cref="ITerm.Roller"/>
		public IDieRoller Roller { private get; set; } = Instances.DefaultRoller;

		private IExpression _expression = EmptyExpression.Instance;

		public bool IsEmpty { get; private set; } = true;

		/// <summary>
		/// Construct a new DiceExpression class with an empty expression.
		/// </summary>
		private DiceExpression() { }

		/// <summary>
		/// Construct a new DiceExpression class with the given expression.
		/// </summary>
		internal DiceExpression(IExpression expr)
		{
			_expression = expr;
			IsEmpty = _expression is EmptyExpression;
		}

		/// <summary>
		/// Create a DiceExpression that has nothing in it.
		/// </summary>
		/// <returns>An empty dice expression.</returns>
		public static DiceExpression Empty() => new();

		/// <summary>
		/// Create a DiceExpression with a die that has the specified number of sides
		/// </summary>
		/// <param name="sides">The number of sides on the Die to add to this DiceExpression</param>
		/// <returns>A new DiceExpression representing the selected Die</returns>
		public static DiceExpression Die(int sides) => Dice(sides, 1);

		/// <summary>
		/// Create a DiceExpression with the specified parameters
		/// </summary>
		/// <param name="sides">The number of sides per Die</param>
		/// <param name="multiplicity">The number of Dice</param>
		/// <returns>A new DiceExpression representing the selected Dice</returns>
		public static DiceExpression Dice(int sides, int multiplicity) => Dice(sides, multiplicity, null);

		/// <summary>
		/// Create a DiceExpression with the specified parameters
		/// </summary>
		/// <param name="sides">The number of sides per Die</param>
		/// <param name="multiplicity">The number of Dice</param>
		/// <param name="drop">Optional number of dice to drop out of the total rolled. The highest rolled Dice will be kept.</param>
		/// <returns>A new DiceExpression representing the selected Dice</returns>
		public static DiceExpression Dice(int sides, int multiplicity, int? drop)
		{
			DiceExpression result = new()
			{
				_expression = new DiceTerm(new DiceSettings(sides, multiplicity)
				{
					OptionString = drop == null ? "" : $"d{drop}"
				}),
				IsEmpty = false
			};

			return result;
		}

		/// <summary>
		/// Create a DiceExpression with the specified parameters
		/// </summary>
		/// <param name="options">The dice optiosn to use</param>
		/// <returns>A new DiceExpression representing the selected Dice</returns>
		public static DiceExpression Dice(DiceSettings options)
		{
			DiceExpression result = new()
			{
				_expression = new DiceTerm(options),
				IsEmpty = false
			};

			return result;
		}

		/// <summary>
		/// Add a constant to this DiceExpression with the specified integer value
		/// </summary>
		/// <param name="value">An integer constant to add to this DiceExpression</param>
		/// <returns>A DiceExpression representing the previous terms in this DiceExpression plus this newly added Constant</returns>
		public static DiceExpression Constant(decimal value)
		{
			DiceExpression result = new()
			{
				_expression = new ConstantTerm(value),
				IsEmpty = false
			};

			return result;
		}


		// #doc
		public DiceExpression Plus(decimal value)
			=> Plus(new ConstantTerm(value));
		public DiceExpression Minus(decimal value)
			=> Minus(new ConstantTerm(value));
		public DiceExpression Times(decimal value)
			=> Times(new ConstantTerm(value));
		public DiceExpression DividedBy(decimal value)
			=> DividedBy(new ConstantTerm(value));
		public DiceExpression Modulo(decimal value)
			=> Modulo(new ConstantTerm(value));

		public DiceExpression Plus(DiceTerm value)
			=> Plus(value);
		public DiceExpression Minus(DiceTerm value)
			=> Minus(value);
		public DiceExpression Times(DiceTerm value)
			=> Times(value);
		public DiceExpression DividedBy(DiceTerm value)
			=> DividedBy(value);
		public DiceExpression Modulo(DiceTerm value)
			=> Modulo(value);

		private DiceExpression Plus(IExpression operand)
			=> AddExpression(BinaryOperation.Add, operand);
		private DiceExpression Minus(IExpression operand)
			=> AddExpression(BinaryOperation.Subtract, operand);
		private DiceExpression Times(IExpression operand)
			=> AddExpression(BinaryOperation.Multiply, operand);
		private DiceExpression DividedBy(IExpression operand)
			=> AddExpression(BinaryOperation.Divide, operand);
		private DiceExpression Modulo(IExpression operand)
			=> AddExpression(BinaryOperation.Modulo, operand);

		// #move
		private DiceExpression AddExpression(Func<IExpression, IExpression, IOperation> binaryOperation, IExpression operand)
		{
			_expression = binaryOperation(_expression, operand);
			return this;
		}

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


		/// <summary>
		/// Returns a string that represents this Dice Expression
		/// </summary>
		/// <returns>A string representing this Dice Expression</returns>
		public override string ToString() => _expression?.ToString() ?? "<no expression>";
	}
}