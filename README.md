Okul Bilgi Sistemi ya da kısaca OBS, Web Tabanlı Programlama dersi için geliştirilmiş, üniversitelerde yönetici ve akademisyenlerin öğrenci süreçlerini uçtan uca yönetmesi amacıyla yapılmış, cyberpunk temalı şık bir ASP.NET Core MVC web uygulamasıdır.

🚀 Proje Amacı

Bu proje, bir üniversite veya okul ortamındaki öğrenci kayıtlarını, akademik kadro yönetimini ve not sistemini dijital ortama taşıyarak web sayfası üzerinden kontrol etmeyi amaçlar. Özellikle görselleştirilmiş veriler (Dashboard) ile sistemin anlık durumunu izlemeyi kolaylaştırır.

🎯 Hedef Kullanıcı Kitlesi

Sistem Yöneticisi (Admin): Tüm sisteme hakim, yeni bölümler, yeni hocalar, duyurular ve yeni öğrenciler ekleyebilen kullanıcı. Öğrencilere not ekleyemez.

Bölüm memuru (Memur): Duyuru-bölüm ve kullanıcı kısımlarına karışamaz. Diğer tüm işlemleri yapabilir. 

Akademisyenler: Kendilerine atanan derslere not girişi yapabilen ve kendi öğrencilerini takip edebilen kullanıcılar. Sistemde herhangi bir değişiklik yapamazlar.

------------------------------------
🛠 Kullanılan Teknolojiler
Dil: C#

Framework: ASP.NET Core MVC 8

ORM: EF Core (Code First)

Veritabanı: MSSQL Server

Frontend: HTML5, CSS3, JavaScript, Chart.js (Grafikler için)

Güvenlik: BCrypt.Net-Next (Şifre Hashleme ve Güvenli Depolama)

Kütüphaneler: FontAwesome (İkonlar için), Google Fonts (Orbitron & Rajdhani)

------------------------------------
📖 Senaryo ve Kullanım Amacı

Uygulama başlatıldığında cyberpunk temalı bir Dashboard ekranı kullanıcıyı karşılar.

Dashboard: Toplam öğrenci, hoca ve ders sayıları dinamik olarak listelenir. Chart.js ile ders başarı ortalamaları grafiksel olarak sunulur.

Öğrenci Yönetimi: Yeni öğrenci kayıtları yapılır, mevcut öğrenciler bölümlerine göre listelenir ve güncellenir.

Akademik Kadro: Hocaların unvanlarına ve bölümlerine göre yönetildiği alandır.

Not Giriş Terminali: Hocalar, sadece kendi derslerinin bağlı olduğu bölümlerdeki öğrencilere not girişi yapabilir. Sistem, ders-bölüm uyumunu otomatik denetler.

Öğrenci Giriş Terminali: Öğrenci sadece kendi id ve şifresini girerek kendisine sistemden verilen notları görebilir. Diğer kısımlara ulaşamaz.

------------------------------------
🎥 Tanıtım Videosu
https://www.youtube.com/watch?v=jHpkXPkxJnQ
