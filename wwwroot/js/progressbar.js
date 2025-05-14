function checkPasswordStrength(password) {
    const strengthBar = document.getElementById("passwordStrengthBar");
    const strengthText = document.getElementById("passwordStrengthText");

    let strength = 0;

    if (password.length >= 6) strength += 1;
    if (password.match(/[a-z]/)) strength += 1;
    if (password.match(/[A-Z]/)) strength += 1;
    if (password.match(/[0-9]/)) strength += 1;
    if (password.match(/[@$!%*?&#^()_+\-=\[\]{ };':"\\|,.<>\/?]+/)) strength += 1;

    let strengthPercent = (strength / 5) * 100;
    strengthBar.style.width = strengthPercent + "%";

    switch (strength) {
        case 0:
            strengthBar.className = "progress-bar bg-danger";
            strengthText.textContent = "";
            break;
        case 1:
            strengthBar.className = "progress-bar bg-danger";
            strengthText.textContent = "Weak";
            break;
        case 2:
            strengthBar.className = "progress-bar bg-warning";
            strengthText.textContent = "Moderate";
            break;
        case 3:
            strengthBar.className = "progress-bar bg-info";
            strengthText.textContent = "Good";
            break;
        case 4:
            strengthBar.className = "progress-bar bg-primary";
            strengthText.textContent = "Strong";
            break;
        case 5:
            strengthBar.className = "progress-bar bg-success";
            strengthText.textContent = "Very Strong";
            break;
    }
}
