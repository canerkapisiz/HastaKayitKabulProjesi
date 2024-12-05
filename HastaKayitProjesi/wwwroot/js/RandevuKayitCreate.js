document.querySelector("form").addEventListener("submit", function (event) {
        const tcNo=document.getElementById("TcKimlikNo").value.trim();
        const bolumId = document.getElementById("BolumId").value;
        const doktorId = document.getElementById("DoktorId").value;
        const randevuTarihi = document.getElementById("RandevuTarihi").value.trim();
        const randevuSaati = document.getElementById("RandevuSaati").value.trim();

        let errorMessage = "";

        if(tcNo.length != 11 ) errorMessage += "T.C. kimlik Numarası 11 haneli olmalıdır.\n";
        if (!bolumId) errorMessage += "Lütfen bir bölüm seçiniz.\n";
        if (!doktorId) errorMessage += "Lütfen bir doktor seçiniz.\n";
        if (!randevuTarihi) errorMessage += "Lütfen randevu tarihini giriniz.\n";
        if (!randevuSaati) errorMessage += "Lütfen randevu saatini giriniz.\n";

        if (errorMessage) {
            event.preventDefault(); // Formun gönderilmesini engelle
            alert(errorMessage); // Kullanıcıya hata mesajı göster
        }
    });

    
    let tarih = "";
    let formattedDatet = null;
    document.addEventListener("DOMContentLoaded", function () {
        // Sayfa yüklendiğinde bu fonksiyon çalışır.

        const randevuTarihiInput = document.getElementById("RandevuTarihi");
        // Randevu tarihi input alanını seçer.

        const takvimContainer = document.getElementById("takvimContainer");
        // Takvimin yer aldığı konteyneri seçer.

        const takvimBody = document.getElementById("takvimBody");
        // Takvimin gövde kısmını seçer (günlerin oluşturulacağı yer).

        const takvimBaslik = document.getElementById("takvimBaslik");
        // Takvimin başlık kısmını seçer (yıl ve ay bilgisi için).

        const oncekiAyButton = document.getElementById("oncekiAy");
        // "Önceki Ay" butonunu seçer.

        const sonrakiAyButton = document.getElementById("sonrakiAy");
        // "Sonraki Ay" butonunu seçer.

        const takvimAcButton = document.getElementById("takvimAcButton");
        // "Takvim Aç" butonunu seçer.

        

        let currentDate = new Date();
        // Mevcut tarihi alır ve başlangıç değeri olarak kullanır.

        const formatDate = (year, month, day) =>
            `${year}-${String(month).padStart(2, "0")}-${String(day).padStart(2, "0")}`;
        // Tarihi "YYYY-MM-DD" formatında döndüren bir fonksiyon tanımlar.

        function updateCalendarTitle(year, month) {
            takvimBaslik.textContent = `${year} - ${month + 1}`;
            // Takvim başlığını günceller (örneğin: "2024 - 12").
        }

        function renderCalendar(year, month) {
            takvimBody.innerHTML = ""; // Takvim gövdesini temizler.
            updateCalendarTitle(year, month); // Takvim başlığını günceller.

            const firstDayOfMonth = new Date(year, month, 1);
            // Ayın ilk gününü belirler.

            const lastDayOfMonth = new Date(year, month + 1, 0);
            // Ayın son gününü belirler.

            const daysInMonth = lastDayOfMonth.getDate();
            // O ayın kaç gün olduğunu bulur.

            const startDay = firstDayOfMonth.getDay();
            // Ayın ilk gününün haftanın hangi günü olduğunu bulur (0 = Pazar).

            let tr = document.createElement("tr");
            // Yeni bir satır (tr) oluşturur.
            let sayac = 0;
            // Takvim başlangıcındaki boşlukları ekler.
            if (startDay != 6 && startDay != 0) {
                for (let i = 0; i < startDay; i++) {
                    if (i >= 1 && i <= 5) {
                        const td = document.createElement("td");
                        td.textContent = ""; // Boş hücre.
                        tr.appendChild(td); // Satıra ekler.
                        sayac++;
                    }
                }
            }

            // Ayın günlerini oluşturur.
            for (let day = 1; day <= daysInMonth; day++) {
                const currentDate = new Date(year, month, day);
                const currentWeekday = currentDate.getDay();

                if (currentWeekday >= 1 && currentWeekday <= 5) {
                    // Sadece hafta içi (Pazartesi-Cuma) günlerini işler.
                    const td = document.createElement("td");
                    td.textContent = day; // Gün numarasını hücreye yazar.
                    td.style.cursor = "pointer"; // Hücreye tıklanabilirlik özelliği ekler.

                    td.addEventListener("click", function () {
                        
                        // Gün seçildiğinde bu olay çalışır.
                        randevuTarihiInput.value = formatDate(
                            currentDate.getFullYear(),
                            currentDate.getMonth() + 1,
                            currentDate.getDate()
                        );
                        formattedDatet = formatDatett(currentDate.getDate(), currentDate.getMonth() + 1, currentDate.getFullYear());
                        function formatDatett(day, month, year) {
                            // Gün ve ay değerlerini iki haneli yapmak için
                            const formattedDay = day.toString().padStart(2, '0');
                            const formattedMonth = month.toString().padStart(2, '0');
                            return `${year}-${formattedMonth}-${formattedDay}`; // YYYY-MM-DD formatı
                        }
                        document.getElementById("saatAcmaButon").disabled = false;
                        takvimContainer.style.display = "none"; // Takvimi kapatır.
                    });

                    tr.appendChild(td); // Hücreyi satıra ekler.
                    sayac++;
                    if (sayac % 5 === 0 && sayac != 0) {
                        // Eğer bir hafta dolduysa yeni satır başlatır.
                        takvimBody.appendChild(tr);
                        tr = document.createElement("tr");
                    }

                }
            }

            // Kalan günleri ekler.
            if (sayac > 0) {
                takvimBody.appendChild(tr);
            }
        }

        function toggleCalendar() {
            // Takvimi açma/kapatma işlemini yapar.
            if (takvimContainer.style.display === "none") {
                renderCalendar(currentDate.getFullYear(), currentDate.getMonth());
                takvimContainer.style.display = "block";

                const rect = randevuTarihiInput.getBoundingClientRect();
                // Takvim konumunu ayarlar (input'un hemen altına yerleştirir).
                takvimContainer.style.top = `${rect.bottom + window.scrollY}px`;
                takvimContainer.style.left = `${rect.left + window.scrollX}px`;
            } else {
                takvimContainer.style.display = "none"; // Takvimi kapatır.
            }
        }

        oncekiAyButton.addEventListener("click", function () {
            // "Önceki Ay" butonuna tıklandığında bir ay geri gider.
            sayac = 0;
            currentDate.setMonth(currentDate.getMonth() - 1);
            renderCalendar(currentDate.getFullYear(), currentDate.getMonth());
        });

        sonrakiAyButton.addEventListener("click", function () {
            // "Sonraki Ay" butonuna tıklandığında bir ay ileri gider.
            sayac = 0;
            currentDate.setMonth(currentDate.getMonth() + 1);
            renderCalendar(currentDate.getFullYear(), currentDate.getMonth());
        });

        takvimAcButton.addEventListener("click", toggleCalendar);
        // "Takvim Aç" butonuna tıklandığında takvimi açar/kapatır.

        document.addEventListener("click", function (event) {
            // Sayfanın herhangi bir yerine tıklanınca çalışır.
            // Eğer takvim dışında bir yere tıklanırsa takvimi kapatır.
            if (
                !takvimContainer.contains(event.target) &&
                event.target !== takvimAcButton &&
                event.target !== randevuTarihiInput
            ) {
                takvimContainer.style.display = "none";
            }
        });
    });
    

    let doktorId = 9;

    document.getElementById("DoktorId").addEventListener("change", function () {
        doktorId = this.value;
        document.getElementById("takvimAcButton").disabled = false;
    });
document.addEventListener("DOMContentLoaded", function () {
        const saatSecenekleriContainer = document.getElementById("saatSecenekleri");

        const randevuSaatiInput = document.getElementById("RandevuSaati");

        const saatAcmaButon = document.getElementById("saatAcmaButon");

        const zamanlar = [
            ["09:00", "10:00", "11:00", "13:00", "14:00", "15:00", "16:00"],
            ["09:10", "10:10", "11:10", "13:10", "14:10", "15:10", "16:10"],
            ["09:20", "10:20", "11:20", "13:20", "14:20", "15:20", "16:20"],
            ["09:30", "10:30", "11:30", "13:30", "14:30", "15:30", "16:30"],
            ["09:40", "10:40", "11:40", "13:40", "14:40", "15:40", "16:40"],
            ["09:50", "10:50", "11:50", "13:50", "14:50", "15:50", "16:50"],
        ];

        zamanlar.forEach((satir) => {
            const tr = document.createElement("tr");
            satir.forEach((zaman) => {
                const td = document.createElement("td");
                const button = document.createElement("button");
                button.type = "button";
                button.className = "btn btn-outline-secondary btn-sm";
                button.textContent = zaman;


                button.addEventListener("click", function () {
                    randevuSaatiInput.value = zaman;
                    const modal = bootstrap.Modal.getInstance(document.getElementById('saatSecModal'));
                    modal.hide();
                });

                td.appendChild(button);
                tr.appendChild(td);
            });
            saatSecenekleriContainer.appendChild(tr);
        });


        saatAcmaButon.addEventListener("click", function () {
            fetch(`/RandevuKayit/GetRandevuSaatleri?doktorId=${doktorId}&tarih=${formattedDatet}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Veri alınamadı: " + response.statusText);
                    }
                    return response.json();
                })
                .then(data => {
                    if (data && data.length > 0) {
                        // Saatleri döngüyle işleme
                        data.forEach(saat => {
                            // `saatSecenekleriContainer` içinde tüm td'leri al
                            const tdElements = saatSecenekleriContainer.querySelectorAll("td");

                            tdElements.forEach(td => {
                                // Eğer td'nin içeriği gelen saatle eşleşiyorsa
                                if (td.textContent.trim() === saat) {
                                    const button = td.querySelector(".btn-outline-secondary");

                                    if (button) {
                                        // Sınıfı siyah yapmak için değiştirme
                                        button.classList.remove("btn-outline-secondary"); // Eski sınıfı kaldır
                                        button.classList.add("btn-dark"); // Siyah buton sınıfını ekle

                                        // Butonu tıklanamaz hale getirme
                                        button.disabled = true; // Tıklanamaz yapar
                                    }
                                }
                            });
                        });
                    } else {
                        console.log("Veri yok veya boş.");
                    }
                })
                .catch(error => console.error("Hata:", error));
        });
    });
    

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