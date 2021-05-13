
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
		private IExpression _expression = EmptyExpression.Instance;

		/// <summary>
		/// A value indicating if this dice expression contains any evaluatable expressions.
		/// </summary>
		public bool IsEmpty { get; private set; } = true;

		/// <summary>
		/// An alias for the opposite of <see cref="IsEmpty"/>, is true when
		/// this dice expression has expressions that can be evaluated.
		/// </summary>
		public bool CanEvaulate => !IsEmpty;

		/// <summary>
		/// A user-specified string associated with this expression.
		/// </summary>
		public string Comment { get; set; } = string.Empty;

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
		/// DiceExpression that has nothing in it.
		/// </summary>
		/// <returns>An empty dice expression.</returns>
		public static DiceExpression Empty => new();

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

		#region Fluent Operations

		/// <inheritdoc cref="Plus(IExpression)"/>
		public DiceExpression Plus(decimal value)
			=> Plus(new ConstantTerm(value));

		/// <inheritdoc cref="Minus(IExpression)"/>
		public DiceExpression Minus(decimal value)
			=> Minus(new ConstantTerm(value));

		/// <inheritdoc cref="Times(IExpression)"/>
		public DiceExpression Times(decimal value)
			=> Times(new ConstantTerm(value));

		/// <inheritdoc cref="DividedBy(IExpression)"/>
		public DiceExpression DividedBy(decimal value)
			=> DividedBy(new ConstantTerm(value));

		/// <inheritdoc cref="Modulo(IExpression)"/>
		public DiceExpression Modulo(decimal value)
			=> Modulo(new ConstantTerm(value));

		/// <inheritdoc cref="Plus(IExpression)"/>
		public DiceExpression Plus(DiceTerm value)
			=> Plus(value);

		/// <inheritdoc cref="Minus(IExpression)"/>
		public DiceExpression Minus(DiceTerm value)
			=> Minus(value);

		/// <inheritdoc cref="Times(IExpression)"/>
		public DiceExpression Times(DiceTerm value)
			=> Times(value);

		/// <inheritdoc cref="DividedBy(IExpression)"/>
		public DiceExpression DividedBy(DiceTerm value)
			=> DividedBy(value);

		/// <inheritdoc cref="Modulo(IExpression)"/>
		public DiceExpression Modulo(DiceTerm value)
			=> Modulo(value);

		/// <summary>
		/// Adds an addition operation with the given operand
		/// </summary>
		/// <param name="operand">The operand to add</param>
		/// <returns>This expression</returns>
		private DiceExpression Plus(IExpression operand)
			=> AddExpression(BinaryOperation.Add, operand);

		/// <summary>
		/// Adds a subtraction operation with the given operand
		/// </summary>
		/// <param name="operand">The operand to subtract</param>
		/// <returns>This expression</returns>
		private DiceExpression Minus(IExpression operand)
			=> AddExpression(BinaryOperation.Subtract, operand);

		/// <summary>
		/// Adds a multiply operation with the given operand
		/// </summary>
		/// <param name="operand">The operand to multiply against</param>
		/// <returns>This expression</returns>
		private DiceExpression Times(IExpression operand)
			=> AddExpression(BinaryOperation.Multiply, operand);

		/// <summary>
		/// Adds a divide operation with the given operand
		/// </summary>
		/// <param name="operand">The operand to divide by</param>
		/// <returns>This expression</returns>
		private DiceExpression DividedBy(IExpression operand)
			=> AddExpression(BinaryOperation.Divide, operand);

		/// <summary>
		/// Adds a modulo operation with the given operand
		/// </summary>
		/// <param name="operand">The operand to modulo by</param>
		/// <returns>This expression</returns>
		private DiceExpression Modulo(IExpression operand)
			=> AddExpression(BinaryOperation.Modulo, operand);

		#endregion Fluent Operations

		/// <summary>
		/// Adds a binary operation expression to this expression.
		/// </summary>
		/// <param name="binaryOperation">The operation to add</param>
		/// <param name="operand">The term to add on the other side of the binary operation</param>
		/// <returns>This expression</returns>
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
			if (IsEmpty)
			{
				throw new Exceptions.ImpossibleDieException($"This {nameof(DiceExpression)} has no expression to evaluate.");
			}

			EvaluationContext context = new(roller);
			ExpressionResult results = (this as IExpression).Evaluate(context);
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
		/// <param name="context">The evaluation context</param>
		/// <returns>The evaluated expression's result</returns>
		ExpressionResult IExpression.Evaluate(EvaluationContext context) => _expression.Evaluate(context);


		/// <summary>
		/// Returns a string that represents this Dice Expression
		/// </summary>
		/// <returns>A string representing this Dice Expression</returns>
		public override string ToString() => _expression?.ToString() ?? "<no expression>";
	}
}