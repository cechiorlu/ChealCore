// Delete role modal scripts
const roleModal = document.getElementById("delete-role-modal");

const deleteRoleBtn = document.getElementById("delete-role-trigger");

const closeRoleBtn = document.getElementById("close-delete-role-modal-btn");

const cancelDeleteRoleBtn = document.getElementById("cancel-delete-role")

deleteRoleBtn.onclick = function () {
    roleModal.style.display = "block";
    console.log(modal, deleteBtn, closeBtn, cancelDeleteBtn)
}

closeRoleBtn.onclick = function () {
    roleModal.style.display = "none";
}

cancelDeleteRoleBtn.onclick = function () {
    roleModal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == roleModal) {
        roleModal.style.display = "none";
    }
}
