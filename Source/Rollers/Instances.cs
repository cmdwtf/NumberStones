using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdwtf.NumberStones.Rollers
{
	/// <summary>
	/// #DOC
	/// </summary>
	public static class Instances
	{
		public static IDieRoller DefaultRoller => MersenneTwisterRoller;
		public static IDieRoller DotNetRoller = new DotNetDieRoller();
		public static IDieRoller MaxRoller = new MaxDieRoller();
		public static IDieRoller MinRoller = new MinDieRoller();
		public static IDieRoller MersenneTwisterRoller = new MersenneTwisterDieRoller();
	}
}
