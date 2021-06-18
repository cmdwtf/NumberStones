using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using cmdwtf.NumberStones.Shufflers;

namespace cmdwtf.NumberStones.Collections
{
	public class Deck<T> : IDeck<T> where T : IComparable<T>
	{
		public T this[int index]
		{
			get => throw new NotImplementedException();
			set => throw new NotSupportedException("Setting via indexer is not supported.");
		}

		public IEnumerable<ICard<T>> All => throw new NotImplementedException();
		public IEnumerable<ICard<T>> Outstanding => throw new NotImplementedException();
		public IEnumerable<ICard<T>> Remaining => throw new NotImplementedException();
		public bool IsFixedSize => false;
		public bool IsReadOnly => false;
		public int Count => throw new NotImplementedException();
		public bool IsSynchronized => false;
		public object SyncRoot { get; } = new();

		public Deck(IEnumerable<T> items)
		{
			_cardsInDeck.AddRange(items.Select(i => new Card<T>(i)));
		}

		private readonly List<Card<T>> _cardsInDeck = new();
		private readonly List<Card<T>> _cardsDealt = new();
		private readonly List<Card<T>> _cardsInserted = new();

		public void Add(T item) => _cardsInDeck.Add(new(item));
		public object Clone() => throw new NotImplementedException();
		public bool Contains(T item) => throw new NotImplementedException();
		public IEnumerable<ICard<T>> Draw(int count) => throw new NotImplementedException();
		public ICard<T> Draw() => throw new NotImplementedException();
		public bool TryDraw([NotNullWhen(true)] out ICard<T>? drawnCard) => throw new NotImplementedException();
		public int IndexOf(T item) => throw new NotImplementedException();
		public void Insert(int index, T item) => throw new NotImplementedException();
		public void InsertAt(Index? index, T item) => throw new NotImplementedException();
		public bool Remove(T item) => throw new NotImplementedException();
		public void RemoveAt(int index) => throw new NotImplementedException();
		public void Reset() => throw new NotImplementedException();
		public void Reset(bool removeInserted) => throw new NotImplementedException();
		public void Shuffle() => throw new NotImplementedException();

		//////////////////////////////////////////////////////////////////////////
		object? IList.this[int index]
		{
			get => this[index];
			set => throw new NotSupportedException("Setting via indexer is not supported.");
		}
		int IList.Add(object? value) => throw new NotSupportedException($"Please use the typed {nameof(Add)}.");
		void IList.Clear() => throw new NotSupportedException($"{nameof(IList.Clear)} is not supported.");
		void ICollection<T>.Clear() => throw new NotSupportedException($"{nameof(ICollection<T>.Clear)} is not supported.");
		bool IList.Contains(object? value) => throw new NotSupportedException($"Please use the typed {nameof(Contains)}.");
		void ICollection.CopyTo(Array array, int index) => throw new NotSupportedException($"{nameof(ICollection.CopyTo)} is not supported.");
		void ICollection<T>.CopyTo(T[] array, int arrayIndex) => throw new NotSupportedException($"{nameof(ICollection<T>.CopyTo)} is not supported.");
		IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw new NotImplementedException();
		IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();
		int IList.IndexOf(object? value) => throw new NotSupportedException($"Please use the typed {nameof(IndexOf)}.");
		void IList.Insert(int index, object? value) => throw new NotSupportedException($"Please use the typed {nameof(Insert)}.");
		void IList.Remove(object? value) => throw new NotSupportedException($"Please use the typed {nameof(Remove)}.");
		void IList.RemoveAt(int index) => RemoveAt(index);
		void IShuffleable.Shuffle(IShuffler shuffler) => shuffler.Shuffle(_cardsInDeck);
	}
}
