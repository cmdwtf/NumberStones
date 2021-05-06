using System;

namespace cmdwtf.NumberStones.Exceptions
{
	/// <summary>
	/// Exception that is thrown when a die is attempted to be constructed with an invalid number of sides
	/// </summary>
	public class ImpossibleDieException : Exception
	{

		/// <summary>
		/// Initializes a new instance of the ImpossibleDieException class.
		/// </summary>
		public ImpossibleDieException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ImpossibleDieException class with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public ImpossibleDieException(string message)
		   : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ImpossibleDieException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		public ImpossibleDieException(string message, Exception innerException)
		   : base(message, innerException)
		{
		}
	}
}