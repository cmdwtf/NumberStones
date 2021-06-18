using cmdwtf.NumberStones.Shufflers;

namespace cmdwtf.NumberStones.Collections
{
	public interface IShuffleable
	{
		void Shuffle();
		void Shuffle(IShuffler shuffler);
	}
}
