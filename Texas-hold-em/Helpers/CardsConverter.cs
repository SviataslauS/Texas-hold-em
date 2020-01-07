using System;
using System.Collections.Generic;
using System.Text;
using Texas_hold_em.Entities;

namespace Texas_hold_em.Helpers
{
    class CardsConverter
    {
        public static List<Card> StringToList(string encodedCards, int cardsAmount)
        {
            if(encodedCards.Length != cardsAmount*2)
            {
                throw new Exception("Each card should be encoded as 2 characters");
            }

            List<Card> cards = new List<Card>(cardsAmount);
            var maxIndex = (cardsAmount * 2) - 1;
            for (int index = 0; index <= maxIndex; index = index + 2)
            {
                cards.Add(new Card(encodedCards.Substring(index, 2)));
            }

            return cards;
        }
    }
}
