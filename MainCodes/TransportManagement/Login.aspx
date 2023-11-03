<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TransportManagement.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/bootstrap.rtl.min.css" />
    <link rel="stylesheet" href="Content/bootstrap.css" />

    <style type="text/css">
        body, html {
          height: 100%;
          margin: 0%;
          font: 400 15px/1.8 "Lato", sans-serif;
          color: #777;
        }

        .bgimg-1, .bgimg-2, .bgimg-3 {
          position: relative;
          opacity: 1;
          background-position: center;
          background-repeat: no-repeat;
          background-size: cover;
        }
        .bgimg-1 {
          background-image: url("Content/login_bg_new.jpg");
          height: 100%;
          opacity: 0.85;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btn_login">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="bgimg-1">
            <section class="vh-100">
              <div class="container py-5 h-100">
                <div class="row d-flex justify-content-center align-items-center h-100">
                  <div class="col col-xl-7">
                    <div class="card" style="border-radius: 1rem;">
                      <div class="row g-0">
                        <div class="col-md-6 col-lg-5 d-none d-md-block">
                          <img src="Content/esms_large.jpg"
                            alt="login form" class="img-fluid" style="border-radius: 1rem 0 0 1rem;" />
                        </div>
                        <div class="col-md-6 col-lg-7 d-flex align-items-center">
                          <div class="card-body p-4 p-lg-5 text-black">
 
                              <%--<div class="d-flex align-items-center mb-3 pb-1">
                                <i class="fas fa-cubes fa-2x me-3" style="color: #ff6219;"></i>
                                <span class="h1 fw-bold mb-0">Logo</span>
                              </div>--%>

                              <h5 class="fw-normal mb-3 pb-3" style="letter-spacing: 1px;">Sign into your account</h5>

                              <div class="form-outline mb-4">
                                <asp:TextBox ID="txtUserName" placeholder="Enter a valid User ID" runat="server" class="form-control form-control-lg"></asp:TextBox>                      
                                <label class="form-label" for="txtUserName">User ID</label>                                
                              </div>

                              <div class="form-outline mb-4">
                                <asp:TextBox ID="txtpassword" type="password" runat="server"  class="form-control form-control-lg"></asp:TextBox>                        
                                <label class="form-label" for="txtpassword">Password</label>
                              </div>

                              <div class="pt-1 mb-4">                                
                                  <asp:LinkButton runat="server" ID="btn_login" style="padding-left: 2.5rem; padding-right: 2.5rem;" OnClick="btn_login_Click" class="btn btn-primary btn-block waves-effect waves-light" Text="Login"></asp:LinkButton>
                              </div>

                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <p class="text-center text-danger">
                                            <asp:Label runat="server" ID="lbl_error"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                              <div>
                                  <asp:LinkButton ID="lnkForgPass" Text="Forgot Password?" runat="server" OnClick="lnkForgotPassword_Click" class="small text-muted"></asp:LinkButton>
                              </div>                               
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </section>


        </div>
    <script src="Scripts/jquery-3.6.0.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    </form>
    </body>
</html>
