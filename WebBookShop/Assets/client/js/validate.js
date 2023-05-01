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

function start(){
    // handleSubmit();
    handleCheckOut();
    handleRegister();
   
}

start();