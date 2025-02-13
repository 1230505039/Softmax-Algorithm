# Softmax-Algorithm
**Kodlar Softmax klasörü adı altındaki Program.cs dosyasındadır. /Softmax/Program.cs**

📊 **Softmax Algoritmasının Projedeki Rolü**
Bu projede, Kırklareli'nin üç mahallesi için toplu taşıma hattı optimizasyonunu gerçekleştirirken **Softmax algoritması**, kriterlerin ağırlıklarını belirlemek için kullanılmıştır. Softmax, farklı önem derecelerine sahip kriterleri normalize ederek tutarlı bir karar verme süreci sağlar.

**Nasıl Çalışır?**  
1. **Kriter Önem Skorları**:  
   Projede 5 kritere (nüfus yoğunluğu, mevcut ulaşım altyapısı, maliyet analizi, çevresel etki, sosyal fayda) verilen önem skorları (`[9, 7, 8, 6, 9]`) başlangıç değerleri olarak belirlenir.  

2. **Softmax ile Ağırlık Hesaplama**:  
   - Her skorun üstel değeri alınır:  
     exp_score = e^skor
   - Tüm üstel değerler toplanır:  
     sum_exp = ∑ exp_score
   - Her kriterin ağırlığı, üstel değerin toplama oranı olarak hesaplanır:  
     ağırlık = exp_score / sum_exp 

   Bu işlem sonucunda ağırlıklar `[0.39, 0.05, 0.14, 0.02, 0.39]` olarak belirlenir.  

3. **Ağırlıkların Anlamı**:  
   - Softmax, kriterlerin **nispi önemini** yansıtır.  
   - Yüksek skorlu kriterler (nüfus yoğunluğu ve sosyal fayda) daha yüksek ağırlık alır.  
   - Tüm ağırlıkların toplamı **1** olur, böylece tutarlı bir optimizasyon sağlanır.  

4. **Mahalle Skorlaması**:  
   Her mahallenin kriter puanları, Softmax ağırlıklarıyla çarpılarak toplam skor hesaplanır:  
   toplam_skor = ∑ (kriter_puanı × ağırlık)  

**Neden Softmax?**  
- **Objektiflik**: Kriter önemlerini matematiksel olarak normalize eder.  
- **Esneklik**: Yeni kriterler eklenirse bile ağırlıklar dinamik olarak güncellenebilir.  
- **Optimizasyon**: Maliyet-fayda analiziyle birlikte en verimli güzergah seçimini kolaylaştırır.                                                     
