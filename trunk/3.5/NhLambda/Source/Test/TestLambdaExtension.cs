
using System;

using NHibernate.Criterion; 

using NUnit.Framework;

namespace NHibernate.LambdaExpressions.Test
{

    [TestFixture]
    public class TestLambdaExtension
    {

        private void AssertCriteriaAreEqual(DetachedCriteria expected, DetachedCriteria actual)
        {
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void TestSimpleEq()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Name", "test name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add<Person>(p => p.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSimpleEqWithMemberExpression()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Name", "test name"));

            string name = "test name";
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add<Person>(p => p.Name == name);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSimpleGt()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Gt("Age", 10));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add<Person>(p => p.Age > 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestOrderAsc()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .AddOrder(Order.Desc("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .AddOrder<Person>(p => p.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
