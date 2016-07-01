<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="XemDiemSinhVien.aspx.cs" Inherits="DATN.WEBSITE.XemDiemSinhVien" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
    <div style="margin-top: 10px; margin-left: 80px; margin-right: 80px">
        <dx:ASPxTreeList ID="treegrd" runat="server" Width="100%">
            <Columns>
                <dx:TreeListDataColumn FieldName="NAME" Caption="Tên môn học" VisibleIndex="0" Width="300px" />
                <dx:TreeListDataColumn FieldName="MA_MONHOC" Caption="Mã môn học" VisibleIndex="0" Width="50%"/>
                <dx:TreeListDataColumn FieldName="SO_TC" Caption="Số TC" VisibleIndex="0" Width="30%"/>
                <dx:TreeListDataColumn FieldName="CACH_TINHDIEM" Caption="% Điểm" VisibleIndex="0" Width="50%"/>
                <dx:TreeListDataColumn FieldName="DIEM_BT" Caption="% Bài tập" VisibleIndex="0" Width="30%" />
                <dx:TreeListDataColumn FieldName="DIEM_GK" Caption="% Giữa kỳ" VisibleIndex="0" Width="30%"/>
                <dx:TreeListDataColumn FieldName="DIEM_CK" Caption="% Cuối kỳ" VisibleIndex="0" Width="30%"/>
                <dx:TreeListDataColumn FieldName="DIEM_TONG" Caption="Điểm tổng" VisibleIndex="0" Width="30%"/>
                <dx:TreeListDataColumn FieldName="DIEM_CHU" Caption="Điểm chữ" VisibleIndex="0" Width="30%"/>
                <dx:TreeListDataColumn FieldName="DIEM_HE4" Caption="Điểm hệ 4" VisibleIndex="0" Width="30%"/>
            </Columns>
            <Settings GridLines="Both" HorizontalScrollBarMode="Visible" />
            <Styles>
                <SelectedNode BackColor="#99FFCC">
                    
                </SelectedNode>
            </Styles>
            <Border BorderColor="#0099FF" BorderStyle="Groove" />
        </dx:ASPxTreeList>
    </div>
</asp:Content>

