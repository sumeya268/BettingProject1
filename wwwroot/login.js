const API_URL = "/api";

document.getElementById("loginForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const username = document.getElementById("loginUsername").value.trim();
    const password = document.getElementById("loginPassword").value.trim();

    if (!username || !password) {
        showToast("Please enter both username and password");
        return;
    }

    const payload = { username, password };

    try {
        const res = await fetch(`${API_URL}/users/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!res.ok) {
            const msg = await res.text();
            showToast("Login failed: " + msg);
            return;
        }

        const user = await res.json();

        // Save user locally for later pages (place bet, my bets)
        localStorage.setItem("currentUser", JSON.stringify(user));

        showToast("Login successful!");

        // Redirect after short delay so toast is visible
        setTimeout(() => {
            window.location.href = "index.html";
        }, 800);

    } catch (err) {
        console.error(err);
        showToast("Login error — check backend connection");
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
