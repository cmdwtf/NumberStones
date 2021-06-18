using System;

namespace cmdwtf.NumberStones.Collections
{
	/// <summary>
	/// A record representing a card that can be used in a deck.
	/// </summary>
	/// <typeparam name="T">The underlying value the card represents.</typeparam>
	public record Card<T>(T Value) : ICard<T> where T : IComparable<T>
	{
		/// <inheritdoc cref="ICard{T}.Drawn"/>
		public bool Drawn { get; internal set; }
		/// <inheritdoc cref="ICard{T}.InDeck"/>
		public bool InDeck => !Drawn;
		/// <inheritdoc cref="ICard{T}.BelongsToDeck"/>
		public bool BelongsToDeck { get; internal set; }

		/// <inheritdoc cref="IComparable{T}.CompareTo(T?)"/>
		public int CompareTo(Card<T>? other) => throw new NotImplementedException();
		//Value.CompareTo(other?.Value);

		/// <summary>
		/// Implicitly converts a card to it's value.
		/// </summary>
		/// <param name="card">The card to comvert to it's value.</param>
		public static implicit operator T(Card<T> card) => card.Value;
	}
}
