using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace projekt_maja_proba
{
    class Naslovna
    {
        public Form form;

        Button igrajLevele = new Button()
        {
            AutoEllipsis = true,
            BackColor = Color.Transparent,
            BackgroundImageLayout = ImageLayout.None,
            FlatStyle = FlatStyle.Popup,
            Font = new Font("Courier New", 15F, FontStyle.Bold),
            ForeColor = Color.Gainsboro,
            Location = new Point(227, 237),
            Name = "igrajLevele",
            Size = new Size(210, 40),
            TabIndex = 0,
            Text = "Igraj levele",
            UseVisualStyleBackColor = false
        };

        Button vidiStatistiku = new Button()
        {
            AutoEllipsis = true,
            BackColor = Color.Transparent,
            BackgroundImageLayout = ImageLayout.None,
            FlatStyle = FlatStyle.Popup,
            Font = new Font("Courier New", 15F, FontStyle.Bold),
            ForeColor = Color.Gray,
            Location = new Point(800, 450),
            Name = "vidiStatitsku",
            Size = new Size(210, 40),
            Text = "Statistika",
            UseVisualStyleBackColor = false
        };

        Button vjezba = new Button()
        {
            AutoEllipsis = true,
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Popup,
            Font = new Font("Courier New", 15F, FontStyle.Bold),
            ForeColor = Color.Gray,
            Location = new Point(671, 237),
            Name = "vjezba",
            Size = new Size(210, 40),
            TabIndex = 1,
            Text = "Vježba",
            UseVisualStyleBackColor = true
        };

        Label naslov = new Label()
        {
            AutoSize = true,
            BackColor = Color.Transparent,
            Font = new Font("Courier New", 48F, FontStyle.Bold),
            ForeColor = Color.Gainsboro,
            Location = new Point(323, 93),
            Name = "naslov",
            Size = new Size(448, 72),
            TabIndex = 2,
            Text = "Daktilograf"
        };


        public Naslovna(Form form)
        {
            this.form = form;

            igrajLevele.Parent = form;
            vjezba.Parent = form;
            naslov.Parent = form;
            vidiStatistiku.Parent = form;

            vjezba.Click += new EventHandler(vjezba_Click);
            igrajLevele.Click += new EventHandler(igrajLevele_Click);
            vidiStatistiku.Click += new EventHandler(vidiStatistiku_Click);
        }

        private void igrajLevele_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);

            form.prikazLevela();
        }

        private void vjezba_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);

            form.prikazZaVjezbe();
        }

        private void vidiStatistiku_Click(object sender, EventArgs e)
        {
            promijeniVidljivost(false);

            form.prikazStatistike();
        }

        internal void promijeniVidljivost(bool value)
        {
            vidiStatistiku.Visible = igrajLevele.Visible = vjezba.Visible = naslov.Visible = value;
        }
    }
}
