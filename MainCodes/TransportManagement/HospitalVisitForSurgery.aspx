﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="HospitalVisitForSurgery.aspx.cs" Inherits="TransportManagement.HospitalVisitForSurgery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/HospitalVisitForSurgery.js"></script>
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
                                    <h5 class="m-t-0 header-title"><b>Hospital visit for Surgery</b></h5>
                                    <hr />                                                                     
                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">Date *</label>                                                
                                                <asp:Label ID="lblTestDate" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" ></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>   --%>                                  
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdoType" runat="server" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True">
                                                        <asp:ListItem Selected="True" Value="0">Student</asp:ListItem>
                                                        <asp:ListItem Value="1">Teacher</asp:ListItem>
                                                    </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="4">
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
                                                                <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="60"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="SchoolCode">School Code *</label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtSchoolCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength ="3" AutoComplete="off" Enabled="False" ></asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupSchool" runat="server" ClientIDMode="Static"  OnClick="btnLookupSchool_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
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
                                                                <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtStudentName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txtStudentName" />
                                                            </div>                                                            
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentCode">                                                                
                                                                Student Id </label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtStudentCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9">                                                
                                                                    </asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupStudent" runat="server"   ClientIDMode="Static"  OnClick="btnLookupStudent_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
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

                                    <div runat="server" id="pnlTeacher" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Teacher Information</div>
                                        <div class="panel-body" runat="server">
                                        <div class="row">
                                            <%--left side--%>
                                            <div class="col-xs-12 col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="TeacherName">Teacher Name *</label>
                                                            <asp:TextBox ID="txtTeacherName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25">                                                
                                                        </asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtTeacherName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtTeacherName" ValidChars=" " />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="TeacherCode">Teacher Id </label>
                                                            <div class="input-group input-group-sm mb-3">
                                                                <asp:TextBox ID="txtTeacherCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9">                                                
                                                                </asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtTeacherCode_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtTeacherCode" />
                                                                <span class="input-group-append">                                    
                                                                    <asp:LinkButton ID="btnLookupTeacher" runat="server" ClientIDMode="Static" OnClick="btnLookupTeacher_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                                        <i class="fa fa-search"></i>
                                                                    </asp:LinkButton>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>                                                    
                                                </div>                                               
                                            </div>

                                            <%--Right side--%>
                                            <div runat="server" id="pnlTeacher_Sub" class="col-xs-12 col-sm-6">
                                                <table class="auto-style2">
                                                    <tr>
                                                        <td><asp:Label ID="Label17"  runat="server" Text="Father / Spouse Name" ></asp:Label></td>
                                                        <td><asp:Label ID="lblFatherName_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label18"  runat="server" Text="Age" ></asp:Label></td>
                                                        <td><asp:Label ID="lblAge_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label19"  runat="server" Text="Gender" ></asp:Label></td>
                                                        <td><asp:Label ID="lblGender_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label20"  runat="server" Text="School" ></asp:Label></td>
                                                        <td><asp:Label ID="lblSchoolName_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label21"  runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                        <td><asp:Label ID="lblWearingGlasses_Teacher" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label22"  runat="server" Text="Decreased Vision" ></asp:Label></td>
                                                        <td><asp:Label ID="lblDecreasedVision_Teacher" runat="server" ></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        </div>
                                    </div>

                                                                        <table class="auto-style1">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlTestSummary" style="line-height:3px" runat="server" GroupingText="Visit Summary">
                                                    <table class="auto-style2">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label26" runat="server" Text="Visit Date:" ></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSubjectiveRefPrevVisitDate" runat="server"  ></asp:Label>
                                                                     </div>
                                                                </div>

                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblAutoRefResult" runat="server" Text="Autoref Results"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblRight" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblLeft" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAutoRef_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAutoRef_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblVisualAcuity" runat="server" Text="Visual Acuity Distance"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblUnaided" runat="server" Text="Un-aided"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Unaided_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Unaided_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblWithGlasses" runat="server" Text="With Glasses"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_WithGlasses_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_WithGlasses_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblPinHole" runat="server" Text="Pin Hole"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_PinHole_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_PinHole_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblVisualAcuityNear" runat="server" Text="Visual Acuity Near"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuityNear_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuityNear_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                 <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Near_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Near_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNeedsCycloRefraction" runat="server" Text="Needs Cyclo Refraction"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                 <div class="form-group">
                                                                    <asp:Label ID="lblNeedsCycloRefraction_Status" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction" runat="server" Text="Subjective Refraction"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblDistance" runat="server" Text="Visual Acuity - Distance"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistance_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistance_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblNearAdd" runat="server" Text="Near add"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>  
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblDouchromeTest" runat="server" Text="Douchrome Test"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDouchromeTest_Result" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblObjectiveRefraction" runat="server" Text="Objective Refraction"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>  
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy" runat="server" Text="Retinoscopy"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>    
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>   
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCondition" runat="server" Text="Condition"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCondition_Remarks_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCondition_Remarks_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>    
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblFinalPresentation" runat="server" Text="Final Presentation"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblFinalPresentation_Results_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblFinalPresentation_Results_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td> 
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOrthopticAssessment" runat="server" Text="Orthoptic Assessment"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                     <asp:Label ID="lblOrthopticAssessment_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblOrthopticAssessment_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOrthopticAssessment_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblOrthopticAssessment_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblHirschberg" runat="server" Text="Hirschberg"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblHirschberg_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblHirschberg_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRedGlow" runat="server" Text="Red Glow"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRedGlow_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblRedGlow_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblPupilReflex" runat="server" Text="Pupil Reflex"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblPupilReflex_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblPupilReflex_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCoverUnCoverTest" runat="server" Text="Cover Un Cover Test"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCoverUnCoverTest_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblCoverUnCoverTest_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOther" runat="server" Text="Other"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOtherRemarks" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle" runat="server" Text="Extra Occular Muscle"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblExtraOccularMuscle_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_Remarks" runat="server" Text="Remarks"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblExtraOccularMuscle_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblDiag" runat="server" Text="Diagnosis"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblDiagnosis" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblDiagnosisRemarks" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSubDiagnosis" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblNext" runat="server" Text="NextVisit"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblNextVisit" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSurgery" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSurgeryDetail" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                        <asp:Label ID="lblSurgeryDetailRemarks" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>

                                    </table>


                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="TransDate">
                                                        Surgery Date *</label>                                                
                                                        <asp:Label ID="Label14" runat="server" ></asp:Label>
                                                        <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="5" AutoComplete="off" Width="125px" ></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                        </asp:CalendarExtender>
                                                        <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True" TabIndex="6" > </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div> 

                                            <div class="row">
                                                <div class="container">                                                
                                                     <div class="row">
                                                         <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="ddlHospital">Select Hospital</label>                                                                
                                                                <asp:DropDownList ID="ddlHospital" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHospital_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                     </div>

                                                    <div class="row">
                                                         <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="ddlOphthalmologist">Select Ophthalmologist</label>                                                                
                                                                <asp:DropDownList ID="ddlOphthalmologist" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                     </div>

                                                    <div class="row">
                                                         <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="ddlOrthoptist">Select Orthoptist</label>                                                                
                                                                <asp:DropDownList ID="ddlOrthoptist" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                     </div>

                                                    <div class="row">
                                                         <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="ddlSurgeon">Select Surgeon</label>                                                                
                                                                <asp:DropDownList ID="ddlSurgeon" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                         <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="ddlHospital">Select Optometrist</label>                                                                
                                                                <asp:DropDownList ID="ddlOptometrist" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                     </div>
                                                </div>
                                            </div>

                                    <div runat="server" id="pnl_Nameofsurgery" class="panel panel-primary">
                                        <div class="panel-heading" runat="server">Name of surgery</div>
                                        <div class="panel-body" runat="server">
                                            <div class="row">
                                                
                                                <div class="container">   
                                                    <div class="row">
                                                        <div class="col col-lg-4">
                                                            <b>Right</b>
                                                        </div>
                                                        <div class="col col-lg-4">
                                                            <b>Left</b>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col col-lg-4">                                                            
                                                            <asp:CheckBoxList ID="chkListSurgeryRight" runat="server"  TabIndex="24">
                                                                <asp:ListItem Value="0" Text="">Cataract</asp:ListItem>
                                                                <asp:ListItem Value="1" Text="">Squint</asp:ListItem>
                                                                <asp:ListItem Value="2" Text="">Pterygium</asp:ListItem>
                                                                <asp:ListItem Value="3" Text="">Corneal defect</asp:ListItem>
                                                                <asp:ListItem Value="4" Text="">Ptosis</asp:ListItem>
                                                                <asp:ListItem Value="5" Text="">Keratoconus</asp:ListItem>
                                                                <asp:ListItem Value="6" Text="">Chalazia</asp:ListItem>
                                                                <asp:ListItem Value="7" Text="">Hordeola</asp:ListItem>
                                                                <asp:ListItem Value="8" Text="">Others</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <div id="DivtxtOtherSurgeryRight" style="display:none;">
                                                                <asp:TextBox ID="txtOtherSurgeryRight"  CssClass="form-control form-control-sm" MaxLength="500" runat="server"></asp:TextBox>
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="col col-lg-4">
                                                            <asp:CheckBoxList ID="chkListSurgeryLeft" runat="server" TabIndex="26">
                                                                <asp:ListItem Value="0" Text="">Cataract</asp:ListItem>
                                                                <asp:ListItem Value="1" Text="">Squint</asp:ListItem>
                                                                <asp:ListItem Value="2" Text="">Pterygium</asp:ListItem>
                                                                <asp:ListItem Value="3" Text="">Corneal defect</asp:ListItem>
                                                                <asp:ListItem Value="4" Text="">Ptosis</asp:ListItem>
                                                                <asp:ListItem Value="5" Text="">Keratoconus</asp:ListItem>
                                                                <asp:ListItem Value="6" Text="">Chalazia</asp:ListItem>
                                                                <asp:ListItem Value="7" Text="">Hordeola</asp:ListItem>
                                                                <asp:ListItem Value="8" Text="">Others</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <div id="DivtxtOtherSurgeryLeft" style="display:none;" >
                                                                <asp:TextBox ID="txtOtherSurgeryLeft" CssClass="form-control form-control-sm" MaxLength="500" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>                                                    
                                                </div> 
                                                <div class="container">
                                                    <div class="row">
                                                       <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="txtSquintDiagOthers">Comments by Surgeon after surgery</label>
                                                                <asp:TextBox ID="txtCommentsSurgeon" runat="server" CssClass="form-control form-control-sm" MaxLength="120" TabIndex="222"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>                                                     
                                                </div>                                                 
                                            </div>
                                        </div>
                                    </div>                                                                        
                                    
                                    <div runat="server" id="pnlFollowupVisit" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Follow up visits after surgery</div>
                                        <div class="panel-body" runat="server">
                                             <div class="row">
                                                 <div class="container">
                                                     <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="TransDate">Date</label>                                                
                                                                <asp:Label ID="Label1" runat="server" ></asp:Label>
                                                                <asp:TextBox ID="txtFollowupDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" ></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFollowupDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>       
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
                                                        <asp:CheckBox ID="chkNotRequired" runat="server" Text="Not Comfortable" TabIndex="10" />
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
                                                        <asp:FileUpload ID="btnBrowse" ClientIDMode="Static" runat="server" onchange="ShowPreview(this)" accept="image/*" multiple="false" CssClass="btn btn-default btn-sm" Width="105px" TabIndex="11" />
                                                        <asp:Label ID="lblFileUploadStudent" runat="server"></asp:Label>
                                                        &nbsp;<asp:Button ID="AddButton" runat="server"  Text="Load Image" CssClass="btn btn-default btn-sm" Visible="False" OnClick="AddButton_Click" TabIndex="12" />                                                
                                                    </div>
                                                </div>                                        
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="btnWebCam" runat="server" Text="Capture from Webcam" TabIndex="13"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">
                                                Capture Date </label>                                                
                                                <asp:TextBox ID="txtCaptureDate" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px" TabIndex="14"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtCaptureDate" runat="server" TargetControlID="txtCaptureDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>                                   
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureRemarks">
                                                Capture Remarks </label>
                                                <asp:TextBox ID="txtCaptureRemarks" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="120" TabIndex="15" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtCaptureRemarks" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtCaptureRemarks" ValidChars=" " />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnSaveImage" runat="server" OnClick="btnSaveImage_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Insert Image" TabIndex="16"></asp:LinkButton>
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
                                                <asp:LinkButton ID="btnEdit" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" OnClick="btnEdit_Click" TabIndex="20"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" OnClick="btnSave_Click" TabIndex="21"></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" OnClick="btnDelete_Click" TabIndex="22"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" OnClick="btnAbort_Click" TabIndex="23"></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server"  CssClass="btn btn-default btn-sm"  Text="Refresh" OnClick="btnRefresh_Click" TabIndex="24"></asp:LinkButton>
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
        <asp:HiddenField ID="hfTeacherIDPKID" runat="server" OnValueChanged="hfTeacherIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfAutoRefTestTransID" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestTransDate" runat="server"/>
        <asp:HiddenField ID="hfLookupResultSchool" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSchool_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultTeacher" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultTeacher_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfImageBytes" Value="" ClientIDMode="Static" OnValueChanged="hfImageBytes_ValueChanged" runat="server" />
    </ContentTemplate>
    <Triggers>

        <asp:PostBackTrigger ControlID="rdoOldNewTest" />

        <asp:PostBackTrigger ControlID="btnEdit" />
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDelete" />
        <asp:PostBackTrigger ControlID="btnAbort" />
        <asp:PostBackTrigger ControlID="AddButton" />
        <asp:PostBackTrigger ControlID="rdoType" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
