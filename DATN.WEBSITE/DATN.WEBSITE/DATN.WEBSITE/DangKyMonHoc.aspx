<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="DangKyMonHoc.aspx.cs" Inherits="DATN.WEBSITE.DangKyMonHoc" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        .grid th
        {
            text-align: center;
            
        }
        .grid td {
            padding: 5px 0 5px 0;

        }
    </style>
    <table runat="server" style="margin-top: 150px; width: auto; margin-left: 100px; border-style: ridge; border-color: skyblue">
        <tbody>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblKhoahoc" runat="server" Text="Lọc theo khóa:" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="cboKhoa" AutoPostBack="True" OnSelectedIndexChanged="cboKhoa_OnSelectedIndexChanged"
                        ValueField="ID_KHOAHOC" TextField="TEN_KHOAHOC">
                    </dx:ASPxComboBox>
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblLopHoc" runat="server" Text="Lọc theo lớp:" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="cboLopHoc" Width="500px" AutoPostBack="True" ValueField="ID_LOPHOC" TextField="TEN_LOP" OnSelectedIndexChanged="cboLopHoc_OnSelectedIndexChanged" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" ID="lblNgayHoc" Text="Lọc theo ngày học:" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="cboNgayHoc" ValueField="THU" TextField="NGAY" AutoPostBack="True" OnSelectedIndexChanged="cboNgayHoc_OnSelectedIndexChanged"/>
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" ID="lblMonhoc" Text="Lọc theo môn học:" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="cboMonHoc" Width="500px" ValueField="ID_MONHOC" TextField="TEN_MONHOC" AutoPostBack="True" OnSelectedIndexChanged="cboMonHoc_OnSelectedIndexChanged"/>
                </td>

            </tr>
        </tbody>
    </table>
    <div style="height: 10px"></div>
    <div runat="server" style="font-family: Time New Roman; height: 250px; margin-left: 50px; margin-right: 50px; overflow: auto">
        <asp:GridView ID="grdDanhSachLopHP" runat="server" AutoGenerateColumns="false" CssClass="grid" ShowHeader="True" OnRowDataBound="grdDanhSachLopHP_OnRowDataBound">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" HeaderText="Chọn" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkChon" runat="server" AutoPostBack="True" OnCheckedChanged="chkChon_OnCheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="txtID_LopHocPhan" Text='<%#Eval("ID_LOPHOCPHAN") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField  DataField="ID_LOPHOCPHAN" HeaderText="" Visible="False"/>--%>

                <asp:TemplateField HeaderText="Mã môn học" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="False" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="txtMaMonHoc" Text='<%#Eval("MA_MONHOC") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Tên môn học" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="False" ItemStyle-Width="15%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="txtTenMonHoc" Text='<%#Eval("TEN_MONHOC") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="STC" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="True" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="txtSTC" Text='<%#Eval("SO_TC") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="MA_MONHOC" HeaderText="Mã môn học" Visible="True" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />--%>
                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TEN_MONHOC" HeaderText="Tên môn học" Visible="True" ItemStyle-Width="15%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left"/>--%>
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="MA_LOP_HOCPHAN" HeaderText="Mã học phần" Visible="True" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TEN_LOP_HOCPHAN" HeaderText="Tên lớp học phần" Visible="True" ItemStyle-Width="23%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="SO_TC" HeaderText="STC" Visible="True" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="SOLUONG" HeaderText="SL" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="SOSVDKY" HeaderText="DDK" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TEN_GIANGVIEN" HeaderText="Giảng viên" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="THU" HeaderText="Thứ" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="SO_TIET" HeaderText="Số tiết" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TUAN_BD" HeaderText="Tuần BD" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TUAN_KT" HeaderText="Tuần KT" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TEN_PHONG" HeaderText="Phòng học" ItemStyle-Width="20%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>
        </div>
    <div style="height: 10px"></div>
    <table style="width: auto; height: 20px; margin-left: 50px; margin-right: 50px">
        <tbody>
            <tr>
                <td>
                    <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnHoTroID3" Text="Hỗ trợ dùng ID3" CssClass="button" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnHoTroDungC45" Text="Hỗ trợ dùng C45" CssClass="button" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnLuuDangKy" Text="Lưu đăng ký" OnClick="btnLuuDangKy_OnClick" CssClass="button" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnHuyDangKy" Text="Hủy đăng ký" CssClass="button" OnClick="btnHuyDangKy_OnClick" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnInPhieuDangKy" Text="Xuất phiếu đăng ký" CssClass="button" />
                </td>
            </tr>
        </tbody>
    </table>
     <div style="height: 10px"></div>
    <div runat="server" style="font-family: Time New Roman; height: 250px; margin-left: 50px; margin-right: 50px; overflow: auto">
        <asp:GridView ID="grdViewDanhSachDaDangKy" runat="server" AutoGenerateColumns="False" CssClass="grid" ShowHeader="True" OnRowDataBound="grdDanhSachLopHP_OnRowDataBound">
            <Columns>
                <%--<!--0-->--%>
                <asp:TemplateField HeaderText="Xóa" Visible="True" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkXoa" runat="server" AutoPostBack="True" OnCheckedChanged="chkXoa_OnCheckedChanged"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--1-->--%>
                <asp:TemplateField HeaderText="HP Đăng ký" Visible="False" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("ID_DANGKY") %>' ID="lbl_Iddangky"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--2-->--%>
                <asp:TemplateField HeaderText="HP Sinh Viên" Visible="False">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("ID_SINHVIEN") %>' ID="lbl_Idsinhvien"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--3-->--%>
                <asp:TemplateField HeaderText="HP Lớp học phần" Visible="False" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("ID_LOPHOCPHAN") %>' ID="lbl_IdLophocphan" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--4-->--%>
                <asp:TemplateField HeaderText="Mã môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("MA_MONHOC") %>' ID="lbl_Mamonhoc"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--5-->--%>
                <asp:TemplateField HeaderText="Tên môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("TEN_MONHOC") %>' ID="lbl_Tenmonhoc"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--6-->--%>
                <asp:TemplateField HeaderText="STC" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("SO_TC") %>' ID="lbl_SoTC"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--7-->--%>
                <asp:TemplateField HeaderText="Học phí" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("DON_GIA") %>' ID="lbl_DonGia"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--8-->--%>
                <asp:TemplateField HeaderText="Thành tiền" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("THANH_TIEN") %>' ID="lbl_ThanhTien"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--9-->--%>
                <asp:TemplateField HeaderText="Ngày đăng ký" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="4%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("NGAY_DANGKY") %>' ID="lbl_NgayDangKy"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<!--10-->--%>
                <asp:TemplateField HeaderText="Trạng thái" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("TRANGTHAI") %>' ID="lbl_TrangThai"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

