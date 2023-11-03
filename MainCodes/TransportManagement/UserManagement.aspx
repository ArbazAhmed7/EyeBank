<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="TransportManagement.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">

                        <div class="info" style="padding-left:5px;">
                            <asp:Label ID="Label1" Text="This Form is used to assign Role to any created User." runat="server"></asp:Label></p>
                            <asp:Label ID="Label2" Text="To Assign any role to user, First select any user then select any role from 'Role Description' dropdown and click Save button." runat="server"></asp:Label></p>
                            <asp:Label ID="Label3" Text="To Remove role association with user, User can click Delete link button from the Grid." runat="server"></asp:Label></p>
                        </div>

                        <asp:Panel ID="pnlCom" runat="server" DefaultButton="btnSave">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>Role Management</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                    

                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="txtUseID">User ID*</label>
                                             
                                                <div class="input-group input-group-sm mb-3">
                                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength="200"></asp:TextBox>
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
                                                <label for="RoleDescription">
                                                Role Description *</label>
                                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control form-control-sm">                                                     
                                                </asp:DropDownList>    
                                                
                                            </div>
                                        </div>

                                         <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="txtUseID"></label>                                             
                                                <div class="input-group input-group-sm mb-3">                                                    
                                                    <span class="input-group-append">                                    
                                                        <asp:LinkButton ID="lnkAddRole" OnClick="lnkAddRole_Click" Text="Add new Role" runat="server"></asp:LinkButton>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                         
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label for="UserManagementButton">
                                            &nbsp;</label>
                                            <div class="form-group text-left">                                                 
                                                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Save"></asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default" Text="Abort"></asp:LinkButton>
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

                                        <asp:GridView ID="gvUserRole" ClientIDMode="Static" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-striped table-bordered m-b-0" DataKeyNames="ID" OnRowDeleting="gvUserRole_RowDeleting"  >
                                            <%-- OnRowEditing="gvRole_RowEditing" OnRowDeleting="gvRole_RowDeleting" OnPageIndexChanging="gvRole_PageIndexChanging"--%>
                                            <Columns>                                   
                                                <asp:TemplateField>
                                                    <ItemTemplate>      
                                                        <asp:LinkButton ID="lnkDelete" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete" runat="server" CommandName="Delete" /> 
                                                    </ItemTemplate>     
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="User ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Role" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Form" runat="server" Text='<%# Eval("RoleDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
 

        <asp:HiddenField ID="hfLookupSelectedUser" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupSelectedUser_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfAutoCompleteSelectedUser" Value="0" ClientIDMode="Static" OnValueChanged="hfAutoCompleteSelectedUser_ValueChanged"  runat="server" />
        <asp:HiddenField ID="hfSelectedUserAutoID" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupSelectedUser_ValueChanged" runat="server" />
    </ContentTemplate>

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

    <script type="text/javascript">
        function pageLoad() {
            UserIdAutoComplete();
        }

        function UserIdAutoComplete() {
            $("[id$=txtUserId]")
                .blur(function () {
                    var keyEvent = $.Event("keydown");
                    keyEvent.keyCode = $.ui.keyCode.ENTER;
                    $(this).trigger(keyEvent);
                })
                .autocomplete({
                    autoFocus: true,
                    source: function (request, response) {
                        $.ajax({
                            url: "UserManagement.aspx/AutoComplete",
                            data: "{'Term' :'" + request.term + "','TermType' :'UserID','Id' :'0'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                console.log(data);
                                response($.map(data.d, function (item) {

                                    return {
                                        label: item.label,
                                        val: item.val,
                                        json: item
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus);
                            }
                        });
                    },
                    focus: function (event, ui) {
                        // this is for when focus of autocomplete item 
                        //$("[id$=txtUseID]").val(ui.item.label);
                        return false;
                    },
                    select: function (event, ui) {
                        // this is for when select autocomplete item
                        $("[id$=txtUserId]").val(ui.item.label);
                        $("[id$=hfAutoCompleteSelectedUser]").val(ui.item.val);
                        //window.location.reload(true);
                        __doPostBack('UpdatePanel1', '');
                        return false;
                    }

                }).data("ui-autocomplete")._renderItem = function (ul, item) {
                    // here return item for autocomplete text box, Here is the place 
                    // where we can modify data as we want to show as autocomplete item
                    return $("<li>").append(item.label).appendTo(ul);
                };
        } 
    </script>

</asp:Content>
