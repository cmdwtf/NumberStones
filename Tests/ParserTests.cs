using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class ParserTests
	{
		[DataTestMethod]
		[DataRow("d6")]
		[DataRow("4d6")]
		[DataRow("4d6d1")]
		[DataRow("4d6dl1")]
		[DataRow("4d6dl")]
		[DataRow("4d6kh3")]
		[DataRow("1d6 + 1")]
		[DataRow("1d6+1")]
		[DataRow("1 + 1d6")]
		[DataRow("1+1d6")]
		[DataRow("1d6 * 3")]
		public void SimpleExpressionParse(string expression)
		{
			DiceExpression? p = Dice.Parse(expression);
			Assert.IsNotNull(p);
			Assert.IsFalse(p.IsEmpty);
		}
	}
}
