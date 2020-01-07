using System.Collections.Generic;
using Texas_hold_em.Helpers;

namespace Texas_hold_em.Entities
{
    public class Board
    {
        public static readonly int CARDS_AMOUNT = 5;
        public List<Card> Cards { get; set; }

        public Board(string boardCards)
        {
            Cards = CardsConverter.StringToList(boardCards, CARDS_AMOUNT);
        }
    }
}
