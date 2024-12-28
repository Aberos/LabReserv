// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    'use strict'
    feather.replace({ 'aria-hidden': 'true' })
})

window.toastMessage = {
    error: (value) => {
        if (value.responseText) {
            if (window.utils.isJsonString(value.responseText)) {
                let errorObject = JSON.parse(value.responseText);
                if (errorObject.errors) {
                    let errors = Object.values(errorObject.errors).flat();
                    for (let error of errors) {
                        $.toast({
                            heading: 'Error',
                            text: error,
                            icon: 'error',
                            showHideTransition: 'fade',
                            position: 'bottom-right',
                        })
                    }
                }
            } else {
                $.toast({
                    heading: 'Error',
                    text: value.responseText,
                    icon: 'error',
                    showHideTransition: 'fade',
                    position: 'bottom-right',
                })
            }
        }
    }
}


window.tables = {
    init: (id, columns, path) => {
        return $(`#${id}`).DataTable({
            ordering: false,
            serverSide: true,
            ajax: {
                url: path,
                type: 'GET',
                data: function (d) {
                    return $.extend({}, d, {
                        requestId: d.draw,
                        search: d.search.value,
                        page: (d.start / d.length) + 1,
                        pageSize: d.length
                    });
                },
                dataFilter: function (data) {
                    var json = jQuery.parseJSON(data);
                    return JSON.stringify({
                        draw: json.requestId,
                        recordsTotal: json.totalCount,
                        recordsFiltered: json.totalCount,
                        data: json.data
                    });
                }
            },
            columns: columns,
            lengthMenu: [10, 25, 50, 100],
            pageLength: 10,
            lengthChange: false,
            layout: {
                topStart: {
                    buttons: [
                        {
                            text: 'Add',
                            className: 'btn btn-sm btn-primary',
                            action: function (e, dt, node, config) {
                                $(document).trigger(`${id}-add`);
                            }
                        }
                    ]
                }
            }
        });
    }
}

window.utils = {
    isJsonString: (str) => {
        try {
            const parsed = JSON.parse(str);
            return typeof parsed === 'object' && parsed !== null;
        } catch (e) {
            return false;
        }
    }
}