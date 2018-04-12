using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MVC2015.Utility.Common
{
    /// <summary>
    /// This Attribute is to define the Stored Procuredure's Parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ParameterAttribute : Attribute
    {
        /// <summary>
        /// The Paremeter Name, that include the @ character
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Field DB Type, this type is the name in Database
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// DB Type Length
        /// </summary>
        /// <remarks>
        /// If the Type is (N)varchar(max), the length set to -1.
        /// If for Decimal/Numeric this field not work, Please use Precision and Scale.
        /// </remarks>
        public int Length { get; set; }

        /// <summary>
        /// The parameter Direction
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of digits used to represent the System.Data.SqlClient.SqlParameter.Value
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// Gets or sets the number of decimal places to which System.Data.SqlClient.SqlParameter.Value is resolved.
        /// </summary>
        public byte Scale { get; set; }

        public ParameterAttribute(string name)
        {
            this.Name = name;
            this.Direction = ParameterDirection.Input;
        }
        public ParameterAttribute(string name, ParameterDirection direction)
        {
            this.Name = name;
            this.Direction = direction;
        }
    }
}
