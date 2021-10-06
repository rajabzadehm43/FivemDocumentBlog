const setImagePreview = (input: HTMLInputElement, img: HTMLImageElement) => {

    input.onchange = (event) => {
        let reader = new FileReader();
        reader.onload = () => {
            img.src = reader.result.toString();
        }
        reader.readAsDataURL(input.files[0]);
    }
}

const setClickOnElement = (source: string, target: string) => {

    let sourceEl = document.querySelector(source);
    let targetEl = document.querySelector(target) as HTMLElement;

    sourceEl.addEventListener("click", (event) => {
        targetEl.click();
    });
}

const uploadFile = (url: string, form: HTMLFormElement, cb: any) => {

    const fData = new FormData(form);

    const xhr = new XMLHttpRequest();
    xhr.open('POST', url, true);
    xhr.send(fData);

    if (!cb) {
        return;
    }
    xhr.onerror = (err) => {
        console.log(err);
        console.log(xhr.responseText);
        console.log('---------------');
    }
    cb(xhr);

    /*xhr.onreadystatechange = () => {

        if (xhr.readyState !== 4 || xhr.status !== 200)
            return;

        if (!cb)
            return;

        cb(xhr);
    }*/

}

const getFormData = (el: HTMLFormElement) => new FormData(el);

const sendHttpRequest = (url: string, method: string, data: FormData, cb: any) => {
    const xhr = new XMLHttpRequest();
    xhr.open(method, url, true);

    if (method.toLowerCase() != 'get') {
        xhr.send(data);
    } else {
        xhr.send();
    }

    if (!cb) {
        return;
    }
    xhr.onerror = (err) => {
        console.log(err);
        console.log(xhr.responseText);
        console.log('---------------');
    }
    cb(xhr);
}




/********************** Auto Actions ******************************/

// data request #click
document.querySelectorAll('[data-req-type="click"]')
    .forEach(el => {

        el.addEventListener('click', (event) => {
            let target = document.querySelector(el.getAttribute("data-click-target")) as HTMLElement;
            if (!target) return;
            target.click();
        });

    });

// data request #preview
document.querySelectorAll('input[type=file][data-req-type="preview"]')
    .forEach(el => {
        document.querySelectorAll(el.getAttribute("data-preview-target"))
            .forEach(tr => {
                setImagePreview(el as HTMLInputElement, tr as HTMLImageElement);
            });
    });

// data request #active-link
document.querySelectorAll('[data-req-type="active-link"]')
    .forEach(el => {
        const activeLink = el.getAttribute("data-active-link") ?? el.getAttribute("href");
        const activeClass = el.getAttribute('data-active-class') ?? "active";

        if (location.pathname === activeLink) {
            el.classList.add(activeClass);
        }

    });