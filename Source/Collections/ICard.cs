using System;

namespace cmdwtf.NumberStones.Collections
{
	/// <summary>
	/// Represents a card in a <see cref="IDeck{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of value the card holds.</typeparam>
	public interface ICard<T> : IComparable<Card<T>> where T : IComparable<T>
	{
		/// <summary>
		/// If true, the card has been drawn/removed from the deck.
		/// </summary>
		bool Drawn { get; }

		/// <summary>
		/// If true, the card is currently in the deck.
		/// </summary>
		bool InDeck { get; }

		/// <summary>
		/// If true, the card belongs to a deck, regardless of it's drawn status.
		/// </summary>
		bool BelongsToDeck { get; }
	}
}
