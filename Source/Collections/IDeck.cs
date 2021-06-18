using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace cmdwtf.NumberStones.Collections
{
	public interface IDeck<T> : IShuffleable, IList, IList<T>, ICollection, ICollection<T>, IEnumerable, ICloneable where T : IComparable<T>
	{
		/// <summary>
		/// Draw as many T (as cards) as the count.
		/// </summary>
		/// <param name="count">The number of T to draw.</param>
		/// <returns>An enumerable of T that contains the specified count, or less if the deck ran out.</returns>
		IEnumerable<ICard<T>> Draw(int count);

		/// <summary>
		/// Gets one T as a card, and marks it as dealt.
		/// </summary>
		/// <returns>The drawn item.</returns>
		/// <exception cref="InvalidOperationException">If the deck is empty.</exception>
		ICard<T> Draw();

		/// <summary>
		/// Attempts to draw a card from the deck.
		/// </summary>
		/// <param name="drawnCard">The card drawn, or null if there was no card to draw.</param>
		/// <returns>True if the draw was successful.</returns>
		bool TryDraw([NotNullWhen(true)] out ICard<T>? drawnCard);

		/// <summary>
		/// Inserts the given item at the specified index in the remaining items.
		/// If the item has never been seen before, it will be added to the collection.
		/// If it was from the collection and was already dealt, it will be unmarked as dealt.
		/// </summary>
		/// <param name="index">The index of where to insert the item in the remaining items.</param>
		/// <param name="item">The item to add.</param>
		/// <exception cref="IndexOutOfRangeException">If the index is out of range.</exception>
		void InsertAt(Index? index, T item);

		/// <summary>
		/// Resets the deck to the original state.
		/// </summary>
		void Reset();

		/// <summary>
		/// Resets the deck to the original state, optionally removing any items that were inserted after creation.
		/// </summary>
		/// <param name="removeInserted">If true, removes inserted items from the collection.</param>
		void Reset(bool removeInserted);

		/// <summary>
		/// Gets a collection of all of the items in the deck.
		/// </summary>
		IEnumerable<ICard<T>> All { get; }

		/// <summary>
		/// Gets a collection of all of the items that originated from the deck, but
		/// have been dealt or removed.
		/// </summary>
		IEnumerable<ICard<T>> Outstanding { get; }

		/// <summary>
		/// Gets a collection of all of the items that remain in the deck that haven't
		/// been dealt or removed.
		/// </summary>
		IEnumerable<ICard<T>> Remaining { get; }
	}
}
