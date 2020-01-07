using System;
using Texas_hold_em.Entities;

namespace Texas_hold_em.Helpers
{
    public class WithAlphComparison : Hand, IComparable<WithAlphComparison>
    {
        public Hand ComparableHand { get; set; }
        public Hand EqualHand { get; set; }
        public int? OriginalComparisonWithNextResult { get; set; }

        public WithAlphComparison(Hand hand)
        {
            ComparableHand = hand;
        }

        public int CompareTo(WithAlphComparison other)
        {
            OriginalComparisonWithNextResult = ComparableHand.CompareTo(other.ComparableHand);
            int res;
            if(OriginalComparisonWithNextResult != null && OriginalComparisonWithNextResult == 0)
            {
                EqualHand = other.ComparableHand;
                res = string.Compare(this.ComparableHand.EncodedCards, other.ComparableHand.EncodedCards);
            }
            else
            {
                res = (int)OriginalComparisonWithNextResult;
            }

            return res;
        }
    }
}
