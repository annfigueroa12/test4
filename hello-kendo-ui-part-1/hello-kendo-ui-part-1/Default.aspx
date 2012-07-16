<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="Content/kendo/2012.2.710/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/kendo/2012.2.710/kendo.default.min.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <div id="employeesGrid"></div>

    <script src="Scripts/jquery-1.7.2.min.js"></script>
    <script src="Scripts/kendo/2012.2.710/kendo.web.min.js"></script>

    <script>

        $(function () {

            $("#employeesGrid").kendoGrid({
                columns: [
                        { field: "FirstName", title: "First Name" },
                        { field: "LastName", title: "Last Name" }
                    ],
                dataSource: new kendo.data.DataSource({
                    transport: {
                        read: "api/employees"
                    },
                    schema: {                   
                        data: "Data",
                        total: "Count"
                    },
                    pageSize: 3,
                    serverPaging: true
                }),
                pageable: true
            });
        
        });

    
    </script>

</body>
</html>