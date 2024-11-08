using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Oyunu
{
    public partial class Form1 : Form
    {
        // Oyuncular X ve O olarak tanımlanır
        public enum oyuncu
        {
            X, O
        }

        oyuncu gunceloyuncu; // Şuanki oyuncu 
        Random random = new Random(); // Bilgisayarın rastgele hareketi için random komudu
        int oyuncuskoru = 0; // Oyuncu Skoru
        int pcskoru = 0; // Bilgisayar Skoru
        List<Button> butonlar; // Ekranda butonları tutan liste




        public Form1()
        {
            InitializeComponent(); // Formu başlatır
            yenidenbaslat(); // Oyunun başlatır
        }

        // Bilgisayarın Hareketleri
        private void pchareketi(object sender, EventArgs e)
        {
            // Bilgisayarın yapacağı en iyi hamleyi bulmak için kontrol 
            Button hamle = KazanmaHamlesiBul("O") ?? KazanmaHamlesiBul("X") ?? OrtaHamleyiBul() ?? RastgeleHamle();

            // Hamle bulununca
            if (hamle != null)
            {
                hamle.Enabled = false;
                gunceloyuncu = oyuncu.O;
                hamle.Text = gunceloyuncu.ToString();
                hamle.BackColor = Color.Red;
                butonlar.Remove(hamle);
                oyunkontrol();
                zamanlayıcı.Stop();
            }
        }

        // Kazanma veya engelleme hamlesini bulmak için
        private Button KazanmaHamlesiBul(string oyuncu)
        {
            // Olası kazanç dizilerini kontrol 
            List<Button[]> kazananDiziler = new List<Button[]>
    {
        new Button[] { button1, button2, button3 },
        new Button[] { button4, button5, button6 },
        new Button[] { button7, button8, button9 },
        new Button[] { button1, button4, button7 },
        new Button[] { button2, button5, button8 },
        new Button[] { button3, button6, button9 },
        new Button[] { button1, button5, button9 },
        new Button[] { button3, button5, button7 }
    };

            // Her kazanan diziyi kontrol eder, iki hücrede aynı oyuncu varsa ve üçüncü hücre boşsa oraya hamle yapılır
            foreach (var dizi in kazananDiziler)
            {
                if (dizi.Count(b => b.Text == oyuncu) == 2 && dizi.Any(b => b.Text == ""))
                {
                    return dizi.First(b => b.Text == "");
                }
            }
            return null;
        }

        // Orta kareyi tercih eder
        private Button OrtaHamleyiBul()
        {
            if (button5.Text == "")
            {
                return button5;
            }
            return null;
        }

        // Eğer stratejik hamle yoksa rastgele bir hamle yapar
        private Button RastgeleHamle()
        {
            if (butonlar.Count > 0)
            {
                int index = random.Next(butonlar.Count);
                return butonlar[index];
            }
            return null;
        }

        // Oyuncu hareketi
        private void oyuncuhareketi(object sender, EventArgs e)
        {
            var button = (Button)sender; // Tıklanan butonu al
            gunceloyuncu = oyuncu.X; // Oyuncunun hamlesi X olacak
            button.Text = gunceloyuncu.ToString(); // Butona X yaz
            button.Enabled = false; // Butonu devre dışı bırakır
            button.BackColor = Color.Green; // Buton rengini yeşil yapar
            butonlar.Remove(button); // Butonu listeden çıkarır
            oyunkontrol(); // Oyun kontrolü yapılır
            zamanlayıcı.Start(); // Zamanlayıcıyı başlatıt




        }

        // Yeniden başlat butonuna tıklanması
        private void yenidenbaslat(object sender, EventArgs e)
        {
            yenidenbaslat();
        }


        private void oyunkontrol()

            // Kazanma Kontrolü
        {
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
               || button4.Text == "X" && button4.Text == "X" && button6.Text == "X"
               || button7.Text == "X" && button8.Text == "X" && button9.Text == "X"
               || button1.Text == "X" && button4.Text == "X" && button7.Text == "X"
               || button2.Text == "X" && button5.Text == "X" && button8.Text == "X"
               || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
               || button1.Text == "X" && button5.Text == "X" && button9.Text == "X"
               || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                zamanlayıcı.Stop(); // Zamanlayıcıyı durdur
                MessageBox.Show("Oyuncu Kazandı"); // Oyuncunun kazandığını bildirir
                oyuncuskoru++; // Oyuncunun skorunu artırır
                label1.Text = "Oyuncu Skoru: " + oyuncuskoru; // Skoru güncelle
                yenidenbaslat(); // Oyunu yeniden başlatır

            }
            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
               || button4.Text == "O" && button4.Text == "O" && button6.Text == "O"
               || button7.Text == "O" && button8.Text == "O" && button9.Text == "O"
               || button1.Text == "O" && button4.Text == "O" && button7.Text == "O"
               || button2.Text == "O" && button5.Text == "O" && button8.Text == "O"
               || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
               || button1.Text == "O" && button5.Text == "O" && button9.Text == "O"
               || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                zamanlayıcı.Stop(); // Zamanlayıcıyı durdurur
                MessageBox.Show("Bilgisayar Kazandı"); // Bilgisayarın kazandığını bildirir
                pcskoru++; // Bilgisayarın skorunu artırır
                label2.Text = "Bilgisayar Skoru: " + pcskoru; // Skoru güncelle
                yenidenbaslat(); // Oyunu yeniden başlatır
            }

            // Beraberlik durumu
            if (butonlar.All(b => b.Text != " "))
            {
                zamanlayıcı.Stop();
                MessageBox.Show("Beraberlik!");
                yenidenbaslat();
            }
        }

        
        // Oyunu yeniden başlatır
        private void yenidenbaslat()
        {
            butonlar = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            // Tüm butonları sıfırlar 
            foreach (Button x in butonlar)
            {
                x.Enabled = true;
                x.Text = " ";
                x.BackColor = DefaultBackColor;
            }

        }
    }
}
