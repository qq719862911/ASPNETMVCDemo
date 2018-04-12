using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace MVC2015.Utility.Common
{
    public static class IQueryableSortHelper
    {
        public static IQueryable<T> SortWith<T>(this IQueryable<T> query, string sort, string dir = "ASC") where T : class, new()
        {



            //The sort field can't be null

            if (string.IsNullOrWhiteSpace(sort))
            {
                throw new ArgumentNullException("SortField value can not be null or empty string. Please set the SortField value.");
            }

            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = "ASC";
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "it");
            Expression body = param;

            if (Nullable.GetUnderlyingType(body.Type) != null)
            {
                body = Expression.Property(body, "Value");
            }

            //Get sort type.
            PropertyInfo sortProperty = typeof(T).GetProperty(sort, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (sortProperty == null)
            {
                throw new Exception(string.Format("The {0} is not exsited.", sortProperty));
            }

            //Get sort lambda(Function(it) it.sort).
            //body = Expression.MakeMemberAccess(body, sortProperty);
            body = Expression.MakeMemberAccess(body, sortProperty);
            LambdaExpression keySelectorLambda = Expression.Lambda(body, param);

            //Get sort direction method
            string queryMethod = dir == "DESC" ? "OrderByDescending" : "OrderBy";

            //Get sort direction lambda
            query = query.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), queryMethod, new System.Type[] {
                                                    typeof(T),
                                                    body.Type
                                                    }, query.Expression, Expression.Quote(keySelectorLambda)));
            return query;
        }
    }
}
