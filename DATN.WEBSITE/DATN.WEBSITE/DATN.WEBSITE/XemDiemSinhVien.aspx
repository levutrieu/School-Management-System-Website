<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="XemDiemSinhVien.aspx.cs" Inherits="DATN.WEBSITE.XemDiemSinhVien" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <link href="css/bootstrap.min.css" type="text/css" rel="Stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/animate.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    
    <link href="CSS/popuppage.css" rel="stylesheet" type="text/css" />
    <div style="align-content: center">
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
                        <dx:ASPxButton runat="server" Text="Xem điểm" ID="btnXemDiem" AutoPostBack="True" OnClick="btnXemDiem_OnClick_OnClick" />
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
        
        <script>
            // Get the modal
            var modal = document.getElementById('btnXemDiem');

            // Get the button that opens the modal
            var btn = document.getElementById("myBtn");

            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];

            // When the user clicks the button, open the modal
            btn.onclick = function () {
                modal.style.display = "block";
            }

            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }

            // When the user clicks anywhere outside of the modal, close it
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
</script>

        <div style="height: 20px"></div>
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

