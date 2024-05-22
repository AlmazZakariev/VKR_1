const requests = document.querySelectorAll('.request');

requests.forEach(request => {
    const id = request.dataset.id;
    console.log(id);
    let regBtn = request.querySelector('.reg')
   //const roomNumberBlock = request.querySelector(`.form-group[data-id="${id}"]`);

    regBtn.addEventListener('click', () => {
        request.querySelector(`[for="test-${id}"]`).classList.remove('d-none');
        
    });
});