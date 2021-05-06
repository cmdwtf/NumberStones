using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdwtf.NumberStones.Parser
{
	public class DiceParser : IDiceParser
	{
		public DiceExpression Parse(string expression)
		{
			return new DiceExpression();
		}
	}
}
