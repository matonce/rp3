﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

        internal bool handled;

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
            if (e.KeyChar == (char)Keys.Space)
                handled = true;
            else
                handled = false;

            tipkanje.kontrolaZaTipkanje.KeyDownHandler(sender, e);
        }

        internal void prikazNaslovne()
        {
            BackgroundImage = Properties.Resources.Slika;

            naslovna.promijeniVidljivost(true);
        }

        internal void prikazZaVjezbe()
        {
            trenutno = 1;

            BackgroundImage = null;
            BackColor = Color.AntiqueWhite;

            vjezbe.promijeniVidljivost(true);
        }

        internal void prikazStatistike()
        {
            BackgroundImage = null;
            BackColor = Color.AntiqueWhite;

            vidiStatistiku.promijeniVidljivost(true);
        }

        internal void prikazLevela()
        {
            BackgroundImage = null;
            BackColor = Color.Snow;

            leveli.promijeniVidljivost(true);
        }

        internal void prikazSpremljenihVjezbi()
        {
            trenutno = 2;

            BackgroundImage = null;
            BackColor = Color.Snow;
            
            spremljeneVjezbe.promijeniVidljivost(true);
        }

        internal void prikazVjezbiSLevela(int indeksLevela)
        {
            trenutno = 3;

            if (indeksLevela != -1)
                this.indeksLevela = indeksLevela;

            //leveli.promijeniVidljivost(false);
            //tipkanje.promijeniVidljivost(false, "");
            vjezbeSLevela.promijeniVidljivost(true, this.indeksLevela);
        }

        internal void prikazTipkanja(string stringovi, int indeksVjezbe, String ime) // pripremljene vjezbe
        {
            this.indeksVjezbe = indeksVjezbe;

            BackColor = Color.White;

            vjezbeSLevela.promijeniVidljivost(false, -1);
            tipkanje.promijeniVidljivost(true, stringovi, ime);

            KeyPress += PritisnutaTipka;
        }

        internal void prikazTipkanjaZaVjezbe(List<String> stringovi, string ime) // custom vjezbe
        {
            BackColor = Color.White;

            vjezbe.promijeniVidljivost(false);
            tipkanje.promijeniVidljivostZaVjezbe(true, stringovi, ime);

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
                tipkanje.promijeniVidljivostZaVjezbe(false, null, "");
                prikazZaVjezbe();
            }
            else if (trenutno == 2)
            {
                tipkanje.promijeniVidljivostZaVjezbe(false, null, "");
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
                    tipkanje.promijeniVidljivostZaVjezbe(false, null, "");
                    prikazLevela();
                }
                else // prikaz vjezbe nakon ove; njen task je u 's'
                {
                    Console.WriteLine("stringic je " + rez.Item2);
                    MessageBox.Show("Slijedi vježba \"" + rez.Item1 + "\".");
                    prikazTipkanja(rez.Item2, indeksVjezbe + 1, rez.Item1.ToString());
                }
            }
        }
    }
}
