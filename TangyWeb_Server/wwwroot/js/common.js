window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 });
    }
}

window.ShowSweetAlert = (type, message) => {
    if (type === "success") {
        Swal.fire({
            title: "Operation Successful",
            text: message,
            icon: "success",
            confirmButtonText: "OK"
        })
    }
    if (type === "error") {
        Swal.fire({
            title: "Operation Failed",
            text: message,
            icon: "error",
            confirmButtonText: "OK"
        });
    }
}