namespace Fogcation
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
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
            this.lblVacation = new System.Windows.Forms.Label();
            this.lstVacation = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPercentage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.colDelta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.lblFuture.Location = new System.Drawing.Point(12, 255);
            this.lblFuture.Name = "lblFuture";
            this.lblFuture.Size = new System.Drawing.Size(136, 17);
            this.lblFuture.TabIndex = 4;
            this.lblFuture.Text = "Calculate balance as of";
            // 
            // dtFuture
            // 
            this.dtFuture.CustomFormat = "";
            this.dtFuture.Location = new System.Drawing.Point(151, 252);
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
            this.lblFuturePayPeriod.Location = new System.Drawing.Point(365, 255);
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
            this.colDelta,
            this.colBalanceHours,
            this.colBalanceDays});
            this.lstLog.FullRowSelect = true;
            this.lstLog.GridLines = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.HideSelection = false;
            this.lstLog.Location = new System.Drawing.Point(15, 290);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(647, 124);
            this.lstLog.TabIndex = 15;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // colType
            // 
            this.colType.Text = "";
            this.colType.Width = 254;
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
            // lblVacation
            // 
            this.lblVacation.AutoSize = true;
            this.lblVacation.Location = new System.Drawing.Point(12, 69);
            this.lblVacation.Name = "lblVacation";
            this.lblVacation.Size = new System.Drawing.Size(91, 17);
            this.lblVacation.TabIndex = 16;
            this.lblVacation.Text = "Vacation Days:";
            // 
            // lstVacation
            // 
            this.lstVacation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVacation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colPercentage,
            this.colTime});
            this.lstVacation.FullRowSelect = true;
            this.lstVacation.GridLines = true;
            this.lstVacation.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVacation.HideSelection = false;
            this.lstVacation.Location = new System.Drawing.Point(15, 89);
            this.lstVacation.Name = "lstVacation";
            this.lstVacation.Size = new System.Drawing.Size(556, 145);
            this.lstVacation.TabIndex = 17;
            this.lstVacation.UseCompatibleStateImageBehavior = false;
            this.lstVacation.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            this.colDate.Width = 385;
            // 
            // colPercentage
            // 
            this.colPercentage.Text = "% day";
            this.colPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPercentage.Width = 50;
            // 
            // colTime
            // 
            this.colTime.Text = "Time in hours";
            this.colTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colTime.Width = 100;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(587, 89);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "&Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(587, 119);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(587, 149);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "&Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // colDelta
            // 
            this.colDelta.Text = "Change in hours";
            this.colDelta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colDelta.Width = 116;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 428);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstVacation);
            this.Controls.Add(this.lblVacation);
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
            this.MinimumSize = new System.Drawing.Size(690, 409);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fogcation - Fog Creek Vacation Calculator";
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
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView lstVacation;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colPercentage;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.Label lblVacation;
        private System.Windows.Forms.ColumnHeader colDelta;
    }
}

