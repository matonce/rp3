using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba
{
    class Vjezbe
    {
        Form form;
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        int br_rijeci, br_rijeci_gen, br_slova;
        List<CheckBox> listaCheckBoxova = new List<CheckBox>();
        String[] abeceda = { "a", "b", "c", "č", "ć", "d", "đ", "e", "f", "g", "h", "i", "j", "k", "l", "j", "m", "n", "o", "p", "r", "s", "š", "t", "u", "v", "z", "ž", "x", "y", "w", "q" };

        NumericUpDown numBrojRijeci = new NumericUpDown()
        {
            Location = new Point(825, 150),
            Maximum = new decimal(new int[] {50,0,0,0}),
            Minimum = new decimal(new int[] {1, 0, 0, 0 }),
            Name = "num_broj_rijeci",
            Size = new Size(78, 20),
            TabIndex = 0,
            Value = new decimal(new int[] { 10, 0, 0, 0 }),
            Visible = false
        };

        Label brojRijeci = new Label()
        {
            AutoSize = true,
            Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(510, 150),
            Name = "broj_riječi",
            Size = new Size(128, 20),
            TabIndex = 1,
            Text = "Broj riječi u vježbi:",
            Visible = false
        };

        Label imeVjezbe = new Label()
        {
            AutoSize = true,
            Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(50, 440),
            Name = "broj_riječi",
            Size = new Size(128, 20),
            Text = "Ime vježbe:",
            Visible = false
        };

        TextBox textBox = new TextBox()
        {
            AutoSize = true,
            Font = new Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(170, 440),
            Name = "ime_tb",
            Size = new Size(128, 20),
            Text = "",
            Visible = false
        };

        NumericUpDown numBrojRijeci_gen = new NumericUpDown()
        {
            Location = new Point(275, 380),
            Maximum = new decimal(new int[] { 50, 0, 0, 0 }),
            Minimum = new decimal(new int[] { 1, 0, 0, 0 }),
            Name = "num_broj_rijeci",
            Size = new Size(78, 20),
            TabIndex = 0,
            Value = new decimal(new int[] { 10, 0, 0, 0 }),
            Visible = false
        };

        Label brojRijeci_gen = new Label()
        {
            AutoSize = true,
            Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(50, 380),
            Name = "broj_riječi",
            Size = new Size(128, 20),
            TabIndex = 1,
            Text = "Broj riječi u vježbi:",
            Visible = false
        };

        NumericUpDown numBrojSlova_gen = new NumericUpDown()
        {
            Location = new Point(285, 410),
            Maximum = new decimal(new int[] { 15, 0, 0, 0 }),
            Minimum = new decimal(new int[] { 1, 0, 0, 0 }),
            Name = "num_broj_rijeci",
            Size = new Size(78, 20),
            TabIndex = 0,
            Value = new decimal(new int[] { 5, 0, 0, 0 }),
            Visible = false
        };

        Label brojSlova_gen = new Label()
        {
            AutoSize = true,
            Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(50, 410),
            Name = "broj_riječi",
            Size = new Size(128, 20),
            TabIndex = 1,
            Text = "Broj slova u riječima:",
            Visible = false
        };

        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
        {
            Anchor = (((AnchorStyles.Left | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right),
            Location = new Point(50, 130),
            Name = "flowLayoutPanel1",
            Size = new Size(500, 250),
            TabIndex = 11,
            Visible = false
        };

        Button novaIgraButton = new Button()
        {
            AutoSize = true,
            BackColor = Color.BlanchedAlmond,
            Font = new Font("Courier New", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(510, 81),
            Name = "nova_igra",
            Size = new Size(186, 24),
            TabIndex = 2,
            Text = "Započni vježbu sa stvarnim riječima",
            Visible = false
        };

        Button pregledSpremljenih = new Button()
        {
            AutoSize = true,
            BackColor = Color.BlanchedAlmond,
            Font = new Font("Courier New", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(510, 260),
            Name = "pregled",
            Size = new Size(186, 24),
            TabIndex = 2,
            Text = "Pregled spremljenih vježbi",
            Visible = false
        };

        Button spremiVjezbu = new Button()
        {
            AutoSize = true,
            BackColor = Color.BlanchedAlmond,
            Font = new Font("Courier New", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(50, 480),
            Name = "nova_igra",
            Size = new Size(186, 24),
            TabIndex = 2,
            Text = "Spremi generiranu vježbu",
            Visible = false
        };

        Button generirajButton = new Button()
        {
            AutoSize = true,
            BackColor = Color.BlanchedAlmond,
            Font = new Font("Courier New", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
            Location = new Point(50, 81),
            Name = "generiraj_vjezbu",
            Size = new Size(186, 24),
            TabIndex = 3,
            Text = "Generiraj vježbu sa označenim slovima",
            Visible = false
        };

        Button buttonNatragNaNaslovnu = new Button()
        {
            BackColor = Color.SeaShell,
            Font = new Font("Courier New", 7F),
            Location = new Point(50, 12),
            Name = "buttonNatragNaNaslovnu",
            Size = new Size(100, 40),
            TabIndex = 3,
            Text = "naslovna",
            UseVisualStyleBackColor = false,
            Visible = false
        };

        public Vjezbe(Form form)
        {
            this.form = form;

            numBrojRijeci.Parent = form;
            brojRijeci.Parent = form;
            novaIgraButton.Parent = form;
            buttonNatragNaNaslovnu.Parent = form;
            generirajButton.Parent = form;
            flowLayoutPanel.Parent = form;
            brojRijeci_gen.Parent = form;
            numBrojRijeci_gen.Parent = form;
            numBrojSlova_gen.Parent = form;
            brojSlova_gen.Parent = form;
            textBox.Parent = form;
            imeVjezbe.Parent = form;
            spremiVjezbu.Parent = form;
            pregledSpremljenih.Parent = form; 

            buttonNatragNaNaslovnu.Click += new EventHandler(buttonNatragNaNaslovnu_Click);
            novaIgraButton.Click += new EventHandler(novaIgraButton_Click);
            generirajButton.Click += new EventHandler(generirajButton_Click);
            spremiVjezbu.Click += new EventHandler(spremiVjezbu_Click);
            pregledSpremljenih.Click += new EventHandler(pregledSpremljenih_Click);
        }

        private void novaIgraButton_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);
            br_rijeci = (int) numBrojRijeci.Value;
            List<String> stringovi = new List<String>();

            stringovi = form.radSBazom.odaberiRijeci(br_rijeci);
            for(int i = 0; i < stringovi.Count; i++) Console.WriteLine(stringovi[i]);
            form.prikazTipkanjaZaVjezbe(stringovi, "stvarne riječi");      
        }

        private void buttonNatragNaNaslovnu_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);
            form.prikazNaslovne();
        }

        private void generirajButton_Click(object sender, EventArgs e)
        {
            List<String> slova = dohvatiOznacenaSlova();

            if (slova.Count > 0)
            {
                promijeniVidljivost(false);
                br_rijeci_gen = (int)numBrojRijeci_gen.Value;
                br_slova = (int)numBrojSlova_gen.Value;

                List<String> stringovi = new List<String>();
                stringovi = generirajVjezbu(br_rijeci_gen, br_slova, slova);

                for (int i = 0; i < stringovi.Count; i++) Console.WriteLine(stringovi[i]);
                form.prikazTipkanjaZaVjezbe(stringovi, "custom");
            }
            else
                MessageBox.Show("Označi barem jedno slovo!", "Upozorenje");
        }

        public List<String> generirajVjezbu(int broj_rijeci, int broj_slova, List<String> listaSlova)
        {
            List<String> ret = new List<String>();
            for(int i = 0; i < broj_rijeci; i++)
            {
                String rijec = "";
                for(int j = 0; j < broj_slova; j++)
                {
                    int rand = RandomNumber(0, listaSlova.Count);
                    rijec += listaSlova.ElementAt(rand);
                }
                ret.Add(rijec);
            }
            return ret;
        }

        public List<String> dohvatiOznacenaSlova()
        {
            List<String> ret = new List<String>();
            foreach (var checkBox in listaCheckBoxova)
            {
                if (checkBox.Checked) ret.Add(checkBox.Name);
            }
            return ret;
        }

        public void promijeniVidljivost(bool value)
        {
            if (value)
            {
                for (int i = 0; i < abeceda.Length; ++i)
                {
                    CheckBox slovo = new CheckBox()
                    {
                        BackColor = Color.RosyBrown,
                        Font = new Font("Courier New", 12F),
                        ForeColor = Color.Snow,
                        Name = abeceda.ElementAt(i),
                        Text = abeceda.ElementAt(i),
                        UseVisualStyleBackColor = false
                    };

                    flowLayoutPanel.Controls.Add(slovo);
                    listaCheckBoxova.Add(slovo);
                    textBox.Text = "";
                }
            }
            else
            {
                flowLayoutPanel.Controls.Clear();
            }

            flowLayoutPanel.Visible = generirajButton.Visible = brojRijeci.Visible = novaIgraButton.Visible = numBrojRijeci.Visible = buttonNatragNaNaslovnu.Visible = value;
            numBrojRijeci_gen.Visible = brojRijeci_gen.Visible = value;
            numBrojSlova_gen.Visible = brojSlova_gen.Visible = value;
            imeVjezbe.Visible = textBox.Visible = spremiVjezbu.Visible = pregledSpremljenih.Visible = value;
        }

        public static int RandomNumber(int min, int max)
        {
            lock (syncLock) return random.Next(min, max);
        }

        public void spremiVjezbu_Click (object sender, EventArgs e)
        {
            Console.WriteLine("usao u click");
            String ime, slova = "";
            int br_rijeci, br_slova;
            if (textBox.Text == "")
            {
                MessageBox.Show("Potrebno je unijeti ime vježbe, ako ju želite spremiti!", "Upozorenje");
                return;
            }
            ime = textBox.Text.ToString();
            br_rijeci = (int)numBrojRijeci_gen.Value;
            br_slova = (int)numBrojSlova_gen.Value;

            List<String> lista_slova = dohvatiOznacenaSlova();
            for(int i = 0; i < lista_slova.Count; i++)
            {
                slova += lista_slova.ElementAt(i);
                if (i != lista_slova.Count - 1) slova += "|";
            }
            if (slova == "")
            {
                MessageBox.Show("Morate odabrati barem jedno slovo");
                return;
            }
            form.dodajVjezbu(ime, br_slova, br_rijeci, slova);
        }

        public void pregledSpremljenih_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);
            form.prikazSpremljenihVjezbi();
        }
    }
}
