using System;
using System.Collections.Generic;
using System.Linq;

using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// A static class that loads all IDice implementations and uses them to resolve dice rolls to the proper dice type.
	/// </summary>
	internal static class DiceTypeResolver
	{
		private static readonly Dictionary<DiceType, IDice> _diceTypes = new();

		static DiceTypeResolver()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Where(x => typeof(IDice)
				.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
				.ToList();

			foreach (Type type in types)
			{
				IDice die = Activator.CreateInstance(type) as IDice
					?? throw new InvalidOperationException($"Failed to load any {nameof(IDice)} implementations, cannot roll dice.");
				_diceTypes.Add(die.Type, die);
			}
		}

		/// <summary>
		/// Rolls a die of the specified type.
		/// </summary>
		/// <param name="type">The type of die to roll.</param>
		/// <param name="roller">The die roller to use.</param>
		/// <param name="sides">The number of sides on the die to roll.</param>
		/// <returns>The result of the roll.</returns>
		public static DiceExpressionResult Roll(DiceType type, IDieRoller roller, decimal sides)
		{
			if (_diceTypes.ContainsKey(type) == false)
			{
				throw new Exceptions.ImpossibleDieException($"There is no dice type handler for the dice type {type}");
			}

			return _diceTypes[type].Roll(roller, sides);
		}
	}
}
