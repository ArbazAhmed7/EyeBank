<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPhotos.aspx.cs" Inherits="TransportManagement.LookupControl.ViewPhotos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View All Photos</title>
     
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

            popimage();
        });

        function pageLoad() {
            //hack to show close button icon on jquery dialog
            var bootstrapButton = $.fn.button.noConflict() // return $.fn.button to previously assigned value
            $.fn.bootstrapBtn = bootstrapButton            // give $().bootstrapBtn the Bootstrap functionality                         
        }
        function popimage(btn) {
            if (btn) {
                FromImage = $(btn)
                ToImage = $("#Image1")
                ToImage.attr("src", FromImage.attr("src"))

                pHeight = ($(window).height() * 0.96)
                pWidth = ($(window).width() * 0.80)

                myDialog = $("#imagepop");
                myDialog.dialog({
                    title: "View Photo",
                    modal: true,
                    height: pHeight,
                    width: pWidth,
                    buttons: {

                        Ok: function () {
                            myDialog.dialog("close")
                        }
                    }
                })
            }            
        }
        
    </script>
    <style type="text/css">
        .imgzoom:hover{
             -ms-transform: scale(2); /* IE 9 */
              -webkit-transform: scale(2); /* Safari 3-8 */
              transform: scale(2); 
        }

        .ui-widget-header,.ui-state-default, ui-button {
            background:#0b0a64;
            border: 1px solid #b9cd6d;
            color: #FFFFFF;
            font-weight: bold;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scViewPhotos" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updatePanel1" runat="server">
            <ContentTemplate>

                <div id="wrapper">
	                <div class="content-page">
		                <div class="content">
			                <div class="container">
			
				                <div class="row">                        
					                <div class="col-md-12">
						                <div class="card-box table-responsive">
                                            <br />
                                            <h5 class="m-t-0 header-title">
                                                <b>Picture Lookup</b></h5>
                                            <p class="text-muted font-13 m-b-30">
                                                <hr />
                                                <p>
                                                </p>
                                                <asp:GridView ID="grdPhotos" runat="server"  OnRowDeleting="grdPhotos_RowDeleting" AutoGenerateColumns="false" DataKeyNames="AutoKeyID,FormID" CssClass="table table-striped table-bordered m-b-0" OnRowDataBound="grd_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>                                                                
                                                                <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this photo?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  

                                                        <asp:TemplateField HeaderText="image">
                                                            <ItemTemplate>
                                                                <%--<img src='<%# Eval("Pic") %>' width="150" height="150" id="imageControl" runat="server" />--%>
                                                                <asp:ImageButton ID="btnImage" ClientIDMode="Static" runat="server" width="150" height="150" OnClientClick ="return popimage(this);return false"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Capture Remarks" SortExpression="CaptureRemarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" CssClass="lblCss" Text='<%# Bind("CaptureRemarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:boundfield convertemptystringtonull="true" datafield="CaptureDate" DataFormatString="{0:dd/MMM/yyyy}" headertext="Capture Date" />
                                                    </Columns>
                                                </asp:GridView>
                                            </p>

						                </div>
					                </div>
				                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblNodata" Visible="false" Text="No data" ForeColor="Red" runat="server"></asp:Label>                                       
                                    </div>
                                </div>

                                <div class="row">
                                     <div id="imagepop" style="display:none;text-align:center;height:80%">
                                        <asp:Image ID="Image1" runat="server" ClientIDMode="Static"
                                        style="height:96%"/>
                                    </div>
                                </div>
			
			                </div>
		                </div>
	                </div>
                </div>
                    
                    
                  
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>
</body>
</html>

