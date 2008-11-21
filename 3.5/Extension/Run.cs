
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace Demo
{

    public class Person
    {
        public string Name { get; protected set; }
    }

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

    public class Demo
    {

        /*public static void Test<T>(Func<T, bool> func)
        {
            Console.WriteLine(func);
        }*/

        public static void Test<T>(Expression<Func<T, bool>> e)
        {
            BinaryExpression be = (BinaryExpression)e.Body;
            ConstantExpression ce = (ConstantExpression)be.Right;
            Console.WriteLine(ce.Value.GetType());
        }

        public static void PropertyOf<T>(Expression<Func<T, object>> e)
        {
        }

        [STAThread]
        public static void Main()
        {
            Test((Person p) => p.Name == "test");
            DetachedCriteria criteria =
                DetachedCriteria.For<Person>();
            //criteria.Add(NHibernate.Criterion.Expression.Eq("Name", "test"));
            criteria.Add((Person p) => p.Name == "test");
            Console.WriteLine(criteria);
        }

    }

}

