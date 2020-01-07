using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Texas_hold_em.Entities;
using Texas_hold_em.Helpers;

namespace Texas_hold_em
{
    public class TexasHoldEmService
    {
        public static string Evaluate(string boardAndHandsCards)
        {
            List<string> splitedCardSets = VerifyInput(boardAndHandsCards);

            var board = new Board(splitedCardSets[0]);
            splitedCardSets.RemoveAt(0);

            var hands = new List<Hand>(splitedCardSets.Count);
            foreach (var encodedHand in splitedCardSets)
            {
                hands.Add(new Hand(encodedHand, board.Cards));
            }

            var groupedHandsByValue = hands.GroupBy(h => h.Value[0]).OrderBy(h => h.Key);
            var sb = new StringBuilder();
            foreach(var sameValHands in groupedHandsByValue)
            {
                List<WithAlphComparison> sortedHands = sameValHands.Select(hand => new WithAlphComparison(hand)).ToList();
                sortedHands.Sort();
                var isThereSameCombinationValue = sortedHands.Count() > 1;
                for (var i = 0;i < sortedHands.Count(); i++)
                {
                    var hand = sortedHands[i];
                    var separator = " ";
                    if(isThereSameCombinationValue && hand.OriginalComparisonWithNextResult == 0)
                    {
                        var nextHandIndex = i + 1;
                        if(sortedHands.Count() > nextHandIndex)
                        {
                            if (hand.EqualHand.EncodedCards == sortedHands[nextHandIndex].ComparableHand.EncodedCards)
                            {
                                separator = "=";
                            } 
                        }

                        var prevHandIndex = i - 1;
                        if (prevHandIndex >= 0)
                        {
                            if (hand.EqualHand.EncodedCards == sortedHands[prevHandIndex].ComparableHand.EncodedCards)
                            {
                                sb.Length--;
                                sb.Append("=");
                            }
                        }
                    }

                    sb.Append(hand.ComparableHand.EncodedCards);
                    sb.Append(separator);
                }
            }

            return sb.ToString().TrimEnd(' ');
        }

        private static List<string> VerifyInput(string boardAndHandsCards)
        {
            var validStr = boardAndHandsCards;
            do
            {
                validStr = boardAndHandsCards.Replace("  ", " ");
            }
            while (validStr.Length < boardAndHandsCards.Length);

            var splitedInput = validStr.Split(' ').ToList();
            return splitedInput;
        }
    }
}
