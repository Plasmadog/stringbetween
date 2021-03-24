// ReSharper disable StringLiteralTypo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringBetween
{
    public sealed class Interpolator
    {
        private readonly IList<char> _alphabet;
        private readonly Dictionary<char, int> _index;
        private readonly char _firstCharInAlphabet;
        private readonly char _lastCharInAlphabet;

        private Interpolator(string alphabet)
        {
            _alphabet = alphabet.ToList();
            _index = alphabet.ToDictionary(c => c, c => _alphabet.IndexOf(c));
            _firstCharInAlphabet = _alphabet[0];
            _lastCharInAlphabet = alphabet[_alphabet.Count - 1];
        }


        public string GetStringBetween(string prev, string next)
        {
            if (prev == null)
                throw new ArgumentNullException(nameof(prev));

            if (next == null)
                throw new ArgumentNullException(nameof(next));

            var sb = new StringBuilder(FindCommonBeginning(prev, next, out var p, out var n, out var pos));

            if (p == -1)
            {
                while (n == 0)
                {
                    n = pos < next.Length ? _index[next[pos++]] : _alphabet.Count;
                    sb.Append(_firstCharInAlphabet);
                }

                if (n == 1)
                {
                    sb.Append(_firstCharInAlphabet);
                    n = _alphabet.Count;
                }
            }
            else if (p + 1 == n)
            {
                sb.Append(_alphabet[p]);
                n = _alphabet.Count;
                while ((p = pos < prev.Length ? _index[prev[pos++]] : -1) == _alphabet.Count - 1)
                {
                    sb.Append(_lastCharInAlphabet);
                }
            }

            var middleCharCode = (p + n) >> 1;
            sb.Append(_alphabet[middleCharCode]);

            return sb.ToString();
        }


        private string FindCommonBeginning(string prev, string next, out int p, out int n, out int pos)
        {
            var i = 0;
            while (i < prev.Length && i < next.Length && prev[i] == next[i])
                i++;

            pos = i + 1;
            p = i < prev.Length ? _index[prev[i]] : -1;
            n = i < next.Length ? _index[next[i]] : _alphabet.Count;

            return prev.Substring(0, i);
        }


        public static Interpolator SingleCaseAlpha = new Interpolator("abcdefghijklmnopqrstuvwxyz");
                      
        public static Interpolator DoubleCaseAlpha = new Interpolator("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
                      
        public static Interpolator SingleCaseAlphaNumeric = new Interpolator("0123456789abcdefghijklmnopqrstuvwxyz");
                      
        public static Interpolator DoubleCaseAlphaNumeric = new Interpolator("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

    }
}