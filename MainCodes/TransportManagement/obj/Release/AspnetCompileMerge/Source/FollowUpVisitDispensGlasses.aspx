<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FollowUpVisitDispensGlasses.aspx.cs" Inherits="TransportManagement.FollowUpVisitDispensGlasses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/FollowUpVisitDispensGlasses.js"></script>
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
    <script type="text/javascript">
       

    </script>
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
                                    <h5 class="m-t-0 header-title"><b>Followup visit for dispensing Glasses</b></h5>
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
                                                                    <asp:TextBox ID="txtSchoolCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength ="3" AutoComplete="off" Enabled="False" TabIndex="1" ></asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupSchool" runat="server" OnClick="btnLookupSchool_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
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
                                                        <tr>
                                                            <td><asp:Label ID="Label23"  runat="server" Text="Hospital Visit Date" ></asp:Label></td>
                                                            <td><asp:Label ID="lblHospitalVisit" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label25"  runat="server" Text="Hospital Name" ></asp:Label></td>
                                                            <td><asp:Label ID="lblHospitalName" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label27"  runat="server" Text="Doctor Name" ></asp:Label></td>
                                                            <td><asp:Label ID="lblDoctorName" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label31"  runat="server" Text="Diagnosis" ></asp:Label></td>
                                                            <td><asp:Label ID="lblDiagnosis" runat="server" ></asp:Label></td>
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


                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="4">
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
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="5" AutoComplete="off" Width="125px" ></asp:TextBox>
                                                <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" TabIndex="7" ></asp:TextBox>--%>
                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtTestDate_MaskedEditExtender" runat="server" Mask="99/99/9999" TargetControlID="txtTestDate" AutoComplete="False" CultureName="ur-PK" MaskType="Date" />--%>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>

                                                <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True" TabIndex="6" > </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div> 

                                    <div runat="server" id="pnlRightEye_AutoRef" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Subjective Refraction Test Result</div>
                                        <div class="panel-body" runat="server">

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="StudentName">Date</label>
                                                        <asp:DropDownList ID="ddlOptometristTestDate" runat="server" CssClass="form-control form-control-sm border-end-0 border rounded-pill" OnSelectedIndexChanged="ddlOptometristTestDate_SelectedIndexChanged" AutoPostBack="True" TabIndex="7" >                                                            
                                                        </asp:DropDownList>
                                                    </div>                                                            
                                                </div>
                                            </div>

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
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="Near Add"></asp:Label>
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
                                                        <td>
                                                            <asp:Label ID="lblNearAdd_RightEye" runat="server" ></asp:Label>
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
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Text="Near Add"></asp:Label>
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
                                                            <td>
                                                                <asp:Label ID="lblNearAdd_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
	                                            </div>
                                              </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="pnlVisionwithGlasses" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server" >Vision with Glasses</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="row">
	                                            <div class="col-xs-12 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="rdoDistanceVision_RightEye_Unaided">Right Eye</label>
                                                        <div class="input-group"></div>
                                                         <asp:RadioButtonList ID="rdoDistanceVision_RightEye_WithGlasses" runat="server" TabIndex="8">
                                                            <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                            <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                            <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                            <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                            <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                            <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                            <asp:ListItem Value="6" Text="">6/60</asp:ListItem>                                                        
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                 <div class="col-xs-12 col-sm-6">
                                                    <div class="form-group">
                                                        <label for="rdoDistanceVision_LeftEye_WithGlasses">Left Eye</label>
                                                        <div class="input-group"></div>
                                                         <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_WithGlasses" runat="server" AutoPostBack="True" Width="125px" TabIndex="9">
                                                            <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                            <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                            <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                            <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                            <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                            <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                            <asp:ListItem Value="6" Text="">6/60</asp:ListItem>                                                           
                                                        </asp:RadioButtonList>
                                                    </div>
	                                            </div>
	                                        </div>
	
	                                           
                                        </div>
	
	                                </div>
                                     
                                    <div runat="server" id="pnlStudentComments" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Student's comments</div>
                                        <div class="panel-body" runat="server">
                                      
                                            <div class="row">                                                        
                                                <div class="col-sm-6">
                                                    <div class="form-group">                                                         
                                                        <asp:RadioButtonList ID="rdoStudentSatisficaion" ClientIDMode="Static" runat="server" TabIndex="17">
                                                            <asp:ListItem Value="1" Text="Student satisfied"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="Student not satisfied"></asp:ListItem>                                                            
                                                        </asp:RadioButtonList>                                                       
                                                    </div>
                                                </div>                                                           
                                            </div>

                                            <div id="DIVrdoListStudentNotSatisfiedOpt" style="display:none;" class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:RadioButtonList ID="rdoListStudentNotSatisfiedOpt" ClientIDMode="Static" runat="server">
                                                            <asp:ListItem Value="0" Text="Blur vision"></asp:ListItem>                                                            
                                                            <asp:ListItem Value="1" Text="Headache"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Pseudo"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Others"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="DIVtxtStudentNotSatisfiedOtherComment" style="display:none;" class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label for="txtStudentNotSatisfiedOtherComment">Please specifiy</label>
                                                            <asp:TextBox ID="txtStudentNotSatisfiedOtherComment" MaxLength="2000" CssClass="form-control" 
                                                                ClientIDMode="Static" runat="server"></asp:TextBox>                                          
                                                        </div>
                                                </div>
                                            </div> 
                                            
                                            <div id="DIVrdoRefractionReason" style="display:none;" class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <asp:RadioButtonList ID="rdoRefractionReason" ClientIDMode="Static" runat="server">
                                                            <asp:ListItem Value="0" Text="Optical not correct"></asp:ListItem>                                                            
                                                            <asp:ListItem Value="1" Text="Refraction error"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--style="display:none;"--%>
                                    <%--runat="server" --%>
                                    <div id="DIVpnlSubjectiveRefraction" class="panel panel-primary" style="display:none;">
	                                    <div class="panel-heading" runat="server" >Subjective Refraction</div>
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

                                    <div runat="server" id="pnlFollowupVisit" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Follow up visits</div>
                                        <div class="panel-body" runat="server">
                                             <div class="row">                                                          
                                                <div class="form-group">                                                    
                                                    <asp:RadioButton  ID="rdoNotRequiredFollowup" GroupName="FollowupVisit"  Text="Not required" Checked="false" runat="server" CssClass="form-control no-border" TabIndex="18" />
                                                </div>
                                                 
                                                 <div class="form-group">
                                                     <asp:RadioButton  ID="rdoFollowupAfterSixMonths" GroupName="FollowupVisit"  Text="6 months" Checked="false" runat="server" CssClass="form-control no-border" TabIndex="19" />                                                    
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
                                                        <asp:LinkButton ID="btnWebCam" runat="server" Text="Capture from Webcam" TabIndex="13" OnClick="btnWebCam_Click"></asp:LinkButton>
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
                                                <asp:LinkButton ID="btnSaveImage" runat="server" OnClick="btnSaveImage_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Insert Image" TabIndex="16" Visible="False"></asp:LinkButton>
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
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
