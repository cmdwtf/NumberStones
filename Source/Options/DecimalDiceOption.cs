namespace cmdwtf.NumberStones.Options
{
	/// <summary>
	/// A dice option record base that uses a decimal for a value
	/// </summary>
	public abstract record DecimalDiceOption(decimal Value) : DiceOptionBase<decimal>(Value)
	{

	}
}
