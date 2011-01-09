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
    public partial class Main : Form
    {
        // the amount of vacation time accrued per pay period
        private static TimeSpan tsVacationTimePerPayPeriod = new TimeSpan(6, 40, 0);
        // the number of hours that counts as a work day
        private static int cHoursInAWorkDay = 8;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dtCurr.Value = DateTime.Now;
            dtFuture.Value = DateTime.Now;
            UpdateLogColumns();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            UpdateLogColumns();
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

                    var futureBalance = currBalance.Value;

                    int cPayPeriods = 0;
                    DateTime dt = new DateTime(dtCurr.Value.Year, dtCurr.Value.Month, dtCurr.Value.Day, 12, 0, 0);
                    DateTime dtEnd = new DateTime(dtFuture.Value.Year, dtFuture.Value.Month, dtFuture.Value.Day, 14, 0, 0);
                    while ((dt = dt.AddDays(1)) <= dtEnd)
                    {
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

        private void UpdateLogColumns()
        {
            // the extra 21 avoids triggering a horizontal scrollbar if/when a vertical scrollbar is needed
            lstLog.Columns[0].Width = lstLog.Width - 21 - lstLog.Columns[1].Width - lstLog.Columns[2].Width;
        }

        private void AddOpeningLogEntry(TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "Starting balance on {0}",
                    dtCurr.Value.ToLongDateString()
                ),
                balance
            );
        }

        private void AddPayPeriodLogEntry(DateTime dt, TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "{0} payday! +({1}h {2}m)",
                    dt.ToString("MMMM d, yyyy"),
                    tsVacationTimePerPayPeriod.Hours,
                    tsVacationTimePerPayPeriod.Minutes
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
