namespace cmdwtf.NumberStones.Options
{
	public sealed record Fudge(bool Enabled) : Fate(Enabled)
	{
		// fudge is the same thing as fate dice, no specific implementation needed.
		public override string Name => nameof(Fudge);
	}
}
