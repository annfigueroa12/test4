<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HellojQuery._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
    First Name:
<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
&nbsp;Last Name:
<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
&nbsp;

<input type="button" id="btnSayHello" value="Say Hello" />

<br />
<asp:Label ID="lblResult" runat="server"></asp:Label>
   
<script>

    $(function () {

        $("#btnSayHello").click(function () {

            // get the values of the first and last name textboxes
            var firstName = $("#MainContent_txtFirstName").val();
            var lastName = $("#MainContent_txtLastName").val();

            // set the text of the label
            $("#MainContent_lblResult").html("Hello " + firstName + " " + lastName);

        });


    });


</script>

</asp:Content>
