// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const modal = document.getElementById("holidaymodal");
const modalupdate = document.getElementById("holidaymodal1");

const deletbtn = document.querySelectorAll(".delete-btn");
const btnUpdate = document.querySelectorAll(".btnUpdate");
//const clearfixcancelbtn = document.querySelector(".cancelbtn");
//const clearfixcancelbtnn = document.querySelector(".cancelbtnn");

deletbtn.forEach((eachbtn) => {
    eachbtn.addEventListener("click", (e) => {
        debugger
        document.getElementsByClassName('delete-item')[0].setAttribute("id", e.currentTarget.dataset.id);
        modal.style.display = "block";
    });
});
btnUpdate.forEach((eachbtn) => {
    eachbtn.addEventListener("click", (e) => {

        document.querySelector(".tpd").style.display = "block";
    });
});
//clearfixcancelbtn.addEventListener("click", () => {
//    modal.style.display = "none";
//});

//clearfixcancelbtnn.addEventListener("click", () => {
//    modaldetail.style.display = "none";
//});
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    } else if (event.target == modalupdate) {
        modalupdate.style.display = "none";
    }
};