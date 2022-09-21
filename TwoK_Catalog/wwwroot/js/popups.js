const wrapper = document.querySelector('.wrapper');
const popupLinks = document.querySelectorAll('.popup-link');
const body = document.querySelector('body');//для блокировки скрола
const lockPaddings = document.querySelectorAll('.lock-padding');//элементы с position: fixed 
                                                               //(т.к. на них не влияет padding в body
                                                               //для них надо выставлять его отдельно)

//получаем ширину скрола
const lockPaddingValue = window.innerWidth - document.querySelector('.body').offsetWidth;
console.log("Lock padding value: " + lockPaddingValue);


let unlock = true;

const timeoutOpen = 800;//время в transition для popup
const timeoutClose = 200;//время в transition для popup

//вешаем событие на заранее открытые попапы
let openedPopup = document.querySelector('.popup.open');
if(openedPopup != null) {
    let openedPopupName = openedPopup.getAttribute('id');
    openedPopup = document.getElementById(openedPopupName);
    openedPopup.addEventListener("click", (e) => {
        if (!e.target.closest('.popup__content')) {
            closePopup(e.target.closest('.popup.open'));
        }
    });
    lockBody();
}

//вешаем события на .popup__link
if (popupLinks.length > 0) {
    for (let i = 0; i < popupLinks.length; i++) {
        const popupLink = popupLinks[i];
        popupLink.addEventListener("click", (e) => {
            const popupName = popupLink.getAttribute('href').replace('#', '');
            const curPopup = document.getElementById(popupName);
            openPopup(curPopup);
            e.preventDefault();//запрещаем ссылке перезагружать странницу
        });
    }
}

const popupCloseIcons = document.querySelectorAll('.close-popup');
if (popupCloseIcons.length > 0) {
    for (let i = 0; i < popupCloseIcons.length; i++) {
        const popupCloseIcon = popupCloseIcons[i];
        popupCloseIcon.addEventListener("click", (e) => {
            closePopup(popupCloseIcon.closest('.popup'));
            e.preventDefault();//запрещаем ссылке перезагружать странницу
        });
    }
}

function openPopup(curentPopup) {
    if (curentPopup && unlock) {
        const popupActive = document.querySelector('.popup.open');
        if (popupActive) {
            closePopup(popupActive, false);
        }
        else {
            lockBody();
        }
        curentPopup.classList.add('open');

        //закрываем popup если кликнули куда-либо кроме контента popup'а(на затемнённую область)
        curentPopup.addEventListener("click", (e) => {
            if (!e.target.closest('.popup__content')) {
                closePopup(e.target.closest('.popup'));
            }
        });
    }
}

function closePopup(popupActive, doUnlock = true) {
    if (unlock) {
        popupActive.classList.remove('open');
        if (doUnlock) {
            unlockBody();
        }
    }
}

//добавляет всем элементам отступ равный ширине скрола, чтобы когда он пропал
//(во аремя того как popup виден) всё содержимое страницы не съехало
function lockBody() {
    if (lockPaddings.length > 0) {
        for (let i = 0; i < lockPaddings.length; i++) {
            let rightPadding = getComputedStyle(lockPaddings[i].style.paddingRight).replace('px', '');
            let newPadding = rightPadding + lockPaddingValue;
            lockPaddings[i].style.paddingRight = newPadding + 'px';
        }
    }
    let rightMargin = parseInt(getComputedStyle(wrapper).marginRight);
    let newMargin = rightMargin + lockPaddingValue;
    wrapper.style.marginRight = newMargin + 'px';
    //.lock убирает скрол в <body>
    body.classList.add('lock');

    //блокируем страницу чтобы в момент перехода повторное нажатие на ссылку
    //не вызвало ошибку
    unlock = false;
    setTimeout(() => {
        unlock = true;
    }, timeoutOpen);
}

function unlockBody() {
    setTimeout(() => {
        if (lockPaddings.length > 0) {
            for (let i = 0; i < lockPaddings.length; i++) {
                let rightPadding = getComputedStyle(lockPaddings[i].style.paddingRight).replace('px', '');
                let newPadding = rightPadding - lockPaddingValue;
                lockPaddings[i].style.paddingRight = newPadding + 'px';
            }
        }
        wrapper.style.marginRight = 'auto';
        wrapper.style.marginLeft = 'auto';
        //.lock убирает скрол в <body>
        body.classList.remove('lock');
    }, timeoutClose);

    unlock = false;
    setTimeout(() => {
        unlock = true;
    }, timeoutOpen);
}

document.addEventListener('keydown', (e) => {
    if (e.which === 27) {
        const popupActive = document.querySelector('.popup.open');
        if (popupActive) {
            closePopup(popupActive);
        }
    }
});