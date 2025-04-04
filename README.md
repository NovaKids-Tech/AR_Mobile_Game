# AR Balon Patlatma Oyunu

Bu proje, Unity ile geliştirilmiş, AR (Artırılmış Gerçeklik) teknolojisini kullanan eğitici bir matematik oyunudur. Oyuncular, gerçek dünya üzerinde beliren balonları patlatarak matematik becerilerini geliştirebilirler.

## Oynanış

1. Oyun başladığında bir bonus turla karşılaşırsınız. Bu turda tüm balonları patlatarak puanınızı artırabilirsiniz.
2. Bonus turdan sonra, ekranda matematik soruları görünmeye başlar (örn. "5 + 3 = ?").
3. Sorunun cevabını içeren balonu bulup patlatmanız gerekir.
4. Doğru balonu patlatırsanız 10 puan kazanırsınız.
5. Yanlış balonu patlatırsanız 1 can kaybedersiniz.
6. Balonlar belirli bir süre içinde patlatılmazsa 1 can kaybedersiniz.
7. Canınız sıfıra düştüğünde oyun sona erer.

## Sistem Özellikleri

- Rastgele matematik soruları üretme (toplama, çıkarma, çarpma, bölme)
- AR kamera ile gerçek dünyada balonları görme ve vurma
- Farklı renklerde balonlar
- Puan sistemi ve can mekanizması
- Bonus tur sistemi
- Yüksek skor takibi

## Proje Yapısı

Bu projede aşağıdaki temel script'ler bulunmaktadır:

### `GameManager.cs`
Oyunun temel durumunu (skor, can, oyun sonu) yöneten ana script. UI güncellemelerini ve yüksek skor takibini de gerçekleştirir.

### `MathProblemGenerator.cs`
Rastgele matematik soruları üreten ve ekranda gösteren script. Dört işlem (toplama, çıkarma, çarpma, bölme) desteklenmektedir.

### `SpawnScript.cs`
Balonları belirli aralıklarla ekranda oluşturan script. Bonus tur yönetimi ve balon setlerinin oluşturulmasından sorumludur.

### `BalloonScript.cs`
Her bir balonun davranışını kontrol eden script. Balonun yukarı doğru hareketi, patlatıldığında gerçekleşecek olaylar ve cevap kontrolü gibi işlevleri içerir.

### `BalloonAnswerManager.cs`
Balonlarda gösterilen cevapları yöneten script. Doğru cevabı rastgele bir balona atarken, diğer balonlara yanlış cevaplar atanmasını sağlar.

### `ShootScript.cs`
Oyuncunun dokunma/tıklama işlemlerini algılayıp balonları vurmayı sağlayan script. AR kamera üzerinden ışın gönderip balonları tespit eder.

## Kurulum

1. Projeyi bilgisayarınıza indirin.
2. Unity Hub üzerinden projeyi açın (Unity 2020.3 veya daha yeni bir sürüm önerilir).
3. AR Foundation ve AR Core/AR Kit paketlerinin kurulu olduğundan emin olun.
4. Projeyi bir Android veya iOS cihaza derleyin.
5. Cihazınızın AR desteğine sahip olduğundan emin olun.

## Oyun Ayarları

Bazı oyun parametrelerini `SpawnScript` ve diğer script'lerdeki değişkenleri düzenleyerek ayarlayabilirsiniz:

- `spawnInterval`: Balonların oluşturulma sıklığı (saniye cinsinden)
- `spawnWidth`: Balonların yatay düzlemde dağılım genişliği
- `balloonsPerSet`: Her sette oluşturulacak balon sayısı
- `moveSpeed`: Balonların yukarı hareket hızı

## Geliştirme Notları

- Oyun AR Foundation kullanılarak geliştirilmiştir.
- TextMeshPro kullanılarak metin görüntüleme yapılmaktadır.
- Balonlar 3D modellerdir ve BoxCollider bileşeni ile çarpışma algılaması yapılır.
- Tüm skor ve can bilgileri PlayerPrefs ile cihaza kaydedilir.

## Gelecek Geliştirmeler

- Farklı zorluk seviyeleri
- Çeşitli matematik işlemlerinin seçilebilmesi
- Ses efektleri ve müzik
- Görsel efektlerin iyileştirilmesi



---

__Firma:__ NovaKids.Tech  
__İletişim:__ NovaKids.Tech@gmail.com