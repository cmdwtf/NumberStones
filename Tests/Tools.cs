using System.Diagnostics;

namespace cmdwtf.NumberStones.Tests
{
	/// <summary>
	/// Miscellaneous tools for Tests
	/// </summary>
	public static class Tools
	{
		/// <summary>
		/// A helper to write to the debug console from tests
		/// </summary>
		/// <param name="input">The input to the test</param>
		/// <param name="parsed">The result of the parsed dice expression</param>
		/// <param name="result">The result of the dice roll</param>
		/// <param name="expected">What the test expected to generate</param>
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
