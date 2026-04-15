
/* ---------------------------------------------------
   GLOBAL CONFIG
--------------------------------------------------- */

const API_URL = "/api";

let matches = [];
let selections = [];

/* ---------------------------------------------------
   LOGIN STATE
--------------------------------------------------- */

function isLoggedIn() {
    return localStorage.getItem("currentUser") !== null;
}

function getCurrentUser() {
    return JSON.parse(localStorage.getItem("currentUser"));
}

function saveCurrentUser(user) {
    localStorage.setItem("currentUser", JSON.stringify(user));
}

/* ---------------------------------------------------
   UPDATE HEADER UI
--------------------------------------------------- */

function updateHeaderUI() {
    const authButtons = document.getElementById("authButtons");
    const userPanel = document.getElementById("userPanel");

    if (!authButtons || !userPanel) return;

    if (isLoggedIn()) {
        const user = getCurrentUser();

        authButtons.style.display = "none";
        userPanel.style.display = "flex";

        document.getElementById("usernameDisplay").textContent = user.username;
        document.getElementById("balanceAmount").textContent =
            `£${user.balance.toFixed(2)}`;
    } else {
        authButtons.style.display = "flex";
        userPanel.style.display = "none";
    }
}

/* ---------------------------------------------------
   LOGOUT BUTTON
--------------------------------------------------- */

function initLogoutButton() {
    const btn = document.getElementById("logoutBtn");
    if (!btn) return;

    btn.addEventListener("click", () => {
        localStorage.removeItem("currentUser");
        window.location.href = "login.html";
    });
}

/* ---------------------------------------------------
   TAB NAVIGATION
--------------------------------------------------- */

function initTabNavigation() {
    const buttons = document.querySelectorAll(".nav-btn");
    const sections = document.querySelectorAll(".tab-section");

    buttons.forEach(btn => {
        btn.addEventListener("click", () => {

            buttons.forEach(b => b.classList.remove("active"));
            btn.classList.add("active");

            sections.forEach(sec => sec.style.display = "none");

            const tabId = btn.dataset.tab;
            document.getElementById(tabId).style.display = "block";

            if (tabId === "bettingSection") {
                loadMatchesFromApi("All");
            }

            if (tabId === "myBetsSection" && isLoggedIn()) {
                const user = getCurrentUser();
                loadMyBetsFromApi(user.id);
            }
            if (tabId === "resultsSection") {
            loadResultsFromApi();
            }

        });
    });
}

/* ---------------------------------------------------
   LOAD MATCHES
--------------------------------------------------- */

async function loadMatchesFromApi(filterSport = "All") {
    const container = document.getElementById("matchesContainer");
    if (!container) return;

    const res = await fetch(`${API_URL}/matches`);
    if (!res.ok) {
        container.innerHTML = "Failed to load matches.";
        return;
    }

    matches = await res.json();

    if (filterSport !== "All") {
        matches = matches.filter(m => m.sport === filterSport);
    }

    renderMatches();
}
async function loadResultsFromApi() {
    const container = document.getElementById("resultsList");
    if (!container) return;

    const res = await fetch(`${API_URL}/matches/results`);
    if (!res.ok) {
        container.textContent = "Failed to load results.";
        return;
    }

    const results = await res.json();

    if (results.length === 0) {
        container.textContent = "No results available yet.";
        return;
    }

    container.innerHTML = "";

    results.forEach(r => {
        const div = document.createElement("div");
        div.className = "result-item";

        div.innerHTML = `
            <strong>${r.teams}</strong><br>
            Sport: ${r.sport}<br>
            Status: Finished<br>
            <span class="bet-date">${new Date(r.startTime).toLocaleString()}</span>
        `;

        container.appendChild(div);
    });
}

/* ---------------------------------------------------
   RENDER MATCHES
--------------------------------------------------- */

function renderMatches() {
    const container = document.getElementById("matchesContainer");
    container.innerHTML = "";

    matches.forEach(m => {
        const card = document.createElement("div");
        card.className = "match-card";

        const timeText = new Date(m.startTime).toLocaleString();

        card.innerHTML = `
            <div class="match-sport">${m.sport}</div>
            <div class="match-teams">${m.teams}</div>
            <div class="match-time">${timeText}</div>

            <div class="odds-group">
                <button class="odds-btn">Home ${m.homeOdds}</button>
                <button class="odds-btn">Draw ${m.drawOdds ?? "-"}</button>
                <button class="odds-btn">Away ${m.awayOdds}</button>
            </div>
        `;

        const buttons = card.querySelectorAll(".odds-btn");

        buttons[0].onclick = () => addSelection(m, "Home", m.homeOdds);
        buttons[1].onclick = () => addSelection(m, "Draw", m.drawOdds);
        buttons[2].onclick = () => addSelection(m, "Away", m.awayOdds);

        container.appendChild(card);
    });
}

/* ---------------------------------------------------
   ADD SELECTION
--------------------------------------------------- */

function addSelection(match, pick, odds) {
    if (!isLoggedIn()) {
        document.getElementById("guestWarning").style.display = "block";
        return;
    }

    selections.push({
        matchId: match.id,
        teams: match.teams,
        pick,
        odds: Number(odds)
    });

    renderBetslip();
}

/* ---------------------------------------------------
   REMOVE SELECTION
--------------------------------------------------- */

function removeSelection(index) {
    selections.splice(index, 1);
    renderBetslip();
}

/* ---------------------------------------------------
   RENDER BETSLIP
--------------------------------------------------- */

function renderBetslip() {
    const slip = document.getElementById("betslipList");
    slip.innerHTML = "";

    if (selections.length === 0) {
        slip.innerHTML = "No selections yet. Tap odds to add a bet.";
        updateTotals();
        return;
    }

    selections.forEach((sel, index) => {
        const div = document.createElement("div");
        div.className = "betslip-item";

        div.innerHTML = `
            <strong>${sel.teams}</strong><br>
            ${sel.pick} @ ${sel.odds}
            <button class="remove-btn" onclick="removeSelection(${index})">✖</button>
        `;

        slip.appendChild(div);
    });

    updateTotals();
}

/* ---------------------------------------------------
   UPDATE TOTALS
--------------------------------------------------- */

function updateTotals() {
    let total = 1;

    selections.forEach(s => total *= s.odds);

    document.getElementById("totalOdds").textContent = total.toFixed(2);

    const stake = parseFloat(document.getElementById("stakeInput").value) || 0;
    const potential = stake * total;

    document.getElementById("potentialReturn").textContent = `£${potential.toFixed(2)}`;
}

document.getElementById("stakeInput").addEventListener("input", updateTotals);

/* ---------------------------------------------------
   PLACE BET
--------------------------------------------------- */

function initPlaceBetButton() {
    const btn = document.getElementById("placeBetBtn");
    if (!btn) return;

    btn.addEventListener("click", async () => {
        if (!isLoggedIn()) {
            showToast("You must be logged in");
            return;
        }

        if (selections.length === 0) {
            showToast("Add at least one selection");
            return;
        }

        const stake = parseFloat(document.getElementById("stakeInput").value);
        if (!stake || stake <= 0) {
            showToast("Enter a valid stake");
            return;
        }

        const user = getCurrentUser();

        // ⭐ FINAL CORRECT PAYLOAD ⭐
        const payload = {
            userId: user.id,
            stake,
            selections: selections.map(s => ({
                matchId: s.matchId,
                selectionType: s.pick,
                odds: s.odds
            }))
        };

        const res = await fetch(`${API_URL}/bets`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!res.ok) {
            showToast("Bet failed");
            return;
        }

        const data = await res.json();

        // Update balance
        user.balance = data.newBalance;
        saveCurrentUser(user);
        updateHeaderUI();

        // Clear slip
        selections = [];
        renderBetslip();

        document.getElementById("stakeInput").value = "";
        document.getElementById("totalOdds").textContent = "1.00";
        document.getElementById("potentialReturn").textContent = "£0.00";

        showToast("Bet placed successfully!");

        loadMyBetsFromApi(user.id);
    });
}

/* ---------------------------------------------------
   LOAD USER BETS
--------------------------------------------------- */

async function loadMyBetsFromApi(userId) {
    const container = document.getElementById("myBetsList");
    if (!container) return;

    const res = await fetch(`${API_URL}/bets/my?userId=${userId}`);
    if (!res.ok) {
        container.textContent = "Failed to load bets.";
        return;
    }

    const bets = await res.json();

    if (bets.length === 0) {
        container.textContent = "You have no bets yet.";
        return;
    }

    container.innerHTML = "";

    bets.forEach(b => {
        const div = document.createElement("div");
        div.className = "mybet-item";

        const first = b.lines[0];

        div.innerHTML = `
            <strong>${first.matchName}</strong><br>
            ${first.selectionType} @ ${first.odds}<br>
            Stake: £${b.stake.toFixed(2)}<br>
            Return: £${b.potentialReturn.toFixed(2)}<br>
            <span class="bet-date">${new Date(b.placedAt).toLocaleString()}</span>
        `;

        container.appendChild(div);
    });
}

/* ---------------------------------------------------
   SPORT FILTER
--------------------------------------------------- */

function initSportFilter() {
    const buttons = document.querySelectorAll(".sport-btn");

    buttons.forEach(btn => {
        btn.addEventListener("click", () => {
            buttons.forEach(b => b.classList.remove("active"));
            btn.classList.add("active");

            loadMatchesFromApi(btn.dataset.sport);
        });
    });
}

/* ---------------------------------------------------
   TOAST
--------------------------------------------------- */

function showToast(message) {
    const toast = document.getElementById("toast");
    if (!toast) return;

    toast.innerText = message;
    toast.classList.add("show");

    setTimeout(() => {
        toast.classList.remove("show");
    }, 2000);
}

/* ---------------------------------------------------
   MAIN INITIALISATION
--------------------------------------------------- */

function initMainPage() {
    initTabNavigation();
    initSportFilter();
    initPlaceBetButton();
    initLogoutButton();
    updateHeaderUI();

    loadMatchesFromApi("All");
}

/* ---------------------------------------------------
   RUN ON PAGE LOAD
--------------------------------------------------- */

document.addEventListener("DOMContentLoaded", () => {
    initMainPage();
});
