<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AutoRefractionistInspection.aspx.cs" Inherits="TransportManagement.AutoRefractionistInspection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/AutoRefractionistInspection.js"></script>   
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

        function validateInput() {

            var valRes = true;

            if ($("[id$=txtTestDate]").val().trim() == "") {
                $("[id$=txtTestDate]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtTestDate]").removeAttr("style");
            }

            if ($("[id$=txtSpherical_RightEye]").val().trim() == "") {
                $("[id$=txtSpherical_RightEye]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtSpherical_RightEye]").removeAttr("style");
            }

            if ($("[id$=txtCyclinderical_RightEye]").val().trim() == "") {
                $("[id$=txtCyclinderical_RightEye]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtCyclinderical_RightEye]").removeAttr("style");
            }

            if ($("[id$=txtAxixA_RightEye]").val().trim() == "") {
                $("[id$=txtAxixA_RightEye]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtAxixA_RightEye]").removeAttr("style");
            }

            if ($("[id$=txtSpherical_LeftEye]").val().trim() == "") {
                $("[id$=txtSpherical_LeftEye]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtSpherical_LeftEye]").removeAttr("style");
            }

            if ($("[id$=txtCyclinderical_LeftEye]").val().trim() == "") {
                $("[id$=txtCyclinderical_LeftEye]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtCyclinderical_LeftEye]").removeAttr("style");
            }

            if ($("[id$=txtAxixA_LeftEye]").val().trim() == "") {
                $("[id$=txtAxixA_LeftEye]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtAxixA_LeftEye]").removeAttr("style");
            }

            //if ($("[id$=txtSchoolName]").val().trim() == "") {
            //    $("[id$=txtSchoolName]").attr("style", "border: red 1px solid;")
            //    valRes = false;
            //}
            //else {
            //    $("[id$=txtSchoolName]").removeAttr("style");
            //}

            //if ($("[id$=txtTeacherName]").val().trim() == "") {
            //    $("[id$=txtTeacherName]").attr("style", "border: red 1px solid;")
            //    valRes = false;
            //}
            //else {
            //    $("[id$=txtTeacherName]").removeAttr("style");
            //}

            //if ($("[id$=txtFatherName]").val().trim() == "") {
            //    $("[id$=txtFatherName]").attr("style", "border: red 1px solid;")
            //    valRes = false;
            //}
            //else {
            //    $("[id$=txtFatherName]").removeAttr("style");
            //}

            //if ($("[id$=txtAge]").val().trim() == "") {
            //    $("[id$=txtAge]").attr("style", "border: red 1px solid;")
            //    valRes = false;
            //}
            //else {
            //    $("[id$=txtAge]").removeAttr("style");
            //}

            //var ddlGender = document.getElementById("ddlGender");
            //if (ddlGender.value == "0") {
            //    $("[id$=ddlGender]").attr("style", "border: red 1px solid;")
            //    valRes = false;
            //}
            //else {
            //    $("[id$=ddlGender]").removeAttr("style");
            //}

            //var ddlInLaw = document.getElementById("ddlInLaw");
            //if (ddlInLaw.value == "0") {
            //    $("[id$=ddlInLaw]").attr("style", "border: red 1px solid;")
            //    valRes = false;
            //}
            //else {
            //    $("[id$=ddlInLaw]").removeAttr("style");
            //}

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always"  runat="server">
    <ContentTemplate>
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">                
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title"><b>Auto Refraction Test</b></h5>                                
                                <hr />
                                <div class="row">
                                    <div class="col-sm-3">
                                        <asp:RadioButtonList ID="rdoType" runat="server" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" TabIndex="1">
                                                    <asp:ListItem Selected="True" Value="0">Student</asp:ListItem>
                                                    <asp:ListItem Value="1">Teacher</asp:ListItem>
                                                </asp:RadioButtonList>

                                        <%--<asp:RadioButton ID="rdoStudent" Checked="true" runat="server"  GroupName ="Type" OnCheckedChanged="rdoStudent_CheckedChanged" AutoPostBack="true" Text="Student" />
                                        <asp:RadioButton ID="rdoTeacher" runat="server" GroupName ="Type" OnCheckedChanged="rdoTeacher_CheckedChanged" AutoPostBack="true" Text="Teacher" />--%>
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
                                                            <label for="StudentName">Student Name *</label>
                                                            <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25" TabIndex="2"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="form-group">                                                                
                                                            <label for="StudentCode">Student Id </label>
                                                            <div class="input-group input-group-sm mb-3">
	                                                                <asp:TextBox ID="txtStudentCode" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="9" TabIndex="3"></asp:TextBox>
	                                                            <div class="input-group-append">                                                                                                                                                
		                                                            <asp:LinkButton ID="btnLookupStudent" runat="server" OnClick="btnLookupStudent_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3">
                                                                        <i class="fa fa-search"></i>
                                                                    </asp:LinkButton>
	                                                            </div>
                                                            </div>
                                                        </div>
                                                    </div>                                                        
	                                             </div>
                                            </div>

                                            <%--Right side--%>
                                            <div runat="server" id="pnlStudent_Sub" class="col-xs-12 col-sm-6">
                                                <table class="auto-style2">
                                                <tr>
                                                    <td><asp:Label ID="Label1" runat="server" Text="Father Name" ></asp:Label></td>
                                                    <td><asp:Label ID="lblFatherName_Student" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Label2" runat="server" Text="Age" ></asp:Label></td>
                                                    <td><asp:Label ID="lblAge_Student" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Label3" runat="server" Text="Gender" ></asp:Label></td>
                                                    <td><asp:Label ID="lblGender_Student" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Label4" runat="server" Text="Class" ></asp:Label></td>
                                                    <td><asp:Label ID="lblClass_Student" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Label5" runat="server" Text="School" ></asp:Label></td>
                                                    <td><asp:Label ID="lblSchoolName_Student" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Label6" runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                    <td><asp:Label ID="lblWearingGlasses_Student" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="Label7" runat="server" Text="Decreased Vision" ></asp:Label></td>
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
			                            <div class="col-xs-12 col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="TeacherName">
                                                        Teacher Name *</label>
                                                        <asp:TextBox ID="txtTeacherName" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="25" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="TeacherCode">Teacher Id </label>
                                                        <div class="input-group input-group-sm mb-3">
	                                                                <asp:TextBox ID="txtTeacherCode" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="9" TabIndex="5"></asp:TextBox>
	                                                        <div class="input-group-append">                                                                                                                                                
		                                                        <asp:LinkButton ID="btnLookupTeacher" runat="server" ClientIDMode="Static" OnClick="btnLookupTeacher_Click" Text="Lookup"  CssClass="btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3">
                                                                    <i class="fa fa-search"></i>
                                                                </asp:LinkButton>
	                                                        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
			
			                            </div>
			
			                            <div runat="server" id="pnlTeacher_Sub" class="col-xs-12 col-sm-6">
                                            <table class="auto-style2">
                                            <tr>
                                                <td><asp:Label ID="Label8"  runat="server" Text="Father / Spouse Name" ></asp:Label></td>
                                                <td><asp:Label ID="lblFatherName_Teacher" runat="server"  ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="Label10"  runat="server" Text="Age" ></asp:Label></td>
                                                <td><asp:Label ID="lblAge_Teacher" runat="server"  ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="Label12"  runat="server" Text="Gender" ></asp:Label></td>
                                                <td><asp:Label ID="lblGender_Teacher" runat="server"  ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="Label16"  runat="server" Text="School" ></asp:Label></td>
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

                                <div runat="server" id="pnlTestArea" class="panel panel-primary">
                                    <div class="panel-heading" runat="server" >Test Information</div>
                                    <div class="panel-body" runat="server">
		                            <div class="row">    
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                            <label for="NewOldTest">
                                             
                                                            </label>
                                                        <asp:RadioButtonList ID="rdoOldNewTest"  runat="server" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" Width="425px" RepeatDirection="Horizontal" AutoPostBack="True" TabIndex="6">
                                                                        <asp:ListItem Selected="True" Value="0">New Test</asp:ListItem>
                                                                        <asp:ListItem Value="1">Edit Previous Test Result</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="TransDate">
                                                        Test Date *</label>                                                
                                                        <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="11" Width="125px" TabIndex="7"></asp:TextBox>
                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtTestDate_MaskedEditExtender" runat="server" Mask="99/99/9999" TargetControlID="txtTestDate" AutoComplete="False" CultureName="ur-PK" MaskType="Date" />--%>
                                                        <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                        </asp:CalendarExtender>

                                                        <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="Div1" class="col-xs-12 col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="DataFor">Results for Date: </label>
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
                                                        <label for="RemainingTest">Remaining Test: </label>
                                                        <asp:Label ID="lblRemainingTest" runat="server"  ></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <asp:GridView ID="gvRemainingList" runat="server">
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>








                                
                                


                                <div class="row">
	                                <div class="col-xs-12 col-sm-6">
		                                <div runat="server" id="pnlRightEye" visible="false" class="panel panel-default">
			                                <div class="panel-heading" runat="server" >Right Eye</div>
			                                <div class="panel-body" runat="server">
				                                <div class="row">
                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="Spherical_RightEye">Spherical *</label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                        <asp:DropDownList ID="ddlSpherical_RightEye" runat="server" CssClass="form-control form-control-sm border-end-0 border rounded-pill" Width="120px" AutoPostBack="False" TabIndex="8">                                                
                                                                            <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                            <asp:ListItem>Negative</asp:ListItem>
                                                                            <asp:ListItem>Plano</asp:ListItem>
                                                                            <asp:ListItem>Error</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    <div class="input-group-append">                                                                                                                                                
                                                                        <asp:TextBox ID="txtSpherical_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" AutoPostBack="false" TabIndex="9"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_RightEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtSpherical_RightEye" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="Cyclinderical_RightEye" style="text-align: left">Cyclinderical *</label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                        <asp:DropDownList ID="ddlCyclinderical_RightEye" runat="server"  CssClass="form-control form-control-sm border-end-0 border rounded-pill" Width="120px" TabIndex="10">                                                
                                                                            <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                            <asp:ListItem>Negative</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    <div class="input-group-append">
                                                                        <asp:TextBox ID="txtCyclinderical_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="11" ></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_RightEye" />--%>
	                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtCyclinderical_RightEye" />
                                                                    </div>
                                                                </div>                                                                
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="Axix_RightEye">Axix *</label>
                                                                 
                                                                <div class="input-group input-group-sm mb-3">
                                                                        <asp:TextBox ID="txtAxixA_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="3" Width="120px" TabIndex="12"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_RightEye_MaskedEditExtender" Mask ="999" runat="server" TargetControlID="txtAxixA_RightEye" MaskType="Number" />--%>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_RightEye" />
                                                                    <div class="input-group-append">                                                                                                                                                
                                                                        <asp:TextBox ID="txtAxixB_RightEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="3" Width="120px" Visible="False" AutoPostBack="True"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_RightEye_MaskedEditExtender" Mask ="999" runat="server" TargetControlID="txtAxixB_RightEye" MaskType="Number" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_RightEye" />
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
		                                <div runat="server" id="pnlLeftEye" visible="false" class="panel panel-default">
			                                <div class="panel-heading" runat="server" >Left Eye</div>
			                                <div class="panel-body" runat="server">
				                                <div class="row">
                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="Spherical_LeftEye">Spherical *</label>
                                                                <div class="input-group input-group-sm mb-3">
	                                                                        <asp:DropDownList ID="ddlSpherical_LeftEye" runat="server" CssClass="form-control form-control-sm border-end-0 border rounded-pill" Width="120px" TabIndex="13">
                                                                            <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                            <asp:ListItem>Negative</asp:ListItem>
                                                                            <asp:ListItem>Plano</asp:ListItem>
                                                                            <asp:ListItem>Error</asp:ListItem>
                                                                        </asp:DropDownList>
	                                                                <div class="input-group-append">                                                                                                                                                
		                                                                <asp:TextBox ID="txtSpherical_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="14" ></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_LeftEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtSpherical_LeftEye" />
	                                                                </div>
                                                                </div>                                                                 
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="Cyclinderical_LeftEye">Cyclinderical *</label>

                                                                <div class="input-group input-group-sm mb-3">
	                                                                        <asp:DropDownList ID="ddlCyclinderical_LeftEye" runat="server" CssClass="form-control form-control-sm border-end-0 border rounded-pill" Width="120px" TabIndex="15">
                                                                            <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                            <asp:ListItem>Negative</asp:ListItem>
                                                                        </asp:DropDownList>
	                                                                <div class="input-group-append">                                                                                                                                                
		                                                                    <asp:TextBox ID="txtCyclinderical_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="16"></asp:TextBox>
                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_LeftEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtCyclinderical_LeftEye" />
	                                                                </div>
                                                                </div>                                                               
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="Axix_LeftEye">Axix *</label>
                                                                <div class="input-group input-group-sm mb-3">
	                                                                        <asp:TextBox ID="txtAxixA_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="3" Width="120px" TabIndex="17"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_LeftEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_LeftEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_LeftEye" />
	                                                                <div class="input-group-append">                                                                                                                                                
		                                                                    <asp:TextBox ID="txtAxixB_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary  border-start-0 border rounded-pill ms-n3" MaxLength="3" Width="120px" Visible="False" AutoPostBack="True"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_LeftEye_MaskedEditExtender" runat="server" Mask="999" TargetControlID="txtAxixB_LeftEye" />--%>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_LeftEye" />
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

                                <div runat="server" id="pnlAutoRefHistory" class="panel panel-primary">
	                                <div class="panel-heading" runat="server" >Auto Refraction Test History</div>
	                                <div class="panel-body" runat="server">
		                                <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                    <asp:GridView ID="gvAutoRef" runat="server">
                                                    </asp:GridView>
                                        
                                                </div>
                                            </div>
		                                </div>
	                                </div>
                                </div>
 
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="AutoRefButton">
                                        </label>
                                        <div class="form-group text-left">
                                            <asp:LinkButton ID="btnEdit" runat="server" OnClientClick="return validateInput()" OnClick="btnEdit_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" TabIndex="18"></asp:LinkButton>
                                            <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return validateInput()" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" TabIndex="19"></asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" TabIndex="20"></asp:LinkButton>
                                            <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" TabIndex="21"></asp:LinkButton>
                                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Refresh" TabIndex="22"></asp:LinkButton>
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

        <asp:PostBackTrigger ControlID="btnEdit" />
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDelete" />
        <asp:PostBackTrigger ControlID="btnAbort" />

        <asp:PostBackTrigger ControlID="ddlSpherical_RightEye" />
        <asp:PostBackTrigger ControlID="ddlSpherical_LeftEye" />
        <asp:PostBackTrigger ControlID="ddlCyclinderical_RightEye" />
        <asp:PostBackTrigger ControlID="ddlCyclinderical_LeftEye" />

    </Triggers>
</asp:UpdatePanel>

</asp:Content>
