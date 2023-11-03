<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserCreation.aspx.cs" Inherits="TransportManagement.UserCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    
    <script src="Scripts/FormsScripts/UserCreation.js?v=2"></script>

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
         
 
        function validateInput()
        {
            
            var valRes = true;

            if ($("[id$=txtUseID]").val().trim() == "") {
                $("[id$=txtUseID]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtUseID]").removeAttr("style");
            }

            if ($("[id$=txtPassword]").val().trim() == "") {
                $("[id$=txtPassword]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtPassword]").removeAttr("style");
            }

            if ($("[id$=txtUserName]").val().trim() == "") {
                $("[id$=txtUserName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtUserName]").removeAttr("style");
            }

            if (!valRes) {
                $("[id$=lbl_error]").text('* Mandatory');
            }
            else {
                $("[id$=lbl_error]").text('');
            }

            return valRes;
        }
         
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>        
    <div id="wrapper">
        <div class="content-page">
            <div class="content">
                <div class="container">
 
                <div class="info" style="padding-left:5px;">
                  <asp:Label ID="tt1" Text="To Create New user: Enter New User ID, Password, User Name, Email & click Save button." runat="server"></asp:Label></p>
                </div>
                <div class="info" style="padding-left:5px;">
                  <asp:Label ID="Label1" Text="To Edit already created user: Select User ID and update data & click Save button." runat="server"></asp:Label></p>
                </div>



                    <asp:Panel ID="panelMain" runat="server" DefaultButton="btnSave">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>User Creation</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                </p>

                            
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="txtUseID">User ID*</label>
                                             
                                            <div class="input-group input-group-sm mb-3">
                                                <asp:TextBox ID="txtUseID" runat="server" CssClass="form-control rounded-pill" MaxLength="200"></asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="linkButtonLookup" OnClick="linkButtonLookup_Click" runat="server" ClientIDMode="Static" Text="Abort" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span> 
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="txtUseID">Password*</label>
                                             
                                            <div class="input-group input-group-sm mb-3">
                                                <asp:TextBox ID="txtPassword" type="password" runat="server" CssClass="form-control form-control-sm" MaxLength="500"></asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="linkShowPasswordd" runat="server" ClientIDMode="Static" Text="Abort" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-eye"></i>
                                                    </asp:LinkButton>
                                                </span> 
                                            </div>
                                        </div>
                                    </div>
                                      
                                   
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="Vehicle">
                                                User Name*</label>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control form-control-sm" MaxLength="500">                                                
                                            </asp:TextBox>                                             
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="Vehicle">
                                                Email Address</label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" MaxLength="500">                                                
                                            </asp:TextBox>
                                            

                                        </div>
                                    </div>    
                                     
                                   
   
                                    <div class="col-sm-6">
                                        <label for="CustomerNo">
                                                &nbsp;</label>
                                       <div class="form-group text-left">
                                           <asp:LinkButton ID="btnEdit" OnClientClick="return validateInput()" runat="server" ClientIDMode="Static" OnClick="btnEdit_Click"  Text="Update" CssClass="btn btn-default"></asp:LinkButton>
                                           <asp:LinkButton ID="btnSave" OnClientClick="return validateInput()" runat="server" ClientIDMode="Static" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-default"></asp:LinkButton>
                                           <asp:LinkButton ID="btnDelete" runat="server" ClientIDMode="Static" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to Delete this record?');"  Text="Delete" CssClass="btn btn-default"></asp:LinkButton>
                                           <asp:LinkButton ID="btnAbort" runat="server" ClientIDMode="Static" OnClick="btnAbort_Click"  Text="Abort" CssClass="btn btn-default"></asp:LinkButton>
                                       </div>
                                    </div>
                                </div>
                          
                                    <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <p class="text-center text-danger">
                                                <asp:Label runat="server" ID="lbl_error"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                        </asp:Panel>
                    <div class="row">                        
                        <div class="col-md-12">
                            <div class="card-box table-responsive">
                                <hr />
                                <asp:GridView ID="gridUsers" OnPageIndexChanging="gridUsers_PageIndexChanging" ClientIDMode="Static" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-striped table-bordered m-b-0" AllowPaging="true" PageSize="20" DataKeyNames="UserAutoId"
                                             >
                                    <pagersettings mode="NumericFirstLast"
                                        firstpagetext="First"
                                        lastpagetext="Last"
                                        pagebuttoncount="4"  
                                        position="Bottom"/> 
                                            <Columns>                                                 
                                                <asp:TemplateField HeaderText="User ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldno" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="User Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblexdt" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblniuc" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Active Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIsActive" AutoPostBack="true" OnCheckedChanged="chkIsActive_CheckedChanged" Checked='<%# Eval("IsActive").ToString().Equals("1") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div class="row">
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
    <asp:HiddenField ID="hfUserIDPKID" Value="0" OnValueChanged="hfUserIDPKID_ValueChanged" runat="server" />
    <asp:HiddenField ID="hfLookupResult" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResult_ValueChanged" runat="server" />
    </ContentTemplate>

</asp:UpdatePanel>

<style type="text/css">
        
 
</style>


</asp:Content>
