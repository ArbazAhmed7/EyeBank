<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="rptVisitSummary.aspx.cs" Inherits="TransportManagement.rptVisitSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/rptVisitSummary.js"></script>  
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
                                <h5 class="m-t-0 header-title"><b>Visit Summary</b></h5>                                
                                <hr />
                                <div class="row">
                                    <div class="col-sm-3">
                                        <asp:RadioButtonList ID="rdoType" runat="server" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True">
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
                                                            <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="form-group">                                                                
                                                            <label for="StudentCode">Student Id </label>
                                                            <div class="input-group input-group-sm mb-3">
	                                                                <asp:TextBox ID="txtStudentCode" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="9">                                                
                                                                </asp:TextBox>
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
                                                        <asp:TextBox ID="txtTeacherName" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="25">                                                
                                                    </asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="TeacherCode">Teacher Id </label>
                                                        <div class="input-group input-group-sm mb-3">
	                                                                <asp:TextBox ID="txtTeacherCode" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="9">                                                
                                                                    </asp:TextBox>
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
		                            </div>		 
	                            </div>
                            </div> 
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="TransDate"> Test Date *</label>                                                
                                            <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"> </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="AutoRefButton">
                                        </label>
                                        <div class="form-group text-left">
                                            <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="General Report"></asp:LinkButton>
                                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Refresh"></asp:LinkButton>
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
                            </div>
                        </div>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <asp:HiddenField ID="hfStudentIDPKID" runat="server" OnValueChanged="hfStudentIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfTeacherIDPKID" runat="server" OnValueChanged="hfTeacherIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfLookupResultTeacher" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultTeacher_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="rdoType" />
        <asp:PostBackTrigger ControlID="btnView" />
        <asp:PostBackTrigger ControlID="btnRefresh" />
    </Triggers>
</asp:UpdatePanel>


</asp:Content>
