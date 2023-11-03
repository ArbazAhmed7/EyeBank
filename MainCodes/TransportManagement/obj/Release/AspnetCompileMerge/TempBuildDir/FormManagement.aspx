<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FormManagement.aspx.cs" Inherits="TransportManagement.FormManagement" %>
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
                                    <b>Form Management</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="UserId">
                                                User Id *</label>
                                                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" MaxLength="30">                                                
                                            </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="RoleManagement">
                                                Role *</label>
                                                <asp:DropDownList ID="ddlForm" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label for="UserManagementButton">
                                            &nbsp;</label>
                                            <div class="form-group text-left">
                                                <asp:LinkButton ID="btnEdit" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Text="Update"></asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton ID="btnSave" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Text="Save"></asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton ID="btnAbort" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Text="Abort"></asp:LinkButton>
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
                      <div class="row">
                        
                                <div class="col-md-12">
                                    <div class="card-box table-responsive">
                                     <hr />

                                        <asp:GridView ID="gvRole" ClientIDMode="Static" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-striped table-bordered m-b-0" AllowPaging="true" PageSize="20" DataKeyNames="RoleAutoId" >
                                            <%-- OnRowEditing="gvRole_RowEditing" OnRowDeleting="gvRole_RowDeleting" OnPageIndexChanging="gvRole_PageIndexChanging"--%>

                                            <Columns>
                                                   <%--<asp:TemplateField>    
                                                        <ItemTemplate>    
                                                            <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />   
                                                        </ItemTemplate>     
                                                    </asp:TemplateField> --%>
                                                      <asp:TemplateField>    
                                                        <ItemTemplate>      
                                                            <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandName="Delete" /> 
                                                        </ItemTemplate>     
                                                    </asp:TemplateField> 
                                                <%--<asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            
                                                <asp:TemplateField HeaderText="User Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UserId" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Role" runat="server" Text='<%# Eval("RoleDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblact" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Create Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_InsertDateTime" runat="server" Text='<%# Eval("InsertDateTime", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                </div>

                </div>
            </div>
</div>
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
    </ContentTemplate>

</asp:UpdatePanel>

</asp:Content>
