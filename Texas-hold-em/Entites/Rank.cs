using System;
using System.Collections.Generic;
using System.Text;
using Texas_hold_em.Helpers;

namespace Texas_hold_em.Entities
{
    public enum Rank
    {
        [EnumName("2")]
        two = 2,
        [EnumName("3")]
        three = 3,
        [EnumName("4")]
        four = 4,
        [EnumName("5")]
        five = 5,
        [EnumName("6")]
        six = 6,
        [EnumName("7")]
        seven = 7,
        [EnumName("8")]
        eight = 8,
        [EnumName("9")]
        nine = 9,
        [EnumName("T")]
        T = 10,
        [EnumName("J")]
        J = 11,
        [EnumName("Q")]
        Q = 12,
        [EnumName("K")]
        K = 13,
        [EnumName("A")]
        A = 14
    }
}
