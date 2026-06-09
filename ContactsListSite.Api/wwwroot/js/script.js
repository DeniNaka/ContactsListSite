const API_URL = "/api/contacts";

let contacts = [];
let selectedContact = null;

const list = document.getElementById("contactList");
const info = document.getElementById("contactInfo");

const modal = document.getElementById("contactModal");
const form = document.getElementById("contactForm");

const createBtn = document.getElementById("createBtn");
const editBtn = document.getElementById("editBtn");
const deleteBtn = document.getElementById("deleteBtn");
const cancelBtn = document.getElementById("cancelBtn");

const contactIdInput = document.getElementById("contactId");

const firstNameInput = document.getElementById("firstName");
const lastNameInput = document.getElementById("lastName");
const phoneInput = document.getElementById("mobilePhone");
const jobTitleInput = document.getElementById("jobTitle");
const birthDateInput = document.getElementById("birthDate");

window.onload = loadContacts;

async function loadContacts() {
    const response = await fetch(API_URL);
    contacts = await response.json();

    renderContacts();
}

function renderContacts() {

    list.innerHTML = "";

    contacts.forEach(contact => {

        const li = document.createElement("li");

        li.textContent = contact.fullName;

        li.onclick = () => selectContact(contact);

        list.appendChild(li);
    });
}

function selectContact(contact) {

    selectedContact = contact;

    info.innerHTML = `
        <p><b>Имя:</b> ${contact.firstName}</p>
        <p><b>Фамилия:</b> ${contact.lastName}</p>
        <p><b>Телефон:</b> ${contact.mobilePhone}</p>
        <p><b>Должность:</b> ${contact.jobTitle}</p>
        <p><b>Дата рождения:</b>
            ${new Date(contact.birthDate).toLocaleDateString()}
        </p>
    `;

    editBtn.disabled = false;
    deleteBtn.disabled = false;
}

createBtn.onclick = () => openCreateModal();

editBtn.onclick = () => openEditModal();

cancelBtn.onclick = () => closeModal();

deleteBtn.onclick = () => deleteContact();

function openCreateModal() {

    form.reset();

    contactIdInput.value = "";

    document.getElementById("modalTitle").textContent =
        "Создание контакта";

    clearErrors();

    modal.classList.remove("hidden");
}

function openEditModal() {

    if (!selectedContact) return;

    contactIdInput.value = selectedContact.id;

    firstNameInput.value = selectedContact.firstName;
    lastNameInput.value = selectedContact.lastName;
    phoneInput.value = selectedContact.mobilePhone;
    jobTitleInput.value = selectedContact.jobTitle;
    birthDateInput.value = selectedContact.birthDate.split("T")[0];

    document.getElementById("modalTitle").textContent =
        "Редактирование контакта";

    clearErrors();

    modal.classList.remove("hidden");
}

function closeModal() {
    modal.classList.add("hidden");
}

form.addEventListener("submit", async e => {

    e.preventDefault();

    if (!validateForm())
        return;

    const id = contactIdInput.value;

    const dto = getFormData();

    if (id) {

        await fetch(`${API_URL}/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dto)
        });

    } else {

        await fetch(API_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dto)
        });
    }

    closeModal();
    await loadContacts();

    if (selectedContact) {
        selectedContact = contacts.find(x => x.id === id);
        selectContact(selectedContact);
    }
});

async function deleteContact() {

    if (!selectedContact)
        return;

    if (!confirm("Удалить контакт?"))
        return;

    await fetch(`${API_URL}/${selectedContact.id}`, {
        method: "DELETE"
    });

    selectedContact = null;

    info.innerHTML = "Выберите контакт";

    editBtn.disabled = true;
    deleteBtn.disabled = true;

    await loadContacts();
};

function validateForm() {

    clearErrors();

    let valid = true;

    const dto = getFormData();

    if (dto.firstName.length < 2) {
        document.getElementById("firstNameError").textContent =
            "Минимум 2 символа";
        valid = false;
    }

    if (dto.lastName.length < 2) {
        document.getElementById("lastNameError").textContent =
            "Минимум 2 символа";
        valid = false;
    }

    if (!/^\+\d{11,15}$/.test(dto.mobilePhone)) {
        document.getElementById("phoneError").textContent =
            "Формат: +79991234567";
        valid = false;
    }

    if (dto.jobTitle.length < 2) {
        document.getElementById("jobError").textContent =
            "Минимум 2 символа";
        valid = false;
    }

    const age =
        new Date().getFullYear() -
        new Date(dto.birthDate).getFullYear();

    if (age < 6 || age > 120) {
        document.getElementById("birthError").textContent =
            "Возраст должен быть от 6 до 120 лет";
        valid = false;
    }

    return valid;
}

function clearErrors() {

    document.querySelectorAll(".error")
        .forEach(x => x.textContent = "");
}

function getFormData() {
    return {
        firstName: firstNameInput.value.trim(),
        lastName: lastNameInput.value.trim(),
        mobilePhone: phoneInput.value.trim(),
        jobTitle: jobTitleInput.value.trim(),
        birthDate: birthDateInput.value
    };
}