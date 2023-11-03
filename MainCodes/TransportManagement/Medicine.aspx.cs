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
    public partial class Medicine : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "Medicine"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                txtMedicineID.Focus();
            }
        }

        private void InitForm()
        {
            btnDelete.Visible = false;
            btnEdit.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    var res = dx.sp_tblMedicine_InsertUpdate(Convert.ToInt32(hfMedicineIDPKID.Value), txtMedicineDescription.Text.Trim()).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    var res = dx.sp_tblMedicine_InsertUpdate(Convert.ToInt32(hfMedicineIDPKID.Value), txtMedicineDescription.Text.Trim()).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hfMedicineIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblMedicine_Delete(Convert.ToInt32(hfMedicineIDPKID.Value)).FirstOrDefault();

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
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        private bool ValidateInput()
        {
            ClearValidation();
            if (txtMedicineDescription.Text.Trim() == "")
            {
                lbl_error.Text = "Medicine Description is required.";
                txtMedicineDescription.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            InitForm();
            ClearValidation();
            hfMedicineIDPKID.Value = "0";
            txtMedicineID.Text = "";
            txtMedicineDescription.Text = "";

            txtMedicineID.Focus();
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        [WebMethod]
        public static AutoComplete[] AutoComplete(string Term, string TermType, string Id)
        {

            List<AutoComplete> lst = new List<AutoComplete>();

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

        protected void hfMedicineIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string ClassIDPKID = string.Empty;
            ClassIDPKID = hfMedicineIDPKID.Value;

            LoadMedicineDetail(ClassIDPKID);
            lbl_error.Text = "";
        }

        protected void hfLookupResult_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            //hfMedicineIDPKID.Value = "0";
            selectedPKID = hfLookupResult.Value;
            hfMedicineIDPKID.Value = selectedPKID; //to allow update mode

            LoadMedicineDetail(selectedPKID);
            lbl_error.Text = "";
        }

        protected void btnLookup_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable data = (from a in dx.sp_GetLookupData("Medicine")
                                  select a).ToList().ToDataTable();

                hfLookupResult.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Medicine Id";
                Session["Name"] = "Medicine Description";
                //string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User','','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResult.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookup, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        private void LoadMedicineDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblMedicine_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    txtMedicineID.Text = dt.MedicineAutoId.ToString();
                    txtMedicineDescription.Text = dt.MedicineDescription;

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
                hfLookupResult.Value = "0";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}