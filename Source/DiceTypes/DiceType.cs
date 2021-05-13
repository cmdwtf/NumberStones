namespace cmdwtf.NumberStones.DiceTypes
{
	/// <summary>
	/// An enumeration representing the different types of dice
	///
	/// And yeah, a coin is a die. It's just a D2.
	/// </summary>
	public enum DiceType
	{
		/// <summary>
		/// A standard, numerical die
		/// </summary>
		Numerical = 0,
		/// <summary>
		/// A two sided die
		/// </summary>
		Coin,
		/// <summary>
		/// A 6-sided die based on the Fudge RPG system https://en.wikipedia.org/wiki/Fudge_(role-playing_game_system)
		/// Each die has six sides, two each of "+", "-" and "Blank". This allows rolling multiple,
		/// usually in a set of 4, to return a result between -4 and +4, with the most likely result being 0.
		/// This is an alias for Fate.
		/// </summary>
		Fudge,
		/// <summary>
		/// A 6-sided die based on the Fudge RPG system https://en.wikipedia.org/wiki/Fudge_(role-playing_game_system)
		/// Each die has six sides, two each of "+", "-" and "Blank". This allows rolling multiple,
		/// usually in a set of 4, to return a result between -4 and +4, with the most likely result being 0.
		/// This is an alias for Fudge.
		/// </summary>
		Fate = Fudge,
		/// <summary>
		/// A 6-sided die used by the Magic: The Gathering® Planechase™ Game Format https://magic.wizards.com/en/articles/archive/feature/rules-revealed-2009-08-10
		/// Four of the die's six sides are blank, while one has the "planeswalker symbol" ({PW}), and the opposite face the "chaos symbol" ({CHAOS}).
		/// </summary>
		Planar,
	}
}
