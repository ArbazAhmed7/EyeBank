<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Treatment.aspx.cs" Inherits="TransportManagement.Treatment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/Treatment.js"></script>    
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
        .auto-style2 {
            width: 52px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
    <ContentTemplate>
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">                        
                        <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>Diagnosis / Treatment / Next Visit</b></h5>
                                 
                                    <hr />

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdoType" runat="server" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True">
                                                        <asp:ListItem Selected="True" Value="0">Student</asp:ListItem>
                                                        <asp:ListItem Value="1">Teacher</asp:ListItem>
                                                    </asp:RadioButtonList>
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
                                                                <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtStudentName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txtStudentName" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentCode">                                                                
                                                                Student Id </label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtStudentCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtStudentCode_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtStudentCode" />
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupStudent" runat="server" OnClick="btnLookupStudent_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                                            <i class="fa fa-search"></i>
                                                                        </asp:LinkButton>
                                                                    </span>
                                                              </div>
                                                            </div>
                                                        </div>
                                                        
	                                                </div>
                                                </div>

                                                <%--Right side--%>
                                                <div runat="server" id="pnlStudent_Sub" class="col-xs-12 col-sm-6">
                                                    <table class="auto-style2" style="width:100% !important">
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
                                                <table class="auto-style2" style="width:100% !important">
                                                    <tr>
                                                        <td><asp:Label ID="Label14"  runat="server" Text="Father / Spouse Name" ></asp:Label></td>
                                                        <td><asp:Label ID="lblFatherName_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label15"  runat="server" Text="Age" ></asp:Label></td>
                                                        <td><asp:Label ID="lblAge_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label16"  runat="server" Text="Gender" ></asp:Label></td>
                                                        <td><asp:Label ID="lblGender_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label17"  runat="server" Text="School" ></asp:Label></td>
                                                        <td><asp:Label ID="lblSchoolName_Teacher" runat="server"  ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label18"  runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                        <td><asp:Label ID="lblWearingGlasses_Teacher" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label20"  runat="server" Text="Decreased Vision" ></asp:Label></td>
                                                        <td><asp:Label ID="lblDecreasedVision_Teacher" runat="server" ></asp:Label></td>
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

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="NewOldTest">
                                                </label>                                                
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Value="0">New Test</asp:ListItem>
                                                    <asp:ListItem Value="1">Edit Previous Test Result</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">Test Date *</label>                                                
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="11" Width="125px"></asp:TextBox>
                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtTestDate_MaskedEditExtender" runat="server" Mask="99/99/9999" TargetControlID="txtTestDate" AutoComplete="False" CultureName="ur-PK" MaskType="Date" />--%>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>

                                                <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                 
                                    <div runat="server" id="Panel1" class="panel panel-primary">
                                            <div class="panel-heading" runat="server" >Diagnosis</div>
                                            <div class="panel-body" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td><asp:Label ID="Label3" runat="server" Text="Right" Width="300px" Font-Bold="True" ></asp:Label></td>
                                                                    <td><asp:Label ID="Label4" runat="server" Text="Left" Width="200px" Font-Bold="True" ></asp:Label></td>
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
                                                <%--<div class="row">
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
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                                
                                                        </div>
                                                    </div>
                                                </div>                                                 
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            
                                                        </div>
                                                    </div>
                                                </div>--%>   
                                          
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
                                                


                                            </div>
                                        </div>
                                     
                                    <div runat="server" id="pnlTreatment" class="panel panel-primary">
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
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lnkShowMedicine" runat="server"  Font-Names="Verdana" Font-Size="8pt" OnClick="lnkShowMedicine_Click" Text="Show / Hide Medicine"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Prescription">Prescription: </label>
                                                <asp:CheckBoxList ID="chkMedicine" runat="server" Visible="False"></asp:CheckBoxList>
                                                <asp:DropDownList ID="ddlMedicine" runat="server" CssClass="form-control" Visible="False"></asp:DropDownList>                                          
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

                                <div runat="server" id="pnlParentRemarks" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Parent Remarks</div>
                                        <div class="panel-body" runat="server">    
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="lblWilling">Parent agrees for treatment</label> 
                                                    <asp:RadioButton ID="rdoYes" runat="server" Text ="Yes" AutoPostBack="True" GroupName="agrees" OnCheckedChanged="rdoYes_CheckedChanged" />
                                                    <asp:RadioButton ID="rdoNo" runat="server" Text ="No" AutoPostBack="True" GroupName="agrees" OnCheckedChanged="rdoNo_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="rdoNotAgrees" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" OnSelectedIndexChanged="rdoNotAgrees_SelectedIndexChanged" >
                                                        <asp:ListItem Value="0">Parents are doing treatment by themselves</asp:ListItem>
                                                        <asp:ListItem Value="1">Parents not want to do the treatment</asp:ListItem>
                                                        <asp:ListItem Value="2">Parent can't be contacted</asp:ListItem>
                                                        <asp:ListItem Value="3">Other</asp:ListItem>
                                                    </asp:RadioButtonList>                                                  
                                                </div>
                                            </div>                                            
                                        </div> 
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtOtherRemarks" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="120" Visible="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblHospital" runat="server" Text="Hospital" Visible="False"></asp:Label>
                                                    <asp:DropDownList ID="ddlHospital" runat="server" CssClass="form-control" AutoPostBack="True" Visible="False"> </asp:DropDownList>
                                                </div>
                                            </div> 
                                        </div> 
                                            
                                    </div>
                                </div>


                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="OptometristButton">
                                            </label>
                                            <div class="form-group text-right">
                                                <asp:LinkButton ID="btnMovePrevious" runat="server" OnClick="btnMovePrevious_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Move Previous"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="Treatment"></label>
                                            <div class="form-group text-left">
                                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnRefresh" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="btnRefresh_Click" Text="Refresh"></asp:LinkButton>
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
        <asp:HiddenField ID="hfAutoRefTestTransID" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestTransDate" runat="server"/>
        <asp:HiddenField ID="hfLookupResultTeacher" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultTeacher_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="rdoType" />
        <asp:PostBackTrigger ControlID="rdoOldNewTest" />
        <asp:PostBackTrigger ControlID="ddlPreviousTestResult" />
        <asp:PostBackTrigger ControlID="lblShowStudentDetail" />

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

        <asp:PostBackTrigger ControlID="btnEdit" />
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDelete" />
        <asp:PostBackTrigger ControlID="btnAbort" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>
