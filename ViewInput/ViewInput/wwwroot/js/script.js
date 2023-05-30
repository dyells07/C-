function validateForm() {
    var nameInput = document.getElementById("name");
    var addressInput = document.getElementById("address");

    var nameError = document.getElementById("name-error");
    var addressError = document.getElementById("address-error");

    if (nameInput.value.trim() === "") {
        nameError.innerHTML = "Enter Name.";
        nameInput.style.border = "2px solid red";
        return false;
    } else {
        nameError.innerHTML = "";
        nameInput.style.border = "1px solid #ccc";
    }
    if (addressInput.value.trim() === "") {
        addressInput.style.border = "2px solid red";
        addressError.innerHTML = "Empty Address.";
        return false;
    } else {
        addressError.innerHTML = "";
        addressInput.style.border = "1px solid #ccc";
    }

    return true;
}