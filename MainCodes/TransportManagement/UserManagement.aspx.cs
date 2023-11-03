using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class UserManagement : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "UserManagement"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                InitForm();
            }

        }

        private void InitForm()
        {
            BindRoles();
            BindGrid();
        }

        protected void linkButtonLookup_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new secoffEntities())
                {
                    DataTable data = (from a in ctx.sp_GetLookupData("UserCreation")
                                      select a).ToList().ToDataTable();

                    hfLookupSelectedUser.Value = "0";
                    Session["lookupData"] = data;
                    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupSelectedUser.ID + "','.','height=400,width=450,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", jsReport, false);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BindRoles()
        {
            //ddlRole
            var Roles = dx.Database.SqlQuery<tblRole>("Select RoleAutoId, RoleDescription, Delflag, ISNULL(City, 0) as City from tblRole where ISNULL(Delflag, 'N')='N'").ToList();
            ddlRole.DataSource = Roles;
            ddlRole.DataValueField = "RoleAutoId";
            ddlRole.DataTextField = "RoleDescription";
            ddlRole.DataBind();

        }

        protected void hfLookupSelectedRecord_ValueChanged(object sender, EventArgs e)
        {

            //string id = Convert.ToString(hfLookupSelectedRecord.Value);
            //hfSelectedUserID.Value = id.ToString();

            //LoadRoleData(id);
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                if (Convert.ToInt32(hfSelectedUserAutoID.Value) > 0 && Convert.ToInt32(ddlRole.SelectedValue) > 0)
                {
                    using (secoffEntities ctx = new secoffEntities())
                    {
                        var res = ctx.sp_AssignRoleToUser(Convert.ToInt32(hfSelectedUserAutoID.Value), Convert.ToInt32(ddlRole.SelectedValue)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;
                            BindGrid();
                            //ClearForm();
                            ClearValidation();
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                }
            }
        }

        private bool ValidateInput()
        {
            CleatValidation();
            if (txtUserId.Text.Trim() == "")
            {
                lbl_error.Text = "User ID is required.";
                return false;
            }

            if (ddlRole.Items.Count == 0)
            {
                lbl_error.Text = "Please select or define the Roles to Assign.";
                return false;
            }

            return true;
        }

        private void CleatValidation()
        {
            lbl_error.Text = "";
        }

        private void ClearForm()
        {
            InitForm();
            CleatValidation();
            hfSelectedUserAutoID.Value = "0";
            hfLookupSelectedUser.Value = "0";
            hfAutoCompleteSelectedUser.Value = "0";
            txtUserId.Text = "";
        }

        protected void hfLookupSelectedUser_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = hfLookupSelectedUser.Value;
            hfSelectedUserAutoID.Value = selectedPKID;
            LoadUserDetail(selectedPKID);
        }

        protected void hfAutoCompleteSelectedUser_ValueChanged(object sender, EventArgs e)
        {
            string id = Convert.ToString(hfAutoCompleteSelectedUser.Value);
            hfSelectedUserAutoID.Value = id;
            LoadUserDetail(id);
        }

        protected void gvUserRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ClearValidation();
            string RoleMgmtAutoId = this.gvUserRole.DataKeys[e.RowIndex].Value.ToString();

            try
            {
                if (Convert.ToInt32(RoleMgmtAutoId) > 0)
                {
                    using (var ctx = new secoffEntities())
                    {
                        var res = ctx.sp_UserManagement_RemoveAssignedRoleToUser(Convert.ToInt32(RoleMgmtAutoId)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;
                            //ClearForm();
                            BindGrid();
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void BindGrid()
        {
            try
            {
                using (secoffEntities ctx = new secoffEntities())
                {
                    var detail = ctx.sp_UserManagement_GetAssignedRoles().ToList();
                    if (detail.Count > 0)
                    {
                        gvUserRole.DataSource = detail;
                        gvUserRole.DataBind();

                    }
                    else
                    {
                        gvUserRole.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadUserDetail(string ID)
        {
            ClearValidation();
            if (Convert.ToUInt32(ID) > 0)
            {
                using (secoffEntities ctx = new secoffEntities())
                {
                    var dt = ctx.sp_tblUser_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    txtUserId.Text = dt.UserId;
                }
            }
        }

        [WebMethod]
        public static AutoComplete[] AutoComplete(string Term, string TermType, string Id)
        {

            List<AutoComplete> lst = new List<AutoComplete>();
            //
            if (Term.Length < 2)
                return lst.ToArray();

            int id = Convert.ToInt32(Id);

            try
            {
                using (secoffEntities dx = new secoffEntities())
                {
                    var dt = dx.sp_GetAutoCompleteESMS(Term, TermType, id).ToList();


                    foreach (var item in dt)
                    {
                        lst.Add(
                        new AutoComplete
                        {
                            label = item.Name,
                            val = item.Value
                        }
                        );
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return lst.ToArray();

        }

        protected void lnkAddRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleManagement.aspx");
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }
    }
}