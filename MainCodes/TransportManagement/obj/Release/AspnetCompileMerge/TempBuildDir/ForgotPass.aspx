<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPass.aspx.cs" Inherits="TransportManagement.ForgotPass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>

    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/bootstrap.rtl.min.css" />
    <link rel="stylesheet" href="Content/bootstrap.css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <section class="vh-100" style="background-color: #508bfc;">
                <asp:Panel ID="PnlForgotPass" Visible="false" runat="server">
                    <div class="container py-5 h-100">
                    <div class="row d-flex justify-content-center align-items-center h-100">
                        <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                            <div class="card shadow-2-strong" style="border-radius: 1rem;">
                                <div class="card-body p-5 text-center">
                                 
                            
                                <h5 class="m-t-0 header-title"><b>Forgot Password</b></h5>
                                           
                                <div class="form-outline mb-4">
                                        <label class="form-label" for="txtUseID">User ID*</label>                                                                                         
                                        <asp:TextBox ID="txtUseID" runat="server" CssClass="form-control form-control-sm" MaxLength="200"></asp:TextBox>                                                                                          
                                </div>
                                                         
                                <asp:LinkButton ID="btnSendEmail" OnClick="btnSendEmail_Click"  runat="server" ClientIDMode="Static" CssClass="btn btn-primary btn-block text-uppercase waves-effect waves-light" Text="Send Password over Email"></asp:LinkButton>
                                                    
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <p class="text-center text-danger">
                                                <asp:Label runat="server" ID="lblErrorForgotpass"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

                <asp:Panel ID="pnlEnterPin" Visible="false" runat="server">
                      <div class="container py-5 h-100">
                        <div class="row d-flex justify-content-center align-items-center h-100">
                            <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                                <div class="card shadow-2-strong" style="border-radius: 1rem;">
                                    <div class="card-body p-5 text-center">
                                 
                            
                                    <h5 class="m-t-0 header-title"><b>PIN Code</b></h5>
                                           
                                    <div class="form-outline mb-4">
                                            <label class="form-label" for="txtPinCode">We have emailed you a PIN code on the email address defined in your profile. Please enter the PIN code below.</label>                                                                                         
                                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control form-control-sm" MaxLength="10"></asp:TextBox>                                                                                          
                                            <asp:FilteredTextBoxExtender ID="txtPinCode_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtPinCode" />
                                    </div>
                                                         
                                    <asp:LinkButton ID="lnkSubmitPin" OnClick="lnkSubmitPin_Click"  runat="server" ClientIDMode="Static" CssClass="btn btn-primary btn-block text-uppercase waves-effect waves-light" Text="Submit"></asp:LinkButton>
                                                    
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <p class="text-center text-danger">
                                                    <asp:Label runat="server" ID="lblErrorPIN"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                   </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlChangePass" Visible="false" runat="server">
                      <div class="container py-5 h-100">
                        <div class="row d-flex justify-content-center align-items-center h-100">
                            <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                                <div class="card shadow-2-strong" style="border-radius: 1rem;">
                                    <div class="card-body p-5 text-center">
                                 
                            

                                    <div class="form-outline mb-4">
                                        <asp:Label ID="lblUserIDChangePass" Font-Bold="true" Text="User ID: zeeshan.ahmed" runat="server"></asp:Label>
                                    </div>
                                        
                                    <div class="form-outline mb-4">
                                        <div class="form-group">
                                            <label for="Vehicle">
                                                New Password*</label>
                                            <asp:TextBox ID="txtNewPass" type="password" runat="server" CssClass="form-control form-control-sm" MaxLength="500">                                                
                                            </asp:TextBox>                                            
                                        </div>
                                    </div>

                                        <div class="form-outline mb-4">
                                        <div class="form-group">
                                            <label for="Vehicle">
                                                Confirm Password*</label>
                                            <asp:TextBox ID="txtNewPassConfirm" type="password" runat="server" CssClass="form-control form-control-sm" MaxLength="500">                                                
                                            </asp:TextBox>                                            
                                        </div>
                                    </div>
                                                         
                                    <asp:LinkButton ID="lnkBtnChangePass" OnClick="lnkBtnChangePass_Click" runat="server" ClientIDMode="Static" CssClass="btn btn-primary btn-block text-uppercase waves-effect waves-light" Text="Submit"></asp:LinkButton>
                                                    
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <p class="text-center text-danger">
                                                    <asp:Label runat="server" ID="lblErrorChangePass"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>                                                                                                                                         
                                   </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </section>
        </div>
        <asp:HiddenField ID="hfUserID" Value="" runat="server" />
    </form>

    <script src="Scripts/jquery-3.6.0.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
