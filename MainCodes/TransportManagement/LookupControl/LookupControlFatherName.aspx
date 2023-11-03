<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LookupControlFatherName.aspx.cs" EnableEventValidation = "false" Inherits="TransportManagement.LookupControl.LookupControlFatherName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <link href="../Content/style.css" rel="stylesheet" />
    <link href="../Content/component.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>
    
    <!-- Bootstrap CSS CDN -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous"/>

    <!-- Font Awesome JS -->
    <script defer src="https://use.fontawesome.com/releases/v5.0.13/js/solid.js" integrity="sha384-tzzSw1/Vo+0N5UhStP3bvwWPq+uvzCMfrN1fEFe+xBmv1C/AtVX5K0uZtmcHitFZ" crossorigin="anonymous"></script>
    <script defer src="https://use.fontawesome.com/releases/v5.0.13/js/fontawesome.js" integrity="sha384-6OIrr52G08NpOFSZdxxz1xdNSndlD4vdcf/q2myIUVO0VsqaGHJsB0RaBE01VTOY" crossorigin="anonymous"></script>


<!-- jQuery CDN - Slim version (=without AJAX) -->
     <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    
    
    <!-- Popper.JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.0/umd/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <!-- Bootstrap JS -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/3b5d89da81.js" crossorigin="anonymous"></script>

    <script type="text/javascript">
        $(document).ready(function () {



        });

        function pageLoad() {
            TypeSearch();
            //$("[id$=txtSearch]").focus();
            //keep focus
            var input = $("[id$=txtSearch]");
            var tmp = input.val();
            input.focus().val("").blur().focus().val(tmp);

        }

        function TypeSearch() {
            $("[id$=txtSearch]").on("input", function () {
                if ($("[id$=txtSearch]").val().length > 1) {
                    var dInput = this.value;

                    $("[id$=btnSearch]").trigger("click");

                }
                else if ($("[id$=txtSearch]").val().length == 0) {
                    $("[id$=btnSearch]").trigger("click");
                }
            });
        }



    </script>    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sclookup" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updatePanel1" runat="server">
            <ContentTemplate>
                <div>            
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblSearch" runat="server" Text="Search by Name"></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtSearch" runat="server" Width="201px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                &nbsp;</td>
                            <td class="auto-style3">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>            
                </div> 
                <div>    
                 <asp:GridView ID="gvitems" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
                     Width="399px" OnSelectedIndexChanging="gvitems_SelectedIndexChanging"  CssClass="table table-striped table-bordered m-b-0"
                     Font-Names="Verdana" Font-Size="10pt" OnRowDataBound="gvitems_RowDataBound" OnSelectedIndexChanged="gvitems_SelectedIndexChanged"
                     OnPageIndexChanging="gvitems_PageIndexChanging" OnSorting="gvitems_Sorting">
                    <pagersettings mode="NumericFirstLast"
                        firstpagetext="First"
                        lastpagetext="Last"
                        pagebuttoncount="4"  
                        position="Bottom"/> 
                     <Columns>    
                         <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfSelectedID" runat="server" Value='<%#Eval("Id")%>'/>
                                <asp:LinkButton ID="lnkRecordSelected" runat="server" CausesValidation="False" 
                                    CommandName="Select"
                                    Text="Select" >
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="7%" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Code" SortExpression="Code">

                            <ItemStyle HorizontalAlign="left" Width="70px" Wrap="True" />
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" CssClass="lblCss"
                                    Text='<%# Bind("Code") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="left" Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name" SortExpression="Name">

                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="lblCss"
                                    Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="True" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Father Name" SortExpression="FatherName">
                             <ItemTemplate>
                                 <asp:Label ID="lblFatherName" runat="server" CssClass="lblCss" Text='<%# Bind("FatherName") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>


                         <asp:TemplateField HeaderText="Description" SortExpression="Description">
                             <ItemTemplate>
                                 <asp:Label ID="lblDescription" runat="server" CssClass="lblCss" 
                                     Text='<%# Bind("Description") %>'></asp:Label>
                             </ItemTemplate>
                             <ItemStyle Width="150px" Wrap="False" />
                         </asp:TemplateField>


                     </Columns>    
                     <EmptyDataTemplate>
                         <table class="auto-style1" border="1">
                             <tr>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     <asp:Label ID="lblColumn2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" Text="Code"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblColumn3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" Text="Name"></asp:Label>
                                 </td>
                                 <td>&nbsp;</td>
                             </tr>
                             <tr>
                                 <td style="text-align: center;vertical-align: middle;" class="auto-style4" colspan="4">
                                     <asp:Label ID="Label1" runat="server"  Font-Names="Verdana" Font-Size="12px" ForeColor="Red" Text="No Data" Width="350px"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4">&nbsp;</td>
                             </tr>
                         </table>
                     </EmptyDataTemplate>
                 </asp:GridView>    
             </div>   
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>
</body>
</html>
