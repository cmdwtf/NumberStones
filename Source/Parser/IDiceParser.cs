
namespace cmdwtf.NumberStones
{
	/// <summary>
	/// The DiceParser interface can be implemented to parse a string into a DiceExpression
	/// </summary>
	public interface IDiceParser
	{
		/// <summary>
		/// Create a new DiceExpression by parsing the specified expression string
		/// </summary>
		/// <param name="expression">A dice notation string expression. Ex. 3d6+3</param>
		/// <returns>A DiceExpression parsed from the specified string</returns>
		DiceExpression Parse(string expression);


		/// <summary>
		/// Create a new DiceExpression by parsing the specified expression string
		/// </summary>
		/// <param name="expression">A dice notation string expression. Ex. 3d6+3</param>
		/// <param name="value">A DiceExpression parsed from the specified string</param>
		/// <returns>true, if the parse was successful, otherwise false</returns>
		public bool TryParse(string expression, out DiceExpression value);
	}
}