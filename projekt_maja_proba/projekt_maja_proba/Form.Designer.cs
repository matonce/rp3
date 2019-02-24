namespace projekt_maja_proba
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.igraj_levele = new System.Windows.Forms.Button();
            this.vjezba = new System.Windows.Forms.Button();
            this.naslov = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // igraj_levele
            // 
            this.igraj_levele.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.igraj_levele.AutoEllipsis = true;
            this.igraj_levele.BackColor = System.Drawing.Color.Transparent;
            this.igraj_levele.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.igraj_levele.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.igraj_levele.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.igraj_levele.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.igraj_levele.Location = new System.Drawing.Point(531, 326);
            this.igraj_levele.Name = "igraj_levele";
            this.igraj_levele.Size = new System.Drawing.Size(116, 26);
            this.igraj_levele.TabIndex = 0;
            this.igraj_levele.Text = "Igraj levele";
            this.igraj_levele.UseVisualStyleBackColor = false;
            // 
            // vjezba
            // 
            this.vjezba.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vjezba.AutoSize = true;
            this.vjezba.BackColor = System.Drawing.Color.Transparent;
            this.vjezba.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.vjezba.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.vjezba.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.vjezba.Location = new System.Drawing.Point(543, 358);
            this.vjezba.Name = "vjezba";
            this.vjezba.Size = new System.Drawing.Size(92, 26);
            this.vjezba.TabIndex = 1;
            this.vjezba.Text = "Vježba";
            this.vjezba.UseVisualStyleBackColor = false;
            // 
            // naslov
            // 
            this.naslov.AutoSize = true;
            this.naslov.BackColor = System.Drawing.Color.Transparent;
            this.naslov.Font = new System.Drawing.Font("Courier New", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.naslov.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.naslov.Location = new System.Drawing.Point(438, 136);
            this.naslov.Name = "naslov";
            this.naslov.Size = new System.Drawing.Size(237, 37);
            this.naslov.TabIndex = 2;
            this.naslov.Text = "Daktilograf";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(947, 531);
            this.Controls.Add(this.naslov);
            this.Controls.Add(this.vjezba);
            this.Controls.Add(this.igraj_levele);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daktilograf";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button igraj_levele;
        private System.Windows.Forms.Button vjezba;
        private System.Windows.Forms.Label naslov;
    }
}