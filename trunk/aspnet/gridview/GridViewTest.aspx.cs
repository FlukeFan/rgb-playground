
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            _gridView.EnableViewState = true;
            _gridView.RowCreated += new GridViewRowEventHandler(_gridView_RowCreated);
            _gridView.DataKeyNames = new string[] { "Id", "Prop" };
            _gridView.EnableViewState = true;

            if (!_gridView.Page.IsPostBack)
            {
                _gridView.AllowPaging = true;
                _gridView.PageSize = 10;
                _gridView.PageIndex = 2;
                _gridView.AutoGenerateColumns = false;

                {
                    TemplateField tf = new TemplateField();
                    tf.HeaderText = "Selected";
                    _gridView.Columns.Add(tf);
                }
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

            ObjectDataSource dataSource = new ObjectDataSource(this.GetType().FullName, "RetrievePage");
            dataSource.ObjectCreating += new ObjectDataSourceObjectEventHandler(dataSource_ObjectCreating);
            dataSource.SelectCountMethod = "RetrieveCount";
            dataSource.EnablePaging = true;
            dataSource.EnableViewState = true;

            _gridView.DataSource = dataSource;
        }

        void _gridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                IOrderedDictionary rowValues = _gridView.DataKeys[index].Values;

                CheckBox cb = new CheckBox();
                cb.Text = "select";
                e.Row.Cells[0].Controls.Add(cb);
                e.Row.Cells[1].Text = rowValues["Id"].ToString();
                e.Row.Cells[2].Text = (string)rowValues["Prop"];
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
            list.Add(new Test(1, "test 2 " + startRowIndex.ToString()));
            list.Add(new Test(2, "test 3 " + maximumRows.ToString()));
            list.Add(new Test(3, "test 4 "));
            list.Add(new Test(4, "test 5 "));
            list.Add(new Test(5, "test 6 "));
            list.Add(new Test(6, "test 7 "));
            list.Add(new Test(7, "test 8 "));
            list.Add(new Test(8, "test 9 "));
            list.Add(new Test(9, "test 10 "));

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Id", typeof(int)));
            dt.Columns.Add(new DataColumn("Prop", typeof(string)));
            foreach (Test t in list)
            {
                DataRow dr = dt.NewRow();
                dr[0] = t.Id;
                dr[1] = t.Prop;
                dt.Rows.Add(dr);
            }
            return new DataView(dt);
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


