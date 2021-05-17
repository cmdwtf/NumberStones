namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record representing a dice value that was dropped from a result.
	/// </summary>
	public record DiceDropResult(decimal OriginalValue, string Reason = "")
	{
		/// <summary>
		/// A static instance representing a dice result that was not dropped.
		/// </summary>
		public static DiceDropResult NotDropped { get; } = new(0);


		/// <summary>
		/// An implicit bool conversion so checking if a value is dropped
		/// can be easily determined as a boolean state. Returns true
		/// if an original value is not '0'
		/// </summary>
		/// <param name="value">The result to check</param>
		public static implicit operator bool(DiceDropResult value)
		{
			return value.OriginalValue != 0;
		}
	}
}
