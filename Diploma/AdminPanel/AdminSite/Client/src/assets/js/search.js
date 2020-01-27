document.querySelector('.name').oninput = function () {
    let val = this.value.trim();
    let elasticItems = document.querySelectorAll('.search_name');
    if (val != '') {
        elasticItems.forEach(function (elem) {
            if (elem.innerText.search(val) == -1) {
                elem.parentElement.classList.add('hide')
            }
            else {
                elem.parentElement.classList.remove('hide')
            }
        });
    }
    else {
        elasticItems.forEach(function (elem) {
            elem.parentElement.classList.remove('hide') 
        });
    }
}

document.querySelector('.mail').oninput = function () {
    let val = this.value.trim();
    let elasticItems = document.querySelectorAll('.search_mail');
    if (val != '') {
        elasticItems.forEach(function (elem) {
            if (elem.innerText.search(val) == -1) {
                elem.parentElement.classList.add('hide')
            }
            else {
                elem.parentElement.classList.remove('hide')
            }
        });
    }
    else {
        elasticItems.forEach(function (elem) {
            elem.parentElement.classList.remove('hide')
        });
    }
}