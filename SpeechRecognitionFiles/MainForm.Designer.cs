namespace SpeechRecognition
{
    partial class MainForm
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
            this.btnRecord = new System.Windows.Forms.Button();
            this.checkEcho = new System.Windows.Forms.CheckBox();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.Location = new System.Drawing.Point(90, 200);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(103, 30);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Visible = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // checkEcho
            // 
            this.checkEcho.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkEcho.AutoSize = true;
            this.checkEcho.Checked = true;
            this.checkEcho.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEcho.Location = new System.Drawing.Point(115, 236);
            this.checkEcho.Name = "checkEcho";
            this.checkEcho.Size = new System.Drawing.Size(51, 17);
            this.checkEcho.TabIndex = 1;
            this.checkEcho.Text = "Echo";
            this.checkEcho.UseVisualStyleBackColor = true;
            // 
            // numDuration
            // 
            this.numDuration.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numDuration.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDuration.Location = new System.Drawing.Point(138, 177);
            this.numDuration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDuration.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(54, 20);
            this.numDuration.TabIndex = 2;
            this.numDuration.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(88, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Duration:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.checkEcho);
            this.Controls.Add(this.btnRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Speech Recognition";
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.CheckBox checkEcho;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label label1;
    }
}

