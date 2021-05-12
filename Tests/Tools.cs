using System.Diagnostics;

namespace cmdwtf.NumberStones.Tests
{
	public static class Tools
	{
		public static void Write(string input, DiceExpression? parsed, DiceResult? result = null, object? expected = null)
		{
			Debug.WriteLine($"Input: {input}");

			if (parsed is not null)
			{
				Debug.WriteLine($"Parsed: {input}");
			}
			else
			{
				Debug.WriteLine("Parsed: <null>");
			}

			Debug.WriteLineIf(result is not null, $"Result: {result}");
			Debug.WriteLineIf(expected is not null, $"Expected: {expected}");
		}
	}
}
