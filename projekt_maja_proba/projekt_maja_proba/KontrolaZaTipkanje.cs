using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;

namespace projekt_maja_proba
{
    public partial class KontrolaZaTipkanje : UserControl
    {
        public Form form;
        public int counter;
        string trenutniString;
        Label trenutnaTipka = null;
        Label trenutnoSlovo = null;
        String ime_vjezbe;


        string[] nizStringova;
        List<String> stringovi = new List<String>();
        int trenutniIndeks;

        int greska, tocno;

        //int handled = 0;

        Stopwatch stopwatch = new Stopwatch();

        public KontrolaZaTipkanje(Form form)
        {
            InitializeComponent();

            Location = new Point(100,50);
            Visible = false;

            this.form = form;
        }
        
        internal void pokreniOdbrojavanje(string[] nizStringova, string ime)
        {
            ime_vjezbe = ime;
            this.nizStringova = nizStringova;
            trenutniString = nizStringova[0];
            trenutniIndeks = 0;

            //handled = 1;

            counter = 5;
            greska = tocno = 0;

            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);

            timer1.Interval = 300;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                timer1.Stop();
                label1.Text = ""; // u label1 mi je onaj brojac
                stopwatch.Restart();
                radi();
            }
            else
            {
                label1.Text = counter.ToString();
                counter--;
            }
        }

        private void radi()
        {
            prikaziString(); // ova fja ubacuje slova kao labele (svako slovo jedna labela, da mogu underlineati lakse...)

            form.KeyPreview = true; // ovo je valjda potrebno da mogu pozvati fju KeyDownHandler
            //KeyPress += new KeyPressEventHandler(KeyDownHandler);

            nakonObradenePritisnuteTipke(); // ovdje se sve događa (i to se poziva odmah pri isteku, pa ce se onda inicijalizirati one currentButton 
                                         // i currentLabel labele i uvjet u KeyDownHandler ce biti ok (manje bitno))
        }

        private void prikaziString()
        {
            int duljinaStringa = trenutniString.Length;
            for (int i = 0; i < duljinaStringa; ++i)
            {
                Label novoSlovo = new Label()
                {
                    Text = trenutniString[i].ToString(),
                    Font = new Font("Yu Gothic", 35, FontStyle.Regular),
                    BackColor = Color.Snow,
                    Parent = flowLayoutPanel1,
                    Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right),
                    Size = new Size(70,70),
                    AutoSize = true
                };
            }
        }

        private void nakonObradenePritisnuteTipke()
        {
            if (flowLayoutPanel1.GetNextControl(trenutnoSlovo, true) == null) // ako onog slova koje se sljedece treba promatrtati nema, onda je ovaj task gotov
            {
                osvjeziPrikaz();
            }
            else
            {
                podcrtajSljedeceSlovo();
                if (checkBox1.Checked == true)
                    obojiTipku();
                else
                    pronadiSljedecuTipku();
            }
        }

        private void osvjeziPrikaz() // ovdje samo sve vracam na defaultno, tj. da nema podcrtanog i obojanog, i da se ne reagira na stiskanje tipki
        {
            očistiPrikaz();

            trenutniIndeks++;
            //Console.WriteLine("trenutniIndeks je " + trenutniIndeks + ", a nizStringova je dulj " + nizStringova);
            if (trenutniIndeks < nizStringova.Length)
            {
                trenutniString = nizStringova[trenutniIndeks];
                radi();
            }
            else // ispis i spremanje statistike
            {
                stopwatch.Stop();

                // računanje podataka
                double preciznost = Math.Round((double)tocno / (greska + tocno) * 100, 2);
                double vrijeme = stopwatch.ElapsedMilliseconds / 1000; // u s
                double brzina = Math.Round(((double)tocno / 5) / ((double)stopwatch.ElapsedMilliseconds / 60000), 2);
                bool uvjet = false;

                if (form.radSBazom.postojiLiIme(ime_vjezbe) == 1) uvjet = form.imaLiUvjeta();

                form.radSBazom.dodajRezultat(ime_vjezbe, brzina, preciznost);

                if (uvjet == true && (preciznost < 80 || brzina < 3))
                {
                    // srediti ovo
                    MessageBox.Show("Nisu zadovoljeni uvjeti od 80% preciznosti i 3 wpm.");
                    form.onemoguciReagiranjeNaTipke();
                }
                else
                {
                    // osvježavanje podataka i ispis
                    String s = "";
                    int stariPodaci = form.obnoviStatističkePodatke(brzina, preciznost);

                    if (stariPodaci == 1)
                        s = "Novi rekord!\n\n";

                    MessageBox.Show(s + "Preciznost: " + preciznost + " %.\n"
                        + "Vrijeme: " + vrijeme + " s.\n"
                        + "Brzina: " + brzina + " wpm."); // words per minute, rijec se standradno valjda uzima kao da ima 5 slova, a u 'tocno' mi se nalazi broj slova u vjezbi
                                                          // dijelim sa 60000 jer ms pretvaram u min

                    Console.WriteLine("ovdje pozivam onu fju u form");
                    form.prikazSljedeceg();
                }
            }
        }

        private void otvaranjeSljedeceVjezbe()
        {
            throw new NotImplementedException();
        }

        public void očistiPrikaz()
        {
            flowLayoutPanel1.Controls.Clear();

            //trenutnoSlovo.Font = new Font("Yu Gothic", 35, FontStyle.Regular);

            if (trenutnaTipka != null)
            {
                trenutnaTipka.BackColor = Color.MistyRose;
                trenutnaTipka = trenutnoSlovo = null;
            }
        }

        internal void očisti()
        {
            očistiPrikaz();
            greska = tocno = 0;
        }

        private void podcrtajSljedeceSlovo()
        {
            if (trenutnoSlovo != null) // znaci da je na redu neprvo slovo u tasku, pa onda ono koje je vec podcrtano prije njega moras vratiti na defaultno
            {
                trenutnoSlovo.Font = new Font("Yu Gothic", 35, FontStyle.Regular);
            }

            trenutnoSlovo = (Label)flowLayoutPanel1.GetNextControl(trenutnoSlovo, true); // sada se pomakni na sljedecu tipku
            //Console.WriteLine("trenutno slovo je: " + trenutnoSlovo.Text);
            trenutnoSlovo.Font = new Font("Yu Gothic", 35, FontStyle.Underline); // i podcrtaj ju
        }

        private void pronadiSljedecuTipku() // pretrazi medu svim onim tipkama koje odgovara slovu koje se treba stisnuti
        {
            foreach (var tableLayoutPanel in tableLayoutPanel1.Controls.OfType<TableLayoutPanel>())
                foreach (var label in tableLayoutPanel.Controls.OfType<Label>())
                    if (label.Text == trenutnoSlovo.Text || label.Text == "space" && trenutnoSlovo.Text == " ")
                    {
                        trenutnaTipka = label;
                        return;
                    }
        }

        private void obojiTipku()
        {

            if (flowLayoutPanel1.GetNextControl(trenutnoSlovo, false) != null) // ovaj parametar false znaci da idemo ulijevo, pa onda ovdje provjeravam ima li slova prije ovog u tasku, i ako ima, vrati ga na defaultno (sad vidim da je ovo malo redundantno jer opet provjeravam isto)
            {
                trenutnaTipka.BackColor = Color.MistyRose;
            }

            pronadiSljedecuTipku();

            if (checkBox1.Checked == true)
                trenutnaTipka.BackColor = Color.Red;
        }

        public void KeyDownHandler(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine("stisnuto je " + e.KeyChar.ToString() + ", a trazena tipka je " + trenutnoSlovo.Text);

            if (e.KeyChar.ToString() == trenutnoSlovo.Text)
            {
                form.radSBazom.povecaj(trenutnoSlovo.Text, "Tocno");
                tocno++;
            }
            else
            {
                form.radSBazom.povecaj(trenutnoSlovo.Text, "Netocno");
                greska++;
            }

            // jesu li isti pritisnuto slovo na fizickoj tipkovnici i slovo koje je na redu, 
            // ili ne moraju biti isti ako je oznacen onaj drugi checkbox
            if (e.KeyChar.ToString() == trenutnoSlovo.Text || checkBox2.Checked == true)
            {
                nakonObradenePritisnuteTipke();
            }
        }

        private void KontrolaZaTipkanje_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (trenutnaTipka != null)
            {
                if (checkBox1.Checked == false)
                    trenutnaTipka.BackColor = Color.MistyRose;
                else
                    trenutnaTipka.BackColor = Color.Red;
            }
        }
    }
}
