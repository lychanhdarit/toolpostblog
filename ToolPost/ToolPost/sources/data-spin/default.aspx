<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" EnableEventValidation="false" Inherits="ToolPost.sources.data_spin._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
     <!-- PAGE TITLE -->
        <section id="page-title" class="row">
          
            <div class="col-md-8">
                <h1>DATA SPIN CỦA BẠN</h1>

                <p>
                    <asp:Label ID="lbError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </p>
                <section class="panel">
                    <div class="panel-body">
                        <div class="form-inline">
                            <div class="form-group">
                                <label class="sr-only" for="exampleInputEmail3">Nhập URL RSS</label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Nhập tên cần tìm" Width="100%"></asp:TextBox>
                            </div>
                           
                            <asp:Button ID="btnTim" runat="server" Text="Tìm" OnClick="btnTim_Click" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </section>
                <!-- / section -->
            </div>

        </section>
        <!-- / PAGE TITLE -->

        <div class="container-fluid">

            <div class="row">
                <div class="table">
                    <div class="col-md-4">
                        <div class="box">

                            <div class="box-header">
                                <h3 class="box-title">Dữ liệu</h3>
                            </div>
                            <div class="search box-body">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                               
                                <div class="box-body">
                                     <asp:CheckBox ID="chk" runat="server" Text="Chọn tất cả" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                    <asp:Button ID="btnXoa" runat="server" OnClick="btnXoa_Click" Text="Xóa" CssClass="btn btn-warning" />

                                </div>
                                <asp:GridView ID="grvTaskNew" runat="server"
                                    AutoGenerateColumns="False" Width="100%" EmptyDataText="No data"
                                    ShowHeaderWhenEmpty="True" PageSize="15"
                                    AllowSorting="True" AllowPaging="True" CssClass="table table-bordered table-hover" OnPageIndexChanging="grvTaskNew_PageIndexChanging" DataKeyNames="id" OnSorting="grvTaskNew_Sorting" OnRowDataBound="grvTaskNew_RowDataBound" OnRowCommand="grvTaskNew_RowCommand" OnSelectedIndexChanged="grvTaskNew_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Checked="false" CssClass='<%# Eval("id") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tiêu Đề" SortExpression="Title">
                                            <ItemTemplate>
                                                <asp:Literal ID="lbTieuDe" runat="server" Text='<%# Eval("name") %>'></asp:Literal>
                                            </ItemTemplate>

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


                        </div>

                    </div>
                    <div class="col-md-8">
                        <asp:Label ID="ltThongBaoDD" runat="server" Text="" ForeColor="Red"></asp:Label>
                        
                        <div class="box box-danger">
                            <div class="box-header">
                                <h3 class="box-title">
                                    <asp:Literal ID="ltE" runat="server"></asp:Literal></h3>

                            </div>
                            <div class="box-body">

                                <div class="form-group">
                                    <label>ID :</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-key"></i>
                                        </div>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <!-- /.input group -->
                                </div>
                                <div class="form-group">
                                    <label>File:</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-edit"></i>
                                        </div>
                                        <asp:TextBox ID="txtFile" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <!-- /.input group -->
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtTieuDe" ValidationGroup="g" ></asp:RequiredFieldValidator>Title:</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-edit"></i>
                                        </div>
                                        <asp:TextBox ID="txtTieuDe" runat="server" CssClass="form-control" placeholder="nhập tên data của bạn"></asp:TextBox>
                                    </div>
                                    <!-- /.input group -->
                                </div>
                                <div class="form-group">


                                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="30" Width="100%" placeholder="{đồng nghĩa 1|đồng nghĩa 2|đồng nghĩa 3 } .... xuống dòng khi kết thúc 1 chuỗi .."></asp:TextBox>


                                    <!-- /.input group -->
                                </div>


                                <div class="form-group">
                                    <asp:Button ID="btnSua" runat="server" Text="Cập nhật" CssClass="btn btn-warning" OnClick="btnSua_Click" Visible="false" ValidationGroup="g"  />
                                    <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" CssClass="btn btn-warning" OnClick="btnAdd_Click"   ValidationGroup="g" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- / row -->

        </div>
        <!-- / container-fluid -->
</asp:Content>
