<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TransportManagement.ChangePassword" %>
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
                                    <b>Change Password</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="UserId">
                                                User Id</label>
                                                <asp:TextBox ID="txtUserId" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="OldPassword">
                                                Old Password *</label>
                                                <asp:TextBox ID="txtOldPassword" type="password" runat="server" CssClass="form-control">                                                
                                            </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="NewPassword">
                                                New Password *</label>
                                                <asp:TextBox ID="txtNewPassword" type="password" runat="server" CssClass="form-control">                                                
                                            </asp:TextBox>
                                            </div>
                                        </div>                                        
                                        <div class="col-sm-6">
                                            <label for="ChangePasswordButton">
                                            &nbsp;</label>
                                            <div class="form-group text-left">
                                               
                                                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Save"></asp:LinkButton>                                                
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
                                    <p>
                                    </p>
                                </p>

                            
                            </div>
                        </div>
                    </div>
                            </asp:Panel>
                </div>

                </div>
            </div>
            </div>        
        <asp:HiddenField ID="hfLoginUserId" Value="" runat="server" />
    </ContentTemplate>

</asp:UpdatePanel>

</asp:Content>
