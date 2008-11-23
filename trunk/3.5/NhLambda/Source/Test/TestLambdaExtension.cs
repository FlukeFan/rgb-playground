
using System;

using NHibernate.Criterion; 

using NUnit.Framework;

namespace NHibernate.LambdaExpressions.Test
{

    [TestFixture]
    public class TestLambdaExtension
    {

        [Test]
        public void TestSimpleEq()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Name", "test name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add<Person>(p => p.Name == "test name");

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

    }

}
