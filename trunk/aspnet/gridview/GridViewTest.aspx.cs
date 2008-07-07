
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples
{

    public class Test
    {

        public int Id;
        public string Prop;

        public Test(int id, string prop)
        {
            Id = id;
            Prop = prop;
        }

    }

    public class TestListMap
    {

        private GridView _gridView;

        public TestListMap(GridView gridView)
        {
            _gridView = gridView;

            ObjectDataSource dataSource = new ObjectDataSource(this.GetType().FullName, "RetrievePage");
            dataSource.ObjectCreating += new ObjectDataSourceObjectEventHandler(dataSource_ObjectCreating);
            dataSource.SelectCountMethod = "RetrieveCount";
            dataSource.EnablePaging = true;

            _gridView.DataSource = dataSource;
            _gridView.AllowPaging = true;
            _gridView.PageSize = 10;
            _gridView.AutoGenerateColumns = false;
            _gridView.RowCreated += new GridViewRowEventHandler(_gridView_RowCreated);

            {
                TemplateField tf = new TemplateField();
                tf.HeaderText = "ID Header";
                _gridView.Columns.Add(tf);
            }
            {
                TemplateField tf = new TemplateField();
                tf.HeaderText = "Name Header";
                _gridView.Columns.Add(tf);
            }
        }

        void _gridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = ((Test)e.Row.DataItem).Id.ToString();
                e.Row.Cells[1].Text = ((Test)e.Row.DataItem).Prop;
            }
        }

        void dataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = this;
        }

        public void DataBind()
        {
            _gridView.DataBind();
        }

        public int RetrieveCount()
        {
            return 50;
        }

        public ICollection RetrievePage(int maximumRows,
                                        int startRowIndex)
        {
            List<Test> list = new List<Test>();
            list.Add(new Test(0, "test 1"));
            list.Add(new Test(1, "test 2"));
            return list;
        }

    }

    public class GridViewTest : Page
    {

        public GridView _gridView;
        private TestListMap _testListMap;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _testListMap = new TestListMap(_gridView);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                _testListMap.DataBind();
            }
        }

    }

}


