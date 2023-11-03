<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="AttendanceReport.aspx.cs" Inherits="TransportManagement.AttendanceReport" %>
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
                                    <b>Staff Attendance Report </b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                Date: *</label>                                                
                                                <asp:TextBox ID="txtTestDateFrom" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDateFrom" runat="server" TargetControlID="txtTestDateFrom" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                User Id: *</label>                                                
                                                <asp:CheckBoxList ID="chkUserId" runat="server"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="DailyReport">
                                            &nbsp;</label>
                                            <div class="form-group text-left">                                                
                                                <asp:LinkButton ID="btnView" runat="server"  OnClick="btnView_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Generate Report"></asp:LinkButton>
                                                <asp:LinkButton ID="btnViewExcel" runat="server"  OnClick="btnViewExcel_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Export Data to Excel"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Abort"></asp:LinkButton>
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
                      <div class="row">
                        
                                <div class="col-md-12">
                                    <div class="card-box table-responsive">
                                     <hr />
                                        <asp:GridView ID="gv_TransactionList" runat="server">
                                        </asp:GridView>
                                        
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
        .auto-style1 {
            width: 100%;
        }
    </style>


</asp:Content>
