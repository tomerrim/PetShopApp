// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const hiddenElement = document.querySelectorAll(".hidden");
const observer = new IntersectionObserver((enteries) => {
    enteries.forEach((entry) => {
        entry.target.classList.toggle("show", entry.isIntersecting)
    });
});
hiddenElement.forEach((el) => observer.observe(el));
