<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ToolPost.login._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   
    <div class="login-page">
        <div class="animsition">
            <main class="login-container">

                <div class="panel-container">
                    <section class="panel">
                        <header class="panel-heading">
                            <p>Đăng nhập Hệ thống</p>
                        </header>
                        <div class="panel-body">
                            <form runat="server" defaultbutton="ltBLogin">
                                <div class="form-group">
                                    <label for="username">*Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"  placeholder="Username"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label for="password">*Password</label>
                                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="pull-right forgot-link" NavigateUrl="~/login/recover.aspx"><small>Quên mật khẩu?</small></asp:HyperLink>
                                    <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Password" runat="server" TextMode="Password" ></asp:TextBox>
                                </div>

                                <div class="form-group text-center">
                                    <a href="/" ></a>
                                    <asp:LinkButton ID="ltBLogin" runat="server" CssClass="btn-login btn btn-primary" OnClick="btnDangNhap_Click">Đăng Nhập<i class="ti-unlock"></i><i class="ti-lock"></i> </asp:LinkButton>
                                </div>
                                <hr/>(*) Bắt buộc nhập
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="(*) Nhập username" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="(*) Nhập mật khẩu" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </form>
                            <hr/>
                            <div class="register-now">
                                Bạn chưa có tài khoản? <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/register/">Đăng ký ngay!</asp:HyperLink>
                            </div>
                        </div>
                    </section>
                </div>
            </main>
            <!-- /playground -->
        </div>
    </div>
    
</body>
</html>
