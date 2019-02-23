using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba
{
    public partial class Form : System.Windows.Forms.Form
    {
        Naslovna naslovna;
        Leveli leveli;
        internal Vjezbe vjezbe;
        VjezbeSLevela vjezbeSLevela;
        Tipkanje tipkanje;
        SpremljeneVjezbe spremljeneVjezbe;
        VidiStatistiku vidiStatistiku;

        public RadSBazom radSBazom = new RadSBazom();
        int indeksVjezbe;
        int indeksLevela;

        int trenutno = 0;

        public Form()
        {
            InitializeComponent();

            //KeyPress += new KeyPressEventHandler(PritisnutaTipka);

            Controls.Clear();
            naslovna = new Naslovna(this);
            leveli = new Leveli(this);
            vjezbe = new Vjezbe(this);
            vjezbeSLevela = new VjezbeSLevela(this);
            tipkanje = new Tipkanje(this, 0);
            spremljeneVjezbe = new SpremljeneVjezbe(this);
            vidiStatistiku = new VidiStatistiku(this);

            naslov.Location = new Point((ClientSize.Width - naslov.Size.Width) / 2, ClientSize.Height/5);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }
        
        private void PritisnutaTipka(object sender, KeyPressEventArgs e)
        {
            tipkanje.kontrolaZaTipkanje.KeyDownHandler(sender, e);
        }

        internal void prikazNaslovne()
        {
            BackgroundImage = ((Image)(Properties.Resources.ResourceManager.GetObject("$this.BackgroundImage")));
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.White;

            naslovna.promijeniVidljivost(true);
        }

        internal void prikazZaVjezbe()
        {
            BackgroundImage = null;
            BackColor = Color.BlanchedAlmond;

            vjezbe.promijeniVidljivost(true);
        }

        internal void prikazStatistike()
        {
            BackgroundImage = null;
            BackColor = Color.BlanchedAlmond;

            vidiStatistiku.promijeniVidljivost(true);
        }

        internal void prikazLevela()
        {
            BackgroundImage = null;
            BackColor = Color.White;

            leveli.promijeniVidljivost(true);
        }

        internal void prikazSpremljenihVjezbi()
        {
            trenutno = 1;

            BackgroundImage = null;
            BackColor = Color.White;
            
            spremljeneVjezbe.promijeniVidljivost(true);
        }

        internal void prikazVjezbiSLevela(int indeksLevela)
        {
            trenutno = 2;

            if (indeksLevela != -1)
                this.indeksLevela = indeksLevela;

            //leveli.promijeniVidljivost(false);
            //tipkanje.promijeniVidljivost(false, "");
            vjezbeSLevela.promijeniVidljivost(true, this.indeksLevela);
        }

        internal void prikazTipkanja(string stringovi, int indeksVjezbe) // pripremljene vjezbe
        {
            this.indeksVjezbe = indeksVjezbe;

            BackColor = Color.White;

            vjezbeSLevela.promijeniVidljivost(false, -1);
            tipkanje.promijeniVidljivost(true, stringovi);

            KeyPress += PritisnutaTipka;
        }

        internal void prikazTipkanjaZaVjezbe(List<String> stringovi) // custom vjezbe
        {
            BackColor = Color.White;

            vjezbe.promijeniVidljivost(false);
            tipkanje.promijeniVidljivostZaVjezbe(true, stringovi);

            KeyPress += PritisnutaTipka;
        }

        internal void onemoguciReagiranjeNaTipke()
        {
            KeyPress -= PritisnutaTipka;
        }

        private void Form_Load(object sender, EventArgs e)
        {

        }

        internal int obnoviStatističkePodatke(double brzina, double preciznost)
        {
            return radSBazom.obnoviPodatke(indeksVjezbe, brzina, preciznost);
        }

        internal bool imaLiUvjeta()
        {
            Console.WriteLine("usao u form");
            return radSBazom.imaLiUvjeta(indeksVjezbe);
        }

        internal void dodajVjezbu(String ime, int br_slova, int br_rijeci, String slova)
        {
            radSBazom.dodajVjezbu(ime, br_slova, br_rijeci, slova);
        }

        internal void prikazSljedeceg() // pripremljene vj
        {
            onemoguciReagiranjeNaTipke();

            if (trenutno == 1)
            {
                tipkanje.promijeniVidljivostZaVjezbe(false, null);
                prikazSpremljenihVjezbi();
            }
            else
            {
                Console.WriteLine("tu sam, prikazujem levele ili sljed vj");
                Tuple<String, String> rez = radSBazom.otkljucajNovuVjezbu(indeksVjezbe, indeksLevela);

                if (rez.Item1 == "") // ovo je bila posljednja vjezba u trenutnom levelu - odi na popis levela
                {
                    if (rez.Item2 != "")
                        MessageBox.Show("Otključan je novi level!");
                    else
                        MessageBox.Show("Svi leveli su prijeđeni!");
                    tipkanje.promijeniVidljivostZaVjezbe(false, null);
                    prikazLevela();
                }
                else // prikaz vjezbe nakon ove; njen task je u 's'
                {
                    Console.WriteLine("stringic je " + rez.Item2);
                    MessageBox.Show("Slijedi vježba \"" + rez.Item1 + "\".");
                    prikazTipkanja(rez.Item2, indeksVjezbe + 1);
                }
            }
        }
    }
}
