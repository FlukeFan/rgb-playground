
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace NHibernate.LambdaExpressions
{

    /// <summary>
    /// Extension methods for NHibernate DetachedCriteria class
    /// </summary>
    public static class DetachedCriteriaExtension
    {

        /// <summary>
        /// Add criterion expressed as a lambda expression
        /// </summary>
        /// <typeparam name="T">Type (same as DetachedCriteria type)</typeparam>
        /// <param name="criteria">DetachedCriteria instance</param>
        /// <param name="e">Lambda expression</param>
        /// <returns>DetachedCriteria instance</returns>
        public static DetachedCriteria Add<T>(this DetachedCriteria criteria, Expression<Func<T, bool>> e)
        {
            BinaryExpression be = (BinaryExpression)e.Body;
            MemberExpression me = (MemberExpression)be.Left;

            var valueExpression = Expression<Func<object>>.Lambda<Func<object>>(be.Right).Compile();
            var value = valueExpression.Invoke();

            criteria.Add(NHibernate.Criterion.Expression.Eq(me.Member.Name, value));
            return criteria;
        }

    }

}

