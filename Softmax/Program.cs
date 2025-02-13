using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public class Mahalle
    {
        public string Ad { get; set; }
        public int nufusYogunlugu { get; set; }
        public int mevcutUlasimAltyapisi { get; set; }
        public int maliyetAnalizi { get; set; }
        public int cevreselEtki { get; set; }
        public int sosyalFayda { get; set; }

        public Mahalle(string ad, int nufusYogunlugu, int mevcutUlasimAltyapisi, int maliyetAnalizi, int cevreselEtki, int sosyalFayda)
        {
            Ad = ad;
            this.nufusYogunlugu = nufusYogunlugu;
            this.mevcutUlasimAltyapisi = mevcutUlasimAltyapisi;
            this.maliyetAnalizi = maliyetAnalizi;
            this.cevreselEtki = cevreselEtki;
            this.sosyalFayda = sosyalFayda;
        }
    }
    static void Main()
    {
        // Mahalle örneklerinin oluşturulması
        var mahalleA = new Mahalle("Karakaş Mahallesi", 9, 6, 5, 7, 8);
        var mahalleB = new Mahalle("Karacaibrahim Mahallesi", 7, 8, 6, 6, 7);
        var mahalleC = new Mahalle("İstasyon Mahallesi", 8, 5, 7, 8, 9);

        var mahalleler = new List<Mahalle> { mahalleA, mahalleB, mahalleC };

        // Kriter önem skorları [Nüfus, Mevcut Ulaşım, Maliyet, Çevresel, Sosyal]
        double[] criteriaImportance = { 9, 7, 8, 6, 9 };

        // Softmax ile ağırlıkların hesaplanması
        double[] expScores = criteriaImportance.Select(score => Math.Exp(score)).ToArray();
        double sumExpScores = expScores.Sum();
        double[] weights = expScores.Select(score => score / sumExpScores).ToArray();

        // Hesaplamalar
        var sonuclar = new Dictionary<string, double>();
        var maliyetFayda = new Dictionary<string, double>();

        foreach (var mahalle in mahalleler)
        {
            double[] skorlar = { mahalle.nufusYogunlugu, mahalle.mevcutUlasimAltyapisi,
                                 mahalle.maliyetAnalizi, mahalle.cevreselEtki, mahalle.sosyalFayda };
            double toplamSkor = skorlar.Zip(weights, (s, w) => s * w).Sum();
            sonuclar[mahalle.Ad] = toplamSkor;

            // Maliyet-fayda analizi
            double fayda = mahalle.nufusYogunlugu + mahalle.mevcutUlasimAltyapisi +
                           mahalle.cevreselEtki + mahalle.sosyalFayda;
            double gercekMaliyet = 10 - mahalle.maliyetAnalizi;
            maliyetFayda[mahalle.Ad] = gercekMaliyet != 0 ? fayda / gercekMaliyet : 0;
        }

        // sonucların yazdırılması
        Console.WriteLine("Kriter Ağırlıkları (Softmax):");
        string[] criteria = { "Nüfus Yoğunluğu", "Mevcut Ulaşım Altyapısı",
                             "Maliyet Analizi", "Çevresel Etki", "Sosyal Fayda" };
        for (int i = 0; i < criteria.Length; i++)
        {
            Console.WriteLine($"{criteria[i]}: {weights[i]:F4}");
        }

        Console.WriteLine("\nToplam Skorlar:");
        foreach (var sonuc in sonuclar)
        {
            Console.WriteLine($"{sonuc.Key}: {sonuc.Value:F4}");
        }

        var enIyi = sonuclar.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        Console.WriteLine($"\nOptimal Mahalle: {enIyi} (Skor: {sonuclar[enIyi]:F4})");

        Console.WriteLine("\nMaliyet-Fayda Oranları:");
        foreach (var mf in maliyetFayda)
        {
            Console.WriteLine($"{mf.Key}: {mf.Value:F2} : 1");
        }
    }
}