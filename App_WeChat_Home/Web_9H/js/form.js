function beginCall() { }

function successCall(data) {
    if (data) {
        if (data.code == 0) {
            location.href = '/home';
        } else {
            alert(data.msg);
        }
    }
}