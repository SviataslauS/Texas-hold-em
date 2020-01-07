using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Texas_hold_em.Helpers
{
    public static class EnumNameParser
    {
        public static T ParseString<T>(string enumString) where T : struct
        {
            var members = typeof(T).GetMembers()
                                                .Where(x => x.GetCustomAttributes(typeof(EnumNameAttribute), false).Length > 0)
                                                .Select(x =>
                                                    new
                                                    {
                                                        Member = x,
                                                        Attribute = x.GetCustomAttributes(typeof(EnumNameAttribute), false)[0] as EnumNameAttribute
                                                    });

            foreach (var item in members)
            {
                if (item.Attribute.Name.Equals(enumString, StringComparison.InvariantCultureIgnoreCase))
                    return (T)Enum.Parse(typeof(T), item.Member.Name);
            }

            throw new Exception("Enum member " + enumString + " was not found.");
        }
    }
}
