using System;
using System.Collections.Generic;
using System.Text;

namespace Texas_hold_em.Helpers 
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumNameAttribute : Attribute
    {
        readonly string name;

        public EnumNameAttribute(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
