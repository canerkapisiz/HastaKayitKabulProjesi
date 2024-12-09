// Dinamik doktor seçimi
document.getElementById("BolumId").addEventListener("change", function () {
        const bolumId = this.value;
        const doktorSelect = document.getElementById("DoktorId");

        doktorSelect.innerHTML = '<option value="">Doktor Seçiniz...</option>'; // Mevcut doktorları temizle

        if (bolumId) {
            fetch(`/RandevuKayit/GetDoktorlarByBolum?bolumId=${bolumId}`)
                .then(response => response.json())
                .then(data => {
                    data.forEach(doktor => {
                        const option = document.createElement("option");
                        option.value = doktor.id;
                        option.textContent = doktor.adSoyad;
                        doktorSelect.appendChild(option);
                    });
                })
                .catch(error => console.error("Doktorlar yüklenirken hata oluştu:", error));
        }
    });