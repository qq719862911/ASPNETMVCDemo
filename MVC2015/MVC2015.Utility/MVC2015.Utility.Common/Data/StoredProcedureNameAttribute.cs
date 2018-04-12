using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC2015.Utility.Common
{
    /// <summary>
    /// This Attribute is to define the Stored Procuredure Model
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class StoredProcedureNameAttribute : Attribute
    {
        /// <summary>
        /// The Stored Procedure Name on database.
        /// </summary>
        public string Name { get; set; }

        public StoredProcedureNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
