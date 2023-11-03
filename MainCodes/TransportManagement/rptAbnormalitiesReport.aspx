<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="rptAbnormalitiesReport.aspx.cs" Inherits="TransportManagement.rptAbnormalitiesReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="Scripts/FormsScripts/rptAbnormalitiesReport.js"></script>
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
                                    <b>Detailed Abnormaility wise Report for Students</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="SchoolCode">
                                                School *</label>
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
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="SchoolName">
                                                School Name </label>
                                                <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="75" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="ClassNo">
                                                Class *</label>
                                                <div class="input-group input-group-sm mb-3"> 
                                                <asp:TextBox ID="txtClassNo" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="2" Enabled="False" ></asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="btnLookupClass" runat="server" OnClick="btnLookupClass_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span>
                                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="ClassNo">
                                                Class *</label>
                                                <div class="input-group input-group-sm mb-3"> 
                                                <asp:TextBox ID="txtClassNo" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="2" Enabled="False" ></asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="btnLookupClass" runat="server" OnClick="btnLookupClass_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span>
                                                
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="ClassNo">
                                                Section *</label>
                                                <div class="input-group input-group-sm mb-3"> 
                                                <asp:TextBox ID="txtClassSection" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="2" Enabled="False" ></asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="btnLookupSection" runat="server" OnClick="btnLookupSection_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span>
                                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    
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
                                
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" Width="425px" AutoPostBack="True" Visible="False">
                                                        <asp:ListItem Selected="True" Value="0">Student</asp:ListItem>
                                                        <asp:ListItem Value="1">Teacher</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="Diagnosis_RightEye"> Abnormalities </label>  
                                                <br />
                                                <asp:CheckBox ID="chkRefractiveError" runat="server" Text="Refractive Error" />
                                                <br />
                                                <asp:CheckBox ID="chkLowVision" runat="server" Text="Low Vision" />
                                                <br />
                                                <asp:CheckBox ID="chkNeedscyclopegicrefration" runat="server" Text="Needs Cycloplegic Refraction" />
                                                <br />
                                                <asp:CheckBox ID="chkSquintStrabismus" runat="server" Text="Squint Strabismus" />
                                                <br />
                                                <asp:CheckBox ID="chkLazyEyeAmblyopia" runat="server" Text="Lazy Eye Amblyopia" />
                                                <br />
                                                <asp:CheckBox ID="chkColorblindnessAchromatopsia" runat="server" Text="Color blindness Achromatopsia" />
                                                <br />
                                                <asp:CheckBox ID="chkCataract" runat="server" Text="Cataract" />
                                                <br />
                                                <asp:CheckBox ID="chkTraumaticCataract" runat="server" Text="Traumatic Cataract" />
                                                <br />
                                                <asp:CheckBox ID="chkKeratoconus" runat="server" Text="Keratoconus" />
                                                <br />
                                                <asp:CheckBox ID="chkAnisometropia" runat="server" Text="Anisometropia" />
                                                <br />
                                                <asp:CheckBox ID="chkPtosis" runat="server" Text="Ptosis" />
                                                <br />
                                                <asp:CheckBox ID="chkNystagmus" runat="server" Text="Nystagmus" />
                                                <br />
                                                <asp:CheckBox ID="chkCorneadefects" runat="server" Text="Cornea defects" />
                                                <br />
                                                <asp:CheckBox ID="chkPuplidefects" runat="server" Text="Pupli defects" />
                                                <br />
                                                <asp:CheckBox ID="chkPterygium" runat="server" Text="Pterygium" />
                                                <br />
                                                <asp:CheckBox ID="chkOther" runat="server" Text="Other" />
                                                <br />
                                                <asp:CheckBox ID="chkPresbyopia" runat="server" Text="Presbyopia" />
                                                <br />
                                                <asp:CheckBox ID="chkMyopia" runat="server" Text="Myopia" />
                                                <br />
                                                <asp:CheckBox ID="chkHypermetropia" runat="server" Text="Hypermetropia" />
                                                <br />
                                                <asp:CheckBox ID="chkAstigmatism" runat="server" Text="Astigmatism" />                                             
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="OpticianReport">
                                            &nbsp;</label>
                                            <div class="form-group text-left">                                                
                                                <asp:LinkButton ID="btnView" runat="server"  ClientIDMode="Static" CssClass="btn btn-default" Text="Generate Report" OnClick="btnView_Click"></asp:LinkButton>
                                                <asp:LinkButton ID="btnViewExcel" runat="server" ClientIDMode="Static" CssClass="btn btn-default" OnClick="btnViewExcel_Click" Text="Export Data to Excel" Visible="False"></asp:LinkButton>
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

        <asp:HiddenField ID="hfClassIDPKID" runat="server" OnValueChanged="hfClassIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfLookupResultClass" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultClass_ValueChanged" runat="server" />

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
