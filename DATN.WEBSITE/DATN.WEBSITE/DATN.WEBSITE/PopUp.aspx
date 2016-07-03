<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="DATN.WEBSITE.PopUp" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function ShowLoginWindow() {
            pcLogin.Show();
        }
        function ShowCreateAccountWindow() {
            pcCreateAccount.Show();
            tbUsername.Focus();
        }
    </script>
</head>
<body style="background-image: none;">
    <form id="MailForm" runat="server">
        <div style="margin: 16px auto; width: 160px;">
            <dx:ASPxButton ID="btShowModal" runat="server" Text="Show Modal Window" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                <ClientSideEvents Click="function(s, e) { ShowLoginWindow(); }" />
            </dx:ASPxButton>
        </div>
        <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
            HeaderText="Login" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">
            <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td rowspan="4">
                                            <div class="pcmSideSpacer">
                                            </div>
                                        </td>
                                        <td class="pcmCellCaption">
                                            <dx:ASPxLabel ID="lblUsername1" runat="server" Text="Username:" AssociatedControlID="tbLogin">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="pcmCellText">
                                            <dx:ASPxTextBox ID="tbLogin" runat="server" Width="150px" ClientInstanceName="tbLogin">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="entryGroup" SetFocusOnError="True"
                                                    ErrorDisplayMode="Text" ErrorTextPosition="Bottom" CausesValidation="True">
                                                    <RequiredField ErrorText="Username required" IsRequired="True" />
                                                    <RegularExpression ErrorText="Login required" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td rowspan="4">
                                            <div class="pcmSideSpacer">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pcmCellCaption">
                                            <dx:ASPxLabel ID="lblPass1" runat="server" Text="Password:" AssociatedControlID="tbPassword">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="pcmCellText">
                                            <dx:ASPxTextBox ID="tbPassword" runat="server" Width="150px" Password="True">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="entryGroup" SetFocusOnError="True"
                                                    ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="Password required" IsRequired="True" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="pcmCheckBox">
                                            <dx:ASPxCheckBox ID="chbRemember" runat="server" Text="Remember me">
                                            </dx:ASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="pcmButton">
                                                <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                    <ClientSideEvents Click="function(s, e) { if(ASPxClientEdit.ValidateGroup('entryGroup')) pcLogin.Hide(); }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                    <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                                </dx:ASPxButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                    <div>
                        <a href="javascript:ShowCreateAccountWindow();" id="hl1" style="float: right; margin: 14px 0 10px 0;">Create Account</a>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="pcCreateAccount" runat="server" CloseAction="CloseButton" CloseOnEscape="true"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCreateAccount"
            HeaderText="Create Account" AllowDragging="True" Modal="True" PopupAnimationType="Fade"
            EnableViewState="False" PopupHorizontalOffset="40" PopupVerticalOffset="40">
            <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('createAccountGroup'); }" />
            <SizeGripImage Width="11px" />
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btCreate">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td rowspan="5">
                                            <div class="pcmSideSpacer">
                                            </div>
                                        </td>
                                        <td class="pcmCellCaption">
                                            <dx:ASPxLabel ID="lblUsername2" runat="server" Text="Username:" AssociatedControlID="tbUsername">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="pcmCellText">
                                            <dx:ASPxTextBox ID="tbUsername" runat="server" Width="150px" ClientInstanceName="tbUsername">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="createAccountGroup"
                                                    SetFocusOnError="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="Username is required" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td rowspan="5">
                                            <div class="pcmSideSpacer">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pcmCellCaption">
                                            <dx:ASPxLabel ID="lblPass2" runat="server" Text="Password:" AssociatedControlID="tbPass1">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="pcmCellText">
                                            <dx:ASPxTextBox ID="tbPass1" runat="server" Width="150px" ClientInstanceName="pass1"
                                                Password="True">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="createAccountGroup"
                                                    SetFocusOnError="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="Password is required" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pcmCellCaption">
                                            <dx:ASPxLabel ID="lblConfPass2" runat="server" Text="Confirm password:" AssociatedControlID="tbConfPass2">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="pcmCellText">
                                            <dx:ASPxTextBox ID="tbConfPass2" runat="server" Width="150px" ClientInstanceName="pass2"
                                                Password="True">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="createAccountGroup"
                                                    SetFocusOnError="True" ErrorText="Password is incorrect" ErrorDisplayMode="Text"
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="Please, confirm your password" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) { e.isValid = (pass1.GetText()==pass2.GetText()); }" />
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pcmCellCaption">
                                            <dx:ASPxLabel ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="tbEmail">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="pcmCellText">
                                            <dx:ASPxTextBox ID="tbEmail" runat="server" Width="150px">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="createAccountGroup"
                                                    SetFocusOnError="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="E-mail is required" />
                                                    <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="pcmButton">
                                                <dx:ASPxButton ID="btCreate" runat="server" Text="OK" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                    <ClientSideEvents Click="function(s, e) {
                                                            if(ASPxClientEdit.ValidateGroup('createAccountGroup')) {
                                                                ASPxClientEdit.ClearGroup('entryGroup');
                                                                tbLogin.SetText(tbUsername.GetText());
                                                                pcCreateAccount.Hide();
                                                            }
                                                        }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btCancel2" runat="server" Text="Cancel" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                    <ClientSideEvents Click="function(s, e) { pcCreateAccount.Hide(); }" />
                                                </dx:ASPxButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>
