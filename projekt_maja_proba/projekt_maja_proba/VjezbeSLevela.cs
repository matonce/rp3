using System;
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
            Size = new Size(793, 357),
            TabIndex = 11,
            Visible = false
        };

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

        public VjezbeSLevela(Form form)
        {
            this.form = form;

            flowLayoutPanel2.Parent = form;
            buttonNatrag.Parent = form;
            buttonNatrag.Click += new EventHandler(buttonNatrag_Click);
        }

        private void buttonNatrag_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false, -1);
            form.prikazLevela();
        }

        public void promijeniVidljivost(bool value, int indeksLevela)
        {
            if (value)
            {
                indeksiVjezbi = form.radSBazom.popisVjezbiSLevelaIzBaze(indeksLevela);

                for (int i = 0; i < indeksiVjezbi.Count; ++i)
                {
                    /*ProsireniButton prošireniButton = new ProsireniButton();
                    prošireniButton.Size = new Size(105, 105);

                    prošireniButton.button1.Name = indeksiVjezbi.ElementAt(i).Item3;
                    prošireniButton.button1.TabIndex = indeksiVjezbi.ElementAt(i).Item1; // prije je bilo >i<
                    prošireniButton.button1.Text = indeksiVjezbi.ElementAt(i).Item2;
                    prošireniButton.button1.UseVisualStyleBackColor = false;

                    prošireniButton.label1.Text = indeksiVjezbi.ElementAt(i).Item5;

                    */
                    Button button = new Button()
                    {
                        BackColor = Color.RosyBrown,
                        Font = new Font("Microsoft JhengHei", 12F),
                        ForeColor = Color.Snow,
                        Name = indeksiVjezbi.ElementAt(i).Item3,
                        Size = new Size(105, 105),
                        TabIndex = indeksiVjezbi.ElementAt(i).Item1, // prije je bilo >i<
                        Text = indeksiVjezbi.ElementAt(i).Item2,
                        UseVisualStyleBackColor = false
                    };

                    if (indeksiVjezbi.ElementAt(i).Item4 == 0)
                        button.Enabled = false;
                        //prošireniButton.Enabled = false;

                    flowLayoutPanel2.Controls.Add(button);
                    //flowLayoutPanel2.Controls.Add(prošireniButton);
                    button.Click += new EventHandler(button_click);
                    //prošireniButton.button1.Click += new EventHandler(button_click);
                }
            }
            else
            {
                flowLayoutPanel2.Controls.Clear();
                indeksiVjezbi.Clear();
            }

            flowLayoutPanel2.Visible = buttonNatrag.Visible = value;
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
