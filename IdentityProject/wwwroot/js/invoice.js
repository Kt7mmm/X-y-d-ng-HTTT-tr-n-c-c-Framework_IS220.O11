/*var inputElement = document.getElementsByName("name")[0];
                var spanElement = document.getElementById("nameSpan");*/
window.addEventListener('DOMContentLoaded', (event) => {
    // Lấy giá trị họ tên từ người dùng đã đăng nhập
    var nameValue = document.getElementById("name").value;

    // Hiển thị giá trị họ tên trong span nameSpan
    var nameSpan = document.getElementById("nameSpan");
    nameSpan.textContent = nameValue;

    // Lấy giá trị email từ người dùng đã đăng nhập
    var emailValue = document.getElementById("email").value;

    // Hiển thị giá trị email trong span emailSpan
    var emailSpan = document.getElementById("emailSpan");
    emailSpan.textContent = emailValue;

    // Lấy giá trị số điện thoại từ người dùng đã đăng nhập
    var phoneValue = document.getElementById("phone").value;

    // Hiển thị giá trị số điện thoại trong span phoneSpan
    var phoneSpan = document.getElementById("phoneSpan");
    phoneSpan.textContent = phoneValue;
});





// Lắng nghe sự kiện khi người dùng chọn input radio
var radioInputs = document.querySelectorAll(".payment-method input[type='radio']");
var paymentSpan = document.getElementById("pay by");
var submitButton = document.getElementById('submit-button');


radioInputs.forEach(function (input) {
    input.addEventListener("change", function () {
        // Kiểm tra xem input radio nào đã được chọn
        if (input.checked) {
            // Lấy giá trị của input radio đã chọn
            var selectedValue = input.value;

            // Hiển thị giá trị trong thẻ span tương ứng
            paymentSpan.textContent = selectedValue;

            if (paymentSpan.innerText.trim() !== '') {
                submitButton.style.display = 'block';
            }
        }
    });
});


document.addEventListener('DOMContentLoaded', function () {

    var emailSpan = document.getElementById('emailSpan');
    var emailInput = document.getElementById('hidden_email');

    // Lấy giá trị từ span
    var emailValue = emailSpan.innerText;

    // Gán giá trị vào trường input
    emailInput.value = emailValue.trim();
});
