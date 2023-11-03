<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DailyReport.aspx.cs" Inherits="TransportManagement.DailyReport" %>
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
                    <asp:Panel ID="pnlCom" runat="server" DefaultButton="btnSave">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>Daily Progress Report </b></h5>
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
                                                <asp:RadioButtonList ID="rdoReportList" CssClass="form-control form-control-sm" runat="server" Width="600px" Visible="False">
                                                    <asp:ListItem Selected="True" Value="0">Daily Progress Report</asp:ListItem>
                                                    <asp:ListItem Value="1">List of Students (with Refractive Error)</asp:ListItem>
                                                    <asp:ListItem Value="2">List of Students (with other Abnormalities)</asp:ListItem>
                                                    <asp:ListItem Value="3">Glass Dispensing Summary (School Wise)</asp:ListItem>
                                                    <asp:ListItem Value="4">Glass Dispensing Detail (School Wise Student Wise)</asp:ListItem>
                                                    <asp:ListItem Value="5">List of Students (Auto Ref Test not performed)</asp:ListItem>
                                                    <asp:ListItem Value="6">List of Students (Optomertrist Test not performed)</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>                                        
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="DailyReport">
                                            &nbsp;</label>
                                            <div class="form-group text-left">                                                
                                                <asp:LinkButton ID="btnSave" runat="server"  OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="View Data"></asp:LinkButton>
                                                <asp:LinkButton ID="btnView" runat="server"  OnClick="btnView_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Generate Report"></asp:LinkButton>
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
                                    <asp:Panel ID="pnlDailyReport"  runat="server" GroupingText="">
    
                                                <table class="auto-style1">
                                                    <tr>
                                                        <td colspan ="5" class="auto-style3" style="text-align: left">
                                                            <strong>Eye Screening Camp</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label1" runat="server"   Text="Student Detail"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label9" runat="server"   Text="Teacher Detail"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                       
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server"  Text="School Name:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            
                                                        </td>                                                                                                     
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server"  Text="School Name:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>                                                       
                                                        <td colspan="2">
                                                            <%--<asp:Label ID="Label55" runat="server"  Text="School Name:" Width="250px"></asp:Label>--%>
                                                            <asp:Label ID="lblSchoolName_Student" runat="server"  Width="500px"></asp:Label>
                                                            
                                                        </td>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td colspan="2">
                                                            <%--<asp:Label ID="Label57" runat="server"  Text="School Name:" Width="250px"></asp:Label>--%>
                                                            <asp:Label ID="lblSchoolName_Teacher" runat="server" Width="500px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                       
                                                        <td colspan="2">
                                                            <%--<asp:Label ID="Label55" runat="server"  Text="School Name:" Width="250px"></asp:Label>--%>
                                                            <asp:Label ID="lblSchoolName_Student_2" runat="server" Width="500px"></asp:Label>
                                                            
                                                        </td>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td colspan="2">
                                                            <%--<asp:Label ID="Label57" runat="server"  Text="School Name:" Width="250px"></asp:Label>--%>
                                                            <asp:Label ID="lblSchoolName_Teacher_2" runat="server"   Width="500px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblSchoolName_Student_3" runat="server" Width="500px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td colspan="2"><asp:Label ID="lblSchoolName_Teacher_3" runat="server"   Width="500px"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label2" runat="server"  Text="Student Enrolled:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentEnrolled" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label10" runat="server"  Text="Teacher Enrolled:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeacherEnrolled" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                       
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label3" runat="server" Text="Students Auto Ref:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentsAutoRef" runat="server"   Width="150px"></asp:Label>
                                                        </td>                                                                                                    
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label11" runat="server" Text="Teachers Auto Ref:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeachersAutoRef" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                                                                      
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label4" runat="server" Text="Students Optometrist:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentsOptometrist" runat="server"   Width="150px"></asp:Label>
                                                        </td>                                                                                                    
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label12" runat="server" Text="Teachers Optometrist:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeachersOptometrist" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label53" runat="server" Text="Students wearing Glasses:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentswearingglasses" runat="server" Width="150px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label54" runat="server" Text="Teachers wearing Glasses:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeacherswearingglasses" runat="server" Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                                                                     
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label5" runat="server" Text="Students suggested Glasses:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentssuggestedGlasses" runat="server"   Width="150px"></asp:Label>
                                                        </td>                                                     
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label13" runat="server" Text="Teachers suggested Glasses:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeacherssuggestedGlasses" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label6" runat="server" Text="Students for Cycloplagic Refraction:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentsforCycloplagicRefraction" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label14" runat="server" Text="Teachers for Cycloplagic Refraction:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeachersforCycloplagicRefraction" runat="server"   Width="150px"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>                                                                                                      
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label7" runat="server" Text="Students with other issues:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentswithotherissues" runat="server"   Width="150px"></asp:Label>
                                                        </td> 
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label15" runat="server" Text="Teachers with other issues:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeacherswithotherissues" runat="server"   Width="150px"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>                                                                                                    
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label8" runat="server" Text="Student for Surgery:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblStudentforSurgery" runat="server"   Width="150px"></asp:Label>
                                                        </td>                                                                                                     
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label16" runat="server" Text="Teacher for Surgery:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lblTeacherforSurgery" runat="server"   Width="150px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" style="text-align: left">

                                                            <asp:Panel ID="pnlAbnormalities" runat="server" GroupingText="List of Students (with other Abnormalities)">
                                                                <asp:GridView ID="gvAbnormalities" runat="server" AutoGenerateColumns="False" Font-Size="8pt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Student Code" SortExpression="StudentCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentCode" runat="server" Text='<%# Bind("StudentCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student Name" SortExpression="StudentName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Father Name" SortExpression="FatherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Class" SortExpression="ClassCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Section" SortExpression="ClassSection">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassSection" runat="server" Text='<%# Bind("ClassSection") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Age" SortExpression="Age">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAge" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <%--<asp:TemplateField HeaderText="Wear Glasses" SortExpression="WearGlasses">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWearGlasses" runat="server" Text='<%# Bind("WearGlasses") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	--%>
                                                                        <asp:TemplateField HeaderText="Diagnosis" SortExpression="Daignosis">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaignosis" runat="server" Text='<%# Bind("Daignosis") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Diagnosis Remarks" SortExpression="DaignosisRemarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaignosisRemarks" runat="server" Text='<%# Bind("DaignosisRemarks") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Sub Diagnosis" SortExpression="SubDaignosis">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubDaignosis" runat="server" Text='<%# Bind("SubDaignosis") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Treatment" SortExpression="Treatment">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTreatment" runat="server" Text='<%# Bind("Treatment") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Medicine" SortExpression="Medicine">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMedicine" runat="server" Text='<%# Bind("Medicine") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Next Visit" SortExpression="NextVisit">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNextVisit" runat="server" Text='<%# Bind("NextVisit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <%--	
                                                                        <asp:TemplateField HeaderText="Father Cell" SortExpression="FatherCell">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFatherCell" runat="server" Text='<%# Bind("FatherCell") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Mother Name" SortExpression="MotherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMotherName" runat="server" Text='<%# Bind("MotherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Mother Cell" SortExpression="MotherCell">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMotherCell" runat="server" Text='<%# Bind("MotherCell") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    
                                                                    
                                                                        <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	--%>
                                                                        <asp:TemplateField HeaderText="Optometrist Name" SortExpression="OptometristName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOptometristName" runat="server" Text='<%# Bind("OptometristName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    
                                                                    
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </asp:Panel>

                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" style="text-align: left">

                                                            <asp:Panel ID="pnlAbnormaties_Teacher" runat="server" GroupingText="List of Teachers (with other Abnormalities)">
                                                                <asp:GridView ID="gvAbnormalities_Teacher" runat="server" AutoGenerateColumns="False" Font-Size="8pt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Teacher Code" SortExpression="TeacherCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTeacherCode" runat="server" Text='<%# Bind("TeacherCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Teacher Name" SortExpression="TeacherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTeacherName" runat="server" Text='<%# Bind("TeacherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <%--<asp:TemplateField HeaderText="Class" SortExpression="ClassCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Section" SortExpression="ClassSection">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassSection" runat="server" Text='<%# Bind("ClassSection") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>	
                                                                        <asp:TemplateField HeaderText="Age" SortExpression="Age">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAge" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <%--<asp:TemplateField HeaderText="Wear Glasses" SortExpression="WearGlasses">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWearGlasses" runat="server" Text='<%# Bind("WearGlasses") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>	
                                                                        <asp:TemplateField HeaderText="Diagnosis" SortExpression="Daignosis">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaignosis" runat="server" Text='<%# Bind("Daignosis") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Diagnosis Remarks" SortExpression="DaignosisRemarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaignosisRemarks" runat="server" Text='<%# Bind("DaignosisRemarks") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Sub Diagnosis" SortExpression="SubDaignosis">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubDaignosis" runat="server" Text='<%# Bind("SubDaignosis") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Treatment" SortExpression="Treatment">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTreatment" runat="server" Text='<%# Bind("Treatment") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Medicine" SortExpression="Medicine">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMedicine" runat="server" Text='<%# Bind("Medicine") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Next Visit" SortExpression="NextVisit">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNextVisit" runat="server" Text='<%# Bind("NextVisit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                       <%-- <asp:TemplateField HeaderText="Father Name" SortExpression="FatherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Father Cell" SortExpression="FatherCell">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFatherCell" runat="server" Text='<%# Bind("FatherCell") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Mother Name" SortExpression="MotherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMotherName" runat="server" Text='<%# Bind("MotherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Mother Cell" SortExpression="MotherCell">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMotherCell" runat="server" Text='<%# Bind("MotherCell") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                                                 
                                                                        <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>	
                                                                        <asp:TemplateField HeaderText="Optometrist Name" SortExpression="OptometristName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOptometristName" runat="server" Text='<%# Bind("OptometristName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    
                                                                    
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </asp:Panel>

                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                    </asp:Panel>            
                        <asp:Panel ID="pnlGlassDispense" GroupingText="Glass Distribution" runat="server" Visible="False">

                                <table class="auto-style1">
                                    <%--<tr>
                                        <td colspan ="5" class="auto-style2" style="text-align: left">
                                            <strong>Glass Distribution</strong></td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label19" runat="server"   Text="Student Detail"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                        <td colspan="2">
                                            <asp:Label ID="Label20" runat="server"   Text="Teacher Detail"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>                                                       
                                        <td style="text-align: left">
                                            <asp:Label ID="Label21" runat="server"  Text="School Name:" Width="250px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server"  Width="280px" Height="60px"></asp:Label>
                                        </td>                                                                                                     
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label23" runat="server"  Text="School Name:" Width="250px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server"   Width="280px" Height="60px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label25" runat="server"  Text="Glasses to be distributed:" Width="250px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label26" runat="server"   Width="250px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label27" runat="server"  Text="Glasses to be distributed:" Width="250px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label28" runat="server"   Width="250px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>                                                       
                                        <td style="text-align: left">
                                            <asp:Label ID="Label29" runat="server" Text="Glasses distributed:"  Width="250px" ></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label30" runat="server"   Width="250px"></asp:Label>
                                        </td>                                                                                                    
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label31" runat="server" Text="Glasses distributed:"  Width="250px" ></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label32" runat="server"   Width="250px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>                                                                                                      
                                        <td style="text-align: left">
                                            <asp:Label ID="Label33" runat="server" Text="Not satisfied:"  Width="250px" ></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label34" runat="server"   Width="250px"></asp:Label>
                                        </td>                                                                                                    
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label35" runat="server" Text="Not satisfied:"  Width="250px" ></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label36" runat="server"   Width="250px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                    </tr>
                                    </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlGlassDispensingDetail" GroupingText="Glass Distribution to Student" runat="server">

                                <table class="auto-style1">
                                    <tr>
                                                        
                                        <td colspan="4" style="text-align: left">
                                                            
                                            <asp:GridView ID="gvGlassDispense" runat="server" AutoGenerateColumns="False" Font-Size="10pt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Glasses to be distributed" SortExpression="StudentGlassestobedistributed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentGlassestobedistributed" runat="server" Text='<%# Bind("StudentGlassestobedistributed") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Glasses distributed" SortExpression="StudentGlassesdistributed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentGlassesdistributed" runat="server" Text='<%# Bind("StudentGlassesdistributed") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Not satisfied" SortExpression="Studentnotsatisfied">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentnotsatisfied" runat="server" Text='<%# Bind("Studentnotsatisfied") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Glasses to be distributed (Teacher)" SortExpression="TeacherGlassestobedistributed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTeacherGlassestobedistributed" runat="server" Text='<%# Bind("TeacherGlassestobedistributed") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Glasses distributed (Teacher)" SortExpression="TeacherGlassesdistributed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTeacherGlassesdistributed" runat="server" Text='<%# Bind("TeacherGlassesdistributed") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Teacher not satisfied" SortExpression="Teachernotsatisfied">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTeachernotsatisfied" runat="server" Text='<%# Bind("Teachernotsatisfied") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                    </EmptyDataTemplate>
                                            </asp:GridView>
                                                            
                                        </td>
                                        <td style="text-align: right">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                    </tr>
                                </table>
                                                
                                </asp:Panel>

                                <asp:Panel ID="pnlGlassDispensingDetail_Teacher" GroupingText="Glass Distribution to Teacher" runat="server">

                                                <table class="auto-style1">
                                                    <tr>
                                                        
                                                        <td colspan="4" style="text-align: left">
                                                            
                                                            <asp:GridView ID="gvGlassDispense_Teacher" runat="server" AutoGenerateColumns="False" Font-Size="10pt">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <%-- <asp:TemplateField HeaderText="Glasses to be distributed (Student)" SortExpression="StudentGlassestobedistributed">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStudentGlassestobedistributed" runat="server" Text='<%# Bind("StudentGlassestobedistributed") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Glasses distributed (Student)" SortExpression="StudentGlassesdistributed">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStudentGlassesdistributed" runat="server" Text='<%# Bind("StudentGlassesdistributed") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student not satisfied" SortExpression="Studentnotsatisfied">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStudentnotsatisfied" runat="server" Text='<%# Bind("Studentnotsatisfied") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Glasses to be distributed" SortExpression="TeacherGlassestobedistributed">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTeacherGlassestobedistributed" runat="server" Text='<%# Bind("TeacherGlassestobedistributed") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Glasses distributed" SortExpression="TeacherGlassesdistributed">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTeacherGlassesdistributed" runat="server" Text='<%# Bind("TeacherGlassesdistributed") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Not satisfied" SortExpression="Teachernotsatisfied">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTeachernotsatisfied" runat="server" Text='<%# Bind("Teachernotsatisfied") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                
                                                </asp:Panel>

                                <asp:Panel ID="pnlGlassDispensingList_Student" runat="server" GroupingText="Glasses Distribution to Students Not Satisfied">
                                                <asp:GridView ID="gvGlassDispensingList_Student" runat="server" AutoGenerateColumns="False" Font-Size="8pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Student Code" SortExpression="StudentCode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStudentCode" runat="server" Text='<%# Bind("StudentCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Student Name" SortExpression="StudentName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Wear Glasses" SortExpression="WearGlasses">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWearGlasses" runat="server" Text='<%# Bind("WearGlasses") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Vision with Glasses (Right Eye)" SortExpression="VisionAcuityRightEye">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVisionAcuityRightEye" runat="server" Text='<%# Bind("VisionAcuityRightEye") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Vision with Glasses (Right Eye)" SortExpression="VisionAcuityLeftEye">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVisionAcuityLeftEye" runat="server" Text='<%# Bind("VisionAcuityLeftEye") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Reason" SortExpression="Unsatisfied_Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnsatisfiedReason" runat="server" Text='<%# Bind("UnsatisfiedReason") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Next Visit" SortExpression="FollowupRequired">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFollowupRequired" runat="server" Text='<%# Bind("FollowupRequired") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	                                                       
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>

                                <asp:Panel ID="pnlGlassDispensingList_Teacher" runat="server" GroupingText="Glasses Distribution to Teachers Not Satisfied">
                                                <asp:GridView ID="gvGlassDispensingList_Teacher" runat="server" AutoGenerateColumns="False" Font-Size="8pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Teacher Code" SortExpression="TeacherCode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTeacherCode" runat="server" Text='<%# Bind("TeacherCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Teacher Name" SortExpression="TeacherName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTeacherName" runat="server" Text='<%# Bind("TeacherName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Wear Glasses" SortExpression="WearGlasses">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWearGlasses" runat="server" Text='<%# Bind("WearGlasses") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Vision with Glasses (Right Eye)" SortExpression="VisionAcuityRightEye">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVisionAcuityRightEye" runat="server" Text='<%# Bind("VisionAcuityRightEye") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Vision with Glasses (Right Eye)" SortExpression="VisionAcuityLeftEye">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVisionAcuityLeftEye" runat="server" Text='<%# Bind("VisionAcuityLeftEye") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Reason" SortExpression="Unsatisfied_Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnsatisfiedReason" runat="server" Text='<%# Bind("UnsatisfiedReason") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	
                                                        <asp:TemplateField HeaderText="Next Visit" SortExpression="FollowupRequired">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFollowupRequired" runat="server" Text='<%# Bind("FollowupRequired") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>	                                                       
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>


                                <asp:Panel ID="pnlCycloplegicRefraction" GroupingText="Cycloplegic Refraction" runat="server">
                                                <table class="auto-style1">
                                                    <%--<tr>
                                                        <td colspan ="5" class="auto-style2" style="text-align: left">
                                                            <strong>Cycloplegic Refraction</strong></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label37" runat="server" Text="Student Detail"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label38" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                       
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server"  Text="School Name:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label40" runat="server"   Width="280px" Height="60px"></asp:Label>
                                                        </td>                                                                                                     
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="Label41" runat="server"  Text="" Width="250px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label42" runat="server"   Width="280px" Height="60px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label43" runat="server"  Text="Cycloplegic Refraction to be done:" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label44" runat="server"   Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label45" runat="server"  Text="" Width="250px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label46" runat="server"   Width="250px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                      
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label47" runat="server" Text="Cycloplegic Refraction done:"  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label48" runat="server"   Width="250px"></asp:Label>
                                                        </td>                                                                                                    
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label49" runat="server" Text=""  Width="250px" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label50" runat="server"   Width="250px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                </asp:Panel>

                                                <table class="auto-style1">
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label51" runat="server" Text="Optometrist Name:" Width="250px" Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label52" runat="server" Width="250px" Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            <asp:Panel ID="pnlAutoRefTestnotperformed" runat="server" GroupingText="List of Students (Auto Ref Test not performed)">
                                            <table class="auto-style1">
                                                    <tr>
                                                        <td colspan="4" style="text-align: left">                                                            
                                                            <asp:GridView ID="gvAutoRefTestnotperformed" runat="server" AutoGenerateColumns="False" Font-Size="10pt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Student Code" SortExpression="StudentCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentCode" runat="server" Text='<%# Bind("StudentCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student Name" SortExpression="StudentName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Class" SortExpression="ClassCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Section" SortExpression="ClassSection">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassSection" runat="server" Text='<%# Bind("ClassSection") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Age" SortExpression="Age">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAge" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Father Name" SortExpression="FatherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Wear Glasses" SortExpression="WearGlasses">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWearGlasses" runat="server" Text='<%# Bind("WearGlasses") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>                                                          
                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlOptomertristTestnotperformed" runat="server" GroupingText="List of Students (Optomertrist Test not performed)">
                                                <table class="auto-style1">
                                                    <tr>
                                                        <td colspan="4" style="text-align: left">                                                            
                                                                <asp:GridView ID="gvOptomertristTestnotperformed" runat="server" AutoGenerateColumns="False" Font-Size="10pt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Student Code" SortExpression="Student Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentCode" runat="server" Text='<%# Bind("StudentCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student Name" SortExpression="Student Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="School Name" SortExpression="School Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Class" SortExpression="ClassCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Section" SortExpression="ClassSection">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClassSection" runat="server" Text='<%# Bind("ClassSection") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Age" SortExpression="Age">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAge" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Father Name" SortExpression="FatherName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Wear Glasses" SortExpression="WearGlasses">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWearGlasses" runat="server" Text='<%# Bind("WearGlasses") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                           

                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                </asp:Panel>
                                                
                                                <asp:Panel ID="pnlDailyStaffPerformance" runat="server" GroupingText="Daily Staff Performance">
                                                <table class="auto-style1">
                                                    <tr>
                                                        <td colspan="4" style="text-align: left">

                                                            
                                                                <asp:GridView ID="gvDailyStaffPerformance" runat="server" AutoGenerateColumns="False" Font-Size="10pt">
                                                                    <Columns>
                                                                        <%--<asp:TemplateField HeaderText="School Name" SortExpression="SchoolName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="User Name" SortExpression="UserName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Role" SortExpression="Role">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRole" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Test Performed" SortExpression="Auto_Ref_Test_Performed">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAutoRefTestPerformed" runat="server" Text='<%# Bind("Auto_Ref_Test_Performed") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <%--<asp:TemplateField HeaderText="Optometrist Test Performed" SortExpression="[Optometrist Test Performed]"></asp:TemplateField>	--%>
                                                                        <asp:TemplateField HeaderText="Identified for refractive error" SortExpression="Students_identified_for_refractive_error">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStudentsidentifiedforrefractiveerror" runat="server" Text='<%# Bind("Students_identified_for_refractive_error") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>	
                                                                        <asp:TemplateField HeaderText="Identified with other abnormalities" SortExpression="Students_identified_with_other_abnormailities">
                                                                             <ItemTemplate>
                                                                                <asp:Label ID="lblOptometristTestPerformed" runat="server" Text='<%# Bind("Students_identified_with_other_abnormailities") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Log In Time" SortExpression="LogInTime">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLogInTime" runat="server" Text='<%# Bind("LogInTime") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Log Out Time" SortExpression="LogOutTime">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLogOutTime" runat="server" Text='<%# Bind("LogOutTime") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Worked Hours" SortExpression="WorkedHours">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWorkedHours" runat="server" Text='<%# Bind("WorkedHours") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" ForeColor="Red" Text="No Data"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            

                                                        </td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
                                        </asp:Panel>
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
        .auto-style2 {
            font-size: x-large;
        }
        .auto-style3 {
            font-size: x-large;
            height: 38px;
        }
    </style>


</asp:Content>
