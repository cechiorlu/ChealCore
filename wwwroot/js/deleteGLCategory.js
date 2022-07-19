// Delete Gl Category modal scripts
const glcModal = document.getElementById("delete-glcategory-modal");

const glcDeleteBtn = document.getElementById("delete-glcategory-trigger");

const glcDetailsBtn = document.getElementById("getdetails-glcategory-trigger");

const glcCloseBtn = document.getElementById("close-delete-glcategory-modal-btn");

const glcCancelDeleteBtn = document.getElementById("cancel-delete-glcategory");

const header = document.getElementsByClassName("mheader");

const footerForm = document.getElementById("glc-footer-form");

glcDetailsBtn.onclick = function () {
    header[0].textContent = 'Details';
    footerForm[0].style.display = "none";
    glcModal.style.display = "block";
}

glcDeleteBtn.onclick = function () {
    footerForm[0].style.display = "block";
    glcModal.style.display = "block";
}

glcCloseBtn.onclick = function () {
    glcModal.style.display = "none";
}

glcCancelDeleteBtn.onclick = function () {
    glcModal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == glcModal) {
        glcModal.style.display = "none";
    }
}


