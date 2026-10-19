window.layout = {
    desktopQuery: window.matchMedia('(min-width: 992px)'),

    init: () => {
        $('#sidebarToggle').on('click', window.layout.toggleSidebar);
        $('#appBackdrop').on('click', window.layout.closeMobileSidebar);
        $('#appSidebar .nav-link').on('click', window.layout.closeMobileSidebar);
        window.layout.desktopQuery.addEventListener('change', window.layout.closeMobileSidebar);
    },

    toggleSidebar: () => {
        if (window.layout.desktopQuery.matches) {
            const collapsed = document.body.classList.toggle('sidebar-collapsed');
            window.localStorage.setItem('sidebar-collapsed', collapsed);
            $('.app-main').one('transitionend', () => $(window).trigger('resize'));
            return;
        }

        document.body.classList.toggle('sidebar-open');
    },

    closeMobileSidebar: () => {
        document.body.classList.remove('sidebar-open');
    }
};

window.toastMessage = {
    show: (text, icon, heading) => {
        $.toast({
            heading: heading,
            text: text,
            icon: icon,
            showHideTransition: 'fade',
            position: 'top-right',
            loader: false
        });
    },

    success: (text) => window.toastMessage.show(text, 'success', 'Success'),

    error: (value) => {
        window.toastMessage.extractErrors(value)
            .forEach(message => window.toastMessage.show(message, 'error', 'Error'));
    },

    extractErrors: (value) => {
        if (!value)
            return ['Unexpected error'];

        if (typeof value === 'string')
            return [value];

        if (Array.isArray(value))
            return value;

        if (value.responseJSON && Array.isArray(value.responseJSON))
            return value.responseJSON.map(error => error.errorMessage);

        if (value.responseJSON?.errors)
            return Object.values(value.responseJSON.errors).flat();

        if (value.responseText && !window.utils.isJsonString(value.responseText))
            return [value.responseText];

        return ['Unexpected error'];
    }
};

window.utils = {
    isJsonString: (text) => {
        try {
            const parsed = JSON.parse(text);
            return typeof parsed === 'object' && parsed !== null;
        } catch {
            return false;
        }
    },

    escapeHtml: (text) => $('<div>').text(text ?? '').html()
};

window.loading = {
    show: () => $('#loadingOverlay').css('display', 'flex'),
    hide: () => $('#loadingOverlay').css('display', 'none')
};

window.confirmDelete = (onConfirm) => {
    $('#confirmDeleteModal').data('onConfirm', onConfirm).modal('show');
};

window.select2 = {
    remote: (selector, url, options) => {
        const element = $(selector);
        const settings = Object.assign({ pageSize: 10, text: item => item.name }, options);

        return element.select2({
            theme: 'bootstrap-5',
            width: '100%',
            dropdownParent: settings.dropdownParent,
            placeholder: settings.placeholder ?? '',
            allowClear: settings.allowClear ?? false,
            multiple: element.prop('multiple'),
            ajax: {
                url: url,
                dataType: 'json',
                delay: 250,
                data: params => ({ search: params.term, page: params.page || 1, pageSize: settings.pageSize }),
                processResults: (result, params) => ({
                    results: result.data.map(item => ({ id: item.id, text: settings.text(item) })),
                    pagination: { more: (params.page || 1) < result.totalPages }
                })
            }
        });
    },

    setSelected: (selector, items) => {
        const element = $(selector).empty();
        (items ?? []).forEach(item => element.append(new Option(item.text, item.id, true, true)));
        element.trigger('change');
    }
};

window.tables = {
    actionsColumn: (tableId) => ({
        data: 'id',
        title: '',
        className: 'table-actions all',
        responsivePriority: 1,
        render: (id) => `
            <button type="button" class="btn btn-sm btn-outline-secondary btn-edit" data-id="${id}" title="Edit">
                <i class="bi bi-pencil"></i><span class="d-none d-lg-inline ms-1">Edit</span>
            </button>
            <button type="button" class="btn btn-sm btn-outline-danger btn-delete" data-id="${id}" title="Delete">
                <i class="bi bi-trash"></i><span class="d-none d-lg-inline ms-1">Delete</span>
            </button>`
    }),

    init: (id, columns, path) => {
        const tableElement = $(`#${id}`);
        tableElement.addClass('table table-hover align-middle w-100');

        const table = tableElement.DataTable({
            ordering: false,
            serverSide: true,
            responsive: true,
            autoWidth: false,
            pageLength: 10,
            lengthChange: false,
            language: {
                search: '',
                searchPlaceholder: 'Search...',
                emptyTable: 'No records found',
                zeroRecords: 'No records found'
            },
            layout: {
                topStart: 'search',
                topEnd: null,
                bottomStart: 'info',
                bottomEnd: 'paging'
            },
            ajax: {
                url: path,
                type: 'GET',
                data: request => ({
                    requestId: request.draw,
                    search: request.search.value,
                    page: (request.start / request.length) + 1,
                    pageSize: request.length
                }),
                dataSrc: json => {
                    json.draw = json.requestId;
                    json.recordsTotal = json.totalCount;
                    json.recordsFiltered = json.totalCount;
                    return json.data;
                }
            },
            columns: [...columns, window.tables.actionsColumn(id)]
        });

        table.on('dt-error', (event, settings, techNote, message) => {
            window.toastMessage.error(message.replace(/^DataTables warning: /, ''));
        });

        tableElement.on('click', '.btn-edit', function () {
            $(document).trigger(`${id}-edit`, [$(this).data('id')]);
        });

        tableElement.on('click', '.btn-delete', function () {
            const rowId = $(this).data('id');
            window.confirmDelete(() => $(document).trigger(`${id}-delete`, [rowId]));
        });

        $(`[data-table-add="${id}"]`).on('click', () => $(document).trigger(`${id}-add`));

        return table;
    }
};

window.forms = {
    serialize: (form) => {
        const data = {};
        form.find('input[name], select[name], textarea[name]').each(function () {
            const element = $(this);
            if (this.type === 'checkbox') {
                data[this.name] = this.checked;
                return;
            }

            const value = element.val();
            if (element.data('type') === 'number') {
                data[this.name] = Array.isArray(value)
                    ? value.map(Number)
                    : (value === '' || value === null ? null : Number(value));
                return;
            }

            data[this.name] = value === '' ? null : value;
        });

        return data;
    },

    setFormFields: (form, values) => {
        Object.entries(values).forEach(([key, value]) => {
            const element = form.find(`[name="${key}"]`);
            if (element.length === 0 || typeof value === 'object')
                return;

            element.val(value ?? '');
            if (element.is('select'))
                element.trigger('change');
        });
    },

    clearFormFields: (form) => {
        form.find('input, textarea, select').each(function () {
            if (this.type === 'checkbox' || this.type === 'radio') {
                this.checked = false;
                return;
            }

            if ($(this).hasClass('select2-hidden-accessible')) {
                $(this).empty().trigger('change');
                return;
            }

            if (this.tagName === 'SELECT') {
                this.selectedIndex = 0;
                return;
            }

            $(this).val('');
        });
    },

    setupEntityTable: (table, entityName) => {
        const tableId = table.table().node().id;
        const modal = $(`#${entityName}-form-modal`);
        const form = $(`#${entityName}-form`);
        const submitButton = modal.find('#btnSubmitForm');
        const label = modal.data('entity-label') ?? entityName;

        const setTitle = (isEdit) => modal.find('.modal-title').text(`${isEdit ? 'Edit' : 'New'} ${label}`);

        $(document).on(`${tableId}-add`, () => {
            setTitle(false);
            form.trigger('form:new');
            modal.modal('show');
        });

        modal.on('hidden.bs.modal', () => window.forms.clearFormFields(form));
        modal.on('shown.bs.modal', () => form.find('input:visible:first').trigger('focus'));

        $(document).on(`${tableId}-edit`, (event, entityId) => {
            $.ajax({
                url: `/${entityName}/${entityId}`,
                type: 'GET',
                beforeSend: window.loading.show,
                success: (result) => {
                    setTitle(true);
                    window.forms.setFormFields(form, result);
                    form.trigger('form:loaded', [result]);
                    modal.modal('show');
                },
                error: window.toastMessage.error,
                complete: window.loading.hide
            });
        });

        $(document).on(`${tableId}-delete`, (event, entityId) => {
            $.ajax({
                url: `/${entityName}/${entityId}`,
                type: 'DELETE',
                beforeSend: window.loading.show,
                success: (message) => {
                    table.ajax.reload(null, false);
                    window.toastMessage.success(message);
                },
                error: window.toastMessage.error,
                complete: window.loading.hide
            });
        });

        submitButton.on('click', () => form.trigger('submit'));

        form.on('submit', (event) => {
            event.preventDefault();
            const data = window.forms.serialize(form);
            const isEdit = Boolean(data.id);

            submitButton.prop('disabled', true);
            $.ajax({
                url: isEdit ? `/${entityName}/${data.id}` : `/${entityName}`,
                type: isEdit ? 'PUT' : 'POST',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: (message) => {
                    table.ajax.reload(null, false);
                    modal.modal('hide');
                    window.toastMessage.success(message);
                },
                error: window.toastMessage.error,
                complete: () => submitButton.prop('disabled', false)
            });
        });
    }
};

$(function () {
    window.layout.init();

    $('#confirmDeleteButton').on('click', function () {
        const modal = $('#confirmDeleteModal');
        const onConfirm = modal.data('onConfirm');
        modal.removeData('onConfirm').modal('hide');
        $(this).trigger('blur');
        if (typeof onConfirm === 'function')
            onConfirm();
    });
});
