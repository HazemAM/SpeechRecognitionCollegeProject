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
            this.components = new System.ComponentModel.Container();
            this.btnRecord = new System.Windows.Forms.Button();
            this.checkEcho = new System.Windows.Forms.CheckBox();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkFullResult = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFileNames = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRecord.Enabled = false;
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.Location = new System.Drawing.Point(141, 295);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(103, 34);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // checkEcho
            // 
            this.checkEcho.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkEcho.AutoSize = true;
            this.checkEcho.Checked = true;
            this.checkEcho.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEcho.Location = new System.Drawing.Point(12, 294);
            this.checkEcho.Name = "checkEcho";
            this.checkEcho.Size = new System.Drawing.Size(78, 17);
            this.checkEcho.TabIndex = 1;
            this.checkEcho.Text = "Echo Input";
            this.toolTip.SetToolTip(this.checkEcho, "Listen to yourself talking.\r\nMake sure you wear a headset.");
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
            this.numDuration.Location = new System.Drawing.Point(189, 271);
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
            this.toolTip.SetToolTip(this.numDuration, "Record duration in milliseconds.");
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
            this.label1.Location = new System.Drawing.Point(139, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Duration:";
            // 
            // checkFullResult
            // 
            this.checkFullResult.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkFullResult.AutoSize = true;
            this.checkFullResult.Location = new System.Drawing.Point(12, 312);
            this.checkFullResult.Name = "checkFullResult";
            this.checkFullResult.Size = new System.Drawing.Size(75, 17);
            this.checkFullResult.TabIndex = 5;
            this.checkFullResult.Text = "Full Result";
            this.toolTip.SetToolTip(this.checkFullResult, "Show the actual numbers.");
            this.checkFullResult.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 258);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // txtFileNames
            // 
            this.txtFileNames.Location = new System.Drawing.Point(266, 283);
            this.txtFileNames.Name = "txtFileNames";
            this.txtFileNames.Size = new System.Drawing.Size(111, 20);
            this.txtFileNames.TabIndex = 11;
            this.txtFileNames.Text = "Forward, Left, Stop, Right, Backward";
            this.toolTip.SetToolTip(this.txtFileNames, "Files names in local path \'sound\\\' separated by a comma.");
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(265, 306);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(113, 23);
            this.btnReload.TabIndex = 12;
            this.btnReload.Text = "Reload";
            this.toolTip.SetToolTip(this.btnReload, "Recalculate MFCC\'s.");
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(390, 343);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.txtFileNames);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.checkFullResult);
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
        private System.Windows.Forms.CheckBox checkFullResult;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtFileNames;
        private System.Windows.Forms.Button btnReload;
    }
}

