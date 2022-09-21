const customSelects = document.querySelectorAll('.select');
const trianglesForSelect = document.querySelectorAll('.triangle-for-select');

if (customSelects.length > 0 && trianglesForSelect.length > 0) {
    for (let i = 0; i < trianglesForSelect.length; i++) {
        let select = customSelects[i];
        let triangle = trianglesForSelect[i];

        select.addEventListener('mouseover', () => {
            if ('mouseOver' !== select.getAttribute('data-state')) {
                select.setAttribute('data-state', 'mouseOver');
                triangle.setAttribute('data-state', 'mouseOver');
            }
        });
        
        select.addEventListener('mouseout', () => {
            if ('mouseOver' === select.getAttribute('data-state')) {
                select.setAttribute('data-state', 'mouseOut');
                triangle.setAttribute('data-state', 'mouseOut');
            }
        });
    }
}