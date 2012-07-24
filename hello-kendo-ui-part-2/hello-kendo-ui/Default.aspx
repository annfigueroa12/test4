<!DOCTYPE html>

<html>
<head>
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

            // select the employeesGrid empty div and call the 
            // kendoGrid function to transform it into a grid
            var grid = $("#employeesGrid").kendoGrid({
                // specify the columns on the grid
                columns: [
                        { field: "FirstName", title: "First Name" },
                        { field: "LastName", title: "Last Name" },
                        "Title",
                        "City",
                        { field: "BirthDate", title: "Birthday", template: '#= kendo.toString(BirthDate,"MM/dd/yyyy") #' },
                        { command: ["edit", "destroy"], title: " " }
                ],
                // the datasource for the grid
                dataSource: new kendo.data.DataSource({
                    // the transport tells the datasource what endpoints
                    // to use for CRUD actions
                    transport: {
                        read: "api/employees",
                        update: {
                            // get the id off of the model object that
                            // kendo ui automatically passes to the url function
                            url: function (employee) {
                                return "api/employees/" + employee.Id
                            },
                            type: "POST"
                        },
                        destroy: {
                            // get the id off of the model object that
                            // kendo ui automatically passes to the url function
                            url: function (employee) {
                                return "api/employees/" + employee.Id
                            },
                            type: "DELETE"
                        },
                        parameterMap: function (options, operation) {
                            // if the current operation is an update
                            if (operation === "update") {
                                // create a new JavaScript date object based on the current
                                // BirthDate parameter value
                                var d = new Date(options.BirthDate);
                                // overwrite the BirthDate value with a formatted value that WebAPI
                                // will be able to convert
                                options.BirthDate = kendo.toString(new Date(d), "MM/dd/yyyy");
                            }
                            // ALWAYS return options
                            return options;
                        }
                    },
                    // the schema defines the schema of the JSON coming
                    // back from the server so the datasource can parse it
                    schema: {
                        // the array of repeating data elements (employees)
                        data: "Data",
                        // the total count of records in the whole dataset. used
                        // for paging.
                        total: "Count",
                        model: {
                            id: "Id",
                            fields: {
                                // specify all the model fields, along with validation rules and whether or
                                // not they can be edited or nulled out.
                                FirstName: { editable: false },
                                LastName: { editable: true, nullable: false, validation: { required: true} },
                                Address: { editable: true, nullable: false, validation: { required: true} },
                                City: { editable: true, nullable: false, validation: { required: true} },
                                BirthDate: { editable: true, type: "date" }
                            }
                        },
                        // map the errors if there are any. this automatically raises the "error" 
                        // event
                        errors: "Errors"
                    },
                    error: function (e) {
                        console.log(e.statusText);
                    },
                    // the number of records to show per page
                    pageSize: 3,
                    // do paging on the server
                    serverPaging: true
                }),
                // paging is enabled in the grid
                pageable: true,
                // editing happens inline, one row at a time.
                editable: "inline"
            }).data("kendoGrid");

        });

    
    </script>

</body>
</html>