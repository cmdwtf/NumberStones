using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Expression
{
	/// <summary>
	/// A record representing the current evaluation state of an expression
	/// </summary>
	public record EvaluationContext(IDieRoller Roller)
	{

	}
}
