<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ToolPost.blogs.categorys._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">

    <!-- PAGE TITLE -->
    <section id="page-title" class="row">
        <div class="col-md-8">
            <h1>QUẢN LÝ DANH MỤC BLOG</h1>
        </div>
    </section>
    <!-- / PAGE TITLE -->

    <div class="container-fluid white-bg">

        <div class="row">
            <div class="col-md-12">
                <div class="box box-danger">
                    <div class="box-header">
                        <h3 class="box-title">Nhập dữ liệu</h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <label>ID:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-edit"></i>
                                </div>
                                <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <label>TÊN DANH MỤC:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-edit"></i>
                                </div>
                                <asp:TextBox ID="txtTieuDe" runat="server" CssClass="form-control" placeholder="nhập tên blog của bạn"></asp:TextBox>
                            </div>
                            <!-- /.input group -->
                        </div>

                        <div class="form-group">
                            <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" CssClass="btn btn-warning" OnClick="btnAdd_Click" ValidationGroup="g" />
                        </div>
                    </div>
                    <!-- /table -->
                </div>
            </div>
            <!-- / col-md-10 -->
            <aside class="col-md-12 ">
                <div class="box-header">
                    <h3 class="box-title">Dữ liệu</h3>
                </div>
                <div class="search box-body ">

                    <table>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="320px" placeholder="Nhập tên danh mục"></asp:TextBox>
                                </div>
                            </td>
                            <td style="padding-left: 5px">
                                <asp:Button ID="btnTim" runat="server" Text="Tìm" OnClick="btnTim_Click" CssClass="btn  btn-warning" />
                            </td>

                        </tr>
                    </table>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <asp:CheckBox ID="check" runat="server" Text="Chọn tất cả" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                    <asp:Button ID="btnXoa" runat="server" OnClick="btnXoa_Click" Text="Xóa danh mục đã chọn" CssClass="btn btn-warning" />

                </div>

                <div class="box-body">

                    <asp:GridView ID="grvTaskNew" runat="server"
                        AutoGenerateColumns="False" Width="100%" EmptyDataText="No data"
                        ShowHeaderWhenEmpty="True" PageSize="15"
                        AllowSorting="True" AllowPaging="True" CssClass="table table-bordered table-hover" OnPageIndexChanging="grvTaskNew_PageIndexChanging" DataKeyNames="ID" OnSorting="grvTaskNew_Sorting" OnRowDataBound="grvTaskNew_RowDataBound" OnRowCommand="grvTaskNew_RowCommand" OnSelectedIndexChanged="grvTaskNew_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Checked="false" CssClass='<%# Eval("id") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tiêu Đề" SortExpression="TieuDe">
                                <ItemTemplate>
                                    <asp:Literal ID="lbTieuDe" runat="server" Text='<%# Eval("Name") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnLock" runat="server" Height="25px" CommandName="xem" CommandArgument='<%# Eval("id") %>' Text="Xem" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
                
            </aside>
            <!-- / aside -->
        </div>
        <!-- / row -->


    </div>
    <!-- / container-fluid -->
</asp:Content>
