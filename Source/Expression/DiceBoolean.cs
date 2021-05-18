using System.Collections.Generic;
using System.Linq;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// An enumeration in class form representing the four states a "boolean" value may be
	/// in a dice result: True and False are self explanatory. Unset is used
	/// as a default value to show that the status isn't a failure, but instead
	/// wasn't checked at all. Indeterminate indicates that the value being checked represents
	/// a response where some of the details being described are made up of conflicting parts.
	///
	/// Is this overkill? You betcha. But I like it a bit better than an enum,
	/// and it's "safer" than a nullable bool.
	/// </summary>
	public class DiceBoolean
	{
		private static SortedList<byte, DiceBoolean> Values { get; } = new();

		/// <summary>
		/// The default value. Indicating the absense of true or false,
		/// this value is used to show that the given <see cref="DiceBoolean"/>
		/// wasn't evaluated at all.
		/// </summary>
		public static DiceBoolean Unset { get; } = new DiceBoolean(_unsetValue);
		/// <summary>
		/// The truthy value
		/// </summary>
		public static DiceBoolean True { get; } = new DiceBoolean(_trueValue);
		/// <summary>
		/// The falsy value
		/// </summary>
		public static DiceBoolean False { get; } = new DiceBoolean(_falseValue);
		/// <summary>
		/// The value that represents a state made where components represented hold conflicting values.
		/// </summary>
		public static DiceBoolean Indeterminate { get; } = new DiceBoolean(_indeterminateValue);

		private const byte _unsetValue = 0;
		private const byte _trueValue = 1;
		private const byte _falseValue = 2;
		private const byte _indeterminateValue = 3;

		private byte Value { get; init; }

		private DiceBoolean(byte value)
		{
			Value = value;
			Values.Add(value, this);
		}

		public static implicit operator bool(DiceBoolean value)
		{
			return value.Value == _trueValue;
		}

		public static implicit operator DiceBoolean(bool value)
		{
			return value
					   ? True
					   : False;
		}

		public static implicit operator DiceBoolean(byte value)
		{
			return Values[value];
		}

		public static implicit operator byte(DiceBoolean value)
		{
			return value.Value;
		}

		public override string ToString()
			=> Value switch
			{
				_unsetValue => "unset",
				_trueValue => "true",
				_falseValue => "false",
				_indeterminateValue => "indeterminate",
				_ => "error"
			};

		public static DiceBoolean Collate(IEnumerable<DiceBoolean> targets)
		{
			if (targets.All(t => t == Unset))
			{
				return Unset;
			}
			else if (targets.All(t => t == True))
			{
				return True;
			}
			else if (targets.All(t => t == False))
			{
				return False;
			}

			return Indeterminate;
		}
	}
}
