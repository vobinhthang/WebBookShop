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
            alert('Nhập lại mật khẩu không đúng!')
            e.preventDefault();
        }
    }
}

function start(){
    // handleSubmit();
    handleRegister();
}

start();