using System.Collections.Generic;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// An enumeration in class form representing the three states a "boolean" value may be
	/// in a dice result: True and False are self explanatory. Unset is used
	/// as a default value to show that the status isn't a failure, but instead
	/// wasn't checked at all.
	///
	/// Is this overkill? You betcha. But I like it a bit better than an enum,
	/// and it's "safer" than a nullable bool.
	/// </summary>
	public class DiceBoolean
	{
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

		private const byte _unsetValue = 0;
		private const byte _trueValue = 1;
		private const byte _falseValue = 2;

		private static SortedList<byte, DiceBoolean> Values { get; } = new();

		private byte Value { get; init; }

		private DiceBoolean(byte value = _unsetValue)
		{
			Value = value;
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
				_ => "error"
			};
	}
}
