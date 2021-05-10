#region "License"

// based on http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/VERSIONS/C-LANG/mt19937ar.cs

/*
   A C-program for MT19937, with initialization improved 2002/1/26.
   Coded by Takuji Nishimura and Makoto Matsumoto.

   Before using, initialize the state by using init_genrand(seed)
   or init_by_array(initKey, key_length).

   Copyright (C) 1997 - 2002, Makoto Matsumoto and Takuji Nishimura,
   All rights reserved.
   Copyright (C) 2005, Mutsuo Saito,
   All rights reserved.

   Redistribution and use in source and binary forms, with or without
   modification, are permitted provided that the following conditions
   are met:

	 1. Redistributions of source code must retain the above copyright
		notice, this list of conditions and the following disclaimer.

	 2. Redistributions in binary form must reproduce the above copyright
		notice, this list of conditions and the following disclaimer in the
		documentation and/or other materials provided with the distribution.

	 3. The names of its contributors may not be used to endorse or promote
		products derived from this software without specific prior written
		permission.
*/

/*
   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
   A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE COPYRIGHT OWNER OR
   CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
   EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
   PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
   PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
   LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
   NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
   SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


   Any feedback is very welcome.
   http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html
   email: m-mat @ math.sci.hiroshima-u.ac.jp (remove space)
*/

/*
   A C#-program for MT19937, with initialization improved 2006/01/06.
   Coded by Mitil.

   Copyright (C) 2006, Mitil, All rights reserved.

   Any feedback is very welcome.
   URL: http://meisui.psk.jp/
   email: m-i-t-i-l [at@at] p-s-k . j-p
		   (remove dash[-], and replace [at@at] --> @)
*/

#endregion

using System;
using System.Linq;
using System.Security.Cryptography;

namespace cmdwtf.NumberStones.Random
{
	public class MersenneTwister19937 : System.Random, IRandom
	{
		// Period parameters
		private const short _n = 624;
		private const short _m = 397;

		// constant vector a
		private const uint _matrixA = 0x9908b0df;
		// most significant w-r bits
		private const uint _upperMask = 0x80000000;
		// least significant r bits
		private const uint _lowerMask = 0x7fffffff;

		// the array for the state vector
		private uint[] _mt = Array.Empty<uint>();
		// mti==N+1 means mt[N] is not initialized
		private ushort _mti;
		private uint[] _mag01 = Array.Empty<uint>();

		#region "Constructor"

		public MersenneTwister19937()
		{
			PreInitialization();

			// csprng init by Mitil. 2006/01/04

			uint[] seedKey = new uint[6];
			byte[] rngCryptoBytes = new byte[8];

			seedKey[0] = (uint)DateTime.Now.Millisecond;
			seedKey[1] = (uint)DateTime.Now.Second;
			seedKey[2] = (uint)DateTime.Now.DayOfYear;
			seedKey[3] = (uint)DateTime.Now.Year;

			RandomNumberGenerator rn = new RNGCryptoServiceProvider();
			rn.GetNonZeroBytes(rngCryptoBytes);

			seedKey[4] = ((uint)rngCryptoBytes[0] << 24) | ((uint)rngCryptoBytes[1] << 16)
				| ((uint)rngCryptoBytes[2] << 8) | ((uint)rngCryptoBytes[3]);
			seedKey[5] = ((uint)rngCryptoBytes[4] << 24) | ((uint)rngCryptoBytes[5] << 16)
				| ((uint)rngCryptoBytes[6] << 8) | ((uint)rngCryptoBytes[7]);

			InitializeByArray(seedKey);
		}

		public MersenneTwister19937(uint s)
		{
			PreInitialization();
			InitializeBySeed(s);
		}

		public MersenneTwister19937(uint[] init_key)
		{
			PreInitialization();
			InitializeByArray(init_key);
		}

		private void PreInitialization()
		{
			_mt = new uint[_n];

			_mag01 = new uint[] { 0, _matrixA };
			/* mag01[x] = x * MATRIX_A  for x=0,1 */

			_mti = _n + 1;
		}

		#endregion

		// initializes mt[N] with a seed
		private void InitializeBySeed(uint s)
		{
			_mt[0] = s;

			for (_mti = 1; _mti < _n; _mti++)
			{
				_mt[_mti] =
					(((uint)1812433253 * (_mt[_mti - 1] ^ (_mt[_mti - 1] >> 30))) + _mti);
				// See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier.
				// In the previous versions, MSBs of the seed affect
				// only MSBs of the array mt[].
				// 2002/01/09 modified by Makoto Matsumoto
			}
		}

		// initialize by an array with array-length
		// initKey is the array for initializing keys
		// key_length is its length
		// slight change for C++, 2004/2/26
		private void InitializeByArray(uint[] initKey)
		{
			uint i, j;
			int k;
			int keyLength = initKey.Length;

			InitializeBySeed(19650218);
			i = 1;
			j = 0;
			k = (_n > keyLength ? _n : keyLength);

			for (; k > 0; k--)
			{
				_mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 30)) * (uint)1664525))
					+ initKey[j] + (uint)j; /* non linear */
				i++;
				j++;
				if (i >= _n)
				{ _mt[0] = _mt[_n - 1]; i = 1; }
				if (j >= keyLength)
				{
					j = 0;
				}
			}
			for (k = _n - 1; k > 0; k--)
			{
				_mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 30)) * (uint)1566083941))
					- (uint)i; /* non linear */
				i++;
				if (i >= _n)
				{ _mt[0] = _mt[_n - 1]; i = 1; }
			}

			_mt[0] = 0x80000000; /* MSB is 1; assuring non-zero initial array */
		}

		#region Generation

		// generates a random number on [0,0xffffffff]-Interval
		internal uint GenerateInt32()
		{
			uint y;

			if (_mti >= _n)
			{ /* generate N words at one time */
				short kk;

				if (_mti == _n + 1)   /* if InitializeBySeed() has not been called, */
				{
					InitializeBySeed(5489); /* a default initial seed is used */
				}

				for (kk = 0; kk < _n - _m; kk++)
				{
					y = ((_mt[kk] & _upperMask) | (_mt[kk + 1] & _lowerMask)) >> 1;
					_mt[kk] = _mt[kk + _m] ^ _mag01[_mt[kk + 1] & 1] ^ y;
				}
				for (; kk < _n - 1; kk++)
				{
					y = ((_mt[kk] & _upperMask) | (_mt[kk + 1] & _lowerMask)) >> 1;
					_mt[kk] = _mt[kk + (_m - _n)] ^ _mag01[_mt[kk + 1] & 1] ^ y;
				}
				y = ((_mt[_n - 1] & _upperMask) | (_mt[0] & _lowerMask)) >> 1;
				_mt[_n - 1] = _mt[_m - 1] ^ _mag01[_mt[0] & 1] ^ y;

				_mti = 0;
			}

			y = _mt[_mti++];

			/* Tempering */
			const int temperingShiftA = 11;
			const int temperingShiftB = 7;
			const int temperingShiftC = 15;
			const int temperingShiftD = 18;

			const uint temperingMaskB = 0x9d2c5680;
			const uint temperingMaskC = 0xefc60000;

			y ^= (y >> temperingShiftA);
			y ^= (y << temperingShiftB) & temperingMaskB;
			y ^= (y << temperingShiftC) & temperingMaskC;
			y ^= (y >> temperingShiftD);

			return y;
		}

		/* generates a random number on [0,0x7fffffff]-Interval */
		internal uint GenerateInt31() => (GenerateInt32() >> 1);

		#endregion

		#region Double Generation

		/* generates a random number on [0,1]-real-Interval */
		internal double GenerateDoubleExclusive() => GenerateInt32() * ((double)1.0 / 4294967295.0);/* divided by 2^32-1 */

		/* generates a random number on [0,1)-real-Interval */
		internal double GenerateDoubleIncludeTop() => GenerateInt32() * ((double)1.0 / 4294967296.0);/* divided by 2^32 */

		/* generates a random number on (0,1)-real-Interval */
		internal double GenerateDouble() => (((double)GenerateInt32()) + 0.5) * ((double)1.0 / 4294967296.0);/* divided by 2^32 */

		/* generates a random number on [0,1) with 53-bit resolution*/
		internal double Generate53BitResult()
		{
			uint a = GenerateInt32() >> 5, b = GenerateInt32() >> 6;
			return (((double)a * 67108864.0) + b) * ((double)1.0 / 9007199254740992.0);
		}

		/* These real versions are due to Isaku Wada, 2002/01/09 added */

		#endregion

		#region System.Random overrides

		public override int Next() => (int)GenerateInt32();

		public override int Next(int maxValue) => (int)(Sample() * maxValue);

		public override int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(minValue), $"{nameof(minValue)} must be smaller than {nameof(maxValue)}");
			}

			int difference = maxValue - minValue;
			return (int)(Sample() * difference) + minValue;
		}

		public override void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer), $"{nameof(buffer)} must not be null.");
			}

			for (int scan = 0; scan < buffer.Length; ++scan)
			{
				buffer[scan] = (byte)Next(byte.MaxValue);
			}
		}

		public override void NextBytes(Span<byte> buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer), $"{nameof(buffer)} must not be null.");
			}

			for (int scan = 0; scan < buffer.Length; ++scan)
			{
				buffer[scan] = (byte)Next(byte.MaxValue);
			}
		}

		public override double NextDouble() => Sample();

		protected override double Sample() => GenerateDoubleIncludeTop();

		#endregion System.Random overrides

		#region IRandom

		public RandomState Save()
		{
			return new()
			{
				NumberGenerated = _mti,
				Seed = _mt.Select(u => (int)u).ToArray(),
			};
		}

		public void Restore(RandomState state)
		{
			PreInitialization();

			if (state.Seed.Length != _mt.Length)
			{
				throw new InvalidOperationException($"{nameof(MersenneTwister19937)} was restored with a bad state. The size of the seed vector did not match what was expected.");
			}

			for (int scan = 0; scan < _mt.Length; ++scan)
			{
				_mt[scan] = (uint)state.Seed[scan];
			}

			_mti = (ushort)state.NumberGenerated;
		}

		#endregion IRandom

	}
}
