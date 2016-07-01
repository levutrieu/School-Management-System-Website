<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="DATN.WEBSITE.LogIn" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="margin-top: 130px">
        <table style="margin-left: 80px; padding: 5px 5px 5px 5px; height: 180px; border-style: double; background-color: powderblue; width: 280px">
            <tbody>
            <tr>
                <td>
                    <p style="font-family: Time New Roman; color: red; font-size: 20px; padding-left: 20px; text-align: center">Nhập thông tin tài khoản</p>
                </td>
            </tr>
                <tr>
                    <td><table style="padding: 40px">
                            <tbody>
                                <tr>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <dx:ASPxLabel ID="lblMaSV" runat="server" Text="Mã sinh viên:" CssClass="lblnhapmasv"/>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <dx:ASPxTextBox runat="server" ID="txtMaSinhVien"/>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <dx:ASPxLabel ID="lblMaKhau" runat="server" Text="Mật khẩu:"/>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td><dx:ASPxTextBox runat="server" ID="txtMaKhau" Password="True"/>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td colspan="3">
                                        <dx:ASPxButton runat="server" Text="Đăng nhập" ID="btnDangNhap" CssClass="button" OnClick="btnDangNhap_OnClick"/>
                                    </td>
                                    
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

