using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba
{
    class VidiStatistiku
    {
        public Form form;

        TableLayoutPanel panel = new TableLayoutPanel()
        {
            Location = new Point(143, 110),
            Name = "panel",
            Size = new Size(393, 757),
            Visible = false
        };

        TableLayoutPanel panel2 = new TableLayoutPanel()
        {
            Location = new Point(643, 110),
            Name = "panel",
            Size = new Size(393, 150),
            Visible = false
        };

        TableLayoutPanel panel3 = new TableLayoutPanel()
        {
            Location = new Point(643, 300),
            Name = "panel",
            Size = new Size(393, 150),
            Visible = false
        };

        public VidiStatistiku(Form form)
        {
            this.form = form;
            buttonNatrag.Parent = form;
            panel.Parent = form;
            panel2.Parent = form;
            panel3.Parent = form;

            buttonNatrag.Click += new EventHandler(buttonNatrag_Click);
        }

        Button buttonNatrag = new Button()
        {
            BackColor = Color.SeaShell,
            Font = new Font("Microsoft JhengHei", 7F),
            Location = new Point(143, 12),
            Name = "buttonNatrag",
            Size = new Size(100, 40),
            TabIndex = 10,
            Text = "natrag",
            UseVisualStyleBackColor = false,
            Visible = false
        };

        public void promijeniVidljivost(bool value)
        {
            if (value)
            {
                List<Tuple<double, double>> rezultati = form.radSBazom.dohvatiZadnjeRezultate();
                int br = 0;

                Label naslov1 = new Label()
                {
                    Font = new Font("Microsoft JhengHei", 20F),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Text = "Zadnji rezultati",
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel.Controls.Add(naslov1);

                for (int i = rezultati.Count - 1; i >= 0; --i)
                {
                    br++;
                    Label label = new Label()
                    {
                        Font = new Font("Microsoft JhengHei", 12F),
                        ForeColor = Color.Black,
                        AutoSize = true,
                        Text = br + ". brzina: " + rezultati.ElementAt(i).Item1 + " wpm, preciznost: " +
                                rezultati.ElementAt(i).Item2 + "%"
                    };
                    panel.Controls.Add(label);
                    if (br == 10) break;
                }

                List<Tuple<String, double>> topSlova = form.radSBazom.dohvatiNajboljaSlova();

                Label naslov2 = new Label()
                {
                    Font = new Font("Microsoft JhengHei", 20F),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Text = "Top 5 slova",
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel2.Controls.Add(naslov2);

                for (int i = 0; i < topSlova.Count; ++i)
                {
                    Label label = new Label()
                    {
                        Font = new Font("Microsoft JhengHei", 12F),
                        ForeColor = Color.Black,
                        AutoSize = true,
                        Text = (i + 1) + ". " + topSlova.ElementAt(i).Item1 + " - točnost: " +
                                topSlova.ElementAt(i).Item2 + "%"
                    };
                    panel2.Controls.Add(label);
                }

                List<Tuple<String, double>> flopSlova = form.radSBazom.dohvatiNajgoraSlova();

                Label naslov3 = new Label()
                {
                    Font = new Font("Microsoft JhengHei", 20F),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Text = "Flop 5 slova",
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel3.Controls.Add(naslov3);

                for (int i = 0; i < flopSlova.Count; ++i)
                {
                    Label label = new Label()
                    {
                        Font = new Font("Microsoft JhengHei", 12F),
                        ForeColor = Color.Black,
                        AutoSize = true,
                        Text = (i + 1) + ". " + flopSlova.ElementAt(i).Item1.ToString() + " - točnost: " +
                                flopSlova.ElementAt(i).Item2.ToString() + "%"
                    };
                    panel3.Controls.Add(label);
                }
            }
            else
            {
                panel.Controls.Clear();
                panel2.Controls.Clear();
                panel3.Controls.Clear();
            }
            panel.Visible = panel2.Visible = buttonNatrag.Visible = panel3.Visible = value;
        }

        private void buttonNatrag_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);
            form.prikazNaslovne();
        }
    }
}
