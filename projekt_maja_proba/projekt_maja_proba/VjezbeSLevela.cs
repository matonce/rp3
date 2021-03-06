﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba
{
    class VjezbeSLevela
    {
        public Form form;

        List<Tuple<int, string, string, int, string>> indeksiVjezbi = new List<Tuple<int, string, string, int, string>>();

        FlowLayoutPanel flowLayoutPanel2 = new FlowLayoutPanel()
        {
            Anchor = (((AnchorStyles.Left | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right),
            Location = new Point(143, 110),
            Name = "flowLayoutPanel1",
            Size = new Size(670, 357),
            TabIndex = 11,
            Visible = false
        };

        Button buttonNatrag = new Button()
        {
            BackColor = Color.MistyRose,
            Font = new Font("Microsoft JhengHei", 7F),
            Location = new Point(50, 12),
            Name = "buttonNatrag",
            Size = new Size(100, 40),
            TabIndex = 10,
            Text = "natrag",
            UseVisualStyleBackColor = false,
            Visible = false
        };

        Label vjezbe = new Label()
        {
            Font = new Font("Courier New", 35F),
            ForeColor = Color.RosyBrown,
            // Location = new Point(473, 12),
            Name = "vjezbe",
            Text = "Vježbe",
            Size = new Size(200, 60),
            Visible = false
        };

        public VjezbeSLevela(Form form)
        {
            this.form = form;

            flowLayoutPanel2.Parent = form;
            buttonNatrag.Parent = form;
            buttonNatrag.Click += new EventHandler(buttonNatrag_Click);
            
            vjezbe.Parent = form;
            vjezbe.Location = new Point(form.ClientSize.Width / 2 - 100, 12);
        }

        private void buttonNatrag_Click(object sender, EventArgs e)
        {
            if (form.handled == false)
            {
                promijeniVidljivost(false, -1);
                form.prikazLevela();
            }
        }

        public void promijeniVidljivost(bool value, int indeksLevela)
        {
            if (value)
            {
                indeksiVjezbi = form.radSBazom.popisVjezbiSLevelaIzBaze(indeksLevela);

                for (int i = 0; i < indeksiVjezbi.Count; ++i)
                {
                    ProsireniButton prošireniButton = new ProsireniButton();
                    prošireniButton.Size = new Size(105, 105);

                    prošireniButton.button1.Name = indeksiVjezbi.ElementAt(i).Item3;
                    prošireniButton.button1.TabIndex = indeksiVjezbi.ElementAt(i).Item1; // prije je bilo >i<
                    prošireniButton.button1.Text = indeksiVjezbi.ElementAt(i).Item2;
                    prošireniButton.button1.UseVisualStyleBackColor = false;

                    prošireniButton.label1.Text = indeksiVjezbi.ElementAt(i).Item5;

                    if (indeksiVjezbi.ElementAt(i).Item4 == 0)
                        prošireniButton.Enabled = false;
                    
                    flowLayoutPanel2.Controls.Add(prošireniButton);
                    prošireniButton.button1.Click += new EventHandler(button_click);
                }
            }
            else
            {
                flowLayoutPanel2.Controls.Clear();
                indeksiVjezbi.Clear();
            }

            vjezbe.Visible = flowLayoutPanel2.Visible = buttonNatrag.Visible = value;
        }

        /*
        private void button_click(object sender, EventArgs e)
        {
            promijeniVidljivost(false, -1);
            form.prikazTipkanja(((Button)sender).Name, ((Button)sender).TabIndex); // u Nameu skrivam niz stringova
        }*/

        private void button_click(object sender, EventArgs e)
        {
            promijeniVidljivost(false, -1);
            form.prikazTipkanja(((Button)sender).Name, ((Button)sender).TabIndex, ((Button)sender).Text); // u Nameu skrivam niz stringova
        }
    }
}
