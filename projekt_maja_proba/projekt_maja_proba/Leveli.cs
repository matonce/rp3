﻿using System;
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
            Location = new Point(143, 110),
            Name = "flowLayoutPanel1",
            Size = new Size(793, 357),
            TabIndex = 11,
            Visible = false
        };

        Button buttonNatragNaNaslovnu = new Button()
        {
            BackColor = Color.SeaShell,
            Font = new Font("Microsoft JhengHei", 7F),
            Location = new Point(143, 12),
            Name = "buttonNatragNaNaslovnu",
            Size = new Size(100, 40),
            TabIndex = 10,
            Text = "naslovna",
            UseVisualStyleBackColor = false,
            Visible = false
        };

        public Leveli(Form form)
        {
            this.form = form;

            flowLayoutPanel1.Parent = form;
            buttonNatragNaNaslovnu.Parent = form;

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
                        Font = new Font("Microsoft JhengHei", 12F),
                        ForeColor = Color.Snow,
                        Name = "buttonLevel" + (i + 1).ToString(),
                        Size = new Size(105, 105),
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
            flowLayoutPanel1.Visible = buttonNatragNaNaslovnu.Visible = value;
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
