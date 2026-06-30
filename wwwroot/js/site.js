
var formRegister = document.getElementById("formRegister");

if (formRegister) {
    var registerName = document.getElementById("registerName")
    var registerEmail = document.getElementById("registerEmail")
    var registerPassword = document.getElementById("registerPassword")
    var registerVerifyPassword = document.getElementById("registerVerifyPassword")
    var registerError = document.getElementById("registerError")

    function registerValidation(event) {
        //Zera o campo de erro
        registerError.innerHTML = ""

        //Verifica se algum input está vazio
        if (registerName.value === "" || registerEmail.value === "" || registerPassword.value === "" || registerVerifyPassword.value === "") {
            event.preventDefault()
            registerError.innerHTML = "Preencha todos os campos"
            return
        }

        //verifica se o email tem @
        if (!registerEmail.value.includes("@")) {
            event.preventDefault()
            registerError.innerHTML = "Insira um email válido"
            return
        }

        //verifica o tamanho da senha
        if (registerPassword.value.length < 8) {
            event.preventDefault()
            registerError.innerHTML = "A senha deve ter no mínimo 8 caracteres"
            return
        }

        //verifica se as senhas são iguais
        if (registerPassword.value != registerVerifyPassword.value) {
            event.preventDefault()
            registerError.innerHTML = "As senhas devem ser iguais"
            return
        }
    }

    formRegister.addEventListener("submit", registerValidation)
}