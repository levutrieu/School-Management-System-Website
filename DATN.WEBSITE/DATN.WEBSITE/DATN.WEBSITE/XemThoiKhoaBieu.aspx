<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="XemThoiKhoaBieu.aspx.cs" Inherits="DATN.WEBSITE.XemThoiKhoaBieu" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
     <div style="align-content: center">
        <table style="text-align: left; margin-left: 500px; font-size: 14px; border-style: groove">
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" Text="Mã sinh viên"/>
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" Text="" ID="txtMaSinhVien"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" Text="Tên sinh viên"/>
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" Text="" ID="txtTenSinhVien"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" Text="Lớp"/>
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" Text="" ID="txtLop"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" Text="Ngành" />
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" Text="" ID="txtNganh"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" Text="Hệ đào tạo"/>
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" Text="" ID="txtHeDaoTao"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" Text="Niên khóa"/>
                </td>
                <td>&nbsp;&nbsp;</td><td>
                    <dx:ASPxLabel runat="server" Text="" ID="txtNienKhoa"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

