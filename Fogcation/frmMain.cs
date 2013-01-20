using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Fogcation
{
    public partial class frmMain : Form
    {
        enum LogEntryType {
            Generic = -1,
            PayPeriod,
            VacationDay,
            UnpaidLeave
        }

        // the number of hours that counts as a work day
        internal static int cHoursInAWorkDay = 8;
        // the default long-form date format
        internal static string sLongDateFormat = "MMMM d, yyyy";
        // the current file format version
        internal static Version fileFormat = new Version(1, 0);

        // the amount of vacation time accrued per pay period
        private static TimeSpan tsVacationTimePerPayPeriod = new TimeSpan(6, 40, 0);
        private dlgVacationDay dlgVacationDay = new dlgVacationDay();

        private VacationData data = new VacationData();
        XmlSerializer serializer = new XmlSerializer(typeof(VacationData));

        public frmMain()
        {
            InitializeComponent();

            lstVacation.ListViewItemSorter = new VacationListViewSorter();
            
            ResizeControls();

            var sDefaultData = ConfigurationManager.AppSettings["DefaultVacationDataFile"];
            LoadData(File.Exists(sDefaultData) ? sDefaultData : null);
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            ScrollToBottom(lstVacation);
            ScrollToBottom(lstLog);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void splitLists_SplitterMoved(object sender, SplitterEventArgs e)
        {
            ResizeControls();
        }

        #region File Menu

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            if (PrepForClose())
            {
                LoadData(null);
            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (PrepForClose() && dlgOpen.ShowDialog(this) == DialogResult.OK)
            {
                LoadData(dlgOpen.FileName);
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            SaveData(false);
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PrepForClose()) e.Cancel = true;
        }

        private bool PrepForClose()
        {
            if (data.fDirty)
            {
                var result = MessageBox.Show("Save changes?", "Save vacation data?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    return false;
                }
                else if (result == DialogResult.Yes)
                {
                    if (!SaveData(false))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool SaveData(bool fForceDialog)
        {
            if (!(String.IsNullOrEmpty(data.sFileName) || data.fDirty || fForceDialog)) return true; // already saved!

            if (String.IsNullOrEmpty(data.sFileName) || fForceDialog)
            {
                if (dlgSave.ShowDialog(this) == DialogResult.OK)
                {
                    data.sFileName = dlgSave.FileName;
                }
                else if (fForceDialog)
                {
                    // if they force the save dialog, but cancel out of it, bail
                    return false;
                }
            }

            if (!String.IsNullOrEmpty(data.sFileName))
            {
                TextWriter WriteFileStream = new StreamWriter(data.sFileName);
                serializer.Serialize(WriteFileStream, data);
                WriteFileStream.Close();
                data.fDirty = false;
                return true;
            }

            return false;
        }

        private void LoadData(string sFileName)
        {
            var fSuccessfulLoad = false;

            if (sFileName != null)
            {
                try
                {
                    FileStream ReadFileStream = new FileStream(sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    data = serializer.Deserialize(ReadFileStream) as VacationData;
                    ReadFileStream.Close();

                    if (data.FileFormat > fileFormat)
                    {
                        throw new InvalidDataException(String.Format("The specified file is file format version {0}. This applicatioin can only open file format version {1} and earlier", data.FileFormat, fileFormat));
                    }

                    fSuccessfulLoad = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ruh roh...\n\n" + ex.ToString(), "Load error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            if (!fSuccessfulLoad)
            {
                data = new VacationData();
                data.StartDate = data.TargetDate = DateTime.Now.Date;
            }

            dlgVacationDay.dt.Value = data.TargetDate.Date;

            data.fLoading = true;
            dtCurr.Value = data.StartDate.Date;
            txtCurrBalance.Text = String.Format("{0}:{1}", (int)data.StartBalance.TotalHours, Math.Abs(data.StartBalance.Minutes));
            lstVacation.Items.Clear();
            foreach (var day in data.VacationDays)
            {
                AddVacationDay(day);
            }
            dtFuture.Value = data.TargetDate.Date;
            data.fLoading = false;
            CalculateBalance();
        }

        #endregion

        private void dtFuture_ValueChanged(object sender, EventArgs e)
        {
            if (!data.fLoading)
            {
                data.TargetDate = dtFuture.Value.Date;
                data.fDirty = true;
                CalculateBalance();
            }
        }

        private void txtCurrBalance_TextChanged(object sender, EventArgs e)
        {
            TimeSpan? ts = TimeSpanFromBalance(txtCurrBalance.Text);
            if (!data.fLoading)
            {
                if(ts.HasValue)
                {
                    data.StartBalance = ts.Value;
                }
                else
                {
                    data.StartBalance = TimeSpan.Zero;
                }
                data.fDirty = true;
                CalculateBalance();
            }
        }

        private void dtCurr_ValueChanged(object sender, EventArgs e)
        {
            if (!data.fLoading)
            {
                data.StartDate = dtCurr.Value.Date;
                data.fDirty = true;
                CalculateBalance();
            }
        }

        #region Buttons for lstVacation

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dt = dlgVacationDay.dt.Value;
            dlgVacationDay.DayType = DayType.FullDay;

            if (dlgVacationDay.ShowDialog(this) == DialogResult.OK)
            {
                AddVacationDay(dlgVacationDay.VacationDay);
            }
            else
            {
                // don't preserve the last entered date if they canceled
                dlgVacationDay.dt.Value = dt;
            }
        }

        private void AddVacationDay(VacationDay dayNew)
        {
            // check for dupes!
            foreach (ListViewItem viewitem in lstVacation.Items)
            {
                if ((viewitem.Tag as VacationDay).Dt.Date == dayNew.Dt.Date)
                {
                    MessageBox.Show(
                        "That date has already been added!",
                        "Cannot add " + dayNew.Dt.ToString(sLongDateFormat),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }
            }

            var item = new ListViewItem(
                new string[] {
                    dayNew.Dt.ToString(sLongDateFormat),
                    VacationDay.PrettyPrintDayType(dayNew.DayType),
                    dayNew.DayType == DayType.CompanyHoliday ? "" : PrettyPrintTimeSpan(dayNew.Hours, false)
                }
            );
            item.Group = GetListViewGroup(lstVacation, dayNew.Dt.Year.ToString());;

            item.UseItemStyleForSubItems = false;
            item.SubItems[2].ForeColor = Color.Red;

            item.Tag = dayNew; // store the VacationDay object for later

            lstVacation.Items.Add(item);
            lstVacation.Sort();
            item.EnsureVisible();

            if (!data.fLoading)
            {
                data.VacationDays.Add(dayNew);
                data.fDirty = true;
                CalculateBalance();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstVacation.SelectedItems.Count < 1) return;

            var itemsToDelete = new List<ListViewItem>();
            var daysToDelete = new List<VacationDay>();
            foreach (ListViewItem item in lstVacation.SelectedItems)
            {
                itemsToDelete.Add(item);
                daysToDelete.Add(item.Tag as VacationDay);
            }
            
            var suffix = itemsToDelete.Count == 1 ? "" : "s";
            var result = MessageBox.Show(
                String.Format(
                    "Are you sure you want to remove the following vacation day{0}?\n\n{1}",
                    suffix,
                    String.Join("\n", daysToDelete.ConvertAll<string>(day => day.ToString()).ToArray())
                ),
                String.Format(
                    "Remove Vacation Day{0}?",
                    suffix
                ),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                itemsToDelete.ForEach(item => item.Remove());

                daysToDelete.ForEach(
                    dayToDelete =>
                        data.VacationDays.Remove(
                            data.VacationDays.Find(vd => vd.Dt.Date == dayToDelete.Dt.Date)
                        )
                );

                data.fDirty = true;
                CalculateBalance();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstVacation.Items.Clear();
            data.VacationDays.Clear();
            data.fDirty = true;
            CalculateBalance();
        }

        #endregion

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
                    lblCurrBalance.ForeColor = currBalance.Value.Ticks < 0 ? Color.Red : Color.SeaGreen;
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

                    var minBalanceForYear = futureBalance;
                    var nYearLast = dt.Year;

                    while ((dt = dt.AddDays(1)) <= dtEnd)
                    {
                        if (dt.Year != nYearLast)
                        {
                            SetYearMin(dt.Year - 1, minBalanceForYear);
                        }

                        if (dictVacation.ContainsKey(dt.Date))
                        {
                            futureBalance += dictVacation[dt.Date].Hours;
                            AddVacationDayLogEntry(dictVacation[dt.Date], futureBalance);
                        }

                        if (futureBalance < minBalanceForYear)
                        {
                            minBalanceForYear = futureBalance;
                        }

                        if (dt.Day == 15 || dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month))
                        {
                            futureBalance += tsVacationTimePerPayPeriod;
                            cPayPeriods++;
                            AddPayPeriodLogEntry(dt, futureBalance);
                        }

                        if (dt.Year != nYearLast)
                        {
                            minBalanceForYear = futureBalance;
                        }
                        nYearLast = dt.Year;
                    }
                    AddClosingLogEntry(cPayPeriods, futureBalance);
                    SetYearMin(dtEnd.Year, minBalanceForYear);
                }
                else
                {
                    errorProvider.SetError(txtCurrBalance, "Invalid balance! Expected hh:mm (e.g. -8:00)");
                    lblCurrBalance.Text = "You're not even trying...";
                }
            }
        }

        private void SetYearMin(int year, TimeSpan minBalanceForYear)
        {
            var group = GetListViewGroup(lstLog, year.ToString());
            group.Header = String.Format("{0}: lowest balance of {1}", year.ToString(), PrettyPrintTimeSpan(minBalanceForYear, true));
        }

        private void ResetCalculatedFields()
        {
            lblCurrBalance.Text = "N/A";
            lblCurrBalance.ForeColor = Color.Black;
            lstLog.Items.Clear();
        }

        private ListViewGroup GetListViewGroup(ListView lst, string sGroupName)
        {
            // we'll need a list of all the groups if we decide to add one later
            var lstGroups = new List<ListViewGroup>();

            // check if there's an existing group with the requested group name
            foreach (ListViewGroup group in lst.Groups)
            {
                if (group.Name == sGroupName)
                {
                    return group;
                }
                lstGroups.Add(group);
            }

            // no group for the specified name, so let's add one (initial header text is the same as the group name)
            var newGroup = new ListViewGroup(sGroupName, sGroupName);
            lstGroups.Add(newGroup);
            lstGroups.Sort((grp1, grp2) => String.Compare(grp1.Name, grp2.Name));

            lst.BeginUpdate();
            lst.Groups.Clear();
            lst.Groups.AddRange(lstGroups.ToArray());
            lst.EndUpdate();

            return newGroup;
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
            var listViews = new ListView[] { lstVacation, lstLog };
            
            foreach(var lst in listViews)
            {
                int otherCols = 0;
                for (int ix = 1; ix < lst.Columns.Count; ix++ )
                {
                    otherCols += lst.Columns[ix].Width;
                }

                // the extra 21 avoids triggering a horizontal scrollbar if/when a vertical scrollbar is needed
                lst.Columns[0].Width = lst.Width - 21 - otherCols;
            }
        }

        private void AddOpeningLogEntry(TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "Starting balance on {0}",
                    dtCurr.Value.ToString(sLongDateFormat)
                ),
                TimeSpan.Zero,
                balance,
                dtCurr.Value.Year,
                LogEntryType.Generic
            );
        }

        private void AddPayPeriodLogEntry(DateTime dt, TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "{0} payday!",
                    dt.ToString(sLongDateFormat),
                    tsVacationTimePerPayPeriod.Hours,
                    tsVacationTimePerPayPeriod.Minutes
                ),
                tsVacationTimePerPayPeriod,
                balance,
                dt.Year,
                LogEntryType.PayPeriod
            );
        }

        private void AddVacationDayLogEntry(VacationDay day, TimeSpan balance)
        {
            AddLogEntry(
                String.Format(
                    "{0}: {1}",
                    day.DayType == DayType.CompanyHoliday ? "Company Holiday" : "Vacation Day",
                    day
                ),
                day.Hours,
                balance,
                day.Dt.Year,
                LogEntryType.VacationDay
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
                TimeSpan.Zero,
                balance,
                dtFuture.Value.Year,
                LogEntryType.Generic
            );
        }

        private void AddLogEntry(string s, TimeSpan delta, TimeSpan balance, int year, LogEntryType type)
        {
            var item = new ListViewItem(
                new string[] {
                    s,
                    delta == TimeSpan.Zero ? "" : PrettyPrintTimeSpan(delta, false),
                    PrettyPrintTimeSpan(balance, false),
                    PrettyPrintTimeSpan(balance, true)
                },
                (int)type
            );
            item.Group = GetListViewGroup(lstLog, year.ToString());
            item.UseItemStyleForSubItems = false;

            var colDelta = delta.Ticks < 0 ? Color.Red : Color.SeaGreen;
            item.SubItems[1].ForeColor = colDelta;

            var colBalance = balance.Ticks < 0 ? Color.Red : Color.SeaGreen;
            item.SubItems[2].ForeColor = colBalance;
            item.SubItems[3].ForeColor = colBalance;

            lstLog.Items.Add(item);
            ScrollToBottom(lstLog);
        }

        private void ScrollToBottom(ListView lst)
        {
            if(lst.Items.Count == 0) return;
            lst.Items[lst.Items.Count - 1].EnsureVisible();
        }

    }
}
