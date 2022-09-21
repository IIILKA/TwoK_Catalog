const menu = document.querySelector('.menu__category-list');
const menu_title = menu.querySelector('.menu__title');

document.addEventListener('click', () => {
    if (event.target.closest('.menu__title')) {
        if ('active' === menu.getAttribute('data-state')) {
            menu.setAttribute('data-state', '');
        } else {
            menu.setAttribute('data-state', 'active');
        }
    }
    else{
        if('active' === menu.getAttribute('data-state') && !event.target.closest('.menu__catlog-drop')){
            menu.setAttribute('data-state', '');
        }
    }
});

const input = document.querySelector('.text-field__input');
const options = document.querySelectorAll('.search__option');
const searchSelect = document.querySelector('.search__select');

searchSelect.addEventListener('change', () => {
    var curIndex = searchSelect.selectedIndex;
    if(options[curIndex].value === 'products'){
        input.setAttribute('placeholder', 'Поиск по сайту');
    }
    else if(options[curIndex].value === 'news'){
        input.setAttribute('placeholder', 'Поиск в новостях');
    }
    else if(options[curIndex].value === 'reviews'){
        input.setAttribute('placeholder', 'Поиск в обзорах');
    }
});     
