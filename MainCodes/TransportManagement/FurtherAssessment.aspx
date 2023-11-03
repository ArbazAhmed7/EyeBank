﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FurtherAssessment.aspx.cs" Inherits="TransportManagement.FurtherAssessment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/FurtherAssessment.js"></script>  
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
                                    <h5 class="m-t-0 header-title"><b>Hospital visit for Further Assessment</b></h5>

                                    <hr />                                    
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdoType" runat="server" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="1" Visible="False">
                                                        <asp:ListItem Selected="True" Value="0">Student</asp:ListItem>
                                                        <asp:ListItem Value="1">Teacher</asp:ListItem>
                                                    </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="6">
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
                                                                <label for="StudentName">
                                                                Student Name *</label>
                                                                <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25" TabIndex="2"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtStudentName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txtStudentName" />
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentCode">
                                                                
                                                                Student Id </label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtStudentCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9" TabIndex="3"></asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupStudent" runat="server" OnClick="btnLookupStudent_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
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
                                                            <td><asp:Label ID="lblFatherName_Student"  runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label8"  runat="server" Text="Age" ></asp:Label></td>
                                                            <td><asp:Label ID="lblAge_Student"  runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label9"  runat="server" Text="Gender" ></asp:Label></td>
                                                            <td><asp:Label ID="lblGender_Student"  runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label10"  runat="server" Text="Class" ></asp:Label></td>
                                                            <td><asp:Label ID="lblClass_Student"  runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label11"  runat="server" Text="School" ></asp:Label></td>
                                                            <td><asp:Label ID="lblSchoolName_Student"  runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label12"  runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                            <td><asp:Label ID="lblWearingGlasses_Student"  runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label13"  runat="server" Text="Decreased Vision" ></asp:Label></td>
                                                            <td><asp:Label ID="lblDecreasedVision_Student"  runat="server" ></asp:Label></td>
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
                                                            <asp:TextBox ID="txtTeacherName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25" TabIndex="4"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtTeacherName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txtTeacherName" />
                                                        </div>                                                        
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="TeacherCode">Teacher Id </label>
                                                            <div class="input-group input-group-sm mb-3">
                                                                <asp:TextBox ID="txtTeacherCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9" TabIndex="5"></asp:TextBox>
                                                                <span class="input-group-append">                                    
                                                                    <asp:LinkButton ID="btnLookupTeacher" runat="server" ClientIDMode="Static" OnClick="btnLookupTeacher_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                                        <i class="fa fa-search"></i>
                                                                    </asp:LinkButton>
                                                                </span>
                                                                <asp:FilteredTextBoxExtender ID="txtTeacherCode_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtTeacherCode" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>                                               
                                            </div>

                                            <%--Right side--%>
                                            <div runat="server" id="pnlTeacher_Sub" class="col-xs-12 col-sm-6">
                                                <table class="auto-style2">
                                                    <tr>
                                                        <td><asp:Label ID="Label14"  runat="server" Text="Father / Spouse Name" ></asp:Label></td>
                                                        <td><asp:Label ID="lblFatherName_Teacher"  runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label15"  runat="server" Text="Age" ></asp:Label></td>
                                                        <td><asp:Label ID="lblAge_Teacher"  runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label16"  runat="server" Text="Gender" ></asp:Label></td>
                                                        <td><asp:Label ID="lblGender_Teacher"  runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label17"  runat="server" Text="School" ></asp:Label></td>
                                                        <td><asp:Label ID="lblSchoolName_Teacher"  runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label18"  runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                        <td><asp:Label ID="lblWearingGlasses_Teacher"  runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label20"  runat="server" Text="Decreased Vision" ></asp:Label></td>
                                                        <td><asp:Label ID="lblDecreasedVision_Teacher"  runat="server" ></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lblShowStudentDetail" runat="server"  Font-Names="Verdana" Font-Size="8pt" OnClick="lblShowStudentDetail_Click" Text="Show / Hide Detail" Visible="False"></asp:LinkButton>
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
                                                                        <asp:Label ID="Label1" runat="server" Text="Visit Date:" ></asp:Label>
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
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
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

                                    <div runat="server" id="pnlTestArea" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Test Information</div>
                                        <div class="panel-body" runat="server">
		                                <div class="row"> 
                                            <div class="col-xs-12 col-sm-6">
                                                
                                                <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="TransDate">
                                                                Test Date *</label>                                                
                                                                <asp:Label ID="lblTestDate" runat="server" ></asp:Label>
                                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" TabIndex="7" ></asp:TextBox>
                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtTestDate_MaskedEditExtender" runat="server" Mask="99/99/9999" TargetControlID="txtTestDate" AutoComplete="False" CultureName="ur-PK" MaskType="Date" />--%>
                                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                                </asp:CalendarExtender>

                                                                <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True" TabIndex="8" > </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                            </div>

                                            <div runat="server" id="Div1" class="col-xs-12 col-sm-6">
                                                <div class="row">
                                                    <div class="container">                                                
                                                         <div class="row">
                                                             <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label for="ddlHospital">Select Hospital</label>                                                                
                                                                    <asp:DropDownList ID="ddlHospital" CssClass="form-control form-control-sm" runat="server" TabIndex="7" OnSelectedIndexChanged="ddlHospital_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                         </div>

                                                         <div class="row">
                                                             <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label for="ddlHospital">Select Ophthalmologist</label>                                                                
                                                                    <asp:DropDownList ID="ddlDoctor" CssClass="form-control form-control-sm" runat="server" TabIndex="8"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                         </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--<div runat="server" id="Div1" class="col-xs-12 col-sm-6">
                                                 <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="DataFor">
                                                        
                                                                Results for Date: </label>
                                                                <asp:Label ID="lblResultDate" runat="server"  ></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                 <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="TotalEnrollments">Enrollments: </label>
                                                                <asp:Label ID="lblTotalEnrollments" runat="server"  ></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="TotalTestConducted">Auto Ref Test Conducted: </label>
                                                            <asp:Label ID="lblTotalTestConducted" runat="server"  ></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="TotalTestConducted">Optometrist Test Conducted: </label>
                                                                <asp:Label ID="lblTotalTestConducted" runat="server"  ></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                 <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="RemainingTest">Remaining Test: </label>
                                                                <asp:Label ID="lblRemainingTest" runat="server"  ></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                            </div>--%>

                                        <%--<div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                        
                                                    <asp:GridView ID="gvRemainingList" runat="server">
                                                    </asp:GridView>
                                                        
                                                </div>
                                            </div>
                                        </div>--%>

                                        </div>
                                        </div>    
                                    </div>
                                <%--</div>--%>


<%--                                    <div runat="server" id="pnlRightEye_AutoRef" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Auto Refraction Test Result</div>
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

                                
                               

                                    
                                    <div runat="server" id="pnlOptometristTest" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Optometrist Test</div>
                                        
                                            <div class="row">                                                
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chkChiefComplain"   Text="Chief Complain" Checked="false" runat="server" CssClass="form-control no-border" AutoPostBack="True" OnCheckedChanged="chkChiefComplain_CheckedChanged" TabIndex="9" >                                                
                                                    </asp:CheckBox>                                                                            
                                                    <asp:TextBox ID="txtChiefComplain" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" Width="800px" TabIndex="10"></asp:TextBox>
                                                </div>                                                    
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                        <asp:CheckBox ID="chkOccularHistory" Text="Occular History" Checked="false" runat="server" CssClass="form-control no-border" AutoPostBack="True" OnCheckedChanged="chkOccularHistory_CheckedChanged" TabIndex="11" >                                                
                                                        </asp:CheckBox>
                                                        <asp:TextBox ID="txtOccularHistory" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" Width="800px" TabIndex="12"></asp:TextBox>                                                                            
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chkMedicalHistory" Text="Medical History" Checked="false" runat="server" CssClass="form-control no-border" AutoPostBack="True" OnCheckedChanged="chkMedicalHistory_CheckedChanged" TabIndex="13" >                                                
                                                    </asp:CheckBox>
                                                    <asp:TextBox ID="txtMedicalHistory" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" Width="800px" TabIndex="14"></asp:TextBox>
                                                </div>
                                            </div> 
                                        </div>--%>

                                             
                                        <div id="aa" class="panel panel-primary">
		                                    <div class="panel-heading" runat="server" >
		                                              
                                                    <asp:RadioButtonList ID="rdoOptometristTest" runat="server" style="width:100%;" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdoOptometristTest_SelectedIndexChanged" Font-Bold="False" TabIndex="15">
                                                        <asp:ListItem style="color:#ffffff" Selected="True" Value="1">Visual Acuity</asp:ListItem>
                                                        <asp:ListItem style="color:#ffffff" Value="2">Subjective Refraction</asp:ListItem>
                                                        <asp:ListItem style="color:#ffffff" Value="3">Objective Refraction</asp:ListItem>
                                                        <asp:ListItem style="color:#ffffff" Value="4">Orthoptic Assessment</asp:ListItem>
                                                        <asp:ListItem style="color:#ffffff" Value="5">Diagnosis</asp:ListItem>
                                                        <asp:ListItem style="color:#ffffff" Value="6">Treatment</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                   
		                                    </div>
		                                </div>
                                             
                                        
                                        <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblTestName" runat="server"  Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    
                                  

                                    <%-- 1st Test Area --%>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6">
	                                        <div id="pnlTest1_RightEye" runat="server">
                                                <%--<div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="DistanceVision_RightEye"> </label>
                                                                    <asp:RadioButtonList ID="rdoGlassesType_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" >
                                                                        <asp:ListItem Selected="True" Value="0">Un-aided</asp:ListItem>
                                                                        <asp:ListItem Value="1">With Glasses</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="DistanceVision_RightEye"> Visual Acuity Distance </label>
                                                                <%--<label for="DistanceVision_RightEye"> Distance Vision </label>--%>
                                                                <table class="auto-style2">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label30" runat="server" Font-Bold="true" Text="Right"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label21" runat="server" Text="Un-aided"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label22" runat="server" Text="With Glasses"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label23" runat="server" Text="Pin Hole"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdoDistanceVision_RightEye_Unaided" runat="server" AutoPostBack="True" Width="125px" TabIndex="16">
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                            </asp:RadioButtonList>

                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdoDistanceVision_RightEye_WithGlasses" runat="server" AutoPostBack="True" Width="125px" TabIndex="17">
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdoDistanceVision_RightEye_PinHole" runat="server" AutoPostBack="True" Width="125px" TabIndex="18">
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="" Enabled="False">HM</asp:ListItem>
                                                                                <asp:ListItem Value="13" Text="" Enabled="False">PL</asp:ListItem>
                                                                                <asp:ListItem Value="14" Text="" Enabled="False">NPL</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">                                                                    		
                                                                <label for="VisualNearVision_RightEye"> Visual Acuity Near </label>
                                                                <%--<label for="NearVision_RightEye"> Near Vision </label>--%>
                                                                    <asp:RadioButtonList ID="rdoNearVision_RightEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" TabIndex="19" >
                                                                        <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value="0">N6</asp:ListItem>
                                                                        <asp:ListItem Value="1">N8</asp:ListItem>
                                                                        <asp:ListItem Value="2">N10</asp:ListItem>
                                                                        <asp:ListItem Value="3">N12</asp:ListItem>
                                                                        <asp:ListItem Value="4">N14</asp:ListItem>
                                                                        <asp:ListItem Value="5">N18</asp:ListItem>
                                                                        <asp:ListItem Value="6">Less then N18</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkNeedsCycloRefraction_RightEye" Text="Needs Cyclo Refraction" Checked="false" runat="server" CssClass="form-control no-border" TabIndex="20" Visible="False">                                                
                                                                </asp:CheckBox>                                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtNeedsCycloRefraction_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" TabIndex="21"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <%--<div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkAmblyphobia_RightEye" Text="Amblyphobia" Checked="false" runat="server" CssClass="form-control" Visible="False" AutoPostBack="True" OnCheckedChanged="chkAmblyphobia_RightEye_CheckedChanged" >                                                
                                                                </asp:CheckBox>                                                                    
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtAmblyphobia_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" Visible="False" MaxLength="200" Visible="False"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkPinhole_RightEye" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" Visible="False" >                                                
                                                                </asp:CheckBox>
                                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtPinhole_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>--%>
	                                        </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-6">
	                                        <div id="pnlTest1_LeftEye" runat="server">
                                                <%--<div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="DistanceVision_LeftEye"> </label>
                                                                    <asp:RadioButtonList ID="rdoGlassesType_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" >
                                                                        <asp:ListItem Selected="True" Value="0">Un-aided</asp:ListItem>
                                                                        <asp:ListItem Value="1">With Glasses</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="DistanceVision_LeftEye"> Visual Acuity Distance </label>
                                                                <%--<label for="DistanceVision_LeftEye"> Distance Vision </label>--%>
                                                                <table class="auto-style2">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label31" runat="server" Font-Bold="true" Text="Left"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label19" runat="server" Text="Un-aided"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label24" runat="server" Text="With Glasses"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label25" runat="server" Text="Pin Hole"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_Unaided" runat="server" AutoPostBack="True" Width="125px" TabIndex="22">
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                            </asp:RadioButtonList>

                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_WithGlasses" runat="server" AutoPostBack="True" Width="125px" TabIndex="23">
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_Pinhole" runat="server" AutoPostBack="True" Width="125px" TabIndex="24">
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                <asp:ListItem Value="12" Text="" Enabled="False">HM</asp:ListItem>
                                                                                <asp:ListItem Value="13" Text="" Enabled="False">PL</asp:ListItem>
                                                                                <asp:ListItem Value="14" Text="" Enabled="False">NPL</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="VisualNearVision_LeftEye"> Visual Acuity Near </label>
                                                                <%--<label for="NearVision_LeftEye"> Near Vision </label>--%>
                                                                    <asp:RadioButtonList ID="rdoNearVision_LeftEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" TabIndex="25" >
                                                                        <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value="0">N6</asp:ListItem>
                                                                        <asp:ListItem Value="1">N8</asp:ListItem>
                                                                        <asp:ListItem Value="2">N10</asp:ListItem>
                                                                        <asp:ListItem Value="3">N12</asp:ListItem>
                                                                        <asp:ListItem Value="4">N14</asp:ListItem>
                                                                        <asp:ListItem Value="5">N18</asp:ListItem>
                                                                        <asp:ListItem Value="6">Less then N18</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkNeedsCycloRefraction_LeftEye" Text="Needs Cyclo Refraction" Checked="false" runat="server" CssClass="form-control no-border" AutoPostBack="True" OnCheckedChanged="chkNeedsCycloRefraction_LeftEye_CheckedChanged" Visible="False" TabIndex="26">                                                
                                                                </asp:CheckBox>                                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtNeedsCycloRefraction_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" TabIndex="27"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkAmblyphobia_LeftEye" Text="Amblyphobia" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkAmblyphobia_LeftEye_CheckedChanged">                                                
                                                                </asp:CheckBox>                                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtAmblyphobia_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkPinhole_LeftEye" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" Visible="False">                                                
                                                                </asp:CheckBox>                                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtPinhole_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- 1st Test Area --%>



                                    <%-- 2nd Test Area --%>                                  
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6">
	                                        <div runat="server" id="pnlTest2_RightEye" class="panel panel-default">
                                                <div class="panel-heading" runat="server" >Right Eye</div>
                                                <div class="panel-body" runat="server">
                                                    <div class="row">
                                                        <div class="container">
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label for="Test2a">2a Test: Best Corrected Visual Acuity</label>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label for="Spherical_RightEye">Spherical *</label>
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                            <asp:DropDownList ID="ddlSpherical_RightEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="28" >
                                                                                <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                <asp:ListItem>Negative</asp:ListItem>
                                                                                <asp:ListItem>Plano</asp:ListItem>
                                                                                <asp:ListItem>Error</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <span class="input-group-append">
                                                                            <asp:TextBox ID="txtSpherical_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="29"></asp:TextBox>
                                                                            <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_RightEye" />--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_RightEye" ValidChars="." />
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
                                                                            <asp:DropDownList ID="ddlCyclinderical_RightEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="30">
                                                                                <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                <asp:ListItem>Negative</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <span class="input-group-append">
                                                                            <asp:TextBox ID="txtCyclinderical_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="31"></asp:TextBox>
                                                                            <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_RightEye" />--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_RightEye" ValidChars="." />
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
                                                                            <asp:TextBox ID="txtAxixA_RightEye" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="32" ></asp:TextBox>
                                                                            <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_RightEye" />--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_RightEye" />
                                                                            <span class="input-group-append">
                                                                            <asp:TextBox ID="txtAxixB_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                            <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixB_RightEye" />--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_RightEye" />
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
                                                                            <asp:DropDownList ID="ddlNear_RightEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="38" Enabled="False">
                                                                                <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <span class="input-group-append">
                                                                            <asp:TextBox ID="txtNear_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="39" ></asp:TextBox>
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
	                                        <div runat="server" id="pnlTest2_LeftEye" class="panel panel-default">
                                                <div class="panel-heading" runat="server" >Left Eye</div>
                                                <div class="panel-body" runat="server">
                                                    <div class="row">
                                                        <div class="container">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group"><label for="Test2a"> </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label for="Spherical_LeftEye">
                                                                    Spherical *</label>
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                            <asp:DropDownList ID="ddlSpherical_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="33" >
                                                                                <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                <asp:ListItem>Negative</asp:ListItem>
                                                                                <asp:ListItem>Plano</asp:ListItem>
                                                                                <asp:ListItem>Error</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <span class="input-group-append">
                                                                        <asp:TextBox ID="txtSpherical_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="34" ></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_LeftEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_LeftEye" ValidChars="." />
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
                                                                            <asp:DropDownList ID="ddlCyclinderical_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="35">
                                                                                <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                <asp:ListItem>Negative</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <span class="input-group-append">
                                                                        <asp:TextBox ID="txtCyclinderical_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="36"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_LeftEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_LeftEye" ValidChars="." />
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
                                                                            <asp:TextBox ID="txtAxixA_LeftEye" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="37"></asp:TextBox>
                                                                            <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_LeftEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_LeftEye" />--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_LeftEye" />
                                                                            <span class="input-group-append">
                                                                            <asp:TextBox ID="txtAxixB_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                            <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_LeftEye_MaskedEditExtender" runat="server" Mask="999" TargetControlID="txtAxixB_LeftEye" />--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_LeftEye" />
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
                                                                            <asp:DropDownList ID="ddlNear_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="40" Enabled="False">
                                                                                <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <span class="input-group-append">
                                                                        <asp:TextBox ID="txtNear_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="41" ></asp:TextBox>
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

                                    <div class="row">
                                        <div id="pnlTest2b" runat="server" class="container">
                                            <%--<div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="Douchrome">
                                                            2b Test: Douchrome Test
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="Douchrome_Type">
                                                            2b Test: Douchrome Test </label>
                                                            <asp:RadioButtonList ID="rdoDouchrome" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="425px" TabIndex="42">
                                                                <asp:ListItem Value="0">Over Corrected</asp:ListItem>
                                                                <asp:ListItem Value="1">Under Corrected</asp:ListItem>
                                                                <asp:ListItem Value="2" Selected="True">Same Intensity</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="Achromatopsia"> 2c Test: Color Blindness Test </label>
                                                            <asp:CheckBoxList ID="chkAchromatopsia" runat="server" AutoPostBack="True" TabIndex="43">
                                                                <asp:ListItem Value="0" Selected="True">Normal</asp:ListItem>
                                                                <asp:ListItem Value="1">Green</asp:ListItem>
                                                                <asp:ListItem Value="2">Red</asp:ListItem>
                                                                <asp:ListItem Value="3">Blue</asp:ListItem>
                                                                <asp:ListItem Value="4">Total CB</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="ObjectiveRefraction"> Objective Refraction required: </label>
                                                            <asp:CheckBox ID="chkObjectiveRefraction" runat="server" TabIndex="44" AutoPostBack="True" OnCheckedChanged="chkObjectiveRefraction_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="Achromatopsia_Type">
                                                            2c Test: Color Blindness Test </label>
                                                                                
                                                        </div>
                                                    </div>
                                                </div>--%>

                                        </div>
                                    </div>                                 
                                    <%-- 2nd Test Area --%>


                                    <%-- 3rd Test Area --%>
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
                                                                            <asp:RadioButtonList ID="rdoRetinoScopy_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" OnSelectedIndexChanged="rdoRetinoScopy_RightEye_SelectedIndexChanged" TabIndex="45" >
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
                                                                            <asp:CheckBoxList ID="rdoCycloplegicRefraction_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="46" >
                                                                                <asp:ListItem Value="0" Selected="True">With movement</asp:ListItem>
                                                                                <asp:ListItem Value="1">Against movement</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Condition_RightEye">
                                                                        Condition </label>
                                                                        <asp:TextBox ID="txtCondition_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="49"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Meridian1_RightEye">
                                                                        Meridian 1 </label>
                                                                        <asp:TextBox ID="txtMeridian1_RightEye" runat="server"  AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Meridian2_RightEye">
                                                                        Meridian 2 </label>
                                                                        <asp:TextBox ID="txtMeridian2_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="51"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="FinalPrescription_RightEye">
                                                                        Final Prescription </label>
                                                                        <asp:TextBox ID="txtFinalPrescription_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="52"></asp:TextBox>
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
                                                                            <asp:RadioButtonList ID="rdoRetinoScopy_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" OnSelectedIndexChanged="rdoRetinoScopy_LeftEye_SelectedIndexChanged" TabIndex="47" >
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
                                                                            <asp:CheckBoxList ID="rdoCycloplegicRefraction_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="48" >
                                                                                <asp:ListItem Value="0" Selected="True">With movement</asp:ListItem>
                                                                                <asp:ListItem Value="1">Against movement</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Condition_LeftEye">
                                                                        Condition </label>
                                                                        <asp:TextBox ID="txtCondition_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="53"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Meridian1_LeftEye">
                                                                        Meridian 1 </label>
                                                                        <asp:TextBox ID="txtMeridian1_LeftEye" runat="server"  AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="54"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Meridian2_LeftEye">
                                                                        Meridian 2 </label>
                                                                        <asp:TextBox ID="txtMeridian2_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="55"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="FinalPrescription_LeftEye">
                                                                        Final Prescription </label>
                                                                        <asp:TextBox ID="txtFinalPrescription_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="50" TabIndex="56"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
						                                </div>
					                                </div>
				                                </div>
			                                </div>
		                                </div>
                                    </div>
                                    <%-- 3rd Test Area --%>
                                     
                                     
                                    <table class="auto-style1">
                                        

                                       

                                        <%-- 4th Test Area --%>

                                        <tr>
                                            <td colspan="2">
                                                <table class="auto-style2">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnlTest4a" runat="server" GroupingText="">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label for="lblHirchberg"> 
                                                                            Test 4a: Hirchberg test </label>
                                                                                <table class="auto-style2">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label26" runat="server" Text="Distance Vision"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label27" runat="server" Text="Near Vision"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:RadioButtonList ID="rdoHirchberg_Distance" runat="server" AutoPostBack="True" Width="425px" TabIndex="57">
                                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="0">Central</asp:ListItem>
                                                                                                <asp:ListItem Value="1">5-10</asp:ListItem>
                                                                                                <asp:ListItem Value="2">10-15</asp:ListItem>
                                                                                                <asp:ListItem Value="3">15-20</asp:ListItem>
                                                                                                <asp:ListItem Value="4">25-30</asp:ListItem>
                                                                                                <asp:ListItem Value="5">30-35</asp:ListItem>
                                                                                                <asp:ListItem Value="6">35-40</asp:ListItem>
                                                                                                <asp:ListItem Value="7">40-45</asp:ListItem>
                                                                                                <asp:ListItem Value="8">45-50</asp:ListItem>
                                                                                                <asp:ListItem Value="9">&gt; 45</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButtonList ID="rdoHirchberg_Near" runat="server" AutoPostBack="True" Width="425px" TabIndex="58">
                                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="0">Central</asp:ListItem>
                                                                                                <asp:ListItem Value="1">5-10</asp:ListItem>
                                                                                                <asp:ListItem Value="2">10-15</asp:ListItem>
                                                                                                <asp:ListItem Value="3">15-20</asp:ListItem>
                                                                                                <asp:ListItem Value="4">25-30</asp:ListItem>
                                                                                                <asp:ListItem Value="5">30-35</asp:ListItem>
                                                                                                <asp:ListItem Value="6">35-40</asp:ListItem>
                                                                                                <asp:ListItem Value="7">40-45</asp:ListItem>
                                                                                                <asp:ListItem Value="8">45-50</asp:ListItem>
                                                                                                <asp:ListItem Value="9">&gt; 45</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>                                                        
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label for="lblOphthalmoscope_RightEye"> Test 4b:Opthalmoscope Red reflex test </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                            
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlOphthalmoscope_RightEye" runat="server" GroupingText="">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:RadioButtonList ID="rdoOphthalmoscope_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="59">
                                                                            <asp:ListItem Selected="True" Value="0">Red Glow Seens</asp:ListItem>
                                                                            <asp:ListItem Value="1">Dull Glow Seens</asp:ListItem>
                                                                            <asp:ListItem Value="2">Absent</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="lblPupillaryReactions_RightEye"> Test 4c: Pupillary Reactions </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="lblPathology_RightEye"> </label>
                                                                            <asp:RadioButtonList ID="rdoPupillaryReactions_RightEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="61">
                                                                                <asp:ListItem Selected="True" Value="0">RRR</asp:ListItem>
                                                                                <asp:ListItem Value="1">APD</asp:ListItem>
                                                                                <asp:ListItem Value="2">RAPD</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="CoverUncovertTest_RightEye"> Test 4d: Cover Uncover Test </label>
                                                                            <asp:RadioButtonList ID="rdoCoverUncovertTest_RightEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" OnSelectedIndexChanged="rdoCoverUncovertTest_RightEye_SelectedIndexChanged" TabIndex="63" >
                                                                                <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                                <asp:ListItem Selected="True" Value="0">Orthophoria</asp:ListItem>
                                                                                <asp:ListItem Value="1">Esophoria</asp:ListItem>
                                                                                <asp:ListItem Value="2">Exophoria</asp:ListItem>
                                                                                <asp:ListItem Value="3">Esotropia</asp:ListItem>
                                                                                <asp:ListItem Value="4">Exotropia</asp:ListItem>
                                                                                <asp:ListItem Value="5">Hypotropia</asp:ListItem>
                                                                                <asp:ListItem Value="6">Hypertropia</asp:ListItem>
                                                                                <asp:ListItem Value="7">Others, pls specify</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <asp:TextBox ID="txtCoverUncovertTestRemarks_RightEye" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        
                                                        </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlOphthalmoscope_LeftEye" runat="server" GroupingText="">
                                                    <%--<div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="lblOphthalmoscope_LeftEye"> </label>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <asp:RadioButtonList ID="rdoOphthalmoscope_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="60">
                                                                    <asp:ListItem Selected="True" Value="0">Red Glow Seens</asp:ListItem>
                                                                    <asp:ListItem Value="1">Dull Glow Seens</asp:ListItem>
                                                                    <asp:ListItem Value="2">Absent</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="lblPupillaryReactions_LeftEye">.</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="lblPathology_LeftEye"> </label>
                                                                    <asp:RadioButtonList ID="rdoPupillaryReactions_LeftEye" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="62">
                                                                        <asp:ListItem Selected="True" Value="0">RRR</asp:ListItem>
                                                                        <asp:ListItem Value="1">APD</asp:ListItem>
                                                                        <asp:ListItem Value="2">RAPD</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="CoverUncovertTest_LeftEye">.</label>
                                                                    <asp:RadioButtonList ID="rdoCoverUncovertTest_LeftEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" OnSelectedIndexChanged="rdoCoverUncovertTest_LeftEye_SelectedIndexChanged" TabIndex="64" >
                                                                        <asp:ListItem Value="-1" Text="">N/A</asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value="0">Orthophoria</asp:ListItem>
                                                                        <asp:ListItem Value="1">Esophoria</asp:ListItem>
                                                                        <asp:ListItem Value="2">Exophoria</asp:ListItem>
                                                                        <asp:ListItem Value="3">Esotropia</asp:ListItem>
                                                                        <asp:ListItem Value="4">Exotropia</asp:ListItem>
                                                                        <asp:ListItem Value="5">Hypotropia</asp:ListItem>
                                                                        <asp:ListItem Value="6">Hypertropia</asp:ListItem>
                                                                        <asp:ListItem Value="7">Others, pls specify</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:TextBox ID="txtCoverUncovertTestRemarks_LeftEye" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlTest4e_RightEye" runat="server" GroupingText="">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label for="ExtraOccularMuscle_RightEye"> Test 4e: Extra Occular Muscle </label>
                                                                            <asp:TextBox ID="txtExtraOccularMuscleRemarks_RightEye" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200" TabIndex="65"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlTest4e_LeftEye" runat="server" GroupingText="">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label for="ExtraOccularMuscle_RightEye"> Extra Occular Muscle </label>
                                                                            <asp:TextBox ID="txtExtraOccularMuscleRemarks_LeftEye" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="200" TabIndex="66"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                        </td>
                                                    </tr>
                                                </table>


                                                    
                                            </td>
                                        </tr>

                                        <%-- 4th Test Area --%>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlDiagnosis" runat="server" GroupingText="">

                                                    <div runat="server" id="Panel1" class="panel panel-primary">
                                            <div class="panel-heading" runat="server" >Diagnosis</div>
                                            <div class="panel-body" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-9">
                                                        <div class="form-group">
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td><asp:Label ID="Label4" runat="server" Text="Right" Width="300px" Font-Bold="True" ></asp:Label></td>
                                                                    <td><asp:Label ID="Label5" runat="server" Text="Left" Width="200px" Font-Bold="True" ></asp:Label></td>
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
                                                                    <td><asp:Label ID="Label6" runat="server" Text="Other" Width="300px" ></asp:Label></td>
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
                                                                        <asp:RadioButtonList ID="rdoSubDiagnosis_RightEye" runat="server" RepeatDirection="Vertical" Width="200px" >
                                                                            <asp:ListItem Value="0">Presbyopia</asp:ListItem>
                                                                            <asp:ListItem Value="1">Myopia</asp:ListItem>
                                                                            <asp:ListItem Value="2">Hypermetropia</asp:ListItem>
                                                                            <asp:ListItem Value="3">Astigmatism</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoSubDiagnosis_LeftEye" runat="server" RepeatDirection="Vertical" Width="200px" >
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

                                        <%--                                                    <div runat="server" id="Panel1" class="panel panel-primary">
                                                        <div class="panel-heading" runat="server" >Diagnosis</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="Diagnosis_RightEye"> </label>
                                                                        <asp:CheckBoxList ID="rdoDiagnosis_RightEye" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoDiagnosis_RightEye_SelectedIndexChanged">
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
                                                                                <asp:ListItem Value="14">Cornea defects</asp:ListItem>
                                                                                <asp:ListItem Value="15">Pupli defects</asp:ListItem>
                                                                                <asp:ListItem Value="16">Pterygium</asp:ListItem>
                                                                                <asp:ListItem Value="13">Other</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        <asp:TextBox ID="txtDiagnosis_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                            <asp:RadioButtonList ID="rdoSubDiagnosis_RightEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" >
                                                                                <asp:ListItem Value="0">Presbyopia</asp:ListItem>
                                                                                <asp:ListItem Value="1">Myopia</asp:ListItem>
                                                                                <asp:ListItem Value="2">Hypermetropia</asp:ListItem>
                                                                                <asp:ListItem Value="3">Astigmatism</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>                                                 
                                                        </div>
                                                    </div>--%>

                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlTreatment" runat="server" GroupingText="">
                                                <div runat="server" id="pnl2" class="panel panel-primary">
                                                <div class="panel-heading" runat="server" >Treatment</div>
                                                <div class="panel-body" runat="server">
	                                                <div class="row">
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="rdoTreatment_Glasses">Glasses</label>
                                                                <asp:RadioButtonList ID="rdoTreatment_Glasses" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" OnSelectedIndexChanged="rdoTreatment_Glasses_SelectedIndexChanged">
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
                                                                <asp:TextBox ID="txtMedicine" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="500"></asp:TextBox>
                                                            </div>
                                                        </div>  
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="Prescription">Duration (Days): </label>
                                                                <asp:TextBox ID="txtFrequency" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="3"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtFrequency_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtFrequency" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group text-left">
                                                                <asp:LinkButton ID="btnAddMedicine" runat="server" OnClick="btnAddMedicine_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Add Medicine" TabIndex="67"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">                        
                                                        <div class="col-md-12">
                                                            <div class="card-box table-responsive">
                                                                <hr />
                                                                <asp:GridView ID="gvMedicine" ClientIDMode="Static" runat="server" AutoGenerateColumns="false"
                                                                    CssClass="table table-striped table-bordered m-b-0" AllowPaging="true" PageSize="5" DataKeyNames="ID" OnRowDeleting="gvMedicine_RowDeleting"  >
                                                                    <Columns>                                   
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>      
                                                                                <asp:LinkButton ID="lnkDelete" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" runat="server" CommandName="Delete" /> 
                                                                            </ItemTemplate>     
                                                                        </asp:TemplateField>                                               
                                                                        <asp:TemplateField HeaderText="Medicine">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Prescription" runat="server" Text='<%# Eval("Medicine") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Duration (Days)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Form" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                    <div runat="server" id="pnlVisit1" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Next Visit</div>
                                        <div class="panel-body" runat="server">
	                                        <div class="row">
                                                <asp:RadioButtonList ID="rdoNextVisit" runat="server" AutoPostBack="True" RepeatDirection="Vertical" Width="425px" OnSelectedIndexChanged="rdoNextVisit_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Cycloplegic Refraction</asp:ListItem>
                                                    <asp:ListItem Value="1">Provide Glasses</asp:ListItem>
                                                    <asp:ListItem Value="4">6 Months</asp:ListItem>
                                                    <asp:ListItem Value="5">1 Year</asp:ListItem>
                                                    <asp:ListItem Value="3">Refer to Hospital</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="row">
                                              <div class="col-xs-12 col-sm-6">
	                                            <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:RadioButtonList ID="rdoSurgery" runat="server" RepeatDirection="Vertical"  AutoPostBack="True" OnSelectedIndexChanged="rdoSurgery_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Fundoscopy</asp:ListItem>
                                                            <asp:ListItem Value="1">Squint Assessment</asp:ListItem>
                                                            <asp:ListItem Value="2">Further Assessment</asp:ListItem>
                                                            <asp:ListItem Value="3">Surgery</asp:ListItem>
                                                        </asp:RadioButtonList>                                           
                                                    </div>
                                                </div>
                                              </div>
                                              <div class="col-xs-12 col-sm-6">

                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                            <asp:RadioButtonList ID="rdoSurgery_Detail" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="rdoSurgery_Detail_SelectedIndexChanged" AutoPostBack="True" Visible="False">
                                                            <asp:ListItem Value="0">Cataract</asp:ListItem>
                                                            <asp:ListItem Value="1">Squint</asp:ListItem>
                                                            <asp:ListItem Value="2">Pterygium</asp:ListItem>
                                                            <asp:ListItem Value="3">Corneal defect</asp:ListItem>
                                                            <asp:ListItem Value="4">Ptosis</asp:ListItem>
                                                            <asp:ListItem Value="5">Keratoconus</asp:ListItem>
                                                            <asp:ListItem Value="6">Chalazia</asp:ListItem>
                                                            <asp:ListItem Value="7">Hordeola</asp:ListItem>
                                                            <asp:ListItem Value="8">Others</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:TextBox ID="txtSurgery_Detail" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False"></asp:TextBox>
                                                    </div>
                                                </div>
	                                            
                                              </div>
                                            </div>

                                            <div class="row">
                                                <asp:RadioButtonList ID="rdoReferal" runat="server" RepeatDirection="Vertical" Width="425px" Visible="False" >
                                                    <asp:ListItem Value="0">6 Months</asp:ListItem>
                                                    <asp:ListItem Value="1">1 Year</asp:ListItem>
                                                </asp:RadioButtonList> 
                                            </div>
                                        </div>
                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlTestSummary" style="line-height:3px" runat="server" GroupingText="">
                                                    <table class="auto-style2">
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblAutoRefResult" runat="server" Text="Autoref Results"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRight" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAutoRef_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblVisualAcuity" runat="server" Text="Visual Acuity Distance"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblUnaided" runat="server" Text="Un-aided"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Unaided_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Unaided_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblWithGlasses" runat="server" Text="With Glasses"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_WithGlasses_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_WithGlasses_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblPinHole" runat="server" Text="Pin Hole"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_PinHole_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblVisualAcuityNear" runat="server" Text="Visual Acuity Near"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuityNear_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                 <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Near_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNeedsCycloRefraction" runat="server" Text="Needs Cyclo Refraction"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction" runat="server" Text="Subjective Refraction"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblDistance" runat="server" Text="Visual Acuity - Distance"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistance_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistance_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblNearAdd" runat="server" Text="Near add"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNearAdd_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNearAdd_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblDouchromeTest" runat="server" Text="Douchrome Test"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy" runat="server" Text="Retinoscopy"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>    
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>   
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCondition" runat="server" Text="Condition"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCondition_Remarks_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCondition_Remarks_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>    
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblFinalPresentation" runat="server" Text="Final Presentation"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblFinalPresentation_Results_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOrthopticAssessment" runat="server" Text="Orthoptic Assessment"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                     <asp:Label ID="lblOrthopticAssessment_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOrthopticAssessment_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblOrthopticAssessment_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblHirschberg" runat="server" Text="Hirschberg"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblHirschberg_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblHirschberg_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRedGlow" runat="server" Text="Red Glow"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRedGlow_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblRedGlow_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblPupilReflex" runat="server" Text="Pupil Reflex"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblPupilReflex_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblPupilReflex_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCoverUnCoverTest" runat="server" Text="Cover Un Cover Test"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCoverUnCoverTest_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblCoverUnCoverTest_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOther" runat="server" Text="Other"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
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
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle" runat="server" Text="Extra Occular Muscle"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblExtraOccularMuscle_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_Remarks" runat="server" Text="Remarks"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-9">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblExtraOccularMuscle_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>--%>

                                    </table>
                                

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group text-right">
                                                <asp:LinkButton ID="btnMovePrevious" runat="server" OnClick="btnMovePrevious_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Move Previous" TabIndex="67"></asp:LinkButton>
                                                <asp:LinkButton ID="btnMoveNext" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="btnMoveNext_Click" Text="Move Next" TabIndex="68"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="OptometristButton">
                                            </label>
                                            <div class="form-group text-left">
                                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" TabIndex="69"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" TabIndex="70"></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" TabIndex="71"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" TabIndex="72"></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="btnRefresh_Click" Text="Refresh" TabIndex="73"></asp:LinkButton>
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
        <%--<asp:HiddenField ID="hfOptometristIDPKID" runat="server" OnValueChanged="hfOptometristIDPKID_ValueChanged" Value="0" />--%>
        <asp:HiddenField ID="hfStudentIDPKID" runat="server" OnValueChanged="hfStudentIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfTeacherIDPKID" runat="server" OnValueChanged="hfTeacherIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfAutoRefTestTransID" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestTransDate" runat="server"/>
        <asp:HiddenField ID="hfLookupResultTeacher" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultTeacher_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="rdoType" />
        <asp:PostBackTrigger ControlID="rdoOldNewTest" />
        <asp:PostBackTrigger ControlID="rdoOptometristTest" />

        <asp:PostBackTrigger ControlID="lblShowStudentDetail" />
        <asp:PostBackTrigger ControlID="ddlPreviousTestResult" />

        <asp:PostBackTrigger ControlID="btnEdit" />
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDelete" />
        <asp:PostBackTrigger ControlID="btnAbort" />
        <asp:PostBackTrigger ControlID="btnMoveNext" />

        <asp:PostBackTrigger ControlID="chkNeedsCycloRefraction_RightEye" />
        <asp:PostBackTrigger ControlID="chkNeedsCycloRefraction_LeftEye" />

        <asp:PostBackTrigger ControlID="rdoCoverUncovertTest_RightEye" />
        <asp:PostBackTrigger ControlID="rdoCoverUncovertTest_LeftEye" />

        <asp:PostBackTrigger ControlID="ddlSpherical_RightEye" />
        <asp:PostBackTrigger ControlID="ddlCyclinderical_RightEye" />
        <asp:PostBackTrigger ControlID="ddlNear_RightEye" />
        <asp:PostBackTrigger ControlID="ddlSpherical_LeftEye" />
        <asp:PostBackTrigger ControlID="ddlCyclinderical_LeftEye" />
        <asp:PostBackTrigger ControlID="ddlNear_LeftEye" />

        <%--<asp:PostBackTrigger ControlID="rdoDiagnosis_RightEye" />--%>

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

        <asp:PostBackTrigger ControlID="rdoNextVisit" />

        <asp:PostBackTrigger ControlID="rdoSurgery" />
        <asp:PostBackTrigger ControlID="rdoSurgery_Detail" />

    </Triggers>
</asp:UpdatePanel>
</asp:Content>
