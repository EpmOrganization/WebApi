using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Framework.ExpressionTree
{
    public class ExpressionHelper
    {
        public static Func<T, Tkey> DynamicLambda<T, Tkey>(string propertyName)
        {

            ParameterExpression p = Expression.Parameter(typeof(T), "p");
            Expression body = Expression.Property(p, typeof(T).GetProperty(propertyName));

            var lambda = Expression.Lambda<Func<T, Tkey>>(body, p);

            return lambda.Compile();
        }
    }
}
