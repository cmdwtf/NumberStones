using System;

namespace cmdwtf.NumberStones.Exceptions
{
	/// <summary>
	/// Exception that is thrown when a dice expression option fails to parse.
	/// </summary>
	public class DiceExpressionOptionParseException : DiceExpressionParseException
	{
		/// <summary>
		/// The entire option string associated with the parse exception.
		/// </summary>
		public string FullOptionString { get; init; } = string.Empty;

		/// <summary>
		/// Initializes a new instance of the DiceExpressionOptionParseException class.
		/// </summary>
		public DiceExpressionOptionParseException() { }


		/// <summary>
		/// Initializes a new instance of the DiceExpressionOptionParseException class with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public DiceExpressionOptionParseException(string message, string fullOptions)
			: base($"{message} ({fullOptions})")
		{
			FullOptionString = fullOptions;
		}

		/// <summary>
		/// Initializes a new instance of the DiceExpressionOptionParseException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		public DiceExpressionOptionParseException(string message, Exception innerException, string fullOptions)
			: base($"{message} ({fullOptions})", innerException)
		{
			FullOptionString = fullOptions;
		}
	}
}
