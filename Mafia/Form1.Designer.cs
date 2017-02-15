namespace Mafia
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numOfPlayersNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.InfoRTB = new System.Windows.Forms.RichTextBox();
            this.labelStartPhase = new System.Windows.Forms.Label();
            this.buttonStartPhase = new System.Windows.Forms.Button();
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.PlayersCardsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.CardsListBox = new System.Windows.Forms.ListBox();
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.Info2RTB = new System.Windows.Forms.RichTextBox();
            this.votedButton = new System.Windows.Forms.Button();
            this.shotButton = new System.Windows.Forms.Button();
            this.buttonStartNight = new System.Windows.Forms.Button();
            this.removeCardButton = new System.Windows.Forms.Button();
            this.addCardButton = new System.Windows.Forms.Button();
            this.addCardCombobox = new System.Windows.Forms.ComboBox();
            this.removeCardCombobox = new System.Windows.Forms.ComboBox();
            this.undoButton = new System.Windows.Forms.Button();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.bombButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfPlayersNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(770, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // numOfPlayersNumericUpDown
            // 
            this.numOfPlayersNumericUpDown.Location = new System.Drawing.Point(101, 23);
            this.numOfPlayersNumericUpDown.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.numOfPlayersNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numOfPlayersNumericUpDown.Name = "numOfPlayersNumericUpDown";
            this.numOfPlayersNumericUpDown.Size = new System.Drawing.Size(42, 20);
            this.numOfPlayersNumericUpDown.TabIndex = 1;
            this.numOfPlayersNumericUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // InfoRTB
            // 
            this.InfoRTB.Location = new System.Drawing.Point(779, 78);
            this.InfoRTB.Name = "InfoRTB";
            this.InfoRTB.ReadOnly = true;
            this.InfoRTB.Size = new System.Drawing.Size(274, 433);
            this.InfoRTB.TabIndex = 7;
            this.InfoRTB.Text = "";
            this.InfoRTB.TextChanged += new System.EventHandler(this.InfoRTB_TextChanged);
            // 
            // labelStartPhase
            // 
            this.labelStartPhase.AutoSize = true;
            this.labelStartPhase.Location = new System.Drawing.Point(29, 25);
            this.labelStartPhase.Name = "labelStartPhase";
            this.labelStartPhase.Size = new System.Drawing.Size(81, 13);
            this.labelStartPhase.TabIndex = 8;
            this.labelStartPhase.Text = "labelStartPhase";
            // 
            // buttonStartPhase
            // 
            this.buttonStartPhase.Location = new System.Drawing.Point(32, 49);
            this.buttonStartPhase.Name = "buttonStartPhase";
            this.buttonStartPhase.Size = new System.Drawing.Size(75, 23);
            this.buttonStartPhase.TabIndex = 9;
            this.buttonStartPhase.Text = "OK";
            this.buttonStartPhase.UseVisualStyleBackColor = true;
            this.buttonStartPhase.Click += new System.EventHandler(this.buttonStartPhase_Click);
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Location = new System.Drawing.Point(149, 23);
            this.textBoxPlayerName.MaxLength = 8;
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.Size = new System.Drawing.Size(75, 20);
            this.textBoxPlayerName.TabIndex = 10;
            // 
            // PlayersCardsRichTextBox
            // 
            this.PlayersCardsRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.PlayersCardsRichTextBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayersCardsRichTextBox.Location = new System.Drawing.Point(3, 518);
            this.PlayersCardsRichTextBox.Name = "PlayersCardsRichTextBox";
            this.PlayersCardsRichTextBox.ReadOnly = true;
            this.PlayersCardsRichTextBox.Size = new System.Drawing.Size(1354, 182);
            this.PlayersCardsRichTextBox.TabIndex = 11;
            this.PlayersCardsRichTextBox.Text = "";
            // 
            // CardsListBox
            // 
            this.CardsListBox.FormattingEnabled = true;
            this.CardsListBox.Location = new System.Drawing.Point(315, 12);
            this.CardsListBox.Name = "CardsListBox";
            this.CardsListBox.Size = new System.Drawing.Size(160, 4);
            this.CardsListBox.TabIndex = 12;
            this.CardsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CardsListBox_MouseDoubleClick);
            // 
            // yesButton
            // 
            this.yesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.yesButton.Location = new System.Drawing.Point(779, 49);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(64, 23);
            this.yesButton.TabIndex = 13;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // noButton
            // 
            this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.noButton.Location = new System.Drawing.Point(849, 50);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(64, 23);
            this.noButton.TabIndex = 14;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(779, 23);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(0, 13);
            this.InfoLabel.TabIndex = 15;
            // 
            // Info2RTB
            // 
            this.Info2RTB.BackColor = System.Drawing.SystemColors.Control;
            this.Info2RTB.Location = new System.Drawing.Point(1059, 79);
            this.Info2RTB.Name = "Info2RTB";
            this.Info2RTB.ReadOnly = true;
            this.Info2RTB.Size = new System.Drawing.Size(299, 433);
            this.Info2RTB.TabIndex = 16;
            this.Info2RTB.Text = "";
            this.Info2RTB.TextChanged += new System.EventHandler(this.Info2RTB_TextChanged);
            // 
            // votedButton
            // 
            this.votedButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.votedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.votedButton.Location = new System.Drawing.Point(943, 20);
            this.votedButton.Name = "votedButton";
            this.votedButton.Size = new System.Drawing.Size(110, 23);
            this.votedButton.TabIndex = 17;
            this.votedButton.Text = "Był przegłosowany";
            this.votedButton.UseVisualStyleBackColor = true;
            this.votedButton.Click += new System.EventHandler(this.votedButton_Click);
            // 
            // shotButton
            // 
            this.shotButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shotButton.Location = new System.Drawing.Point(943, 50);
            this.shotButton.Name = "shotButton";
            this.shotButton.Size = new System.Drawing.Size(110, 23);
            this.shotButton.TabIndex = 18;
            this.shotButton.Text = "Był strzelóny";
            this.shotButton.UseVisualStyleBackColor = true;
            this.shotButton.Click += new System.EventHandler(this.shotButton_Click);
            // 
            // buttonStartNight
            // 
            this.buttonStartNight.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonStartNight.Location = new System.Drawing.Point(779, 49);
            this.buttonStartNight.Name = "buttonStartNight";
            this.buttonStartNight.Size = new System.Drawing.Size(134, 23);
            this.buttonStartNight.TabIndex = 19;
            this.buttonStartNight.Text = "Rozpocznij noc";
            this.buttonStartNight.UseVisualStyleBackColor = true;
            this.buttonStartNight.Click += new System.EventHandler(this.buttonStartNight_Click);
            // 
            // removeCardButton
            // 
            this.removeCardButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeCardButton.Location = new System.Drawing.Point(1119, 50);
            this.removeCardButton.Name = "removeCardButton";
            this.removeCardButton.Size = new System.Drawing.Size(110, 23);
            this.removeCardButton.TabIndex = 21;
            this.removeCardButton.Text = "Odbierz kartę";
            this.removeCardButton.UseVisualStyleBackColor = true;
            this.removeCardButton.Click += new System.EventHandler(this.removeCardButton_Click);
            // 
            // addCardButton
            // 
            this.addCardButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addCardButton.Location = new System.Drawing.Point(1119, 20);
            this.addCardButton.Name = "addCardButton";
            this.addCardButton.Size = new System.Drawing.Size(110, 23);
            this.addCardButton.TabIndex = 20;
            this.addCardButton.Text = "Dodaj kartę";
            this.addCardButton.UseVisualStyleBackColor = true;
            this.addCardButton.Click += new System.EventHandler(this.addCardButton_Click);
            // 
            // addCardCombobox
            // 
            this.addCardCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addCardCombobox.FormattingEnabled = true;
            this.addCardCombobox.Items.AddRange(new object[] {
            "Mrakoszlap",
            "Neprustrzelno westa",
            "Zwierciadlo",
            "Imunita",
            "Prowazochodec"});
            this.addCardCombobox.Location = new System.Drawing.Point(1235, 21);
            this.addCardCombobox.Name = "addCardCombobox";
            this.addCardCombobox.Size = new System.Drawing.Size(121, 21);
            this.addCardCombobox.TabIndex = 22;
            // 
            // removeCardCombobox
            // 
            this.removeCardCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.removeCardCombobox.FormattingEnabled = true;
            this.removeCardCombobox.Location = new System.Drawing.Point(1235, 51);
            this.removeCardCombobox.Name = "removeCardCombobox";
            this.removeCardCombobox.Size = new System.Drawing.Size(121, 21);
            this.removeCardCombobox.TabIndex = 23;
            this.removeCardCombobox.SelectedIndexChanged += new System.EventHandler(this.removeCardCombobox_SelectedIndexChanged);
            // 
            // undoButton
            // 
            this.undoButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.undoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.undoButton.Location = new System.Drawing.Point(1068, 20);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(36, 23);
            this.undoButton.TabIndex = 24;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "Hovorová slezština",
            "Čeština"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(164, 23);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLanguage.TabIndex = 25;
            // 
            // bombButton
            // 
            this.bombButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bombButton.Location = new System.Drawing.Point(779, 15);
            this.bombButton.Name = "bombButton";
            this.bombButton.Size = new System.Drawing.Size(64, 23);
            this.bombButton.TabIndex = 26;
            this.bombButton.Text = "Bomba";
            this.bombButton.UseVisualStyleBackColor = true;
            this.bombButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bombButton_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 712);
            this.Controls.Add(this.bombButton);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.removeCardCombobox);
            this.Controls.Add(this.addCardCombobox);
            this.Controls.Add(this.removeCardButton);
            this.Controls.Add(this.addCardButton);
            this.Controls.Add(this.shotButton);
            this.Controls.Add(this.votedButton);
            this.Controls.Add(this.Info2RTB);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.CardsListBox);
            this.Controls.Add(this.PlayersCardsRichTextBox);
            this.Controls.Add(this.textBoxPlayerName);
            this.Controls.Add(this.buttonStartPhase);
            this.Controls.Add(this.labelStartPhase);
            this.Controls.Add(this.InfoRTB);
            this.Controls.Add(this.numOfPlayersNumericUpDown);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonStartNight);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mafia v3.6";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfPlayersNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numOfPlayersNumericUpDown;
        private System.Windows.Forms.RichTextBox InfoRTB;
        private System.Windows.Forms.Label labelStartPhase;
        private System.Windows.Forms.Button buttonStartPhase;
        private System.Windows.Forms.TextBox textBoxPlayerName;
        private System.Windows.Forms.RichTextBox PlayersCardsRichTextBox;
        private System.Windows.Forms.ListBox CardsListBox;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.RichTextBox Info2RTB;
        private System.Windows.Forms.Button votedButton;
        private System.Windows.Forms.Button shotButton;
        private System.Windows.Forms.Button buttonStartNight;
        private System.Windows.Forms.Button removeCardButton;
        private System.Windows.Forms.Button addCardButton;
        private System.Windows.Forms.ComboBox addCardCombobox;
        private System.Windows.Forms.ComboBox removeCardCombobox;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Button bombButton;
    }
}

