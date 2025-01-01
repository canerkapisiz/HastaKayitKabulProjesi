# Hasta Kayit Kabul Projesi
* .NET Core ve SQL ile geliştirdiğim Hasta Kayıt ve Kabul Sistemi, hastaneye yeni bölümler eklenmesine ve bu bölümlere doktor atanmasına olanak tanır. Hastalar, TC Kimlik Numarasını birincil anahtar olarak kullanarak sisteme kaydedilebilir. Bölüm ve doktor seçimiyle tarih belirlenerek randevu oluşturulabilir. Randevular günlük ya da tüm kayıtlar olarak sorgulanabilir ve gerektiğinde iptal edilebilir.


# Proje Görselleri ve Tanıtımı
## Günlük Doktor Randevuları Listeleme
* Her bölümün günlük randevuları bu ekrandan kolayca görüntülenebilir.
  
Günlük Doktor Randevuları Listeleme | Günlük Doktor Randevuları Listeleme |
------------ |------------ |
![](Photos/1.png) | ![](Photos/2.png) |

## Bölüm Listeleme ve Ekleme
* Hastaneye yeni bölümler eklenebilir, ancak mevcut bölümler yeniden eklenemez.
* Hastanedeki mevcut bölümler listelenebilir.

Bölüm Listeleme ve Ekleme | Bölüm Listeleme ve Ekleme | Bölüm Listeleme ve Ekleme | 
------------ |------------ | ------------ |
![](Photos/3.png) | ![](Photos/4.png) | ![](Photos/5.png) |

## Doktor Listeleme ve Ekleme
* Hasteneye bölüm seçilerek doktor eklenebilir.
* Her bölümün doktorları kendi aralarında gruplanarak listelenebilir.

Doktor Listeleme ve Ekleme | Doktor Listeleme ve Ekleme | Doktor Listeleme ve Ekleme | 
------------ |------------ | ------------ |
![](Photos/6.png) | ![](Photos/7.png) | ![](Photos/8.png) |

## Hasta Listeleme ve Ekleme
* Hastalardan "TC Kimlik No, Ad Soyad, Telefon Numarası ve Adres" bilgileri alınır. Aynı TC Kimlik Numarasına sahip bir hasta yeniden kaydedilemez.
* Hastalar, bilgileriyle birlikte listelenebilir.
  
Hasta Listeleme ve Ekleme | Hasta Listeleme ve Ekleme | Hasta Listeleme ve Ekleme | 
------------ |------------ | ------------ |
![](Photos/9.png) | ![](Photos/10.png) | ![](Photos/11.png) |

## Randevu İşlemleri 
* Hasta, TC Kimlik Numarasını girip bölüm ve doktor seçimi yaparak, tarih ve saat belirterek randevu oluşturabilir.
* Aynı hasta, aynı doktora aynı gün içinde randevu alamaz.
* Bir doktor için belirlenen tarih ve saatte, başka bir hasta randevu oluşturamaz.

Randevu Ekleme | Randevu Ekleme | Randevu Ekleme | 
------------ |------------ | ------------ |
![](Photos/12.png) | ![](Photos/13.png) | ![](Photos/14.png) |
Randevu Silme | Randevu Sorgulama | 
![](Photos/15.png) | ![](Photos/16.png) |

## Randevu İşlemleri 2
* Bölüm ve doktor bilgisine göre, doktorun o günkü hastaları listelenebilir.
* Bölüm ve doktor seçilerek, o gün için randevu oluşturulabilir.

Doktorun Günlük Randevu Listeleme | Doktorun Günlük Randevu Listeleme | Doktora Hasta Kaydı Açma | Doktora Hasta Kaydı Açma | 
------------ |------------ | ------------ |------------ |
![](Photos/17.png) | ![](Photos/18.png) | ![](Photos/19.png) |![](Photos/20.png) |



# Database Görselleri
Bölüm Tablosu | Doktor Tablosu | Hasta Tablosu | Randevu Kayit Tablosu | 
------------ |------------ | ------------ |------------ |
![](Photos/21.png) | ![](Photos/22.png) | ![](Photos/23.png) |![](Photos/24.png) |



