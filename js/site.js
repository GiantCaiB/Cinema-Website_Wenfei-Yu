// Write your Javascript code.
function check1() {
    if (document.loginform.logininput.value == null || document.loginform.logininput.value == "0") {
        alert("need login");
        return false;
    } else {
        return true;
    }
}
function sumall() {
    if(document.aaa.ccc.value=="1"){
        document.getElementById("bbb1").value= "8";
    }
    return false;
}
function paymentcheck() {
    if (document.getElementById("logintest").value == null || document.getElementById("logintest").value == "0") {
        alert("need login");
        return false;
    } else {
        if (document.getElementById("numbercheck").value == "true") {
            if (document.getElementById("expiryMonth").value < 5   && document.getElementById("expiryYear").value=="17")
            {
                alert("This credit card has expired!");
                return false;
            }else{
                return true;
            }
        } else {
            alert("error number");
            return false;
        }
    }
}
function logincheck() {
    if (document.getElementById("logincheck").value == null || document.getElementById("logincheck").value == "0") {
        alert("need login");
        return false;
    } else {
        return true;
    }
}

function capcheck() {
    if (document.getElementById("cap").value < 5) {
        return true;
    } else {
        alert("Shopping Cart is full(maximum 5 items)");
        return false;
    }
}

