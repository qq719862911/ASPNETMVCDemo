using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2015.Utility.Common;

namespace GSIMS.DataProvider
{
    public partial class BaseContext : DbContext
    {
        
        private const string SP_EXECUTE_SQL = "EXEC";

        /// <summary>
        ///  Exec a SP querey that will return elements of the given type.
        /// </summary>
        /// <typeparam name="T1">the return elements of the given type</typeparam>
        /// <typeparam name="T2">the SP type</typeparam>
        /// <param name="sp">the sp entity</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<TResult> InvokeStoredProcedure<TParam, TResult>(TParam sp) where TParam : SPBase
        {
            string spName = DBMethod.GetSPName<TParam>();
            SqlParameter[] paras = DBMethod.GetSPParameters(sp);
            List<TResult> list = Database.SqlQuery<TResult>(ParseSql(spName, paras), paras).ToList();
         
            return list;
        }

        public List<TResult> InvokeStoredProcedure<TParam, TResult>() where TParam : SPBase
        {
            string spName = DBMethod.GetSPName<TParam>();
           
            List<TResult> list = Database.SqlQuery<TResult>(ParseSql(spName, null)).ToList();
            return list;
        }

        /// <summary>
        /// Exec a SP without return
        /// </summary>
        /// <typeparam name="T">elements of the given type</typeparam>
        /// <param name="sp">the sp entity</param>
        /// <remarks></remarks>
        public void InvokeStoredProcedureNonQuery<T>(T sp) where T : SPBase
        {
            CheckSPArgument(sp);
            string spName = DBMethod.GetSPName<T>();
            SqlParameter[] paras = DBMethod.GetSPParameters(sp);
            this.Database.ExecuteSqlCommand(ParseSql(spName, paras), paras);
            //  set the return output value
            DBMethod.SetSPModelValueFormParameters(sp, paras);
        }

        /// <summary>
        /// Parse the Sql from sqlparamter
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private string ParseSql(string spName, SqlParameter[] paras)
        {
            int length = 1;
            if (paras != null)
            {
                length = paras.Length;
            }
            System.Text.StringBuilder sb = new StringBuilder(length);
            sb.Append(SP_EXECUTE_SQL);
            sb.Append(" ");
            sb.Append(spName);
            if (paras != null)
            {
                for (int i = 0; i <= paras.Length - 1; i++)
                {
                    SqlParameter para = paras[i];
                    string str = " ";
                    if (para.Direction == System.Data.ParameterDirection.Output || para.Direction == System.Data.ParameterDirection.InputOutput)
                    {
                        if (i != 0)
                        {
                            str += "," + "@" + para.ParameterName + " " + para.Direction.ToString();
                        }
                        else
                        {
                            str += ("@" + para.ParameterName + " ") + " " + para.Direction.ToString();
                        }
                    }
                    else
                    {
                        if (i != 0)
                        {
                            str += ("," + "@") + para.ParameterName;
                        }
                        else
                        {
                            str += "@" + para.ParameterName;
                        }
                    }

                    sb.Append(str);
                }
            }
            return sb.ToString();
        }


        private void CheckSPArgument(SPBase sp)
        {
            if (sp == null)
            {
                throw new ArgumentNullException("sp", "The sp model class can not be null.");
            }
        }

    }
}
