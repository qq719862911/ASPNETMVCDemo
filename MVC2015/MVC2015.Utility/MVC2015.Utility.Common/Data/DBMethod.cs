using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using System.Globalization;

namespace MVC2015.Utility.Common
{
    /// <summary>
    /// Common DB Help Method
    /// </summary>
    public class DBMethod
    {
        /// <summary>
        /// Get the StoredProcureAttribute Defined SP Name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetSPName<T>()
        {
            //Get the entity Data Type
            var entityType = typeof(T);
            var attrs = entityType.GetCustomAttributes(typeof(StoredProcedureNameAttribute), false);
            if (attrs == null || attrs.Length == 0)
            {
                throw new TypeLoadException("The type \"" + entityType.FullName + "\", it not have the \"StoredProcedureAttribute\", please add it.");
            }

            var attr = attrs[0] as StoredProcedureNameAttribute;
            if (attr == null || String.IsNullOrEmpty(attr.Name))
            {
                throw new ArgumentNullException("The \"Name\" property in \"StoredProcedureAttribute\" (the attribute defined in class) must assign a value.");
            }
            return attr.Name;
        }

        /// <summary>
        /// Get the Parameters from the Model Entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSPParameters<T>(T entity)
        {
            //Get the entity Data Type
            var entityType = entity.GetType();
            var paraList = new List<SqlParameter>();
            SqlParameter para = null;
            //use the type to get the PropertyInfoCollection,then for each it.
            foreach (PropertyInfo pInfo in entityType.GetProperties())
            {
                para = new SqlParameter();
                //in each property, find the DataFieldAttribute, then can get the FieldName and IsAutomatic information.
                ParameterAttribute attr = (ParameterAttribute)Attribute.GetCustomAttribute(pInfo, typeof(ParameterAttribute), false);
                if (attr != null)
                {
                    if (!String.IsNullOrEmpty(attr.Name))
                    {
                        para.ParameterName = attr.Name;
                    }
                    else
                    {
                        para.ParameterName = pInfo.Name;
                    }

                    if (!String.IsNullOrEmpty(attr.TypeName))
                    {
                        SqlDbType dbType;
                        if (Enum.TryParse<SqlDbType>(attr.TypeName, true, out dbType))
                        {
                            para.SqlDbType = dbType;
                        }
                    }

                    if (attr.Length > 0)
                    {
                        para.Size = attr.Length;
                    }
                    else
                    {
                        if (attr.Precision > 0) para.Precision = attr.Precision;
                        if (attr.Scale > 0) para.Scale = attr.Scale;
                    }
                    para.Direction = attr.Direction;
                }
                else
                {
                    para.ParameterName = pInfo.Name;
                }
                //Set the parameter value
                para.Value = GetParaValue(pInfo.GetValue(entity, null));

                paraList.Add(para);
            }
            return paraList.ToArray();

        }


         public static void SetSPModelValueFormParameters<T>(T entity, SqlParameter[] paras)
         {
            //Get the entity Data Type
            var entityType = entity.GetType();
            //use the type to get the PropertyInfoCollection,then for each it.
            foreach( PropertyInfo pInfo in entityType.GetProperties())
            {
                //in each property, find the DataFieldAttribute, then can get the FieldName and IsAutomatic information.
                var attr = (ParameterAttribute)Attribute.GetCustomAttribute(pInfo, typeof(ParameterAttribute), false);
                if( attr != null && (attr.Direction == ParameterDirection.Output || attr.Direction == ParameterDirection.InputOutput) )
                {
                    foreach( SqlParameter para in paras)
                    {
                        if( para.ParameterName == attr.Name || para.ParameterName.ToLowerInvariant() == pInfo.Name.ToLowerInvariant() )
                        {
                            var pType = CheckFieldValueType(para.Value, pInfo.PropertyType, pInfo.Name);
                            pInfo.SetValue(entity, GetFieldValue(para.Value, pType), null);

                            break;
                        }
                    }
                }
                else
                {
                    foreach( SqlParameter para in paras)
                    {
                        if( para.ParameterName == pInfo.Name) 
                        {
                            Type pType = CheckFieldValueType(para.Value, pInfo.PropertyType, pInfo.Name);
                            pInfo.SetValue(entity, GetFieldValue(para.Value, pType), null);

                            break;
                        }

                    }
                }
            }
         }

        /// <summary>
         /// Check the Field Value Type whether match the property's Type.
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="pType"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        private static Type CheckFieldValueType(object fieldValue, Type pType, string propName)
        {
            if (pType.IsGenericType)
            {
                pType = pType.GetGenericArguments()[0];
                return CheckFieldValueType(fieldValue, pType, propName);
            }
            else
            {
                if( Object.ReferenceEquals(fieldValue.GetType(), pType) )
                {
                    return pType;
                }
                else if( Type.GetTypeCode(fieldValue.GetType()) == TypeCode.DBNull )
                {
                    return typeof(DBNull);
                }
                else
                {
                    throw new InvalidCastException(String.Format("Can not convert filedValue type \"{0}\" to Property \"{1}\"'s Type \"{2}\".", fieldValue.GetType().Name, propName, pType.Name));
                }
            }
        }

        /// <summary>
        /// Get the Database table field's value, if the value is DBNull.Value , then return the null value
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        public static Object GetFieldValue(Object fieldValue, Type propType)
        {

            if( Convert.IsDBNull(fieldValue) )
            {
                return null;
            }
            else
            {
                return Convert.ChangeType(fieldValue, propType, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// This Method is to get the Data Type value to the SQL parameter.
        /// And this can change the Nothing value to the DBNull.Value
        /// </summary>
        /// <param name="paraValue">Data Type Value</param>
        /// <returns>SQL parameter value</returns>
        public static object GetParaValue(object paraValue)
        {
            if (paraValue == null)
            {
                return DBNull.Value;
            }
            return paraValue;
        }
    }
}
