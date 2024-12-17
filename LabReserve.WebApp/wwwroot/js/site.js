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
            serverSide: true,
            ajax: {
                url: path,
                type: 'GET'
            },
            columns: columns,
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