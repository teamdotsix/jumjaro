using System;
using System.Collections.Generic;
using System.Text;

namespace Jumjaize
{
    public class HangulMask
    {
        private readonly char _onset;
        private readonly char _nucleus;
        private readonly char _coda;

        public bool HasOnset => _onset != default(char);
        public bool HasNucleus=> _onset != default(char);
        public bool HasCoda => _onset != default(char);

        public HangulMask(char onset = default, char nucleus = default, char coda = default)
        {
            _onset = onset;
            _nucleus = nucleus;
            _coda = coda;
        }

        public static bool operator &(HangulMask mask, char hangul)
        {
            var syllables = new Hangul().Syllabification(hangul);
            var onset = syllables[0];
            var nucleus = syllables[1];
            var coda = syllables[2];

            bool masked = true;
            if(mask._onset != default(char))
            {
                masked &= mask._onset == onset;
            }
            if(mask._nucleus != default(char))
            {
                masked &= mask._nucleus == nucleus;
            }
            if(mask._coda != default(char))
            {
                masked &= mask._coda == coda;
            }
            return masked;
        }

    }
}
