async function searchTraffic() {
    const line = document.getElementById("lineInput").value.trim();
    const list = document.getElementById("trafficList");
    const updateText = document.getElementById("lastUpdate");

    if (!line) {
        list.innerHTML = "<li>Bitte eine Linie eingeben.</li>";
        if (updateText) updateText.textContent = "";
        return;
    }

    list.innerHTML = "<li>Lade...</li>";

    try {
        const res = await fetch(`/api/traffic/search?line=${encodeURIComponent(line)}`);
        const data = await res.json();

        // Absicherung: Ist die Antwort wirklich ein Array?
        if (!Array.isArray(data)) {
            list.innerHTML = "<li>Unerwartete Daten vom Server.</li>";
            console.error("Erwartetes Array, erhalten:", data);
            return;
        }

        list.innerHTML = "";

        if (data.length === 0) {
            list.innerHTML = "<li>Keine Daten gefunden.</li>";
            return;
        }

        data.forEach(item => {
            const li = document.createElement("li");
            li.textContent = `${item.line} von ${item.from} nach ${item.to} – ${item.waitingTimeMinutes} Minuten Wartezeit`;
            list.appendChild(li);
        });

        if (updateText) {
            updateText.textContent = "Letzte Aktualisierung: " + new Date().toLocaleTimeString();
        }

    } catch (err) {
        list.innerHTML = "<li>Fehler beim Laden der Daten.</li>";
        console.error("Fehler bei API-Aufruf:", err);
    }
}

// Initialer Aufruf bei Seite laden (wenn Eingabe vorhanden)
window.addEventListener("DOMContentLoaded", () => {
    const line = document.getElementById("lineInput").value.trim();
    if (line) {
        searchTraffic();
    }
});

// Automatische Aktualisierung alle 60 Sekunden
setInterval(() => {
    const line = document.getElementById("lineInput").value.trim();
    if (line) {
        searchTraffic();
    }
}, 60000);




