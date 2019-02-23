using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba
{
    class SpremljeneVjezbe
    {
        public Form form;

        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
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

        public SpremljeneVjezbe(Form form)
        {
            this.form = form;

            flowLayoutPanel.Parent = form;
            buttonNatrag.Parent = form;
            buttonNatrag.Click += new EventHandler(buttonNatrag_Click);
        }

        private void buttonNatrag_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);
            form.prikazLevela();
        }

        public void promijeniVidljivost(bool value)
        {
            List<String> vjezbe = new List<String>();
            if (value)
            {
                vjezbe = form.radSBazom.dohvatiVjezbe();

                for (int i = 0; i < vjezbe.Count; ++i)
                {
                    Button button = new Button()
                    {
                        BackColor = Color.RosyBrown,
                        Font = new Font("Microsoft JhengHei", 12F),
                        ForeColor = Color.Snow,
                        Name = vjezbe.ElementAt(i),
                        Size = new Size(105, 105),
                        Text = vjezbe.ElementAt(i),
                        UseVisualStyleBackColor = false
                    };

                    flowLayoutPanel.Controls.Add(button);
                    button.Click += new EventHandler(button_click);
                }
            }
            else
            {
                flowLayoutPanel.Controls.Clear();
                vjezbe.Clear();
            }

            flowLayoutPanel.Visible = buttonNatrag.Visible = value;
        }

        private void button_click(object sender, EventArgs e)
        {
            List<String> stringovi = new List<String>();
            List<String> sl = new List<String>();
            promijeniVidljivost(false);
            String ime = ((Button)sender).Name;
            List<Tuple<int, int, string>> vjezba = new List<Tuple<int, int, string>>();
            vjezba = form.radSBazom.nadiSpremljenuVjezbu(ime);

            String slova = vjezba[0].Item3;
            for(int i = 0; i < slova.Length; i++)
            {
                if (slova.ElementAt(i).ToString() != "|") sl.Add(slova.ElementAt(i).ToString());
            }

            stringovi = form.vjezbe.generirajVjezbu(vjezba[0].Item1, vjezba[0].Item2, sl);
            form.prikazTipkanjaZaVjezbe(stringovi); 
        }
    }

}
