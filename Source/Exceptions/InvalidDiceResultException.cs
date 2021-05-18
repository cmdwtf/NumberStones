using System;

namespace cmdwtf.NumberStones.Exceptions
{
	/// <summary>
	/// Exception that is thrown when a part of a dice result is attempted to be examined when
	/// it is not directly applicable.
	/// </summary>
	public class InvalidDiceResultException : Exception
	{

		/// <summary>
		/// Initializes a new instance of the InvalidDiceResultException class.
		/// </summary>
		public InvalidDiceResultException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the InvalidDiceResultException class with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public InvalidDiceResultException(string message)
		   : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the InvalidDiceResultException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		public InvalidDiceResultException(string message, Exception innerException)
		   : base(message, innerException)
		{
		}
	}
}