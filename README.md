
# DERSHANE-ETUT
.NET MVC de C# diliyle yazılmış DAL mimarisi kullanılmadan Entity Framework DatabaseFirst ile geliştirilmiş bir dershane etüt projesi.

Öğrenci etüt almak istediği branşı seçtikten sonra o branşa ait öğretmenler listelenir. Öğretmeni seçtikten sonra "Etüt Ara" butonuna basar ve öğretmene ait etüt saatleri listelenir. Öğrenci önünde listelenen 7 günden bir saat seçer ve "Etüt Al" butonuna basar. Seçilen öğretmende önceden alınan etütler dinamik olarak seçilemez halde gelir. Alınmak istenen etütler adminin önüne pasif olarak düşer ve admin etütü onayladıktan sonra alınan etüt öğretmende görünür olur.


Roller :











## Öğrenci

- İstediği öğretmenden etüt alabilme
- Almış olduğu etütleri görüntüleme.


## Öğretmen

- Kendinden alınmış etütleri görüntüleme.


## Admin

- Öğrencinin almış aldığı etütü aktif etme.
- Kayıtlı tüm etütleri görüntüleme ve silme.
- Tüm öğrenci ve öğretmenleri görünütüleme, silme ve güncelleme işlemi.






  
## Giriş 

![girisEkrani](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/204b135d-6466-42b3-b5e0-81ad638b597d)

  Dropdownlistte seçilen duruma göre "Kayıt Ol" tuşuna bastığımız zaman hangi rol seçilmişse ona ait Kayıt Ekranı gelir.

 Öğretmen

  ![ogretmenKayit](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/d37efb8a-7ce6-4898-b020-dff38509698a)

Öğrenci

  ![ogrenciKayit](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/ca043349-21e7-4b27-906c-e10dc5ae387a)

Burada gerekli validasyonlar yapılır.

TC kimlik numarası 11 haneli ve son basamağı çift olmak zorundadır. Girilen TC veri tabanında kayıtlıysa "TC kullanımda" hatası verir.

Telefon numarası içinde 05 veya 5 ile başlama zorunluluğu vardır ve 05** *** **** gibi kayıt edilmek zorundadır.

E posta içinde "@hotmail, @gmail, @msn " ile kayıt yapılmak zorundadır. Girilen E posta veri tabanında kayıtlıysa "E posta kullanımda" hatası verir.

Şifre içinde en az bir küçük harf, en az bir büyük harf, en az bir rakam, en az bir özel karakter ve en az 8 karakter olması gerekmektedir. 


## Öğrenci Ekranı 

Anasayfa da görüntülenecek bir şey yok.

![ogrenciAnasayfa](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/da889176-4b3c-4663-92b6-329e386a0913)

Etüt Ara sayfasında öğrenci önce branşı seçiyor ve veri tabanından o branşa ait öğretmenler listeleniyor. Sonra "Etüt Ara" butonuna basıyor.

![ogrenciDersSecimEkrani](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/5bc48965-ead0-443e-8c1e-049c54db5fa2)

Burada gördüğünüz gibi 16 Mayıs Salı gününde saat 10:00 alınamaz durumda. Öğrencimiz 12:00 ı seçiyor ve Etüt Al butonuna basıyor ve eütün onaylandığına dair bir uyarı geliyor.

![ogrenciEtutAl](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/60be49b8-8c72-4efd-8157-30fc8221d5db)

Burada da öğrencinin aldığı etütler listeleniyor ve aldığı etütün admin tarafından onaylanıp onaylanmadığını kontrol ediyor.

![ogrenciEtutGoster](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/fda7bf53-d8a9-430a-b945-af568b3198fc)

## Admin Ekranı

Burada dinamik olarak öğrenci ve öğretmen sayısı geliyor.

Onay bekleyen etütler burada listeleniyor.

![adminAnasayfasi](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/87ed9f16-a4bd-40a9-b374-9a37fe84041b)

Admin etütü onayladıktan sonra ekranı kaplayan bir mesaj geliyor.

![adminOnaylananEtut](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/2c903bf9-6cb5-4946-a27f-87107de277f6)


Onaylandıktan sonra Etütler sayfasına geliyor bu etüt.

![adminGuncelEtutler](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/cadd51cb-4334-49a1-a4d6-1e1d2441cd6a)

Düzenle kısmına geldiğimiz de klasik CRUD işlemleri karşımıza çıkıyor. 

![adminDuzenle](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/e5fd46e5-2467-4b6f-a53e-65eac5a89e0b)

Ekstra olarak bir arama butonu ekledim.

![adminArama](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/f3399e52-e67e-47d3-a1db-05dfa728cf7e)

Son olarakta Düzenle işlevini göstereyim. Buradan kayıt olan öğrencinin kayıt durumunu aktif yapabiliyoruz.

![adminOgrenciDuzenle](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/6b56eb65-c666-4b88-8253-52aac1cb788b)

## Öğretmen Ekranı

Admin tarafından o öğretmene ait onaylanan etütler burada listeleniyor.

![ogretmenAnasayfa](https://github.com/itegrisu/DERSHANE-ETUT/assets/104738076/c918d446-ecf9-4037-8157-612987d9673a)


İlk MVC projem olduğu için biraz yalın kaldı fakat kendime geliştirdikçe daha iyi projelere imza atacağımı adım gibi biliyorum. İyi Günler Herkese. :)

## Geri Bildirim

Herhangi bir geri bildiriminiz varsa, lütfen ank_ahmet@msn.com adresinden bana ulaşıp eleştirilerinizi yapabilirsiniz.

  
