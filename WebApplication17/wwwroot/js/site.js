const container = document.querySelector('.box-container');
const contents = document.querySelectorAll('.content');
const buttons = document.querySelectorAll('button');

buttons.forEach(button => button.addEventListener('click', function () {
    container.classList.toggle('right-open');
}));