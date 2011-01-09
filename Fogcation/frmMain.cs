using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Fogcation
{
    public partial class frmMain : Form
    {
        // the number of hours that counts as a work day
        internal static int cHoursInAWorkDay = 8;
        // the default long-form date format
        internal static string sLongDateFormat = "MMMM d, yyyy";

        // the amount of vacation time accrued per pay period
        private static TimeSpan tsVacationTimePerPayPeriod = new TimeSpan(6, 40, 0);

        private dlgVacationDay dlgVacationDay = new dlgVacationDay();

        public frmMain()
        {
            InitializeComponent();
            
            dtCurr.Value = DateTime.Now;
            dtFuture.Value = DateTime.Now;
            dlgVacationDay.dt.Value = DateTime.Now;

            lstVacation.ListViewItemSorter = new VacationListViewSorter();

            ResizeControls();
        }
        
        private void Main_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void dtFuture_ValueChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void txtCurrBalance_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void dtCurr_ValueChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dt = dlgVacationDay.dt.Value;
            dlgVacationDay.Percentage = Percentage.FullDay;

            if (dlgVacationDay.ShowDialog(this) == DialogResult.OK)
            {
                AddVacationDay(dlgVacationDay.VacationDay);
            }
            else
            {
                // don't save the last entered date if they canceled
                dlgVacationDay.dt.Value = dt;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstVacation.SelectedItems.Count < 1) return;

            var item = lstVacation.SelectedItems[0];
            var day = item.Tag as VacationDay;

            var result = MessageBox.Show(
                "Are you sure you want to remove the following vacation day?\n\n" + day,
                "Remove Vacation Day?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                item.Remove();
                lstVacation.Sort();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstVacation.Items.Clear();
        }

        private void AddVacationDay(VacationDay day)
        {
            // check for dupes!
            foreach (ListViewItem viewitem in lstVacation.Items)
            {
                if ((viewitem.Tag as VacationDay).Dt.Date == day.Dt.Date)
                {
                    MessageBox.Show(
                        "That date has already been added!",
                        "Cannot add " + day.Dt.ToString(sLongDateFormat),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }
            }

            var item = new ListViewItem(
                new string[] {
                    day.Dt.ToString(sLongDateFormat),
                    VacationDay.PrettyPrintPercentage(day.DayType),
                    PrettyPrintTimeSpan(day.Hours, false)
                }
            );
            
            item.UseItemStyleForSubItems = false;
            item.SubItems[2].ForeColor = Color.Red;

            item.Tag = day; // store the VacationDay object for later

            lstVacation.Items.Add(item);
            lstVacation.Sort();
            CalculateBalance();
        }

        private void CalculateBalance()
        {
            ResetCalculatedFields();

            if (ValidateDateRange())
            {
                var currBalance = TimeSpanFromBalance(txtCurrBalance.Text);
                if (currBalance.HasValue)
                {
                    errorProvider.SetError(txtCurrBalance, "");
                    lblCurrBalance.Text = PrettyPrintTimeSpan(currBalance.Value, true);
                    AddOpeningLogEntry(currBalance.Value);

                    var dictVacation = new Dictionary<DateTime, VacationDay>();
                    foreach (ListViewItem item in lstVacation.Items)
                    {
                        var day = item.Tag as VacationDay;
                        if (day.Dt >= dtCurr.Value.Date && day.Dt <= dtFuture.Value.Date)
                        {
                            dictVacation.Add(day.Dt.Date, day);
                        }
                    }

                    var futureBalance = currBalance.Value;

                    int cPayPeriods = 0;
                    DateTime dt = new DateTime(dtCurr.Value.Year, dtCurr.Value.Month, dtCurr.Value.Day, 12, 0, 0);
                    DateTime dtEnd = new DateTime(dtFuture.Value.Year, dtFuture.Value.Month, dtFuture.Value.Day, 14, 0, 0);
                    while ((dt = dt.AddDays(1)) <= dtEnd)
                    {
                        if (dictVacation.ContainsKey(dt.Date))
                        {
                            futureBalance += dictVacation[dt.Date].Hours;
                            AddVacationDayLogEntry(dictVacation[dt.Date], futureBalance);
                        }

                        if (dt.Day == 15 || dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month))
                        {
                            futureBalance += tsVacationTimePerPayPeriod;
                            cPayPeriods++;
                            AddPayPeriodLogEntry(dt, futureBalance);
                        }
                    }
                    AddClosingLogEntry(cPayPeriods, futureBalance);
                }
                else
                {
                    errorProvider.SetError(txtCurrBalance, "Invalid balance! Expected hh:mm (e.g. -8:00)");
                    lblCurrBalance.Text = "You're not even trying...";
                }
            }
        }

        private void ResetCalculatedFields()
        {
            lblCurrBalance.Text = "N/A";
            lstLog.Items.Clear();
        }

        private static string PrettyPrintTimeSpan(TimeSpan interval, bool fInWorkdays)
        {
            var fNegative = interval.Ticks < 0;
            if (fNegative) interval = interval.Negate();

            var days = ((int)interval.TotalHours) / cHoursInAWorkDay;
            var hours = ((int)interval.TotalHours) % cHoursInAWorkDay;
            var minutes = interval.Minutes;

            var sFormattedAbsTimeSpan =
                fInWorkdays ?
                String.Format(
                    "{0}d {1}h {2}m",
                    days,
                    hours,
                    minutes
                )
                :
                String.Format(
                    "{0}h {1}m",
                    (int)interval.TotalHours,
                    minutes
                );

            if (fNegative)
            {
                return "-(" + sFormattedAbsTimeSpan + ")";
            }
            else
            {
                return sFormattedAbsTimeSpan;
            }
        }

        private TimeSpan? TimeSpanFromBalance(string sBalance)
        {
            var reBalance = new Regex(@"^(?<negative>[-])?(?<hours>\d+):(?<minutes>\d+)$");
            var m = reBalance.Match(sBalance);
            if (m.Success)
            {
                var fNegative = m.Groups["negative"].Success;
                var hours = int.Parse(m.Groups["hours"].Value);
                var minutes = int.Parse(m.Groups["minutes"].Value);

                hours += minutes / 60;
                minutes = minutes % 60;

                if (fNegative)
                {
                    hours = -hours;
                    minutes = -minutes;
                }

                return new TimeSpan(hours, minutes, 0);
            }

            return null;
        }

        private bool ValidateDateRange()
        {
            UpdatePayPeriods();

            if (dtFuture.Value < dtCurr.Value)
            {
                errorProvider.SetError(dtFuture, "Target date must occur after current balance date.");
                return false;
            }
            else
            {
                errorProvider.SetError(dtFuture, "");
                return true;
            }
        }

        private void UpdatePayPeriods()
        {
            var datePairs = new Dictionary<DateTimePicker, Label>
            {
                { dtCurr, lblCurrPayPeriod },
                { dtFuture, lblFuturePayPeriod }
            };

            foreach (var pair in datePairs)
            {
                if (pair.Key.Value.Day < 15)
                {
                    pair.Value.Text = "(includes last pay period of previous month)";
                }
                else if (pair.Key.Value.Day < DateTime.DaysInMonth(pair.Key.Value.Year, pair.Key.Value.Month))
                {
                    pair.Value.Text = "(includes first pay period of the month)";
                }
                else
                {
                    pair.Value.Text = "(includes last pay period of the month)";
                }
            }
        }

        private void ResizeControls()
        {
            // the extra 21 avoids triggering a horizontal scrollbar if/when a vertical scrollbar is needed
            lstLog.Columns[0].Width = lstLog.Width - 21 - lstLog.Columns[1].Width - lstLog.Columns[2].Width;
            lstVacation.Columns[0].Width = lstVacation.Width - 21 - lstVacation.Columns[1].Width - lstVacation.Columns[2].Width;
        }

        private void AddOpeningLogEntry(TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "Starting balance on {0}",
                    dtCurr.Value.ToString(sLongDateFormat)
                ),
                balance
            );
        }

        private void AddPayPeriodLogEntry(DateTime dt, TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "{0} payday! +({1}h {2}m)",
                    dt.ToString(sLongDateFormat),
                    tsVacationTimePerPayPeriod.Hours,
                    tsVacationTimePerPayPeriod.Minutes
                ),
                balance
            );
        }

        private void AddVacationDayLogEntry(VacationDay day, TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "YAY! Vacation day! {0}",
                    day
                ),
                balance
            );
        }

        private void AddClosingLogEntry(int cPayPeriods, TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "Ending balance after adding {0} pay period{1}",
                    cPayPeriods,
                    cPayPeriods == 1 ? "" : "s"
                ),
                balance
            );
        }

        private void AddLogEntry(DateTime dt, TimeSpan balance)
        {
            AddLogEntry(dt.ToLongDateString(), balance);
        }

        private void AddLogEntry(string s, TimeSpan balance)
        {
            var item = new ListViewItem(
                new string[] {
                    s,
                    PrettyPrintTimeSpan(balance, false),
                    PrettyPrintTimeSpan(balance, true)
                }
            );

            var col = balance.Ticks < 0 ? Color.Red : Color.SeaGreen;
            item.UseItemStyleForSubItems = false;
            item.SubItems[1].ForeColor = col;
            item.SubItems[2].ForeColor = col;

            lstLog.Items.Add(item);
        }
    }
}
