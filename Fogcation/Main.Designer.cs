namespace Fogcation
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lblCurr = new System.Windows.Forms.Label();
            this.dtCurr = new System.Windows.Forms.DateTimePicker();
            this.txtCurrBalance = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblFuture = new System.Windows.Forms.Label();
            this.dtFuture = new System.Windows.Forms.DateTimePicker();
            this.lblCurrPayPeriod = new System.Windows.Forms.Label();
            this.lblFuturePayPeriod = new System.Windows.Forms.Label();
            this.lblCurrBalance = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListView();
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBalanceHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBalanceDays = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurr
            // 
            this.lblCurr.AutoSize = true;
            this.lblCurr.Location = new System.Drawing.Point(12, 9);
            this.lblCurr.Name = "lblCurr";
            this.lblCurr.Size = new System.Drawing.Size(127, 17);
            this.lblCurr.TabIndex = 0;
            this.lblCurr.Text = "Current balance as of";
            // 
            // dtCurr
            // 
            this.dtCurr.CustomFormat = "";
            this.dtCurr.Location = new System.Drawing.Point(151, 6);
            this.dtCurr.Name = "dtCurr";
            this.dtCurr.Size = new System.Drawing.Size(200, 24);
            this.dtCurr.TabIndex = 1;
            this.dtCurr.ValueChanged += new System.EventHandler(this.dtCurr_ValueChanged);
            // 
            // txtCurrBalance
            // 
            this.txtCurrBalance.Location = new System.Drawing.Point(12, 33);
            this.txtCurrBalance.Name = "txtCurrBalance";
            this.txtCurrBalance.Size = new System.Drawing.Size(70, 24);
            this.txtCurrBalance.TabIndex = 3;
            this.txtCurrBalance.Text = "0:00";
            this.toolTip.SetToolTip(this.txtCurrBalance, "Enter current balance in hours:minutes (i.e. -8:00)");
            this.txtCurrBalance.TextChanged += new System.EventHandler(this.txtCurrBalance_TextChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lblFuture
            // 
            this.lblFuture.AutoSize = true;
            this.lblFuture.Location = new System.Drawing.Point(12, 74);
            this.lblFuture.Name = "lblFuture";
            this.lblFuture.Size = new System.Drawing.Size(136, 17);
            this.lblFuture.TabIndex = 4;
            this.lblFuture.Text = "Calculate balance as of";
            // 
            // dtFuture
            // 
            this.dtFuture.CustomFormat = "";
            this.dtFuture.Location = new System.Drawing.Point(151, 71);
            this.dtFuture.Name = "dtFuture";
            this.dtFuture.Size = new System.Drawing.Size(200, 24);
            this.dtFuture.TabIndex = 5;
            this.dtFuture.ValueChanged += new System.EventHandler(this.dtFuture_ValueChanged);
            // 
            // lblCurrPayPeriod
            // 
            this.lblCurrPayPeriod.AutoSize = true;
            this.lblCurrPayPeriod.Location = new System.Drawing.Point(365, 9);
            this.lblCurrPayPeriod.Name = "lblCurrPayPeriod";
            this.lblCurrPayPeriod.Size = new System.Drawing.Size(67, 17);
            this.lblCurrPayPeriod.TabIndex = 2;
            this.lblCurrPayPeriod.Text = "pay period";
            // 
            // lblFuturePayPeriod
            // 
            this.lblFuturePayPeriod.AutoSize = true;
            this.lblFuturePayPeriod.Location = new System.Drawing.Point(365, 74);
            this.lblFuturePayPeriod.Name = "lblFuturePayPeriod";
            this.lblFuturePayPeriod.Size = new System.Drawing.Size(67, 17);
            this.lblFuturePayPeriod.TabIndex = 6;
            this.lblFuturePayPeriod.Text = "pay period";
            // 
            // lblCurrBalance
            // 
            this.lblCurrBalance.AutoSize = true;
            this.lblCurrBalance.Location = new System.Drawing.Point(96, 36);
            this.lblCurrBalance.Name = "lblCurrBalance";
            this.lblCurrBalance.Size = new System.Drawing.Size(60, 17);
            this.lblCurrBalance.TabIndex = 8;
            this.lblCurrBalance.Text = "0d 0h 0m";
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colType,
            this.colBalanceHours,
            this.colBalanceDays});
            this.lstLog.FullRowSelect = true;
            this.lstLog.GridLines = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.HideSelection = false;
            this.lstLog.Location = new System.Drawing.Point(15, 109);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(601, 239);
            this.lstLog.TabIndex = 15;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // colType
            // 
            this.colType.Text = "";
            this.colType.Width = 324;
            // 
            // colBalanceHours
            // 
            this.colBalanceHours.Text = "Balance in hours";
            this.colBalanceHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colBalanceHours.Width = 116;
            // 
            // colBalanceDays
            // 
            this.colBalanceDays.Text = "Balance in work days";
            this.colBalanceDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colBalanceDays.Width = 140;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 362);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblCurrBalance);
            this.Controls.Add(this.lblFuturePayPeriod);
            this.Controls.Add(this.lblCurrPayPeriod);
            this.Controls.Add(this.dtFuture);
            this.Controls.Add(this.lblFuture);
            this.Controls.Add(this.txtCurrBalance);
            this.Controls.Add(this.dtCurr);
            this.Controls.Add(this.lblCurr);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(644, 228);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fogcation - Fog Creek Vacation Calculator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurr;
        private System.Windows.Forms.DateTimePicker dtCurr;
        private System.Windows.Forms.TextBox txtCurrBalance;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblCurrPayPeriod;
        private System.Windows.Forms.DateTimePicker dtFuture;
        private System.Windows.Forms.Label lblFuture;
        private System.Windows.Forms.Label lblFuturePayPeriod;
        private System.Windows.Forms.Label lblCurrBalance;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colBalanceHours;
        private System.Windows.Forms.ColumnHeader colBalanceDays;
    }
}

