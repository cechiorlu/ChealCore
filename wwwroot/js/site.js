// Delete user modal scripts
const modal = document.getElementById("delete-user-modal");

const deleteBtn = document.getElementById("delete-user-trigger");

const closeBtn = document.getElementById("close-delete-user-modal-btn");

const cancelDeleteBtn = document.getElementById("cancel-delete-user")

deleteBtn.onclick = function () {
    modal.style.display = "block";
    console.log(modal, deleteBtn, closeBtn, cancelDeleteBtn)
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
