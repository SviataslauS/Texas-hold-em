using System;
using System.Collections.Generic;
using System.Text;
using Texas_hold_em.Helpers;

namespace Texas_hold_em.Entities
{
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }

        public Card(string card)
        {
            if(card.Length> 2)
            {
                throw new Exception("Card should be passed as a string with 2 characters");
            }

            Rank = EnumNameParser.ParseString<Rank>(card.Substring(0,1));
            Suit = EnumNameParser.ParseString<Suit>(card.Substring(1, 1));
        }
    }
}
