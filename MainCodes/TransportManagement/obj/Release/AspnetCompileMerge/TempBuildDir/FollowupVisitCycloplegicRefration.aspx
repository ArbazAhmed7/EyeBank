<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FollowupVisitCycloplegicRefration.aspx.cs" Inherits="TransportManagement.FollowupVisitCycloplegicRefration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/FollowupVisitCycloplegicRefration.js"></script>
    <script type="text/javascript">
        function ShowPopupAfterSaveConfirmation(title, body) {
            $("#PopupAfterSaveConfirmation .modal-title").html(title);
            $("#PopupAfterSaveConfirmation .modal-body").html(body);
            $("#PopupAfterSaveConfirmation").modal("show");
        }

        function HideBootstrapModal() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove(); $('#Div3').hide();
        }
        function ShowPreview(input) {
            var maxFileSize = 10485760; //// 10MB -> 10 * 1024 * 1024

            if (input.files && input.files[0]) {
                if (input.files[0].size > maxFileSize) {
                    alert("File is too big maximum file size can't be geater than 10 MB");
                    input.value = null;
                    return false;
                }
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    var image = document.getElementById("StudentImage");
                    image.src = e.target.result;
                    $("[id$=hfImageBytes]").val(e.target.result);
                    __doPostBack('UpdatePanel1', '');
                    return false;
                }
                ImageDir.readAsDataURL(input.files[0]);


            }
        }

        //function pageLoad() {
        //    ImageUploadlabelUpdate();
        //}

        //function ImageUploadlabelUpdate() {
        //    if ($("[id$=hfImageBytes]").val()) {
        //        $('[id$=lblFileUploadStudent]').text('File chosen.');
        //    }
        //    else {
        //        $('[id$=lblFileUploadStudent]').text('Choose file.');
        //    }
        //}

        function validateInput() {

            var valRes = true;

            if ($("[id$=txtTestDate]").val().trim() == "") {
                $("[id$=txtTestDate]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtTestDate]").removeAttr("style");
            }

            if ($("[id$=txtSchoolName]").val().trim() == "") {
                $("[id$=txtSchoolName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtSchoolName]").removeAttr("style");
            }

            if ($("[id$=txtStudentCode]").val().trim() == "") {
                $("[id$=txtStudentCode]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtStudentCode]").removeAttr("style");
            }

            if ($("[id$=txtStudentName]").val().trim() == "") {
                $("[id$=txtStudentName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtStudentName]").removeAttr("style");
            }
            
            if (!valRes) {
                $("[id$=lbl_error]").text('* Mandatory');
            }
            else {
                $("[id$=lbl_error]").text('');
            }

            return valRes;
        }

    </script> 
    <style type="text/css">
        .auto-style1 {
            width: 85%;
        }
        .auto-style2 {
            width: 100%;
        }
    </style>
    
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>                                
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">         
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h5 class="m-t-0 header-title"><b>Follow up visit for Cycloplegic Refration</b></h5>
                                    <hr />                                                                     
                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                Date *</label>                                                
                                                <asp:Label ID="lblTestDate" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" ></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>                                                    
                                            </div>
                                        </div>
                                    </div>--%>
                                     
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="5">
                                                    <asp:ListItem Selected="True" Value="0">New Test</asp:ListItem>
                                                    <asp:ListItem Value="1">Edit Previous Visit Result</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="pnlStudent" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Student Information</div>
                                        <div class="panel-body" runat="server">
                                            <div class="row">
                                                <%--left side--%>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="SchoolName">School Name *</label>
                                                                <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="60" TabIndex="1"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="SchoolCode">School Code *</label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtSchoolCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength ="3" AutoComplete="off" Enabled="False" TabIndex="2" ></asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupSchool" runat="server" ClientIDMode="Static" OnClick="btnLookupSchool_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                                            <i class="fa fa-search"></i>
                                                                        </asp:LinkButton>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
	                                                <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentName">Student Name *</label>
                                                                <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25" TabIndex="3"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtStudentName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txtStudentName" />
                                                            </div>                                                            
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentCode">                                                                
                                                                Student Id </label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtStudentCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9" TabIndex="4"></asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupStudent" runat="server"   ClientIDMode="Static" OnClick="btnLookupStudent_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                                            <i class="fa fa-search"></i>
                                                                        </asp:LinkButton>
                                                                    </span>
                                                                    <asp:FilteredTextBoxExtender ID="txtStudentCode_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtStudentCode" />
                                                                </div>
                                                            </div>
                                                        </div>
	                                                </div>
                                                </div>

                                                <%--Right side--%>
                                                <div runat="server" id="pnlStudent_Sub" class="col-xs-12 col-sm-6">
                                                    <table class="auto-style2">
                                                        <tr>
                                                            <td><asp:Label ID="Label7"  runat="server" Text="Father Name" ></asp:Label></td>
                                                            <td><asp:Label ID="lblFatherName_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label8"  runat="server" Text="Age" ></asp:Label></td>
                                                            <td><asp:Label ID="lblAge_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label9"  runat="server" Text="Gender" ></asp:Label></td>
                                                            <td><asp:Label ID="lblGender_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label10"  runat="server" Text="Class" ></asp:Label></td>
                                                            <td><asp:Label ID="lblClass_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label11"  runat="server" Text="School" ></asp:Label></td>
                                                            <td><asp:Label ID="lblSchoolName_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label12"  runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                            <td><asp:Label ID="lblWearingGlasses_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label13"  runat="server" Text="Decreased Vision" ></asp:Label></td>
                                                            <td><asp:Label ID="lblDecreasedVision_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                    </table>                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div runat="server" id="pnl_PreAutoRefTest" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Pre-Autorefraction Test</div>
                                        <div class="panel-body" runat="server">
                                            <div class="row">
                                              <div class="col-xs-12 col-sm-6">
	                                            <table class="auto-style1">
                                                    <tr>
                                                        <td><asp:Label ID="Label28" runat="server"  Text="Right"></asp:Label></td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Spherical"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Cylinderical"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Axix"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSpherical_RightEye" runat="server" ></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCylinderical_RightEye" runat="server" ></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAxix_RightEye" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                              </div>

                                              <div class="col-xs-12 col-sm-6">
	                                            <div id="pnlLeftEye_AutoRef" runat="server">
                                                    <table class="auto-style1">
                                                        <tr>
                                                            <td><asp:Label ID="Label29" runat="server"  Text="Left"></asp:Label></td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Spherical"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Cylinderical"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Axix"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSpherical_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCylinderical_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAxix_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
	                                            </div>
                                              </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="5">
                                                    <asp:ListItem Selected="True" Value="0">New Test</asp:ListItem>
                                                    <asp:ListItem Value="1">Edit Previous Test Result</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                Visit Date *</label>                                                
                                                <asp:Label ID="Label14" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" TabIndex="6"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>

                                                <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True" TabIndex="7" > </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>   

                                    <div runat="server" id="pnlPostAutoRefraction" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server" >Post-Autorefraction Test</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="pnlTest2_RightEye" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Right Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    

                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_RightEye">Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSpherical_RightEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="8" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtSpherical_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="9"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_RightEye_AutoRef" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_RightEye" style="text-align: left">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclinderical_RightEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="10">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtCyclinderical_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="11"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_RightEye_AutoRef" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                         
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_RightEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="12" ></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_RightEye_AutoRef" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixB_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_RightEye_AutoRef" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                </div>                                                                                                                                                           
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="pnlTest2_LeftEye" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Left Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_LeftEye">
                                                                            Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSpherical_LeftEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="13" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtSpherical_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="14" ></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_LeftEye_AutoRef" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_LeftEye">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclinderical_LeftEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="15">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtCyclinderical_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="16"></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_LeftEye_AutoRef" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_LeftEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="17"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_LeftEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_LeftEye_AutoRef" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_LeftEye_MaskedEditExtender" runat="server" Mask="999" TargetControlID="txtAxixB_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_LeftEye_AutoRef" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
	                                           
	                                        </div>
	 
                                        </div>
	
	                                </div>

                                    <div runat="server" id="pnlObjectiveRefTest" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server">Objective Refraction Test</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
		                                            <div runat="server" id="pnlTest3_RightEye" class="panel panel-default">
			                                            <div class="panel-heading" runat="server" >Right Eye</div>
			                                            <div class="panel-body" runat="server">
				                                            <div class="row">
					                                            <div class="container">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="RetinoScopy_RightEye"> Retinoscopy </label>
                                                                                    <asp:RadioButtonList ID="rdoRetinoScopy_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="18" OnSelectedIndexChanged="rdoRetinoScopy_RightEye_SelectedIndexChanged" >
                                                                                        <asp:ListItem Value="0" Selected="True">Normal Reflex</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Dull Reflex</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Scissor Reflex</asp:ListItem>                                                                            
                                                                                    </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                        
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="CycloplegicRefraction_RightEye"> Tests after Cycloplegic Refraction </label>
                                                                                    <asp:RadioButtonList ID="rdoCycloplegicRefraction_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="19" >
                                                                                        <asp:ListItem Value="0" Selected="True">With movement</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Against movement</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                        
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="Condition_RightEye">
                                                                                Condition </label>
                                                                                <asp:TextBox ID="txtCondition_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="22"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="Meridian1_RightEye">
                                                                                Meridian 1 </label>
                                                                                <asp:TextBox ID="txtMeridian1_RightEye" runat="server"  AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="23"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="Meridian2_RightEye">
                                                                                Meridian 2 </label>
                                                                                <asp:TextBox ID="txtMeridian2_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="24"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="FinalPrescription_RightEye">
                                                                                Final Prescription </label>
                                                                                <asp:TextBox ID="txtFinalPrescription_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="25"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>    
					                                            </div>
				                                            </div>
			                                            </div>
		                                            </div>
	                                            </div>																										
	                                            <div class="col-xs-12 col-sm-6">
		                                            <div runat="server" id="pnlTest3_LeftEye" class="panel panel-default">
				                                        <div class="panel-heading" runat="server" >Left Eye</div>
				                                        <div class="panel-body" runat="server">
					                                        <div class="row">
						                                        <div class="container">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="RetinoScopy_LeftEye"> Retinoscopy </label>
                                                                                    <asp:RadioButtonList ID="rdoRetinoScopy_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True"  TabIndex="20" OnSelectedIndexChanged="rdoRetinoScopy_LeftEye_SelectedIndexChanged" >
                                                                                        <asp:ListItem Value="0" Selected="True">Normal Reflex</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Dull Reflex</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Scissor Reflex</asp:ListItem>                                                                            
                                                                                    </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                        
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="CycloplegicRefraction_LeftEye"> Tests after Cycloplegic Refraction </label>
                                                                                    <asp:RadioButtonList ID="rdoCycloplegicRefraction_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="21" >
                                                                                        <asp:ListItem Value="0" Selected="True">With movement</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Against movement</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="Condition_LeftEye">
                                                                                Condition </label>
                                                                                <asp:TextBox ID="txtCondition_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="26"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="Meridian1_LeftEye">
                                                                                Meridian 1 </label>
                                                                                <asp:TextBox ID="txtMeridian1_LeftEye" runat="server"  AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="27"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="Meridian2_LeftEye">
                                                                                Meridian 2 </label>
                                                                                <asp:TextBox ID="txtMeridian2_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="28"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label for="FinalPrescription_LeftEye">
                                                                                Final Prescription </label>
                                                                                <asp:TextBox ID="txtFinalPrescription_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="29"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
						                                        </div>
					                                        </div>
				                                        </div>
			                                        </div>
		                                        </div>											
											</div>	 
                                        </div>	
	                                </div>

                                    <div runat="server" id="pnlSuggstedPresCycRef" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server" >Suggested Prescription after cyclopegic refration</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="Div1" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Right Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_RightEye">Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSpherical_RightEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="30" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtSpherical_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="31"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_RightEye" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_RightEye" style="text-align: left">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclinderical_RightEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="32">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtCyclinderical_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="33"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_RightEye" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                         
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_RightEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_RightEye" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="34" ></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_RightEye" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixB_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_RightEye" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Near_RightEye" style="text-align: left">
                                                                            Near Add *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlNear_RightEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="40" Enabled="False">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtNear_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="41" ></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtNear_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtNear_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtNear_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtNear_RightEye" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>                                                                                                                                                           
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="Div2" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Left Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_LeftEye">
                                                                            Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSpherical_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="35" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtSpherical_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="36" ></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_LeftEye" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_LeftEye">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclinderical_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="37">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtCyclinderical_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="38"></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_LeftEye" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_LeftEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_LeftEye" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="39"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_LeftEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_LeftEye" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_LeftEye_MaskedEditExtender" runat="server" Mask="999" TargetControlID="txtAxixB_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_LeftEye" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Near_LeftEye">
                                                                            Near Add *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlNear_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="42" Enabled="False">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtNear_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="43" ></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtNear_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtNear_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtNear_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtNear_LeftEye" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
											
											</div>	 
                                        </div>	
	                                </div>

                                    <div runat="server" id="Panel1" class="panel panel-primary">
                                            <div class="panel-heading" runat="server" >Diagnosis</div>
                                            <div class="panel-body" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <div class="form-group">
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td><asp:Label ID="Label15" runat="server" Text="Right" Width="300px" Font-Bold="True" ></asp:Label></td>
                                                                    <td><asp:Label ID="Label16" runat="server" Text="Left" Width="200px" Font-Bold="True" ></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblNormal" runat="server" Text="Normal" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkNormal_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkNormal_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkNormal_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkNormal_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblRefractiveError" runat="server" Text="Refractive Error" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkRefractiveError_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkRefractiveError_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkRefractiveError_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkRefractiveError_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblLowVision" runat="server" Text="Low Vision" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkLowVision_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkLowVision_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkLowVision_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkLowVision_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblNeedsCycloplegicRefraction" runat="server" Text="Needs Cycloplegic Refraction" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkNeedsCycloplegicRefraction_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkNeedsCycloplegicRefraction_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkNeedsCycloplegicRefraction_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkNeedsCycloplegicRefraction_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblSquintStrabismus" runat="server" Text="Squint Strabismus" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkSquintStrabismus_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkSquintStrabismus_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkSquintStrabismus_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkSquintStrabismus_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblLazyEyeAmblyopia" runat="server" Text="Lazy Eye Amblyopia" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkLazyEyeAmblyopia_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkLazyEyeAmblyopia_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkLazyEyeAmblyopia_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkLazyEyeAmblyopia_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblColorblindnessAchromatopsia" runat="server" Text="Color blindness Achromatopsia" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkColorblindnessAchromatopsia_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkColorblindnessAchromatopsia_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkColorblindnessAchromatopsia_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkColorblindnessAchromatopsia_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblCataract" runat="server" Text="Cataract" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkCataract_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkCataract_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkCataract_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkCataract_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblTraumaticCataract" runat="server" Text="Traumatic Cataract" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkTraumaticCataract_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkTraumaticCataract_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkTraumaticCataract_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkTraumaticCataract_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblKeratoconus" runat="server" Text="Keratoconus" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkKeratoconus_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkKeratoconus_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkKeratoconus_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkKeratoconus_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblAnisometropia" runat="server" Text="Anisometropia" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkAnisometropia_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkAnisometropia_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkAnisometropia_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkAnisometropia_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblPtosis" runat="server" Text="Ptosis" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkPtosis_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkPtosis_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkPtosis_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkPtosis_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblNystagmus" runat="server" Text="Nystagmus" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkNystagmus_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkNystagmus_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkNystagmus_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkNystagmus_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblCorneadefects" runat="server" Text="Cornea defects" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkCorneadefects_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkCorneadefects_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkCorneadefects_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkCorneadefects_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblPuplidefects" runat="server" Text="Pupli defects" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkPuplidefects_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkPuplidefects_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkPuplidefects_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkPuplidefects_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblPterygium" runat="server" Text="Pterygium" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkPterygium_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkPterygium_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkPterygium_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkPterygium_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:Label ID="lblOther" runat="server" Text="Other" Width="300px" ></asp:Label></td>
                                                                    <td><asp:CheckBox ID="chkOther_RightEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkOther_RightEye_CheckedChanged" /></td>
                                                                    <td><asp:CheckBox ID="chkOther_LeftEye" runat="server" Width="200px" AutoPostBack="True" OnCheckedChanged="chkOther_LeftEye_CheckedChanged" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtDiagnosis_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" Width="200px"></asp:TextBox></td>
                                                                    <td><asp:TextBox ID="txtDiagnosis_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" Width="200px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="chkListRefractiveErrorOpt" runat="server" RepeatDirection="Vertical" Width="200px" >
                                                                            <asp:ListItem Value="0">Presbyopia</asp:ListItem>
                                                                            <asp:ListItem Value="1">Myopia</asp:ListItem>
                                                                            <asp:ListItem Value="2">Hypermetropia</asp:ListItem>
                                                                            <asp:ListItem Value="3">Astigmatism</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="chkListRefractiveErrorOpt_LeftEye" runat="server" RepeatDirection="Vertical" Width="200px" >
                                                                            <asp:ListItem Value="0">Presbyopia</asp:ListItem>
                                                                            <asp:ListItem Value="1">Myopia</asp:ListItem>
                                                                            <asp:ListItem Value="2">Hypermetropia</asp:ListItem>
                                                                            <asp:ListItem Value="3">Astigmatism</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </div>
                                                    </div>
                                                </div>   
                                            </div>
                                        </div>
<%--                                    <div runat="server" id="pnlDiagnosis" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Diagnosis</div>
                                        <div class="panel-body" runat="server">
                                      
                                            <div class="row">                                                        
                                                <div class="col-sm-6">
                                                    <div class="form-group">                                                         

                                                        
                                                        <asp:CheckBoxList ID="rdoDiagnosis" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoDiagnosis_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Normal</asp:ListItem>
                                                                    <asp:ListItem Value="1">Refractive Error</asp:ListItem>
                                                                    <asp:ListItem Value="17">Low Vision</asp:ListItem>
                                                                    <asp:ListItem Value="2">Needs Cycloplegic Refraction</asp:ListItem>
                                                                    <asp:ListItem Value="3">Squint Strabismus</asp:ListItem>
                                                                    <asp:ListItem Value="4">Lazy Eye Amblyopia</asp:ListItem>
                                                                    <asp:ListItem Value="5">Color blindness Achromatopsia</asp:ListItem>
                                                                    <asp:ListItem Value="6">Cataract</asp:ListItem>
                                                                    <asp:ListItem Value="7">Traumatic Cataract </asp:ListItem>
                                                                    <asp:ListItem Value="8">Keratoconus</asp:ListItem>
                                                                    <asp:ListItem Value="9">Anisometropia</asp:ListItem>
                                                                    <asp:ListItem Value="10">Ptosis</asp:ListItem>
                                                                    <asp:ListItem Value="11">Nystagmus </asp:ListItem>
                                                                    <asp:ListItem Value="12">Presbyopia</asp:ListItem>
                                                                    <asp:ListItem Value="14">Cornea defects</asp:ListItem>
                                                                    <asp:ListItem Value="15">Pupli defects</asp:ListItem>
                                                                    <asp:ListItem Value="16">Pterygium</asp:ListItem>
                                                                    <asp:ListItem Value="13">Other</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            <asp:TextBox ID="txtDiagnosis_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>

                                                    </div>
                                                </div>                                                           
                                            </div>

                                            <div id="DIVchkListRefractiveErrorOpt" style="display:none;" class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:CheckBoxList ID="chkListRefractiveErrorOpt" ClientIDMode="Static" runat="server">
                                                            <asp:ListItem Value="1" Text="Myopia"></asp:ListItem>                                                            
                                                            <asp:ListItem Value="2" Text="Hypermetropia"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Astigmatism"></asp:ListItem>                                                            
                                                        </asp:CheckBoxList>                                                        
                                                    </div>
                                                </div>
                                            </div>                                                                         
                                        </div>
                                    </div>--%>

                                    <div runat="server" id="pnlTreatment" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Treatment</div>
                                        <div class="panel-body" runat="server">
	                                        <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label for="rdoTreatment_Glasses">Glasses</label>
                                                        <asp:RadioButtonList ID="rdoTreatment_Glasses" runat="server" RepeatDirection="Vertical" Width="425px" TabIndex="52" AutoPostBack="True" OnSelectedIndexChanged="rdoTreatment_Glasses_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Glasses suggested</asp:ListItem>
                                                            <asp:ListItem Value="1">Glasses not suggested</asp:ListItem>
                                                            <asp:ListItem Value="2">Glasses not willing</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label for="Prescription">Prescription: </label>
                                                        <asp:CheckBoxList ID="chkMedicine" runat="server" TabIndex="53"></asp:CheckBoxList>
                                                        <asp:DropDownList ID="ddlMedicine" runat="server" CssClass="form-control" Visible="False"></asp:DropDownList>                                          
                                                    </div>
                                                </div>                                                                                              
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="pnlFamilyDetail" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Family Details</div>
                                        <div class="panel-body" runat="server">
	                                        <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblMotherName" runat="server" Text="Mother Name"></asp:Label>
                                                        <asp:TextBox ID="txtMotherName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="60" OnTextChanged="txtMotherName_TextChanged">                                                
                                                    </asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtMotherName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtMotherName" ValidChars=" " />

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblMotherCell" runat="server" Text="Mother's Cell"></asp:Label>
                                                        <asp:TextBox ID="txtMotherCell" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="12">                                                
                                                        </asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="txtMotherCell_MaskedEditExtender" Mask ="9999-9999999" runat="server" TargetControlID="txtMotherCell" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblFatherCell" runat="server" Text="Father's Cell"></asp:Label>
                                                        <asp:TextBox ID="txtFatherCell" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="12">                                                
                                                        </asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="txtFatherCell_MaskedEditExtender" Mask ="9999-9999999" runat="server" TargetControlID="txtFatherCell" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblAddress1" runat="server" Text="Address 1"></asp:Label>
                                                        <asp:TextBox ID="txtAddress1" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="40">                                                
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblAddress2" runat="server" Text="Address 2"></asp:Label>
                                                        <asp:TextBox ID="txtAddress2" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="40">                                                
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
                                                        <asp:TextBox ID="txtDistrict" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="20" OnTextChanged="txtDistrict_TextChanged">                                                
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblTown" runat="server" Text="Town"></asp:Label>
                                                        <asp:TextBox ID="txtTown" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="20" OnTextChanged="txtTown_TextChanged">                                                
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                                        <asp:TextBox ID="txtCity" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="20" OnTextChanged="txtCity_TextChanged">                                                
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div> 
                                        </div>
                                    </div>

                                    <div runat="server" id="pnlFollowupVisit" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Follow up visits</div>
                                        <div class="panel-body" runat="server">
                                             <div class="row">                                                          
                                                <div class="form-group">                                                    
                                                    <asp:RadioButton  ID="rdoDispenseGlassesFollowup" GroupName="FollowupVisit"  Text="Dispense Glasses" Checked="false" runat="server" CssClass="form-control no-border" TabIndex="54" />
                                                </div>
                                                 
                                                 <div class="form-group">
                                                     <asp:RadioButton  ID="rdoFollowupAfterSixMonths" GroupName="FollowupVisit"  Text="6 months" Checked="false" runat="server" CssClass="form-control no-border" TabIndex="55" />                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                                                        <div runat="server" id="pnlUploadPicture" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server" >Capture or Upload picture</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:CheckBox ID="chkNotRequired" runat="server" Text="Not Comfortable" TabIndex="44" />
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
                                                        <asp:FileUpload ID="btnBrowse" ClientIDMode="Static" runat="server" onchange="ShowPreview(this)" accept="image/*" multiple="false" CssClass="btn btn-default btn-sm" Width="105px" TabIndex="45" />
                                                        <asp:Label ID="lblFileUploadStudent" runat="server"></asp:Label>
                                                        &nbsp;<asp:Button ID="AddButton" runat="server"  Text="Load Image" CssClass="btn btn-default btn-sm" Visible="False" OnClick="AddButton_Click" TabIndex="46" />                                                
                                                    </div>
                                                </div>                                        
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="btnWebCam" runat="server" Text="Capture from Webcam" TabIndex="47" OnClick="btnWebCam_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">
                                                Capture Date </label>                                                
                                                <asp:TextBox ID="txtCaptureDate" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px" TabIndex="48"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtCaptureDate" runat="server" TargetControlID="txtCaptureDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>                                   
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureRemarks">
                                                Capture Remarks </label>
                                                <asp:TextBox ID="txtCaptureRemarks" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="120" TabIndex="49" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtCaptureRemarks" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtCaptureRemarks" ValidChars=" " />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnSaveImage" runat="server" OnClick="btnSaveImage_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Insert Image" TabIndex="50"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
										</div>	
	                                </div>


                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="OptometristButton">
                                            </label>
                                            <div class="form-group text-left">
                                                <asp:LinkButton ID="btnEdit" runat="server" Visible="false"   ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" OnClick="btnEdit_Click" TabIndex="56"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" OnClick="btnSave_Click" TabIndex="57"></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" Visible="false" OnClientClick="return confirm('Are you sure you want to Delete this record?');"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Delete" OnClick="btnDelete_Click" TabIndex="58"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" OnClick="btnAbort_Click" TabIndex="59"></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server"   CssClass="btn btn-default btn-sm"  Text="Refresh" OnClick="btnRefresh_Click" TabIndex="60"></asp:LinkButton>
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
                                                <asp:Button ID="btnConfirmYes" runat="server" OnClick="btnConfirmYes_Click"  Text="Yes" />
                                                <asp:Button ID="btnConfirmNo" runat="server" OnClick="btnConfirmNo_Click" Text="No" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Modal Popup -->
                                </div>
                            </div>
                        </div>
                            
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestIDPKID" runat="server" OnValueChanged="hfAutoRefTestIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfStudentIDPKID" runat="server" OnValueChanged="hfStudentIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfAutoRefTestTransID" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestTransDate" runat="server"/>
        <asp:HiddenField ID="hfLookupResultSchool" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSchool_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfImageBytes" Value="" ClientIDMode="Static" OnValueChanged="hfImageBytes_ValueChanged" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="rdoOldNewTest" />
        <asp:PostBackTrigger ControlID="btnEdit" />
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDelete" />
        <asp:PostBackTrigger ControlID="btnAbort" />
        <asp:PostBackTrigger ControlID="AddButton" />
        <asp:PostBackTrigger ControlID="rdoTreatment_Glasses" />

        <asp:PostBackTrigger ControlID="chkNormal_RightEye" />
        <asp:PostBackTrigger ControlID="chkNormal_LeftEye" />
        <asp:PostBackTrigger ControlID="chkRefractiveError_RightEye" />
        <asp:PostBackTrigger ControlID="chkRefractiveError_LeftEye" />
        <asp:PostBackTrigger ControlID="chkLowVision_RightEye" />
        <asp:PostBackTrigger ControlID="chkLowVision_LeftEye" />
        <asp:PostBackTrigger ControlID="chkNeedsCycloplegicRefraction_RightEye" />
        <asp:PostBackTrigger ControlID="chkNeedsCycloplegicRefraction_LeftEye" />
        <asp:PostBackTrigger ControlID="chkSquintStrabismus_RightEye" />
        <asp:PostBackTrigger ControlID="chkSquintStrabismus_LeftEye" />
        <asp:PostBackTrigger ControlID="chkLazyEyeAmblyopia_RightEye" />
        <asp:PostBackTrigger ControlID="chkLazyEyeAmblyopia_LeftEye" />
        <asp:PostBackTrigger ControlID="chkColorblindnessAchromatopsia_RightEye" />
        <asp:PostBackTrigger ControlID="chkColorblindnessAchromatopsia_LeftEye" />
        <asp:PostBackTrigger ControlID="chkCataract_RightEye" />
        <asp:PostBackTrigger ControlID="chkCataract_LeftEye" />
        <asp:PostBackTrigger ControlID="chkTraumaticCataract_RightEye" />
        <asp:PostBackTrigger ControlID="chkTraumaticCataract_LeftEye" />
        <asp:PostBackTrigger ControlID="chkKeratoconus_RightEye" />
        <asp:PostBackTrigger ControlID="chkKeratoconus_LeftEye" />
        <asp:PostBackTrigger ControlID="chkAnisometropia_RightEye" />
        <asp:PostBackTrigger ControlID="chkAnisometropia_LeftEye" />
        <asp:PostBackTrigger ControlID="chkPtosis_RightEye" />
        <asp:PostBackTrigger ControlID="chkPtosis_LeftEye" />
        <asp:PostBackTrigger ControlID="chkNystagmus_RightEye" />
        <asp:PostBackTrigger ControlID="chkNystagmus_LeftEye" />
        <asp:PostBackTrigger ControlID="chkCorneadefects_RightEye" />
        <asp:PostBackTrigger ControlID="chkCorneadefects_LeftEye" />
        <asp:PostBackTrigger ControlID="chkPuplidefects_RightEye" />
        <asp:PostBackTrigger ControlID="chkPuplidefects_LeftEye" />
        <asp:PostBackTrigger ControlID="chkPterygium_RightEye" />
        <asp:PostBackTrigger ControlID="chkPterygium_LeftEye" />
        <asp:PostBackTrigger ControlID="chkOther_RightEye" />
        <asp:PostBackTrigger ControlID="chkOther_LeftEye" />

    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
