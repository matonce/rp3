using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba
{
    class Leveli
    {
        public Form form;
        int indeksLevela;

        FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel()
        {
            Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right),
            Location = new Point(150, 160),
            Name = "flowLayoutPanel1",
            Size = new Size(670, 357),
            TabIndex = 11,
            Visible = false
        };
        
        Button buttonNatragNaNaslovnu = new Button()
        {
            BackColor = Color.SeaShell,
            Font = new Font("Microsoft JhengHei", 7F),
            Location = new Point(50, 12),
            Name = "buttonNatragNaNaslovnu",
            Size = new Size(100, 40),
            TabIndex = 10,
            Text = "naslovna",
            UseVisualStyleBackColor = false,
            Visible = false
        };

        Label level = new Label()
        {
            Font = new Font("Courier New", 35F),
            ForeColor = Color.RosyBrown,
            // Location = new Point(473, 12),
            Name = "level",
            Text = "Leveli",
            Size = new Size(200, 60),
            Visible = false
        };

        public Leveli(Form form)
        {
            this.form = form;

            flowLayoutPanel1.Parent = form;
            buttonNatragNaNaslovnu.Parent = form;
            level.Parent = form;

            level.Location = new Point(form.ClientSize.Width / 2 - 100, 12);

            buttonNatragNaNaslovnu.BringToFront();
            buttonNatragNaNaslovnu.Click += new EventHandler(buttonNatragNaNaslovnu_Click);
        }

        private void buttonNatragNaNaslovnu_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);
            form.prikazNaslovne();
        }

        public void promijeniVidljivost(bool value)
        {
            if (value)
            {
                List<Tuple<string, int>> naziviLevela = form.radSBazom.popisLevelaIzBaze();

                for (int i = 0; i < naziviLevela.Count; ++i)
                {
                    Button button = new Button()
                    {
                        BackColor = Color.RosyBrown,
                        Font = new Font("Courier New", 12F),
                        ForeColor = Color.Snow,
                        Name = "buttonLevel" + (i + 1).ToString(),
                        Size = new Size(205, 205),
                        TabIndex = i,
                        Text = naziviLevela.ElementAt(i).Item1,
                        UseVisualStyleBackColor = false
                    };

                    if (naziviLevela.ElementAt(i).Item2 == 0)
                        button.Enabled = false;

                    flowLayoutPanel1.Controls.Add(button);
                    button.Click += new EventHandler(levelButton_Click);
                }
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                //naziviLevela.Clear();
            }
            level.Visible = flowLayoutPanel1.Visible = buttonNatragNaNaslovnu.Visible = value;
            Console.WriteLine("promijenjena vidljivost od levela na " + value);
        }

        private void levelButton_Click(object sender, EventArgs e)
        {
            indeksLevela = ((Button)sender).TabIndex + 1;

            promijeniVidljivost(false);
            form.prikazVjezbiSLevela(indeksLevela);
        }
    }
}
