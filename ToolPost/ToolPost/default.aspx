<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ToolPost._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .menu-home a {
            display: block;
            padding: 50px;
            background: #f3f3f3;
            text-align: center;
            margin: 3px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
    <div class="container menu-home">
        <div class="row">
            <div class="col-md-3">
                <a href="blogs/">Blog</a>
            </div>
            <div class="col-md-3">
                <a href="blogs/categorys/">Danh mục blog</a>
            </div>
            <div class="col-md-3">
                <a href="sources/">Trang nguồn </a>
            </div>
            <div class="col-md-3">
                <a href="sources/data-spin/">Data Spin</a>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <a href="blogs/">Tạo bài</a>
            </div>
            <div class="col-md-3">
                <a href="blogs/">.</a>
            </div>
            <div class="col-md-3">
                <a href="blogs/">.</a>
            </div>
            <div class="col-md-3">
                <a href="blogs/">.</a>
            </div>
        </div>
    </div>
</asp:Content>
