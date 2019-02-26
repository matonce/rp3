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
    class Tipkanje
    {
        Form form;

        OleDbConnection connection = null;
        OleDbDataReader reader = null;

        internal KontrolaZaTipkanje kontrolaZaTipkanje;

        Button buttonNatrag = new Button()
        {
            BackColor = Color.SeaShell,
            Font = new Font("Microsoft JhengHei", 7F),
            Location = new Point(50, 12),
            Name = "buttonNatrag",
            Size = new Size(100, 40),
            TabIndex = 10,
            Text = "natrag",
            UseVisualStyleBackColor = false,
            Visible = false
        };

        public Tipkanje(Form form, int brojRijeci)
        {
            this.form = form;
            kontrolaZaTipkanje = new KontrolaZaTipkanje(form)
            {
                Location = new Point(50, 0),
                Size = new Size(form.ClientSize.Width-100,form.ClientSize.Height-20)
            };

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            @"Data source= C:\Users\Ana\Desktop\Faks\projekt_maja_proba\baza.mdb";
            connection = new OleDbConnection(connectionString);

            kontrolaZaTipkanje.Parent = form;
            buttonNatrag.Parent = form;

            buttonNatrag.BringToFront();
            buttonNatrag.Click += new EventHandler(buttonNatrag_Click);
        }

        private void buttonNatrag_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Zelis li prekinuti vjezbu?", "Poruka", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                promijeniVidljivost(false, "", "");
                form.prikazNaslovne();

                form.onemoguciReagiranjeNaTipke();
            }
        }

        internal void promijeniVidljivost(bool value, string stringovi, string ime)
        {
            kontrolaZaTipkanje.Visible = buttonNatrag.Visible = value;
            if (value)
            {
                kontrolaZaTipkanje.pokreniOdbrojavanje(stringovi.Split('|'), ime);
            }
            else
            {
                //kontrolaZaTipkanje.counter = 5;
                kontrolaZaTipkanje.očisti();
            }
            //Console.WriteLine("promijenjena vidljivost od vjezbi na " + value);
        }

        internal void promijeniVidljivostZaVjezbe(bool value, List<String> stringovi, String ime)
        {
            Console.WriteLine("promijeni vidljivost za vjezbe");
            kontrolaZaTipkanje.Visible = buttonNatrag.Visible = value;
            if (value)
            {
                kontrolaZaTipkanje.pokreniOdbrojavanje(stringovi.ToArray(), ime);
            }
            else
            {
                //kontrolaZaTipkanje.counter = 3;
                kontrolaZaTipkanje.očisti();
            }
            //Console.WriteLine("promijenjena vidljivost od vjezbi na " + value);
        }
    }
}
