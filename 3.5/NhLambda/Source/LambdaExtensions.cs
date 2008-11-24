
using System;
using System.Collections.Generic;
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

        private readonly static IDictionary<ExpressionType, Func<string, object, ICriterion>> _simpleExpressionCreators = null;

        static DetachedCriteriaExtension()
        {
            _simpleExpressionCreators = new Dictionary<ExpressionType, Func<string, object, ICriterion>>();
            _simpleExpressionCreators[ExpressionType.Equal] = Eq;
            _simpleExpressionCreators[ExpressionType.GreaterThan] = Gt;
        }

        private static ICriterion Eq(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Eq(propertyName, value);
        }
        
        private static ICriterion Gt(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Gt(propertyName, value);
        }
        
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

            var valueExpression = System.Linq.Expressions.Expression.Lambda(be.Right).Compile();
            var value = valueExpression.DynamicInvoke();

            Func<string, object, ICriterion> simpleExpressionCreator = _simpleExpressionCreators[be.NodeType];
            ICriterion criterion = simpleExpressionCreator(me.Member.Name, value);
            criteria.Add(criterion);
            return criteria;
        }

    }

}

