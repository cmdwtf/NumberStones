using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cmdwtf.NumberStones.Tests
{
	[TestClass]
	public class IDieRollerTests
	{
		[TestMethod]
		public void DieRollerInformationsNotNullOrEmpty()
		{
			Assert.IsFalse(string.IsNullOrWhiteSpace(new Rollers.MersenneTwisterDieRoller().Information));
			Assert.IsFalse(string.IsNullOrWhiteSpace(new Rollers.DotNetDieRoller().Information));
			Assert.IsFalse(string.IsNullOrWhiteSpace(new Rollers.MaxDieRoller().Information));
			Assert.IsFalse(string.IsNullOrWhiteSpace(new Rollers.MinDieRoller().Information));
		}
	}
}
