var formLogin = document.querySelector('.formLogin')
var formRegister = document.querySelector('.formRegister')
var btnSubmit = document.querySelector('.submit')
var userName = document.querySelector('#username')
var password = document.querySelector('#password')
var passwordConfirm = document.querySelector('#passwordConfirm')
console.log(userName)



function handleRegister(){
    formRegister.onsubmit = function(e){
        if(password.value != passwordConfirm.value){
            alert('Mật khẩu nhập lại không đúng!')
            e.preventDefault();
        }
    }
}


const input1 = document.getElementById('ip__checkout-1');
const input2 = document.getElementById('ip__checkout-2');


const divAccount = document.querySelector('.box__account');

function handleCheckOut() {
    input1.addEventListener('click', function () {

        if (input1.checked) {
            divAccount.style.display = 'none';
            $('.txtlogin').removeAttr('required');
        }
    });

    input2.addEventListener('click', function () {

        if (input2.checked) {
            divAccount.style.display = 'block';
            $('.txtlogin').prop('required', true);
            console.log("change input")
        }
    });
  
}

var mainImg = document.querySelector(".main__img")
var listImg = document.querySelectorAll(".list__img--item")

// console.log(listImg)
for (var i = 0; i < listImg.length; i++) {
    listImg[i].addEventListener('click', function () {
        var imgSmall = this.src
        mainImg.src = imgSmall
    })
}

listImg.forEach(function (image) {
    image.addEventListener('click', function () {
        listImg.forEach(function (img) {
            img.classList.remove('img__active');
        });
        this.classList.add('img__active');
    });
});


function start(){
    // handleSubmit();
    handleCheckOut();
    handleRegister();
   
}

start();