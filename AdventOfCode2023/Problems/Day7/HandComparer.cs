using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Problems.Day7
{
    public class HandComparer : Comparer<Hand>
    {
        public override int Compare(Hand x, Hand y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException();
            }

            if (x.Type != y.Type)
            {
                return x.Type.CompareTo(y.Type);
            }

            for (var i = 0; i < 4; i++)
            {
                if (x.Cards[i] != y.Cards[i])
                {
                    return x.Cards[i].CompareTo(y.Cards[i]);
                }
            }

            return x.Cards[4].CompareTo(y.Cards[4]);
        }
    }
}