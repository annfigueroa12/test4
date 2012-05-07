<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="hello_services_wcf._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <table id="employees"></table>

    <script type="text/javascript">

        $(function () {

            var $table = $("#employees");

            $.ajax({

                url: "Services/Employees.svc",
                success: function (data) {

                    $.each(data, function (index, item) {

                        var $row = $("#template").find(".row-template").clone().attr("id", "row_" + item.id);
                        $row.find(".firstName").html(item.FirstName);
                        $row.find(".lastName").html(item.LastName);

                        $row.find(".delete").click(function () {

                            $.ajax({

                                url: "Services/Employees.svc/" + item.id,
                                type: "DELETE",
                                success: function (data) {

                                    $row.remove();

                                }
                            });

                        });

                        $table.append($row);

                    });

                }

            });

        });
    
    </script>

    <div id="template" style="display: none">
        <table>
            <tr class="row-template">
                <td class="firstName" style="width: 100px;"></td>
                <td class="lastName" style="width: 100px;"></td>
                <td>
                    <input type="button" value="X" class="delete" />
                </td>
            </tr>
        </table>
     </div>



</asp:Content>
