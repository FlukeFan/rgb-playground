
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace NHibernate.LambdaExpressions
{

    public static class DetachedCriteriaExtension
    {

        public static DetachedCriteria Add<T>(this DetachedCriteria criteria, Expression<Func<T, bool>> e)
        {
            BinaryExpression be = (BinaryExpression)e.Body;
            MemberExpression me = (MemberExpression)be.Left;
            ConstantExpression ce = (ConstantExpression)be.Right;
            criteria.Add(NHibernate.Criterion.Expression.Eq(me.Member.Name, ce.Value));
            return criteria;
        }

    }

}

