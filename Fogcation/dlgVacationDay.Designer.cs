namespace Fogcation
{
    partial class dlgVacationDay
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
            this.dt = new System.Windows.Forms.DateTimePicker();
            this.lblDayType = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboDayType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dt
            // 
            this.dt.Location = new System.Drawing.Point(15, 15);
            this.dt.Margin = new System.Windows.Forms.Padding(4);
            this.dt.Name = "dt";
            this.dt.Size = new System.Drawing.Size(232, 24);
            this.dt.TabIndex = 1;
            // 
            // lblDayType
            // 
            this.lblDayType.AutoSize = true;
            this.lblDayType.Location = new System.Drawing.Point(14, 53);
            this.lblDayType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDayType.Name = "lblDayType";
            this.lblDayType.Size = new System.Drawing.Size(62, 17);
            this.lblDayType.TabIndex = 2;
            this.lblDayType.Text = "Day type:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(65, 87);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 26);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(161, 87);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cboDayType
            // 
            this.cboDayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDayType.FormattingEnabled = true;
            this.cboDayType.Items.AddRange(new object[] {
            "100% (Full day)",
            "50% (Half day)",
            "Company Holiday"});
            this.cboDayType.Location = new System.Drawing.Point(97, 50);
            this.cboDayType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDayType.Name = "cboDayType";
            this.cboDayType.Size = new System.Drawing.Size(150, 23);
            this.cboDayType.TabIndex = 5;
            // 
            // dlgVacationDay
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(262, 126);
            this.Controls.Add(this.cboDayType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblDayType);
            this.Controls.Add(this.dt);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgVacationDay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Vacation Day...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDayType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.DateTimePicker dt;
        private System.Windows.Forms.ComboBox cboDayType;

    }
}