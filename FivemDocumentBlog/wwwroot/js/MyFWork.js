var setImagePreview = function (input, img) {
    input.onchange = function (event) {
        var reader = new FileReader();
        reader.onload = function () {
            img.src = reader.result.toString();
        };
        reader.readAsDataURL(input.files[0]);
    };
};
var setClickOnElement = function (source, target) {
    var sourceEl = document.querySelector(source);
    var targetEl = document.querySelector(target);
    sourceEl.addEventListener("click", function (event) {
        targetEl.click();
    });
};
var uploadFile = function (url, form, cb) {
    var fData = new FormData(form);
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url, true);
    xhr.send(fData);
    if (!cb) {
        return;
    }
    xhr.onerror = function (err) {
        console.log(err);
        console.log(xhr.responseText);
        console.log('---------------');
    };
    cb(xhr);
    /*xhr.onreadystatechange = () => {

        if (xhr.readyState !== 4 || xhr.status !== 200)
            return;

        if (!cb)
            return;

        cb(xhr);
    }*/
};
var getFormData = function (el) { return new FormData(el); };
var sendHttpRequest = function (url, method, data, cb) {
    var xhr = new XMLHttpRequest();
    xhr.open(method, url, true);
    if (method.toLowerCase() != 'get') {
        xhr.send(data);
    }
    else {
        xhr.send();
    }
    if (!cb) {
        return;
    }
    xhr.onerror = function (err) {
        console.log(err);
        console.log(xhr.responseText);
        console.log('---------------');
    };
    cb(xhr);
};
/********************** Auto Actions ******************************/
// data request #click
document.querySelectorAll('[data-req-type="click"]')
    .forEach(function (el) {
    el.addEventListener('click', function (event) {
        var target = document.querySelector(el.getAttribute("data-click-target"));
        if (!target)
            return;
        target.click();
    });
});
// data request #preview
document.querySelectorAll('input[type=file][data-req-type="preview"]')
    .forEach(function (el) {
    document.querySelectorAll(el.getAttribute("data-preview-target"))
        .forEach(function (tr) {
        setImagePreview(el, tr);
    });
});
// data request #active-link
document.querySelectorAll('[data-req-type="active-link"]')
    .forEach(function (el) {
    var _a, _b;
    var activeLink = (_a = el.getAttribute("data-active-link")) !== null && _a !== void 0 ? _a : el.getAttribute("href");
    var activeClass = (_b = el.getAttribute('data-active-class')) !== null && _b !== void 0 ? _b : "active";
    if (location.pathname === activeLink) {
        el.classList.add(activeClass);
    }
});
//# sourceMappingURL=MyFWork.js.map