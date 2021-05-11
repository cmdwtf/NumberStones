namespace cmdwtf.NumberStones.Options
{

	public abstract record DecimalDiceOption(decimal Value) : DiceOptionBase<decimal>(Value)
	{

	}
}
