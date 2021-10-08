const bodyEl = document.getElementsByTagName('body')[0]
const footerEl = document.getElementsByTagName('footer')[0]

let bMargin = footerEl.offsetHeight + 23;
bodyEl.style['margin-bottom'] = bMargin + 'px'

let headerSize = parseInt($("header").css("width")) - (2 * parseInt($("header").css("padding-right")));
let HeaderHeightSize = $("header").css("height");
let menuListSize = parseInt($("#menu-list").css("width"));
let Menu_List_Parent = $(".menu-list-parent");
let body_height_size = $("body").height();
let body = $("body");

if (menuListSize >= headerSize || headerSize <= 800) {
    let menu = $("#menu-list");
    let toggleMenuBtn = $("#btn-toggle-menu-list");

    body.attr("device", "Mobile");

    Menu_List_Parent.css({ "top": "" + HeaderHeightSize + "" });
    Menu_List_Parent.height(body_height_size - parseInt(HeaderHeightSize) - 0.5 + "px");
    console.log(body_height_size, parseInt(HeaderHeightSize));

    toggleMenuBtn.click(function () {
        Menu_List_Parent.toggleClass("active");
        body.toggleClass("active");
    })
}