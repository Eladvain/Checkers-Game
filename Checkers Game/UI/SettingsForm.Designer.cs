namespace C20__Ex2
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.ButtonDone = new System.Windows.Forms.Button();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.Location = new System.Drawing.Point(48, 43);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(62, 21);
            this.radioButton6X6.TabIndex = 1;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Text = "6 X 6";
            this.radioButton6X6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton6X6.UseVisualStyleBackColor = true;
            this.radioButton6X6.CheckedChanged += new System.EventHandler(this.radioButtonBoardSize_CheckedChanged);
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.Location = new System.Drawing.Point(116, 43);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(62, 21);
            this.radioButton8X8.TabIndex = 2;
            this.radioButton8X8.TabStop = true;
            this.radioButton8X8.Text = "8 X 8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            this.radioButton8X8.CheckedChanged += new System.EventHandler(this.radioButtonBoardSize_CheckedChanged);
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.Location = new System.Drawing.Point(191, 43);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(78, 21);
            this.radioButton10X10.TabIndex = 3;
            this.radioButton10X10.TabStop = true;
            this.radioButton10X10.Text = "10 X 10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            this.radioButton10X10.CheckedChanged += new System.EventHandler(this.radioButtonBoardSize_CheckedChanged);
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(24, 70);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(59, 17);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players:";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(132, 98);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(118, 22);
            this.textBoxPlayer1.TabIndex = 5;
            this.textBoxPlayer1.TextChanged += new System.EventHandler(this.textBoxPlayer1_TextChanged);
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(37, 126);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(86, 21);
            this.checkBoxPlayer2.TabIndex = 6;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(132, 125);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(118, 22);
            this.textBoxPlayer2.TabIndex = 7;
            this.textBoxPlayer2.Text = "[Computer]";
            // this.textBoxPlayer2.TextChanged += new System.EventHandler(this.textBoxPlayer2_TextChanged);
            // 
            // ButtonDone
            // 
            this.ButtonDone.Location = new System.Drawing.Point(174, 157);
            this.ButtonDone.Name = "ButtonDone";
            this.ButtonDone.Size = new System.Drawing.Size(76, 35);
            this.ButtonDone.TabIndex = 8;
            this.ButtonDone.Text = "Done";
            this.ButtonDone.UseVisualStyleBackColor = true;
            this.ButtonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(37, 98);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(60, 17);
            this.labelPlayer1.TabIndex = 9;
            this.labelPlayer1.Text = "Player1:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 198);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.ButtonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButton10X10);
            this.Controls.Add(this.radioButton8X8);
            this.Controls.Add(this.radioButton6X6);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.Button ButtonDone;
        private System.Windows.Forms.Label labelPlayer1;
    }
}