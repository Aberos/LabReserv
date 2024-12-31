﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
    init: (id, columns, path, enableOptions = true) => {
        if (enableOptions) {
            if (!columns)
                columns = [];

            columns.push({
                data: 'id',
                title: 'Options',
                width: '30%',
                className: 'text-end',
                createdCell: function (td, cellData, rowData, row, col) {
                    $(td).html(`
                        <button class="btn btn-sm btn-secondary btn-edit" data-id="${rowData.id}"><i class="bi bi-pen"></i> Edit</button>
                        <button class="btn btn-sm btn-danger btn-delete" data-id="${rowData.id}"><i class="bi bi-trash"></i> Delete</button>
                    `);


                    $(td).find('.btn-edit').on('click', function () {
                        $(document).trigger(`${id}-edit`, [rowData.id]);
                    });

                    $(td).find('.btn-delete').on('click', function () {
                        var rowId = $(this).data('id');
                        $('#confirmDeleteModal').data('rowId', rowId).modal('show');
                    });
                }
            });
        }

        let table = $(`#${id}`).DataTable({
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
                            text: '<i class="bi bi-plus"></i> Add',
                            className: 'btn btn-sm btn-primary',
                            action: function (e, dt, node, config) {
                                $(document).trigger(`${id}-add`);
                            }
                        }
                    ]
                }
            }
        });

        $('#confirmDeleteButton').on('click', function () {
            var rowId = $('#confirmDeleteModal').data('rowId');
            $(document).trigger(`${id}-delete`, [rowId]);
            document.activeElement.blur();
            $('#confirmDeleteModal').modal('hide');
        });

        $('#confirmDeleteModal').on('hidden.bs.modal', function () {
            $('#confirmDeleteButton').blur();
        });

        return table;
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

window.loading = {
    show: () => {
        $('#loadingOverlay').css('display', 'flex');
    },

    hide: () => {
        $('#loadingOverlay').css('display', 'none');
    }
}