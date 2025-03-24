document.addEventListener("mousemove", function(event) {
    let cursor = document.querySelector(".cursor");
    cursor.style.top = event.pageY + "px";
    cursor.style.left = event.pageX + "px";
});
