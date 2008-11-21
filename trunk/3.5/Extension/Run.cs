
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

            DetachedCriteria criteria1 =
                DetachedCriteria.For<Person>()
                .Add(NHibernate.Criterion.Expression.Eq("Name", "test1"));

            DetachedCriteria criteria2 =
                DetachedCriteria.For<Person>()
                .Add((Person p) => p.Name == "test2");

            DetachedCriteria criteria3 =
                DetachedCriteria.For<Person>()
                .Add<Person>(p => p.Name == "test3");

            Console.WriteLine(criteria1);
            Console.WriteLine(criteria2);
            Console.WriteLine(criteria3);
        }

    }

}

