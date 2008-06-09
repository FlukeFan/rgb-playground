using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using NHibernate.Criterion;
using Atlanta.Application.Domain.DomainBase;

namespace Sl2
{
    public class Page : UserControl
    {

        public Page()
        {
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            //DetachedCriteria dc = DetachedCriteria.For<Page>();
            ICriterion exp = Expression.Eq("MyProperty", "MyValue");
            Order ord = Order.Asc("MyOrder");
            //FilterCondition fc = FilterCondition.Equal;

            string output = "Hello world 4 " + exp.ToString() + ", " + ord.ToString();
            HtmlPage.Window.Alert(output);
        }

    }
}