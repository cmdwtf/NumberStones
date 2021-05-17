namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An abstract record that represents a boolean dice option
	/// </summary>
	public abstract record BoolDiceOption(bool Value) : DiceOptionBase<bool>(Value)
	{
		// Overriden because record ToString() gets stomped.
		/// <inheritdoc cref="DiceOptionBase{T}.ToString"/>
		public override string ToString() => base.ToString();
	}
}
