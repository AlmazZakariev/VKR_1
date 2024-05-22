const requests = document.querySelectorAll('.request');

requests.forEach(request => {
    const id = request.dataset.id;
    let regBtn = request.querySelector('.reg');

    let cancelBtn = request.querySelector('.cancel');
   //const roomNumberBlock = request.querySelector(`.form-group[data-id="${id}"]`);

    regBtn.addEventListener('click', () => {
        request.querySelector(`[for="test-${id}"]`).classList.remove('d-none');
        regBtn.classList.add('d-none');
    });

    cancelBtn.addEventListener('click', () => {
        request.querySelector(`[for="test-${id}"]`).classList.add('d-none');
        regBtn.classList.remove('d-none');
    });
});