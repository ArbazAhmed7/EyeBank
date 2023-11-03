<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="TransportManagement.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
		ul.no-bullets li
		{
			list-style-type: none;
		}
	    .auto-style1 {
            width: 232px;
        }
        .auto-style3 {
            width: 253px;
        }
	    .auto-style6 {
            width: 344px;
        }
        .auto-style7 {
            width: 222px;
        }
        .auto-style8 {
            width: 171px;
        }
        .auto-style9 {
            width: 316px;
        }
        .auto-style12 {
            width: 92px;
        }
	</style>
	<script>		function openLinkAndClose(url) {			var newWindow = window.open(url);			setTimeout(function () {             newWindow.onload = function () {                window.close();			};            },1000)        }</script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">
		<div class="content-page">
			<div class="content">
				<div class="container">

					 <div class="row">
						<div class="col-md-12">
							<div class="card-box">
								<h5 class="m-t-0 header-title"><b>Dashboard</b></h5>
								<p class="text-muted font-13 m-b-30"><hr /></p>

								 
							</div>
						</div>
					</div>
					<asp:Panel ID="pnlDashboard" runat="server">
					<div class="row">
						<div class="col-md-12">
							<table class="auto-style6">
								<tr >
									<td class="auto-style1" scope="row">School Screened</td>
									<td style="text-align: right" class="auto-style8">
                                        <asp:Label ID="lblSchoolScreened" runat="server"></asp:Label>
                                    </td>
								</tr>
								<tr>
									<td class="auto-style1" scope="row">Student Screened</td>
									<td style="text-align: right" class="auto-style8">
                                        <asp:Label ID="lblStudentScreened" runat="server"></asp:Label>
                                    </td>
								</tr>
									<tr>
										<td style="padding-left:20px;" scope="row">Girls Screened</td>
										<td style="text-align: right" class="auto-style8">
                                            <asp:Label ID="lblGirlScreened" runat="server"></asp:Label>
                                        </td>
									</tr>
									<tr>
										<td style="padding-left:20px;" scope="row">Boys Screened</td>
										<td style="text-align: right" class="auto-style8">
                                            <asp:Label ID="lblBoyScreened" runat="server"></asp:Label>
                                        </td>
									</tr>
									<tr>
										<td style="padding-left:20px;" scope="row">Teachers Screened</td>
										<td style="text-align: right" class="auto-style8">
                                            <asp:Label ID="lblTeacherScreened" runat="server"></asp:Label>
                                        </td>
									</tr>
								<tr>
									<td class="auto-style1" scope="row">Prescribed Glasses</td>
									<td style="text-align: right" class="auto-style8">
                                        <asp:Label ID="lblPrescribedGlasses" runat="server"></asp:Label>
                                    </td>
								</tr>								 
							</table>
						</div>
					</div>
					

					<br />
					<div class="row">
						<div class="col-md-12">
							<div class="card-box">
								<h6 class="m-t-0 header-title">Student diagnosed with other issues</h6>
							</div>
						</div>
					</div>

					<div class="row">
						<div class="col-md-12">
							<table class="auto-style6">
								<tr >
									<td class="auto-style7" scope="row">Surgeries</td>
									<td style="text-align: right">
                                        <asp:Label ID="lblSurgeries" runat="server"></asp:Label>
                                    </td>
								</tr>
							</table>
						</div>
					</div>

					<br />
					<div class="row">
						<div class="col-md-12">
							<div class="card-box">
								<h6 class="m-t-0 header-title">Annual Target</h6>
							</div>
						</div>
					</div>

					<div class="row">
						<div class="col-md-12">
							<table>
								<tr>
									<td class="auto-style3" scope="row">Students</td>
									<td style="text-align: right" class="auto-style12">
                                        <asp:Label ID="lblStudentTarget" runat="server"></asp:Label>
                                    </td>
								</tr>
								<tr >
									<td class="auto-style3" scope="row">Achieved</td>
									<td style="text-align: right" class="auto-style12">
                                        <asp:Label ID="lblStudentTargetAchieved" runat="server"></asp:Label>
                                    </td>
								</tr>
								<tr>
									<td class="auto-style3" scope="row">To be achieved</td>
									<td style="text-align: right" class="auto-style12">
                                        <asp:Label ID="lblStudentTobeAchieved" runat="server"></asp:Label>
                                    </td>
								</tr>
							</table>
						</div>
					</div>
					</asp:Panel>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
