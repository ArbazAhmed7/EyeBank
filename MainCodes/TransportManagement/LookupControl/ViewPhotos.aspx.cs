using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;

namespace TransportManagement.LookupControl
{
    public partial class ViewPhotos : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        public string FormID { get; set; }
        public string AutoKeyID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FormID"] != null)
            {
                FormID = Convert.ToString(Request.QueryString["FormID"]);
            }

            if (Request.QueryString["AutoKeyID"] != null)
            {
                AutoKeyID = Convert.ToString(Request.QueryString["AutoKeyID"]);
            }


            if (!Page.IsPostBack)
            {
                if (Convert.ToInt32(AutoKeyID) > 0)
                {
                    LoadGridData(FormID, AutoKeyID);
                }
                else
                {
                    lblNodata.Visible = true;
                }
            }
        }

        private void LoadGridData(string FormID, string AutoKeyID)
        {
            DataTable data = (from a in dx.sp_GetAllPhotosFormWise(FormID, Convert.ToInt32(AutoKeyID))
                              select a).ToList().ToDataTable();

            if (data.Rows.Count > 0)
            {
                grdPhotos.DataSource = data;
                grdPhotos.DataBind();
                lblNodata.Visible = false;
            }
            else
            {
                lblNodata.Visible = true;
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //System.Web.UI.HtmlControls.HtmlImage imageControl = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("imageControl");
                //if (((DataRowView)e.Row.DataItem)["Pic"] != DBNull.Value)
                //{
                //    imageControl.Src = "data:image/png;base64," + Convert.ToBase64String((byte[])(((DataRowView)e.Row.DataItem))["Pic"]);
                //}

                ImageButton btnImage = (ImageButton)e.Row.FindControl("btnImage");
                if (((DataRowView)e.Row.DataItem)["Pic"] != DBNull.Value)
                {
                    btnImage.ImageUrl = "Data:Image/jpg;base64," + Convert.ToBase64String((byte[])(((DataRowView)e.Row.DataItem))["Pic"]);
                }
            }
        }

        protected void grdPhotos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int InstrumentTypeAutoID = Convert.ToInt32(grdPhotos.DataKeys[e.RowIndex].Values[0]);
            string FormID = Convert.ToString(grdPhotos.DataKeys[e.RowIndex].Values[1]);

            var res = dx.sp_DeletePhotoFormWise(FormID, InstrumentTypeAutoID);
            LoadGridData(FormID, AutoKeyID);
        }
    }
}