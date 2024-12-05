// Formun gönderilmesini engellemek ve T.C. Kimlik Numarası kontrolü yapmak için submit olayını dinliyoruz
document.querySelector("form").addEventListener("submit", function (event) {
    // T.C. Kimlik Numarası input elemanından değeri alıyoruz ve boşlukları siliyoruz
    const tcKimlikNo = document.getElementById("TcKimlikNo").value.trim();

    // Hata mesajı göstereceğimiz HTML elemanını seçiyoruz
    const hataMesaji = document.getElementById("hatamesaji");

    // Eğer T.C. Kimlik Numarası 11 karakter uzunluğunda değilse
    if (tcKimlikNo.length !== 11) {
        // Formun gönderilmesini engelliyoruz
        event.preventDefault();
        
        // Hata mesajını gösteriyoruz
        hataMesaji.style.display = "block";
    }
    else {
        // T.C. Kimlik Numarası doğru uzunlukta ise hata mesajını gizliyoruz
        hataMesaji.style.display = "none";
    }
});