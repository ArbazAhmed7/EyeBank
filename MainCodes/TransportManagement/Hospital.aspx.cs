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
    public partial class Hospital : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "Hospital"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                txtCity.Text = "Karachi";
                txtHospitalID.Focus();
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

                    var res = dx.sp_tblHospital_InsertUpdate(Convert.ToInt32(hfHospitalIDPKID.Value), txtHospitalDescription.Text.Trim()
                        , txtAddress.Text.Trim(), txtDistrict.Text.Trim(), txtTown.Text.Trim(), txtCity.Text.Trim()
                        , txtContactNo.Text.Trim()).FirstOrDefault();

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

                    var res = dx.sp_tblHospital_InsertUpdate(Convert.ToInt32(hfHospitalIDPKID.Value), txtHospitalDescription.Text.Trim()
                        , txtAddress.Text.Trim(), txtDistrict.Text.Trim(), txtTown.Text.Trim(), txtCity.Text.Trim()
                        , txtContactNo.Text.Trim()).FirstOrDefault();

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
                if (Convert.ToInt32(hfHospitalIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblHospital_Delete(Convert.ToInt32(hfHospitalIDPKID.Value)).FirstOrDefault();

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
            if (txtHospitalDescription.Text.Trim() == "")
            {
                lbl_error.Text = "Hospital Name is required.";
                txtHospitalDescription.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            InitForm();
            ClearValidation();
            hfHospitalIDPKID.Value = "0";
            txtHospitalID.Text = "";
            txtHospitalDescription.Text = "";
            txtAddress.Text = "";
            txtDistrict.Text = "";
            txtTown.Text = "";
            txtCity.Text = "";
            txtContactNo.Text = "";

            txtHospitalID.Focus();
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

        protected void hfHospitalIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string ClassIDPKID = string.Empty;
            ClassIDPKID = hfHospitalIDPKID.Value;

            LoadHospitalDetail(ClassIDPKID);
            lbl_error.Text = "";
        }

        protected void hfLookupResult_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            //hfHospitalIDPKID.Value = "0";
            selectedPKID = hfLookupResult.Value;
            hfHospitalIDPKID.Value = selectedPKID; //to allow update mode

            LoadHospitalDetail(selectedPKID);
            lbl_error.Text = "";
        }

        protected void btnLookup_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable data = (from a in dx.sp_GetLookupData("Hospital")
                                  select a).ToList().ToDataTable();

                hfLookupResult.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Hospital Id";
                Session["Name"] = "Hospital Name";
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

        private void LoadHospitalDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblHospital_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    txtHospitalID.Text = dt.HospitalAutoId.ToString();
                    txtHospitalDescription.Text = dt.HospitalDescription;
                    txtAddress.Text = dt.Address;
                    txtAddress.Text = dt.Address;
                    txtDistrict.Text = dt.District;
                    txtTown.Text = dt.Town;
                    txtCity.Text = dt.City;
                    txtContactNo.Text = dt.ContactNo;

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

        protected void txtHospitalDescription_TextChanged(object sender, EventArgs e)
        {
            string sSchoolName = txtHospitalDescription.Text;

            txtHospitalDescription.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sSchoolName.ToLower());

            txtAddress.Focus();
        }

        protected void txtAddress_TextChanged(object sender, EventArgs e)
        {
            string sAddress1 = txtAddress.Text;

            txtAddress.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sAddress1.ToLower());

            txtDistrict.Focus();
        }

        protected void txtDistrict_TextChanged(object sender, EventArgs e)
        {
            string sDistrict = txtDistrict.Text;

            txtDistrict.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sDistrict.ToLower());

            txtTown.Focus();
        }

        protected void txtTown_TextChanged(object sender, EventArgs e)
        {
            string sTown = txtTown.Text;

            txtTown.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sTown.ToLower());

            txtCity.Focus();
        }

        protected void txtCity_TextChanged(object sender, EventArgs e)
        {
            string sCity = txtCity.Text;

            txtCity.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sCity.ToLower());

            txtContactNo.Focus();
        }

    }
}