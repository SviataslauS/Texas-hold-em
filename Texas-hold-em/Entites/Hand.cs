using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Texas_hold_em.Helpers;

namespace Texas_hold_em.Entities
{
    public class Hand : IComparable<Hand>
    {
        private static readonly int CARDS_AMOUNT = 2;
        private static readonly int ALL_CARDS_AMOUNT = CARDS_AMOUNT + Board.CARDS_AMOUNT;
        private static readonly int HIGHEST_RANK = (int)Rank.A;
        private static readonly int LOWEST_RANK = (int)Rank.two;
        public int[] Value { get; set; } = new int[6];
        private List<Card> Cards { get; set; }
        public string EncodedCards { get; set; }

        public Hand(string handCards, List<Card> boardCards)
        {
            EncodedCards = handCards;

            Cards = CardsConverter.StringToList(handCards, CARDS_AMOUNT);
            Cards.AddRange(boardCards);

            var ranksAmounts = new int[HIGHEST_RANK + 1];
            for (int i = 0; i < ALL_CARDS_AMOUNT; i++)
            {
                ranksAmounts[(int)Cards[i].Rank]++;
            }

            var sameRankCountGr1 = 1;
            var sameRankCountGr2 = 1;
            var smallGroupRank = 0;
            var largeGroupRank = 0;
            for (int rank = HIGHEST_RANK; rank >= LOWEST_RANK; rank--)
            {
                if (ranksAmounts[rank] > sameRankCountGr1)
                {
                    if (sameRankCountGr1 != 1)
                    {
                        sameRankCountGr2 = sameRankCountGr1;
                        smallGroupRank = largeGroupRank;
                    }

                    sameRankCountGr1 = ranksAmounts[rank];
                    largeGroupRank = rank;

                }
                else if (ranksAmounts[rank] > sameRankCountGr2)
                {
                    sameRankCountGr2 = ranksAmounts[rank];
                    smallGroupRank = rank;
                }
            }

            var (flush, topFlushRanks) = FindFlush();
            var (straight, topStraightValue) = FindStraight(ranksAmounts);
            int[] orderedRanks = OrderRanksByDesc(ranksAmounts);

            FillHandValue(sameRankCountGr1, 
                sameRankCountGr2, 
                smallGroupRank,
                largeGroupRank,
                flush,
                topFlushRanks,
                straight,
                topStraightValue,
                orderedRanks);

        }

        public Hand()
        {
        }

        private void FillHandValue(int sameRankCountGr1, int sameRankCountGr2, int smallGroupRank,
            int largeGroupRank, bool flush, List<int> topFlushRanks, bool straight, int topStraightValue, int[] orderedRanks)
        {
            if (straight && flush && topStraightValue == topFlushRanks[0])  //straight flush
            {
                Value[0] = 9;
                Value[1] = topStraightValue;
                Value[2] = topFlushRanks[1];
                Value[3] = topFlushRanks[2];
                Value[4] = topFlushRanks[3];
                Value[5] = topFlushRanks[4];
                return;
            }

            if (sameRankCountGr1 == 4)  //four of a kind
            {
                Value[0] = 8;
                Value[1] = largeGroupRank;
                Value[2] = orderedRanks[0];
                return;
            }

            if (sameRankCountGr1 == 3 && sameRankCountGr2 == 2)  //full house
            {
                Value[0] = 7;
                Value[1] = largeGroupRank;
                Value[2] = smallGroupRank;
                return;
            }

            if (flush)
            {
                Value[0] = 6;
                Value[1] = topFlushRanks[0];
                Value[2] = topFlushRanks[1];
                Value[3] = topFlushRanks[2];
                Value[4] = topFlushRanks[3];
                Value[5] = topFlushRanks[4];
                return;
            }

            if (straight)
            {
                Value[0] = 5;
                Value[1] = topStraightValue;
                return;
            }

            if (sameRankCountGr1 == 3 && sameRankCountGr2 != 2) //three of a kind
            {
                Value[0] = 4;
                Value[1] = largeGroupRank;
                Value[2] = orderedRanks[0];
                Value[3] = orderedRanks[1];
                return;
            }

            if (sameRankCountGr1 == 2 && sameRankCountGr2 == 2) //two pair
            {
                Value[0] = 3;
                Value[1] = largeGroupRank > smallGroupRank ? largeGroupRank : smallGroupRank;
                Value[2] = largeGroupRank < smallGroupRank ? largeGroupRank : smallGroupRank;
                Value[3] = orderedRanks[0];
            }

            if (sameRankCountGr1 == 2 && sameRankCountGr2 == 1) //pair
            {
                Value[0] = 2;
                Value[1] = largeGroupRank;
                Value[2] = orderedRanks[0];
                Value[3] = orderedRanks[1];
                Value[4] = orderedRanks[2];
                return;
            }

            if (sameRankCountGr1 == 1) //high card
            {
                Value[0] = 1;
                Value[1] = orderedRanks[0];
                Value[2] = orderedRanks[1];
                Value[3] = orderedRanks[2];
                Value[4] = orderedRanks[3];
                Value[5] = orderedRanks[4];
                return;
            }
        }

        private static int[] OrderRanksByDesc(int[] ranksAmounts)
        {
            int[] orderedRanks = new int[ALL_CARDS_AMOUNT];
            int index = 0;
            for (int rank = HIGHEST_RANK; rank >= LOWEST_RANK; rank--)
            {
                if (ranksAmounts[rank] == 1)
                {
                    orderedRanks[index] = rank;
                    index++;
                }
            }

            return orderedRanks;
        }

        private static (bool, int) FindStraight(int[] ranksAmounts)
        {
            int topStraightValue = 0;
            bool straight = false;
            var limitSuperior = HIGHEST_RANK - 4;
            for (int rank = LOWEST_RANK; rank <= limitSuperior; rank++)
            {
                if (ranksAmounts[rank] > 0 && ranksAmounts[rank + 1] > 0 && ranksAmounts[rank + 2] > 0 &&
                    ranksAmounts[rank + 3] > 0 && ranksAmounts[rank + 4] > 0)
                {
                    straight = true;
                    topStraightValue = rank + 4;
                    break;
                }
            }
            if (!straight && ranksAmounts[14] > 0 && ranksAmounts[2] > 0 && ranksAmounts[3] > 0 &&
                ranksAmounts[4] > 0 && ranksAmounts[5] > 0)
            {
                straight = true;
                topStraightValue = 5;
            }

            return (straight, topStraightValue);
        }

        private (bool, List<int>) FindFlush()
        {
            var suitsAmounts = new int[4];
            for (int i = 0; i < ALL_CARDS_AMOUNT; i++)
            {
                suitsAmounts[(int)Cards[i].Suit]++;
            }
            int? flushSuit = null;
            for (int i = 0; i < 4; i++)
            {
                if (suitsAmounts[i] == 5)
                {
                    flushSuit = i;
                    break;
                }
            }
            var flush = flushSuit != null;
            var topFlushRanks = new List<int>(5);
            if (flush)
            {
                foreach (var card in Cards.Where(c => c.Suit == (Suit)flushSuit))
                {
                    topFlushRanks.Add((int)card.Rank);
                }
                topFlushRanks = topFlushRanks.OrderByDescending(r => r).ToList();
            }

            return (flush, topFlushRanks);
        }
        
        public static int Compare(Hand frst, Hand scnd)
        {
            for (int x = 0; x < 6; x++)
            {
                if (frst.Value[x] > scnd.Value[x])
                    return 1;
                else if (frst.Value[x] != scnd.Value[x])
                    return -1;
            }

            return 0;
        }
        
        public int CompareTo(Hand other)
        {
            return Compare(this, other);
        }
    }
}
