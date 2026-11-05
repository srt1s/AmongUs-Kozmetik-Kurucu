# Among Us Kozmetik Menusu Otomatik Kurucu

Among Us (Epic Games ve Steam) oyununda tum kostum, sapka, pet, vizor ve isim plakalarini kullanabilmenizi saglayan otomatik kurulum ve mod yonetim araci.

---

## Ozellikler

- Otomatik Dizin Tespiti: Epic Games Launcher manifestlerini ve Steam Library dizinlerini otomatik olarak tarar, Among Us'in kurulu oldugu yeri kullaniciya gerek kalmadan bulur.
- Cift Mimari Destegi: Hem 64-bit (x64) hem de 32-bit (x86) Among Us surumlerini destekler ve dogru BepInEx / Doorstop paketini yukler.
- Grafiksel ve Komut Satiri Arayuzleri: C# Windows Forms ile hazirlanmis grafik arayuzlu `Kurucu.exe` ve alternatif konsol scripti `Kurulum.bat`.
- Oyun Ici Entegrasyon: Klavyeden `DELETE` tusuna basarak acilan mod menusu uzerinden calisir.
- Tum Kozmetikler Acik: Oyun icindeki tum skinler, petler, sapkalar, vizorler ve ozel arkaplanlar lobide kullanilabilir hale gelir.

---

## Dosya Yapisi

- `Kurucu.exe`: Grafiksel kurulum programi (Windows Forms).
- `Kurucu.cs`: Kurulum programinin C# kaynak kodu.
- `Kurulum.bat`: Konsol uzerinden hizlica kurulum yapabilen toplu islem dosyasi.
- `NASIL_KULLANILIR.txt`: Hizli adim adim rehber.
- `Dosyalar/`:
  - `x64/`: 64-bit BepInEx, .NET Core runtime ve MalumMenu bilesenleri.
  - `x86/`: 32-bit BepInEx, .NET Core runtime ve MalumMenu bilesenleri.

---

## Kurulum Talimatlari

### Yontem 1: Grafik Arayuz ile Kurulum (Onerilen)

1. Klasor icindeki `Kurucu.exe` dosyasini calistirin.
2. Program Among Us oyun dizinini otomatik olarak bulacaktir.
   - Eger oyun ozel bir dizine kuruluysa "Gozat" butonu ile `Among Us.exe` dosyasinin bulundugu ana klasoru secin.
3. "KURULUMU YAP VE TUM KOZMETIKLERI AC" butonuna tiklayin.
4. Dosyalar birkac saniye icerisinde oyun dizinine kopyalanacaktir.

### Yontem 2: Toplu Islem Dosyasi ile Kurulum

1. `Kurulum.bat` dosyasina cift tiklayin.
2. Otomatik tarama ile oyun dizini bulunup dosyalar ilgili klasore yerlestirilecektir.

---

## Oyun Ici Kullanim

1. Among Us oyununu Epic Games veya Steam uzerinden normal sekilde baslatin.
2. Oyun ana ekrani acildiginda klavyenizden `DELETE` (veya `Del`) tusuna basin.
3. Acilan pencerede "Tum Kozmetikleri Ac" seceneginin etkin oldugundan emin olun.
4. Bir lobiye girip gardroptan istediginiz kostum, pet, sapka veya vizoru ucretsizce secip kullanabilirsiniz.

---

## Kaldirma (Uninstall)

Modu oyundan tamamen kaldirmak icin Among Us kurulum dizinine gidip su dosya ve klasorleri silmeniz yeterlidir:
- `BepInEx` klasoru
- `dotnet` klasoru
- `winhttp.dll`
- `doorstop_config.ini`
- `.doorstop_version`

---

## Yasal Uyari

Bu proje yalnizca egitim ve kisisel inceleme amaciyla olusturulmustur. Innersloth LLC ile hicbir resmi baglantisi bulunmamaktadir.
