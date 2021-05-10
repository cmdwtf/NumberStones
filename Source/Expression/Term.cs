using cmdwtf.NumberStones.Rollers;

namespace cmdwtf.NumberStones.Expression
{
	public abstract record Term : ITerm
	{
		public IDieRoller Roller { protected get; set; } = Instances.DefaultRoller;

		public abstract ExpressionResult Evaluate();
	}
}
