            console.log("meet");
document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("form");

    const titleInput = document.getElementById("Title");
    const descriptionInput = document.getElementById("Description");
    const dueDateInput = document.getElementById("DueDate"); 
    const prioritySelect = document.getElementById("Priority");
    const statusSelect = document.getElementById("Status");

    const titleErr = document.getElementById("title_err");
    const descriptionErr = document.getElementById("description_err");
    const dueDateErr = document.getElementById("dueDate_err");
    const priorityErr = document.getElementById("priority_err");
    const statusErr = document.getElementById("status_err");

    const btnInsert = document.querySelector(".btn-success");
    const today = new Date().toISOString().split("T")[0];
    dueDateInput.setAttribute("min", today);
    function validateTitle() {
        const value = titleInput.value.trim();
        if (value === "") {
            titleErr.textContent = "* Title is required";
            return false;
        } else if (value.length < 3) {
            titleErr.textContent = "* Title must be at least 3 characters";
            return false;
        } else if (value.length > 50) {
            titleErr.textContent = "* Title must be less than 50 characters";
            return false;
        } else {
            titleErr.textContent = "";
            return true;
        }
    }

    function validateDescription() {
        const value = descriptionInput.value.trim();
        if (value === "") {
            descriptionErr.textContent = "* Description is required";
            return false;
        } else if (value.length < 10) {
            descriptionErr.textContent = "* Description must be at least 10 characters";
            return false;
        } else if (value.length > 200) {
            descriptionErr.textContent = "* Description must be less than 200 characters";
            return false;
        } else {
            descriptionErr.textContent = "";
            return true;
        }
    }

    function validateDueDate() {

        if (dueDateInput.value === "") {
            dueDateErr.textContent = "* Due Date is required";
            return false;
        } else {
            dueDateErr.textContent = "";
            return true;
        }
    }

    function validatePriority() {
        if (prioritySelect.value === "") {
            priorityErr.textContent = "* Priority is required";
            return false;
        } else {
            priorityErr.textContent = "";
            return true;
        }
    }

    function validateStatus() {
        if (statusSelect.value === "") {
            statusErr.textContent = "* Status is required";
            return false;
        } else {
            statusErr.textContent = "";
            return true;
        }
    }

    function validateForm() {
        const isTitleValid = validateTitle();
        const isDescValid = validateDescription();
        const isDueValid = validateDueDate();
        const isPriorityValid = validatePriority();
        const isStatusValid = validateStatus();

        return isTitleValid && isDescValid && isDueValid && isPriorityValid && isStatusValid;
    }


    
    titleInput.addEventListener("input", validateTitle);
    descriptionInput.addEventListener("input", validateDescription);
    dueDateInput.addEventListener("change", validateDueDate);
    prioritySelect.addEventListener("change", validatePriority);
    statusSelect.addEventListener("change", validateStatus);

    btnInsert.addEventListener("click", (e) => {
        e.preventDefault();
        console.log("Submit clicked");
        if (validateForm()) {
            console.log("Form is valid");
            form.submit();
        } else {
            console.log("Form is invalid");
        }
    });
});

