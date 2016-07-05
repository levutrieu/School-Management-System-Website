<%@ Page Title="" Language="C#" MasterPageFile="~/HomeDefault.Master" AutoEventWireup="true" CodeBehind="DangKyMonHoc.aspx.cs" Inherits="DATN.WEBSITE.DangKyMonHoc" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function thongBao() {
            $('#ThongBao').modal('show');
        }
    </script>

    <script type="text/javascript">
        function popUpPage() {
            $('#thuattoan').modal('show');
        }
    </script>
    
    <script type="text/javascript">
        function thuatToanID() {
            $('#thuattoanid3').modal('show');
        }
    </script>

    <style type="text/css">
        .grid th {
            text-align: center;
        }

        .grid td {
            padding: 5px 0 5px 0;
        }
    </style>
 <asp:Panel runat="server" ID="paneInfo">
        <table runat="server" style="margin-top: 10px; width: auto; margin-left: 300px;margin-right: 200px ;border-style: ridge; border-color: skyblue">
            <tbody><tr>
                    <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Lọc theo khóa:" /></td>
                    <td>
                        <dx:ASPxComboBox runat="server" ID="cboKhoa" AutoPostBack="True" OnSelectedIndexChanged="cboKhoa_OnSelectedIndexChanged"
                            ValueField="ID_KHOAHOC" TextField="TEN_KHOAHOC">
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblLopHoc" runat="server" Text="Ngành:" />
                    </td>
                    <td>
                        <dx:ASPxComboBox runat="server" ID="cboNganh" Width="300px" AutoPostBack="True" ValueField="ID_NGANH" TextField="TEN_NGANH" OnSelectedIndexChanged="cboNganh_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" ID="lblMonhoc" Text="Lọc theo môn học:" />
                    </td>
                    <td>
                        <dx:ASPxComboBox runat="server" ID="cboMonHoc" Width="300px"   ValueField="ID_MONHOC" TextField="TEN_MONHOC" 
                            AutoPostBack="True" OnSelectedIndexChanged="cboMonHoc_OnSelectedIndexChanged" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td></tr>
            </tbody>
        </table>
    </asp:Panel>

    <div style="height: 10px"></div>

    <asp:Panel runat="server" ID="paneDSHocPhan">
        <div runat="server" style="font-family: Time New Roman; height: 250px; margin-left: 50px; margin-right: 50px; overflow: auto">
            <asp:GridView ID="grdDanhSachLopHP" runat="server" AutoGenerateColumns="false" CssClass="grid" ShowHeader="True"
                OnRowDataBound="grdDanhSachLopHP_OnRowDataBound">
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
                    <asp:TemplateField HeaderText="Loai lop" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="False" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="txtIS_LYTHUYET" Text='<%#Eval("IS_LYTHUYET") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="MA_LOP_HOCPHAN" 
                        HeaderText="Mã học phần" Visible="True" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderStyle-HorizontalAlign="Center" 
                        HeaderStyle-VerticalAlign="Middle" DataField="TEN_LOP_HOCPHAN" HeaderText="Tên lớp học phần" Visible="True" ItemStyle-Width="15%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="SOLUONG" HeaderText="SL" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="SOSVDKY" HeaderText="DDK" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TEN_GIANGVIEN" HeaderText="Giảng viên" ItemStyle-Width="7%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" />  
                    <asp:TemplateField HeaderText="Thời khóa biểu" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="True" 
                        ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="txtThoiKhoaBieu" Text='<%#Eval("THOIKHOABIEU") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TUAN_BD" HeaderText="Tuần BD" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataField="TUAN_KT" HeaderText="Tuần KT" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" />
                
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <div style="height: 10px"></div>

    <asp:Panel runat="server" ID="paneChucNang">
        <table style="width: auto; height: 20px; margin-left: 50px; margin-right: 50px">
            <tbody>
                <tr>
                    <td>
                        <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnHoTroID3" Text="Hỗ trợ dùng ID3" CssClass="button" OnClick="btnHoTroID3_OnClick" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnHoTroDungC45" Text="Hỗ trợ dùng C45" CssClass="button" OnClick="btnHoTroDungC45_OnClick" />
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
                        <%--<dx:ASPxButton BackColor="#2979ce" runat="server" ID="btnInPhieuDangKy" Text="Xuất phiếu đăng ký" CssClass="button" />--%>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>

    <div style="height: 10px"></div>

    <asp:Panel runat="server" ID="paneDSDangKy">
        <div runat="server" style="font-family: Time New Roman; height: 250px; margin-left: 50px; margin-right: 50px; overflow: auto">
            <asp:GridView ID="grdViewDanhSachDaDangKy" runat="server" AutoGenerateColumns="False" CssClass="grid" ShowHeader="True" OnRowDataBound="grdDanhSachLopHP_OnRowDataBound">
                <Columns>
                    <%--<!--0-->--%>
                    <asp:TemplateField HeaderText="Xóa" Visible="True" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkXoa" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--1-->--%>
                    <asp:TemplateField HeaderText="HP Đăng ký" Visible="False" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("ID_DANGKY") %>' ID="lbl_Iddangky" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--2-->--%><asp:TemplateField HeaderText="HP Sinh Viên" Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("ID_SINHVIEN") %>' ID="lbl_Idsinhvien" />
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
                            <asp:Label runat="server" Text='<%#Eval("MA_MONHOC") %>' ID="lbl_Mamonhoc" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--5-->--%>
                    <asp:TemplateField HeaderText="Tên môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TEN_MONHOC") %>' ID="lbl_Tenmonhoc" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--6-->--%>
                    <asp:TemplateField HeaderText="STC" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("SO_TC") %>' ID="lbl_SoTC" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--7-->--%>
                    <asp:TemplateField HeaderText="Học phí" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("DON_GIA") %>' ID="lbl_DonGia" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--8-->--%>
                    <asp:TemplateField HeaderText="Thành tiền" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("THANH_TIEN") %>' ID="lbl_ThanhTien" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--9-->--%>
                    <asp:TemplateField HeaderText="Ngày đăng ký" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="4%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("NGAY_DANGKY") %>' ID="lbl_NgayDangKy" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<!--10-->--%>
                    <asp:TemplateField HeaderText="Trạng thái" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TRANGTHAI") %>' ID="lbl_TrangThai" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content" style="width: 300px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Đăng nhập</h4>
                </div>
                <div class="modal-body">
                    <table style="padding: 40px">
                        <tbody>
                            <tr>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <dx:ASPxLabel ID="lblMaSV" runat="server" Text="Mã sinh viên:" CssClass="lblnhapmasv" />
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <dx:ASPxTextBox runat="server" ID="txtMaSinhVien" />
                                </td></tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <dx:ASPxLabel ID="lblMaKhau" runat="server" Text="Mật khẩu:" />
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <dx:ASPxTextBox runat="server" ID="txtMaKhau" Password="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;</td>
                                <td colspan="3">
                                    <dx:ASPxButton runat="server" Text="Đăng nhập" ID="btnDangNhap" CssClass="button" OnClick="btnDangNhap_OnClick" />
                                </td>

                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="ThongBao" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" >

            <!-- Modal content-->
            <div class="modal-content" style="width: 350px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Thông báo</h4>
                </div>
                <div class="modal-body">
                    <dx:ASPxLabel runat="server" Text="" ID="txtThongbao" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Đồng ý</button>
                </div>
            </div>

        </div>
    </div>

    <!--  ui C45 -->

    <div class="modal fade" id="thuattoan" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Hỗ trợ tư vấn dùng C45</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GridThuatToan" runat="server" AutoGenerateColumns="False" CssClass="grid">
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn" Visible="True" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chonmonhoc" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HP Lớp học phần" Visible="False" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("ID_MONHOC") %>' ID="lblID_MONHOC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<!--4-->--%><asp:TemplateField HeaderText="Mã môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("MA_MONHOC") %>' ID="lblMA_MONHOC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<!--5-->--%>
                            <asp:TemplateField HeaderText="Tên môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TEN_MONHOC") %>' ID="lblTEN_MONHOC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<!--6-->--%>
                            <asp:TemplateField HeaderText="STC" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("SO_TC") %>' ID="lblSO_TC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" runat="server" OnServerClick="btnDuDoan_Click" >Dự đoán</button>
                    <%--<button type="button" class="btn btn-primary" runat="server" onclick="btnLamMoi_Click">Làm mới</button>--%>
                </div>

            </div>
        </div>

    </div>
    
    <!-- UI ID3 -->
    
     <div class="modal fade" id="thuattoanid3" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Hỗ trợ tư vấn dùng ID3</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="ThuatTuanID" runat="server" AutoGenerateColumns="False" CssClass="grid">
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn" Visible="True" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chonmonhoc" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HP Lớp học phần" Visible="False" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("ID_MONHOC") %>' ID="lblID_MONHOC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<!--4-->--%><asp:TemplateField HeaderText="Mã môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("MA_MONHOC") %>' ID="lblMA_MONHOC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<!--5-->--%>
                            <asp:TemplateField HeaderText="Tên môn học" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TEN_MONHOC") %>' ID="lblTEN_MONHOC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<!--6-->--%>
                            <asp:TemplateField HeaderText="STC" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width="3%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("SO_TC") %>' ID="lblSO_TC" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" runat="server" OnServerClick="btnDuDoanID_Click">Dự đoán</button>
                    <%--<button type="button" class="btn btn-primary" runat="server" onclick="btnLamMoi_Click">Làm mới</button>--%>
                </div>

            </div>
        </div>

    </div>

</asp:Content>