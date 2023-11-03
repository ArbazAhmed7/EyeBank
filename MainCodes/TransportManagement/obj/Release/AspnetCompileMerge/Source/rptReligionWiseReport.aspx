<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="rptReligionWiseReport.aspx.cs" Inherits="TransportManagement.rptReligionWiseReport" %>
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
                    <asp:Panel ID="pnlCom" runat="server" DefaultButton="btnView">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>Religion Wise Report</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    
                                    <div id="aa" class="panel panel-primary">
		                                <div class="panel-heading" runat="server" >
		                                              
                                                <asp:RadioButtonList ID="rdoReligionType" runat="server" style="width:100%;" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdoReligionType_SelectedIndexChanged" Font-Bold="False" TabIndex="15">
                                                    <asp:ListItem style="color:#ffffff" Selected="True" Value="1">City</asp:ListItem>
                                                    <asp:ListItem style="color:#ffffff" Value="2">District</asp:ListItem>
                                                    <asp:ListItem style="color:#ffffff" Value="3">Town</asp:ListItem>
                                                    <asp:ListItem style="color:#ffffff" Value="4">School Level</asp:ListItem>
                                                    <asp:ListItem style="color:#ffffff" Value="5">School</asp:ListItem>
                                                </asp:RadioButtonList>
                                                   
		                                </div>
		                            </div>
                                    <asp:Panel ID="pnlCity" GroupingText="City" runat="server">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBoxList ID="chkCity" runat="server" ></asp:CheckBoxList>
                                                </div>
                                            </div>                                                                                              
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDistrict" GroupingText="District" runat="server">
                                        <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:CheckBoxList ID="chkDistrict" runat="server" ></asp:CheckBoxList>
                                            </div>
                                        </div>                                                                                              
                                    </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlTown" GroupingText="Town" runat="server">
                                        <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:CheckBoxList ID="chkTown" runat="server" ></asp:CheckBoxList>
                                            </div>
                                        </div>                                                                                              
                                    </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlSchoolLevel" GroupingText="School Level" runat="server">
                                        <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:CheckBoxList ID="chkSchoolLevel" runat="server" ></asp:CheckBoxList>
                                            </div>
                                        </div>                                                                                              
                                    </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlSchool" GroupingText="School" runat="server">                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div class="input-group input-group-sm mb-3">
                                                    <asp:TextBox ID="txtSchoolCode" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Enabled="False" ></asp:TextBox>
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
                                                    <label for="SchoolName">
                                                    School Name </label>
                                                    <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="75" ></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlDateRange" GroupingText="Date falling between" runat="server" Visible="False">     
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                From Date *</label>                                                
                                                <asp:TextBox ID="txtTestDateFrom" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDateFrom" runat="server" TargetControlID="txtTestDateFrom" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
 
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                To Date *</label>                                                
                                                <asp:TextBox ID="txtTestDateTo" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDateTo" runat="server" TargetControlID="txtTestDateTo" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>                                      
                                    </div>     
                                    </asp:Panel>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="OpticianReport">
                                            &nbsp;</label>
                                            <div class="form-group text-left">                                                
                                                <asp:LinkButton ID="btnView" runat="server"  ClientIDMode="Static" CssClass="btn btn-default" Text="Generate Report" OnClick="btnView_Click"></asp:LinkButton>
                                                <asp:LinkButton ID="btnViewExcel" runat="server" ClientIDMode="Static" CssClass="btn btn-default" OnClick="btnViewExcel_Click" Text="Export Data to Excel"></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Text="Refresh" OnClick="btnRefresh_Click"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server" ClientIDMode="Static" CssClass="btn btn-default" OnClick="btnAbort_Click" Text="Abort"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdoReportType" runat="server" Width="177px" AutoPostBack="True" Visible="False">
                                                        <asp:ListItem Selected="True" Value="0">Direct Report</asp:ListItem>
                                                        <asp:ListItem Value="1">PDF Format</asp:ListItem>
                                            </asp:RadioButtonList>
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
                      <div class="row">
                        
                                <div class="col-md-12">
                                    <div class="card-box table-responsive">
                                     <hr />
                                        
                                    </div>
                                </div>
                            </div>

                        <div class="row">
                    </div>
                </div>

                </div>
            </div>
        </div>

        <asp:HiddenField ID="hfSelectedRoleID" Value="0" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfLookupResultSchool" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSchool_ValueChanged" runat="server" />

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnView" />
        <asp:PostBackTrigger ControlID="btnViewExcel" />
    </Triggers>
</asp:UpdatePanel>
    <style type="text/css">
         .ui-autocomplete {
          position: absolute;
          top: 100%;
          left: 0;
          z-index: 1000;
          display: none;
          float: left;
          min-width: 160px;
          padding: 5px 0;
          margin: 2px 0 0;
          list-style: none;
          font-size: 14px;
          text-align: left;
          background-color: #ffffff;
          border: 1px solid #cccccc;
          border: 1px solid rgba(0, 0, 0, 0.15);
          border-radius: 4px;
          -webkit-box-shadow: 0 6px 12px rgba(0, 0, 0, 0.175);
          box-shadow: 0 6px 12px rgba(0, 0, 0, 0.175);
          background-clip: padding-box;
        }

        .ui-autocomplete > li > div {
          display: block;
          padding: 3px 20px;
          clear: both;
          font-weight: normal;
          line-height: 1.42857143;
          color: #333333;
          white-space: nowrap;
        }

        .ui-state-hover,
        .ui-state-active,
        .ui-state-focus {
          text-decoration: none;
          color: #262626;
          background-color: #f5f5f5;
          cursor: pointer;
        }

        .ui-helper-hidden-accessible {
          border: 0;
          clip: rect(0 0 0 0);
          height: 1px;
          margin: -1px;
          overflow: hidden;
          padding: 0;
          position: absolute;
          width: 1px;
        }
    </style>

</asp:Content>
