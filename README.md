# 🌌 Okul Bilgi Sistemi (OBS)

<p align="center">
  <img src="https://img.shields.io/badge/Framework-ASP.NET%20Core%20MVC%208-blueviolet" alt="Framework">
  <img src="https://img.shields.io/badge/Database-MSSQL%20Server-red" alt="Database">
</p>

**Geliştirici:** Yasin Deniz Cüre  
**Öğrenci No:** 132230035

---

## 🚀 Proje Hakkında

**Okul Bilgi Sistemi (OBS)**, Web Tabanlı Programlama dersi için geliştirilmiş; üniversitelerde yönetici ve akademisyenlerin öğrenci süreçlerini uçtan uca yönetmesi amacıyla tasarlanmış, **cyberpunk temalı** şık bir ASP.NET Core MVC web uygulamasıdır.

Bu proje, akademik süreçleri dijital ortama taşıyarak merkezi bir kontrol mekanizması sunar. Özellikle görselleştirilmiş veriler (**Dashboard**) ile sistemin anlık durumunu izlemeyi kolaylaştırır.

---

## 🎯 Hedef Kullanıcı Kitlesi ve Yetkiler

| Rol | Yetki Açıklaması |
| :--- | :--- |
| **🕹️ Sistem Yöneticisi** | Tüm sisteme hakimdir. Bölüm, hoca, duyuru ve öğrenci ekleyebilir. (Not girişi yapamaz). |
| **📂 Bölüm Memuru** | Operasyonel süreçleri yönetir. Duyuru, bölüm ve kullanıcı yönetimi hariç tüm işlemleri yapabilir. |
| **🎓 Akademisyen** | Sadece kendine atanan derslere not girişi yapabilir ve öğrencilerini takip edebilir. |
| **👤 Öğrenci** | Sadece kendi notlarını görüntüleyebilir; sistemin diğer kısımlarına erişimi yoktur. |

---

## 🛠 Kullanılan Teknolojiler

### **Backend & Veritabanı**
* **Dil:** C#
* **Framework:** ASP.NET Core MVC 8
* **ORM:** EF Core (Code First)
* **Veritabanı:** MSSQL Server
* **Güvenlik:** BCrypt.Net-Next (Şifre Hashleme)

### **Frontend**
* **Tasarım:** HTML5, CSS3, JavaScript
* **Grafikler:** Chart.js (Başarı ortalamaları ve istatistikler)
* **İkon & Font:** FontAwesome, Google Fonts (Orbitron & Rajdhani)

---

## 📖 Senaryo ve Kullanım Amacı

* **Dashboard:** Toplam öğrenci, hoca ve ders sayıları dinamik olarak listelenir. Chart.js ile ders başarı ortalamaları grafiksel olarak sunulur.
* **Öğrenci Yönetimi:** Yeni kayıt, bölüme göre listeleme ve güncelleme işlemleri yapılır.
* **Akademik Kadro:** Hocaların unvan ve bölümlerine göre yönetildiği modüldür.
* **Not Giriş Terminali:** Hocalar, sistemin otomatik ders-bölüm denetimi altında sadece yetkili oldukları öğrencilere not girişi yapar.
* **Öğrenci Giriş Terminali:** Öğrencilerin kendi kimlik bilgileriyle sadece kişisel notlarını görebildiği paneldir.

---

## 🎥 Tanıtım Videosu

Projenin çalışma şeklini ve arayüz detaylarını aşağıdaki linkten izleyebilirsiniz:

[▶️ Proje Tanıtımını İzle](https://www.youtube.com/watch?v=jHpkXPkxJnQ)

---
<p align="center">© 2024 Yasin Deniz Cüre - Web Tabanlı Programlama Projesi</p>
