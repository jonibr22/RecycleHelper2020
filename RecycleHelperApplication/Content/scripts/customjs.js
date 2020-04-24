function showAlert(type, message, form) {
    if (typeof (form) == 'undefined') form = "";
    if (message == '') {
        message = 'Data yang telah diubah, tidak dapat dikembalikan.';
    }
    if (type == "confirmation") {
        //alert konfirmasi
        swal({
            title: 'Konfirmasi',
            text: message,
            type: 'warning',
            allowOutsideClick: false,
            allowEscapeKey: false,
            showCancelButton: true,
            customClass: 'swalustom',
            confirmButtonColor: '#2574A9',
            cancelButtonColor: '#cdcdcd',
            confirmButtonText: 'Ya',
            cancelButtonText: 'Tidak'
        }).then((isConfirm) => {
            if (isConfirm) {
                //document.forms[form].submit();         
                $('#' + form).submit();
            }
        })
    }
    else if (type == "error") {
        $.growl.error({
            title: "Gagal!",
            message: message,
            location: 'br',
            size: 'large'
        });
    }
    else if (type == "success") {
        $.growl.notice({
            title: "Sukses!",
            message: message,
            location: 'br',
            size: 'large',
        });
    }
};