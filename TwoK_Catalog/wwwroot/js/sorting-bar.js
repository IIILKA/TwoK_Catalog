const sortTitles = document.querySelectorAll('.sorting-bar__head');


//вешаем события на .sorting-bar__label
if(sortTitles.length > 0) {
    for(let i = 0; i < sortTitles.length; i++) {
        const sortTitle = sortTitles[i];
        sortTitle.addEventListener("click", (e) => {
            let parentBox = e.target.closest('.sorting-bar__box');
            let isActive = parentBox.getAttribute('data-isActive');
            if(isActive == "true") {
                parentBox.setAttribute('data-isActive', 'false');
            }
            else if(isActive == "false") {
                parentBox.setAttribute('data-isActive', 'true');
            }
        });
    }
}

const checkboxPictures = document.querySelectorAll('.sorting-bar__picture-checkbox');

//вешаем события на .sorting-bar__picture-checkbox
if(checkboxPictures.length > 0) {
    for(let i = 0; i < checkboxPictures.length; i++) {
        const checkboxPicture = checkboxPictures[i];
        checkboxPicture.addEventListener("click", (e) => {
            let isActive = e.target.getAttribute('data-isActive');
            if(isActive == "true") {
                e.target.setAttribute('data-isActive', 'false');
            }
            else if(isActive == "false") {
                e.target.setAttribute('data-isActive', 'true');
            }
        });
    }
}