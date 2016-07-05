<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="DATN.WEBSITE.PopUp" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
     <script type="text/javascript">
         function openModal() {
             $('#myModal').modal('show');
         }
</script>
</head>
<body style="background-image: none;">
    <div class="container">
  <h2>Modal Example</h2>
        <form runat="server">
            <asp:Button ID="Button1" runat="server" Text="Open" OnClick="ButtonSend_OnClicknd_Click"/>
            <div class="modal fade" id="myModal" role="dialog">
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
                                        <dx:ASPxButton runat="server" Text="Đăng nhập" ID="btnDangNhap" CssClass="button" OnClick="btnDangNhap_OnClickOnClick"/>
                                    </td>
                                    
                                </tr>
                            </tbody>
                        </table>
        </div>
      </div>
      
    </div>
  </div>
  
        </form>
  <!-- Trigger the modal with a button -->
  <%--<button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>--%>
        <!-- Modal -->
  
</div>

</body>
</html>

