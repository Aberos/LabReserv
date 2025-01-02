// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    'use strict'
    feather.replace({ 'aria-hidden': 'true' })
})

window.toastMessage = {
    error: (value) => {
        if (value) {
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
            } else if (Array.isArray(value)) {
                for (let error of value) {
                    $.toast({
                        heading: 'Error',
                        text: error,
                        icon: 'error',
                        showHideTransition: 'fade',
                        position: 'bottom-right',
                    })
                }
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

window.forms = {
    setFormFields: (form, obj) => {
        for (const key in obj) {
            if (obj.hasOwnProperty(key)) {
                const element = form.find(`[name="${key}"]`);
                if (element.length > 0) {
                    element.val(obj[key]);
                }
            }
        }
    },


    clearFormFields: (form) => {
        form.find('input, textarea, select').each(function () {
            switch (this.type) {
                case 'checkbox':
                case 'radio':
                    this.checked = false;
                    break;
                default:
                    $(this).val('');
                    break;
            }
        });
    },

    parseErrors: (result) => {
        if (result.responseJSON && Array.isArray(result.responseJSON)) {
            return result.responseJSON.map(x => `${x.propertyName}: ${x.errorMessage}`);
        }

        return result;
    },

    setupEntityTable: (table, entityName) => {
        const tableId = table.settings()[0].sTableId;
        const entityFormModal = $(`#${entityName}-form-modal`);
        const entityForm = $(`#${entityName}-form`);
        const submitButton = entityFormModal.find(`#btnSubmitForm`);

        $(document).on(`${tableId}-add`, function () {
            entityFormModal.modal('show');
        });

        entityFormModal.on('hidden.bs.modal', function () {
            window.forms.clearFormFields(entityForm);
        });

        $(document).on(`${tableId}-edit`, function (e, entityId) {
            $.ajax({
                url: `/${entityName}/` + entityId,
                type: 'GET',
                beforeSend: () => {
                    window.loading.show();
                },
                success: (res) => {
                    window.forms.setFormFields(entityForm, res);
                    entityFormModal.modal('show');
                },
                error: (res) => {
                    window.toastMessage.error(window.forms.parseErrors(res));
                },
                complete: (data) => {
                    window.loading.hide();
                }
            });
        });

        $(document).on(`${tableId}-delete`, function (e, entityId) {
            $.ajax({
                url: `/${entityName}/` + entityId,
                type: 'DELETE',
                beforeSend: () => {
                    window.loading.show();
                },
                success: (res) => {
                    table.ajax.reload();
                },
                error: (res) => {
                    window.toastMessage.error(window.forms.parseErrors(res));
                },
                complete: (data) => {
                    window.loading.hide();
                }
            });
        });

        submitButton.click(function () {
            entityForm.submit();
        });

        entityForm.submit(function (e) {
            e.preventDefault();
            let data = Object.fromEntries([...new FormData(this)]);
            let url = `/${entityName}`;
            let typeRequest = 'POST';
            if (data.id) {
                url += '/' + data.id;
                typeRequest = 'PUT';
            }

            submitButton.prop("disabled", true);
            $.ajax({
                url: url,
                type: typeRequest,
                contentType: "application/json",
                data: JSON.stringify(data),
                success: (res) => {
                    table.ajax.reload();
                    entityFormModal.modal('hide');
                },
                error: (res) => {
                    window.toastMessage.error(window.forms.parseErrors(res));
                },
                complete: (data) => {
                    submitButton.prop("disabled", false);
                }
            });
        });
    }
}