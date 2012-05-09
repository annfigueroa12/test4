<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="hello_services._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <table id="employees"></table>

    <script type="text/javascript">
            
    // document ready function
        $(function () {

            // select the employees table from the page and
            // store it in a variable for later use.
            var $employees = $("#employees");

            // make an ajax call to the employees WebAPI service
            // to retrieve a JSON response of all the employees
            $.ajax({
                // the url to the service
                url: "api/employees",
                // the format that the data should be in when
                // it is returned
                contentType: "json",
                // the function that executes when the server
                // responds to this ajax request successfully
                success: function (data) {

                    // iterate over the data items returned from the server
                    // the index variable is the position in the colleciton.
                    // the item variable is the item itself
                    $.each(data, function (index, item) {

                        // create a row template
                        var $row = $("#templates").find(".row-template").clone();

                        // set the first and last name column text for the row
                        $row.find(".firstName").html(item.FirstName);
                        $row.find(".lastName").html(item.LastName);

                        // find the button and set its click event
                        $row.find(".delete").click(function () {

                            // call the delete method on the employees service
                            $.ajax({
                                // append the current employee id onto the url
                                url: "api/employees/" + item.Id,
                                // set the request type to be a DELETE
                                type: "DELETE",
                                // remove the row on a success response from the server
                                success: function () {

                                    $row.remove();

                                }
                            });

                        });

                        // append the row to the table
                        $employees.append($row);

                    });

                }

            });

        });
    
    </script>

    <div id="templates" style="display: none">
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
