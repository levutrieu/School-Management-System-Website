<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="XemThoiKhoaBieu.aspx.cs" Inherits="DATN.WEBSITE.XemThoiKhoaBieu" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpreadsheet.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpreadsheet" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        .tbl_TKB {
            margin-left: 30px;
            margin-right: 30px;
            width: auto;
            color: black;
            font-family: Time New Roman;
            font-weight: bold;
            border-collapse: collapse;
            font-size: 14px;}

        .table th {
            text-align: center;
            }

        .td {
                /*width: 2%;*/
                text-align: center;
            }

            .tbl_TKBtd {
                vertical-align: middle;
                width: 14.3%;
                text-align: center;
                border: 1px solid black;
            }

            .tbl_TKB tr {
                height: 30px;
                border: 1px solid black;
            }

        .auto-style1 {
            height: 18px;
        }

        .table td {
            padding: 2px 0 0px 0;
        }

        .table tr {
            height: 10px;
        }
    </style>
    <div style="align-content: center; height: auto">
        <asp:Panel runat="server" ID="groupnhapthongtin">
            <table style="margin-left: 450px; margin-right: 500px; border: 1px solid dodgerblue; font-size: 13px; width: 300px; text-align: center">
                <tr>
                    <td style="text-align: center; align-content: center" class="auto-style1">
                        <dx:ASPxLabel runat="server" Text="Nhập mã sinh viên" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; width: auto; margin: 0 auto; padding: 5px 0 5px 0">
                        <dx:ASPxTextBox runat="server" ID="textmasv" Theme="Office2010Blue" Height="20px" Width="320px" Paddings="10px 0 10px 0" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <dx:ASPxButton runat="server" Text="Xem" ID="btnXemThoiKhoaBieu" AutoPostBack="True" OnClick="btnXemThoiKhoaBieu_OnClick" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel runat="server" ID="groupinfo">
            <table style="text-align: left; margin-left: 450px; font-size: 14px; width: 400px; border: 1px ridge darkgrey; height: auto" class="table">
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Mã sinh viên" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="" ID="txtMaSinhVien" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Tên sinh viên" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="" ID="txtTenSinhVien" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Lớp" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="" ID="txtLop" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Ngành" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="" ID="txtNganh" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Hệ đào tạo" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="" ID="txtHeDaoTao" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Niên khóa" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="" ID="txtNienKhoa" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="height: 20px"></div>
        <asp:Panel runat="server" ID="grouploc">
            <table style="text-align: left; margin-left: 450px; font-size: 20px; width: 400px">
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Chọn học kỳ:" />
                    </td>
                    <td>&nbsp; &nbsp;</td>
                    <td>
                        <dx:ASPxComboBox runat="server" ID="cboHocKy" Width="300px" ValueField="ID_NAMHOC_HKY_HTAI" TextField="TEN_HOKY_NH" AutoPostBack="True" OnSelectedIndexChanged="cboHocKy_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td><dx:ASPxLabel runat="server" Text="Chọn tuần" />
                    </td>
                    <td>&nbsp; &nbsp;</td>
                    <td>
                        <dx:ASPxComboBox runat="server" ID="cboTuan" Width="300px" ValueField="TUAN" TextField="NAME_TUAN" AutoPostBack="True" OnSelectedIndexChanged="cboTuan_OnSelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

    </div>

    <div style="height: 20px"></div>
    <div style="margin-left: 50px; margin-right: 50px">
        <asp:Panel runat="server" ID="thoikhoabieu">
           <%-- <dx:ASPxSpreadsheet ID="ASPxSpreadsheet1" runat="server"></dx:ASPxSpreadsheet>
            <dx:ASPxGridView runat="server" ID="grdthoikhoabieu"></dx:ASPxGridView>--%>

        </asp:Panel>
    </div>
    <div style="height: 20px"></div>
</asp:Content>


