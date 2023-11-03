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
    public partial class RoleManagement : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "Roles"))
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
            btnDelete.Visible = false;
            BindFormsDDL();
            //BindGrid();
        }

        private void BindFormsDDL()
        {
            try
            {

                var listForms = (from a in dx.sp_GetAllFormsData()
                                 select a).ToList();

                if (listForms.Count > 0)
                {
                    ddlForm.DataSource = listForms;
                    ddlForm.DataTextField = "FormDescription";
                    ddlForm.DataValueField = "FormAutoId";
                    ddlForm.DataBind();
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void linkButtonLookup_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("Roles")
                                  select a).ToList().ToDataTable();

                hfLookupSelectedRecord.Value = "0";
                Session["lookupData"] = data;
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupSelectedRecord.ID + "','.','height=400,width=450,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", jsReport, false);
            }
            catch (Exception ex)
            {

            }
        }



        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    var res = dx.sp_RoleManagement_InsertUpdate
                        (
                            Convert.ToInt32(hfLookupSelectedRecord.Value)
                            , txtRoleDescription.Text.Trim()
                            , Convert.ToInt32(ddlForm.SelectedValue)
                            , 1
                            , 1
                            , 1
                            , 1
                            , Convert.ToInt32(ddlCity.SelectedValue)
                        ).FirstOrDefault();

                    if (res.ResponseCode >= 0)
                    {
                        lbl_error.Text = res.RetMessage;

                        //ClearForm();
                        if (res.ResponseCode == 0)
                        {
                            //old updated
                            BindGrid(GetSelectedMasterID());
                        }
                        else if (res.ResponseCode > 0)
                        {
                            //new inserted with AuoID in ResponseCode
                            hfSelectedRoleID.Value = res.ResponseCode.ToString();
                            hfLookupSelectedRecord.Value = res.ResponseCode.ToString();
                            hfAutoCompleteSelectedRecord.Value = res.ResponseCode.ToString();
                            BindGrid(res.ResponseCode);
                        }

                        ShowConfirmAddMoreRecord();
                    }
                    else
                    {
                        lbl_error.Text = res.RetMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        private void ShowConfirmAddMoreRecord()
        {
            string title = "Confirmation";
            string body = "Record Saved succussfully.<br/> Do you want to add more records?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopupAfterSaveConfirmation('" + title + "', '" + body + "');", true);
        }

        protected void btnConfirmYes_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        private bool ValidateInput()
        {
            CleatValidation();
            if (txtRoleDescription.Text.Trim() == "")
            {
                lbl_error.Text = "Role Description is required.";
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            InitForm();
            hfLookupSelectedRecord.Value = "0";
            hfAutoCompleteSelectedRecord.Value = "0";
            txtRoleDescription.Text = "";

            //gvRole.DataSource = null;
            gvRole.DataBind();
        }

        private void CleatValidation()
        {
            lbl_error.Text = "";
        }

        private void LoadRoleData(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    int PkID = Convert.ToInt32(ID);
                    var master = dx.sp_RoleManagement_GetMaster(PkID).ToList().FirstOrDefault();
                    txtRoleDescription.Text = master.RoleDescription;
                    ddlCity.SelectedValue = Convert.ToString(master.City);

                    BindGrid(PkID);
                    btnDelete.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void BindGrid(int RoleID)
        {
            try
            {
                var detail = dx.sp_RoleManagement_GetDetail(RoleID).ToList();

                if (detail.Count > 0)
                {
                    gvRole.DataSource = detail;
                    gvRole.DataBind();

                }
                else
                {
                    gvRole.DataBind();
                }
            }
            catch (Exception ex)
            {

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

        protected void hfLookupSelectedRecord_ValueChanged(object sender, EventArgs e)
        {
            hfAutoCompleteSelectedRecord.Value = "0";
            string id = Convert.ToString(hfLookupSelectedRecord.Value);
            hfSelectedRoleID.Value = id.ToString();

            LoadRoleData(id);
        }

        protected void hfAutoCompleteSelectedRecord_ValueChanged(object sender, EventArgs e)
        {
            hfLookupSelectedRecord.Value = "0";
            string id = Convert.ToString(hfAutoCompleteSelectedRecord.Value);
            hfSelectedRoleID.Value = id.ToString();

            LoadRoleData(id);
        }

        protected void gvRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ClearValidation();
            string RoleMgmtAutoId = this.gvRole.DataKeys[e.RowIndex].Value.ToString();

            if (Convert.ToInt32(RoleMgmtAutoId) > 0)
            {
                using (var ctx = new secoffEntities())
                {
                    var res = ctx.sp_RoleManagement_DeleteRoleDetailEntry(Convert.ToInt32(RoleMgmtAutoId)).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;
                        //ClearForm();
                        BindGrid(GetSelectedMasterID());
                    }
                    else
                    {
                        lbl_error.Text = res.RetMessage;
                    }

                }
            }
        }

        private int GetSelectedMasterID()
        {
            return Convert.ToInt32(hfSelectedRoleID.Value);
        }
        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int RoleAutoID = GetSelectedMasterID();
            if (RoleAutoID > 0)
            {
                using (var ctx = new secoffEntities())
                {
                    var res = ctx.sp_RoleManagement_DeleteRoleMaster(RoleAutoID).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;
                        ClearForm();
                    }
                    else
                    {
                        lbl_error.Text = res.RetMessage;
                    }

                }
            }
        }
    }
}