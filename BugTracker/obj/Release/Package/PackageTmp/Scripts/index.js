$(document).ready(function () {

    $('#TicketDataTable').dataTable({
        "bServerSide": true,
        "sAjaxSource": "Tickets/AjaxHandler",
        "bProcessing": true,
        "aoColumns": [
                        {
                            "sName": "ID",
                            "bSearchable": false,
                            "bSortable": false,
                            "fnRender": function (oObj) {
                                return '<a href=\"Index/' +
                                oObj.aData[0] + '\">View</a>';
                            }
                        },
                        { "sName": "Project" },
                        { "sName": "Ticket" },
                        { "sName": "Created By" },
                        { "sName": "Create Date" },
                        { "sName": "Assigned To" },
                        { "sName": "Type" },
                        { "sName": "Priority" },
                        { "sName": "Status" },
                        { "sName": "Last Updated" }

        ]
    });
});