
// Delete modal scripts
var modal = document.getElementById("delete-modal");

var deleteBtn = document.getElementById("delete-action-trigger");

var closeBtn = document.getElementById("close-delete-modal-btn");

var cancelDeleteBtn = document.getElementById("cancel-delete")

deleteBtn.onclick = function () {
    modal.style.display = "block";
}

closeBtn.onclick = function () {
    modal.style.display = "none";
}

cancelDeleteBtn.onclick = function () {
    modal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}