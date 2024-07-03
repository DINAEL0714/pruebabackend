function fetchRents() {
    const startDate = document.getElementById('startDate').value;
    const endDate = document.getElementById('endDate').value;
    let url = '/api/rents';
    
    if (startDate && endDate) {
        url = `/api/rents/bydate?startDate=${startDate}&endDate=${endDate}`;
    }

    fetch(url)
        .then(response => response.json())
        .then(data => {
            const tbody = document.getElementById('rentsTable').getElementsByTagName('tbody')[0];
            tbody.innerHTML = '';
            data.forEach(rent => {
                const row = tbody.insertRow();
                row.insertCell(0).innerText = rent.idcard;
                row.insertCell(1).innerText = rent.name;
                row.insertCell(2).innerText = new Date(rent.date).toLocaleDateString();
                row.insertCell(3).innerText = rent.time;
                row.insertCell(4).innerText = rent.balance;
                row.insertCell(5).innerText = rent.plate;
                row.insertCell(6).innerText = rent.brand;
            });
        })
        .catch(error => console.error('Error:', error));
}

function fetchStats() {
    fetch(`/api/rents/stats`)
        .then(response => response.json())
        .then(data => {
            const dailyRentsDiv = document.getElementById('dailyRents');
            dailyRentsDiv.innerHTML = '';
            data.DailyRents.forEach(stat => {
                dailyRentsDiv.innerHTML += `<p>${stat.Date}: ${stat.Count} alquileres</p>`;
            });

            const monthlyRentsDiv = document.getElementById('monthlyRents');
            monthlyRentsDiv.innerHTML = '';
            data.MonthlyRents.forEach(stat => {
                monthlyRentsDiv.innerHTML += `<p>${stat.Year}-${stat.Month}: ${stat.Count} alquileres</p>`;
            });
        })
        .catch(error => console.error('Error:', error));
}

setInterval(fetchStats, 60000); // Actualiza las estadísticas cada minuto
fetchStats(); // Llama inmediatamente para cargar las estadísticas iniciales
document.addEventListener("DOMContentLoaded", fetchRents); // Cargar datos iniciales
