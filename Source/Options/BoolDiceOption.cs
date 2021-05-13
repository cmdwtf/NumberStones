namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// An abstract record that represents a boolean dice option
	/// </summary>
	public abstract record BoolDiceOption(bool Value) : DiceOptionBase<bool>(Value)
	{

	}
}
