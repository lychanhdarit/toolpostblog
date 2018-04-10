<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="default.aspx.cs" Inherits="ToolPost.blogs._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
    <!-- PAGE TITLE -->
    <section id="page-title" class="row">

        <div class="col-md-8">
            <h1>QUẢN LÝ BLOG</h1>
          
        </div>
    </section>
    <!-- / PAGE TITLE -->

    <div class="container-fluid white-bg">

        <div class="row">
            <div class="col-md-4">
                 <div class="box box-danger">
                    
                    <div class="box-body">
                        <div class="form-group">
                            <label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtID" ForeColor="Red" ValidationGroup="g" Display="Dynamic" ></asp:RequiredFieldValidator>ID Blog hoặc tên miền:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-key"></i>
                                </div>
                                <asp:TextBox ID="txtID" runat="server" CssClass="form-control" placeholder="nhập id blogspot hoặc tên miền wordpress"></asp:TextBox>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <label>Tên blog:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-edit"></i>
                                </div>
                                <asp:TextBox ID="txtTieuDe" runat="server" CssClass="form-control" placeholder="nhập tên blog của bạn"></asp:TextBox>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <label><asp:RequiredFieldValidator ID="rp" runat="server" ErrorMessage="*" ControlToValidate="txtEmailSecrect"  ForeColor="Red" ValidationGroup="g" Display="Dynamic" ></asp:RequiredFieldValidator>Email Secrect:(xem cách lấy email của blogspot - wordpress.com)</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                   <i class="fa fa-envelope" aria-hidden="true"></i>
                                </div>
                                <asp:TextBox ID="txtEmailSecrect" runat="server" CssClass="form-control" placeholder="nhập email mà bạn cài đặt để đăng bài"></asp:TextBox>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                             <label>Chọn danh mục:<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/blog/danh-muc-blog.aspx" Target="_blank">(thêm danh mục quản lý)</asp:HyperLink></label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                   <i class="fa fa-envelope" aria-hidden="true"></i>
                                </div>
                                <asp:DropDownList ID="ddlDanhmuc" runat="server" DataTextField="Name" DataValueField="id" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <!-- /.input group -->
                        </div>
                         <div class="form-group">
                             <label>Chọn email đăng bài:<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/email/">(thêm email đăng bài)</asp:HyperLink></label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                   <i class="fa fa-envelope" aria-hidden="true"></i>
                                </div>
                                <asp:DropDownList ID="ddlEmail" runat="server" DataTextField="email" DataValueField="email" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <label>isActived:</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkActived" runat="server" Checked="true" />
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" CssClass="btn btn-success" OnClick="btnAdd_Click"  ValidationGroup="g" />
                        </div>
                    </div>
                <!-- /table -->
                </div>
            </div>
            <!-- / col-md-10 -->
            <aside class="col-md-8 ">
                <div class="box-header">
                        <h3 class="box-title">Dữ liệu</h3>
                    </div>
                    <div class="search box-body">

                        <table>
                            <tr>
                                <td>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="320px" placeholder="Nhập tên blog"></asp:TextBox>
                                    </div>
                                </td>
                                <td style="padding-left:5px">
                                    <asp:Button ID="btnTim" runat="server" Text="Tìm" OnClick="btnTim_Click" CssClass="btn btn-default" />
                                </td>
                                <td style="padding-left:5px">
                                    <asp:Button ID="btnChuaDuyet" runat="server" Text="Xem blog đã tắt " OnClick="btnChuaDuyet_Click" CssClass="btn btn-blue" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        
                        <asp:GridView ID="grvTaskNew" runat="server"
                            AutoGenerateColumns="False" Width="100%" EmptyDataText="No data"
                            ShowHeaderWhenEmpty="True" PageSize="15"
                            AllowSorting="True" AllowPaging="True"  CssClass="table table-bordered table-hover" OnPageIndexChanging="grvTaskNew_PageIndexChanging" DataKeyNames="idBlog" OnSorting="grvTaskNew_Sorting" OnRowDataBound="grvTaskNew_RowDataBound" OnRowCommand="grvTaskNew_RowCommand" OnSelectedIndexChanged="grvTaskNew_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Checked="false" CssClass='<%# Eval("idBlog") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="btnPost" Visible="false" runat="server" CssClass="btnDang" Height="23px" NavigateUrl='<%# "~/blogger/post-auto/?idBlog="+Eval("idBlog").ToString() %>' ToolTip="Xem mục con">Đăng_tin</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trạng_thái" SortExpression="IsActived">
                                    <ItemTemplate>
                                        <%# Eval("isActived").ToString().ToLower() == "false" ? "<span class='label label-danger'>Không sử dụng</span> ": "<span class='label label-success' >Đang sử dụng!</span>" %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ID Blog" SortExpression="idBlog">
                                    <ItemTemplate>
                                        <asp:Literal ID="lbID" runat="server" Text='<%# Eval("idBlog") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tiêu Đề" SortExpression="TieuDe">
                                    <ItemTemplate>
                                        <asp:Literal ID="lbTieuDe" runat="server" Text='<%# Eval("NameBlog") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Secret" SortExpression="EmailSecret">
                                    <ItemTemplate>
                                        <asp:Literal ID="lbEmail" runat="server" Text='<%# Eval("EmailSecrect") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Email" SortExpression="email">
                                    <ItemTemplate>
                                        <asp:Literal ID="lbEmailPost" runat="server" Text='<%# Eval("emailPost") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>--%>
                                

                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="box-body">
                        <asp:CheckBox ID="check" runat="server" Text="Chọn tất cả" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                         <asp:Button ID="btnXoa" runat="server" OnClick="btnXoa_Click" Text="Xóa blog đã chọn" CssClass="btn btn-warning" />
                        <asp:Button ID="btnAn" runat="server" Text="Tắt blog đã chọn" CssClass="btn btn-warning" OnClick="btnAn_Click" />
                        <asp:Button ID="btnDuyet" runat="server" Text="Bật blog đã chọn" OnClick="btnDuyet_Click" CssClass="btn btn-warning" />
                    </div>
            </aside>
            <!-- / aside -->
        </div>
        <!-- / row -->


    </div>
    <!-- / container-fluid -->
</asp:Content>
