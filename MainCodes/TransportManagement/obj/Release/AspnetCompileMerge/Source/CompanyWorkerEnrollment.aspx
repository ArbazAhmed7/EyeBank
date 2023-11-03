<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyWorkerEnrollment.aspx.cs" Inherits="TransportManagement.CompanyWorkerEnrollment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">
                        <asp:Panel ID="pnlCom" runat="server" DefaultButton="btnSave">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>Company Worker Enrollment</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                      <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">Test: </label>  
                                                <asp:RadioButtonList ID="rdoInstitutionType" runat="server" RepeatDirection="Horizontal" Width="300px">
                                                    <asp:ListItem Value="0" Selected="True">New</asp:ListItem>
                                                    <asp:ListItem Value="1">Display</asp:ListItem>
                                                    <asp:ListItem Value="2">Edit</asp:ListItem>
                                                </asp:RadioButtonList>                                              
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">
                                                Enrollment Date </label>                                                
                                                <asp:TextBox ID="txtEnrollmentDate" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtEnrollmentDate_CalendarExtender" runat="server" TargetControlID="txtEnrollmentDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="SchoolName">
                                               Company Name </label>
                                                <asp:TextBox ID="txtCompanyName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="75" ></asp:TextBox>
                                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="txtCompanyName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CompanyCode">
                                                 Code*</label>
                                                <div class="input-group input-group-sm mb-3">
                                                <%--<asp:TextBox ID="txtSchoolCode" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Enabled="False" ></asp:TextBox>
                                                <span class="input-group-append">                                    --%>
                                                    <%--<asp:LinkButton ID="btnLookupSchool" runat="server" OnClick="btnLookupSchool_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>--%>
                                                </span>
                                                
                                                </div> 
                                            </div>
                                        </div>                                       
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="SchoolName">
                                               Company Name </label>
                                                <asp:TextBox ID="TextBox1" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="75" ></asp:TextBox>
                                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="txtCompanyName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CompanyCode">
                                                 Code*</label>
                                                <div class="input-group input-group-sm mb-3">
                                                <%--<asp:TextBox ID="txtSchoolCode" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Enabled="False" ></asp:TextBox>
                                                <span class="input-group-append">                                    --%>
                                                    <%--<asp:LinkButton ID="btnLookupSchool" runat="server" OnClick="btnLookupSchool_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>--%>
                                                </span>
                                                
                                                </div> 
                                            </div>
                                        </div>                                       
                                        <%--<div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="ClassSection">
                                                Class Section </label>
                                                <asp:TextBox ID="txtClassSection" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="2" ></asp:TextBox>                                  
                                            </div>
                                        </div>    --%>   
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="StudentName">
                                                Student Name *</label>
                                                <%--<asp:TextBox ID="txtStudentName" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="25" AutoPostBack="True" OnTextChanged="txtStudentName_TextChanged" ></asp:TextBox>--%>
                                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="txtStudentName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtStudentName" ValidChars=" " />--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="StudentCode">
                                                Student Id </label>
                                                <div class="input-group input-group-sm mb-3">
                                                    <asp:TextBox ID="txtStudentCode" AutoComplete="off" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength="9" Enabled="False"></asp:TextBox>
                                                    <span class="input-group-append">                                    
                                                        <%--<asp:LinkButton ID="btnLookupStudent" runat="server" OnClick="btnLookupStudent_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                            <i class="fa fa-search"></i>
                                                        </asp:LinkButton>--%>
                                                    </span>
                                                 </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="StudentRegNo">
                                                Student Manual No. </label>
                                                <asp:TextBox ID="txtStudentRegNo" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="20" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="FatherName">
                                                Father Name </label>
                                                <%--<asp:TextBox ID="txtFatherName" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="25" AutoPostBack="True" OnTextChanged="txtFatherName_TextChanged" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtFatherName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtFatherName" ValidChars=" " />--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Age">
                                                Age </label>
                                                <asp:TextBox ID="txtAge" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="2" ></asp:TextBox>
                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtAge_MaskedEditExtender" Mask ="99" runat="server" TargetControlID="txtAge" />--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtAge_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAge" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Gender">
                                                Gender </label>
                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" >                                                
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="WearGlasses"> </label>
                                                <%--<asp:CheckBox ID="chkWearGlasses" Checked="false" runat="server" Text ="Wear Glasses" CssClass="form-control no-border" AutoPostBack="True" OnCheckedChanged="chkWearGlasses_CheckedChanged">                                                
                                                </asp:CheckBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="DecreasedVision"> </label>
                                                <asp:CheckBox ID="chkDecreasedVision" Checked="false" Text="Decreased Vision" runat="server" CssClass="form-control no-border" >                                                
                                                </asp:CheckBox>
                                            </div>
                                        </div>
                                     </div>

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Religion"> Religion </label>
                                                <asp:RadioButtonList ID="rdoReligion" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Muslim</asp:ListItem>
                                                    <asp:ListItem Value="1">Non-Muslim</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                     </div>


                                    <div class="row">
                                        <div class="col-sm-3" style ="display:none" >
                                            <div class="form-group">
                                                <label for="OccularHistory">
                                                Occular History </label>
                                                <%--<asp:CheckBox ID="chkOccularHistory" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkOccularHistory_CheckedChanged">                                                
                                                </asp:CheckBox>--%>
                                                <asp:TextBox ID="txtOccularHistory" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" style ="display:none">
                                            <div class="form-group">
                                                <label for="MedicalHistory">
                                                Medical History </label>
                                                <%--<asp:CheckBox ID="chkMedicalHistory" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkMedicalHistory_CheckedChanged">                                                
                                                </asp:CheckBox>--%>
                                                <asp:TextBox ID="txtMedicalHistory" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" style ="display:none">
                                            <div class="form-group">
                                                <label for="ChiefComplain">
                                                Chief Complain </label>
                                                <%--<asp:CheckBox ID="chkChiefComplain" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkChiefComplain_CheckedChanged">                                                
                                                </asp:CheckBox>--%>
                                                <asp:TextBox ID="txtChiefComplain" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="StudentPicture">
                                                Upload Student Picture </label>                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkNotRequired" runat="server" Text="Not Comfortable" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:Image ID="StudentImage" ClientIDMode="Static" runat="server" Height="150px" ImageUrl="~/Captures/StudentDefaultImage.jpg" meta:resourcekey="EmpImageResource1" Width="120px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:FileUpload ID="btnBrowse" ClientIDMode="Static" runat="server" onchange="ShowPreview(this)" accept="image/*" multiple="false" CssClass="btn btn-default btn-sm" Width="105px" />
                                                <asp:Label ID="lblFileUploadStudent" runat="server"></asp:Label>
                                                &nbsp;<%--<asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Load Image" CssClass="btn btn-default btn-sm" Visible="False" />                                                --%>
                                            </div>
                                        </div>                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">
                                                Capture Date </label>                                                
                                                <asp:TextBox ID="txtCaptureDate" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtCaptureDate" runat="server" TargetControlID="txtCaptureDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>                                   
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureRemarks">
                                                Capture Remarks </label>
                                                <asp:TextBox ID="txtCaptureRemarks" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="120" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtCaptureRemarks" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtCaptureRemarks" ValidChars=" " />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                              <%--  <asp:LinkButton ID="btnSaveImage" runat="server" OnClick="btnSaveImage_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Insert Image"></asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <%--<asp:LinkButton ID="btnWebCam" runat="server" Text="Capture from Webcam" OnClick="btnWebCam_Click"></asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <%--<asp:LinkButton ID="btnViewAllPhotos" runat="server" Text="View Selected Student Picture(s)" OnClick="btnViewAllPhotos_Click"></asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                          <div class="col-sm-6">
                                            <label for="StudentEnrollmentButton">
                                            &nbsp;</label>
                                            <div class="form-group text-left">
                                                <asp:LinkButton ID="btnEdit" runat="server" OnClientClick="return validateInput()" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return validateInput()" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm"  Text="Refresh" ></asp:LinkButton>
                                                <%--<asp:LinkButton ID="btnEdit" runat="server" OnClientClick="return validateInput()" OnClick="btnEdit_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return validateInput()" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" ></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="btnRefresh_Click" Text="Refresh" ></asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <p class="text-center text-danger">
                                                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                    <!-- Modal Popup -->
                                    <div id="PopupAfterSaveConfirmation" class="modal fade" role="dialog">
                                        <div class="modal-dialog">
                                            <!-- Modal content-->
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal">
                                                        &times;</button>
                                                    <h4 class="modal-title">
                                                    </h4>
                                                </div>
                                                <div class="modal-body">
                                                </div>
                                                <div class="modal-footer">
                                                    <%--<asp:Button ID="btnConfirmYes" runat="server" OnClick="btnConfirmYes_Click"  Text="Yes" />
                                                    <asp:Button ID="btnConfirmNo" runat="server" OnClick="btnConfirmNo_Click" Text="No" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Modal Popup -->
                                    </div>
                                    
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>
                            </div>
                        </div>
                    </div>
                            </asp:Panel>
                </div>

                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <%--<asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfClassIDPKID" runat="server" OnValueChanged="hfClassIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfSectionIDPKID" runat="server" OnValueChanged="hfSectionIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfStudentIDPKID" runat="server" OnValueChanged="hfStudentIDPKID_ValueChanged" Value="0" />

        <asp:HiddenField ID="hfLookupResultSchool" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSchool_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultClass" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultClass_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultSection" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSection_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />--%>

        <%--<asp:HiddenField ID="hfImageBytes" Value="" ClientIDMode="Static" OnValueChanged="hfImageBytes_ValueChanged" runat="server" />--%>
    </ContentTemplate>

    <Triggers>  
        <asp:PostBackTrigger ControlID="btnSave" /> 
        <asp:PostBackTrigger ControlID="btnEdit" />  
    </Triggers>  
</asp:UpdatePanel>
</asp:Content>
