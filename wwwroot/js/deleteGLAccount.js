// Delete Gl Account modal scripts
const modal = document.getElementById("delete-glaccount-modal");

const deleteBtn = document.getElementById("delete-glaccount-trigger");

const closeBtn = document.getElementById("close-delete-glaccount-modal-btn");

const cancelDeleteBtn = document.getElementById("cancel-delete-glaccount")

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

