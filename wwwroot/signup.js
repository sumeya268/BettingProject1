const API_URL = "/api";

document.getElementById("signupForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const username = document.getElementById("signupUsername").value.trim();
    const email = document.getElementById("signupEmail").value.trim();
    const password = document.getElementById("signupPassword").value.trim();

    if (!username || !email || !password) {
        showToast("Please fill in all fields");
        return;
    }

    const payload = { username, email, password };

    try {
        const res = await fetch(`${API_URL}/users/signup`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!res.ok) {
            const msg = await res.text();
            showToast("Signup failed: " + msg);
            return;
        }

        const user = await res.json();

        // Save user locally
        localStorage.setItem("currentUser", JSON.stringify(user));

        showToast("Signup successful!");

        // Redirect after short delay so toast is visible
        setTimeout(() => {
            window.location.href = "index.html";
        }, 800);

    } catch (err) {
        console.error(err);
        showToast("Signup error — check backend connection");
    }
});

function showToast(message) {
    const toast = document.getElementById("toast");
    toast.innerText = message;
    toast.classList.add("show");

    setTimeout(() => {
        toast.classList.remove("show");
    }, 2000);
}
