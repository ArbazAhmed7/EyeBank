using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransportManagement.LookupControl
{
    public partial class LookupControlFatherName : System.Web.UI.Page
    {
        public string hfName { get; set; }
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["hfName"] != null)
            {
                hfName = Convert.ToString(Request.QueryString["hfName"]);
            }

            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();

                Session["SearchedCode"] = string.Empty;

                if (Session["lookupData"] != null)
                {
                    dt = (DataTable)Session["lookupData"];
                    gvitems.DataSource = dt;
                    gvitems.DataBind();

                    Session["SearchingData"] = dt;
                }
            }

            //txtSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtSearch = new DataTable();
            dtSearch = (DataTable)Session["SearchingData"];

            DataView dv = new DataView();
            dv = dtSearch.DefaultView;


            DataTable dtFinalResult = new DataTable();
            dtFinalResult.Columns.AddRange(new DataColumn[5] {
                                                            new DataColumn("Id", typeof(string)),
                                                           new DataColumn("Code", typeof(string)),
                                                           new DataColumn("Name",typeof(string)),
                                                           new DataColumn("FatherName",typeof(string)),
                                                           new DataColumn("Description",typeof(string))
                                                         });
            if (txtSearch.Text.Trim() != "")
            {
                dv.RowFilter = "Code LIKE '%" + txtSearch.Text + "%' OR Name LIKE '%" + txtSearch.Text + "%'";

                foreach (DataRow dr in dv.ToTable().Rows)
                {
                    dtFinalResult.ImportRow(dr);
                }

                gvitems.DataSource = dtFinalResult;
                gvitems.DataBind();
            }
            else
            {
                dv.RowFilter = "";
                foreach (DataRow dr in dv.ToTable().Rows)
                {
                    dtFinalResult.ImportRow(dr);
                }
                gvitems.DataSource = dtFinalResult;
                gvitems.DataBind();
            }
        }

        protected void gvitems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                try
                {
                    if (Session["Code"] != null)
                    {
                        e.Row.Cells[1].Text = Session["Code"].ToString();
                        e.Row.Cells[2].Text = Session["Name"].ToString();
                        e.Row.Cells[3].Text = Session["FatherName"].ToString();
                        e.Row.Cells[4].Text = Session["Description"].ToString();
                    }
                    else
                    {
                        e.Row.Cells[1].Text = "Code";
                        e.Row.Cells[2].Text = "Name";
                        e.Row.Cells[3].Text = "FatherName";
                        e.Row.Cells[4].Text = "Description";
                    }
                }
                catch (Exception ex)
                {
                    e.Row.Cells[1].Text = "Code";
                    e.Row.Cells[2].Text = "Name";
                    e.Row.Cells[3].Text = "FatherName";
                    e.Row.Cells[4].Text = "Description";
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvitems, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void gvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvitems.Rows)
            {
                if (row.RowIndex == gvitems.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    //string code = row.Cells[0].Text;
                    //string name = row.Cells[1].Text;

                    string sReturn = row.Cells[1].Text.Trim() + "@" + row.Cells[2].Text.Trim();
                    Session["Return_Result"] = sReturn;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }


        protected void gvitems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvitems.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                gvitems.DataSource = Session["SortedView"];
                gvitems.DataBind();
            }
            else
            {
                DataTable dtSearch = new DataTable();
                dtSearch = (DataTable)Session["SearchingData"];
                gvitems.DataSource = dtSearch;
                gvitems.DataBind();
            }
        }

        protected void gvitems_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataTable dtSearch = new DataTable();
            dtSearch = (DataTable)Session["SearchingData"];

            DataView sortedView = new DataView(dtSearch);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvitems.DataSource = sortedView;
            gvitems.DataBind();
        }

        protected void gvitems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            HiddenField field = (HiddenField)gvitems.Rows[e.NewSelectedIndex].FindControl("hfSelectedID");

            //string script = "window.opener.document.getElementById('hfLookupResult').value = " + field.Value + "; " +
            //                "window.opener.document.getElementById('hfLookupResult').dispatchEvent(new Event('change'));" +
            //                "window.opener.__doPostBack();" +
            //                "window.close()";

            //string script2 = "window.opener.document.getElementById('hfLookupResult').value = " + field.Value + "; " +
            //                "window.opener.ParentJSFunction();" +
            //                "window.close();";

            string script3 = "window.opener.document.getElementById('" + hfName + "').value = " + field.Value + "; " +
                            "window.opener.__doPostBack('hfLookupResult','');" +
                            "window.close()";

            //ClientScript.RegisterStartupScript(this.GetType(), "onclick", script3, true);
            ScriptManager.RegisterStartupScript(this.updatePanel1, typeof(string), "onclick", script3, true);
        }

    }
}