using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Fogcation
{
    [Serializable]
    public class VacationData
    {
        [XmlIgnore]
        public string sFileName = null;
        [XmlIgnore]
        public bool fLoading = false;
        [XmlIgnore]
        public bool fDirty = false;

        [XmlIgnore]
        public Version FileFormat = frmMain.fileFormat;

        [XmlAttribute("FileFormat")]
        public string XmlFileFormat
        {
            get { return FileFormat.ToString(); }
            set { FileFormat = new Version(value); }
        }

        public DateTime StartDate;
        public DateTime BoostDate;

        [XmlIgnore]
        public TimeSpan StartBalance;

        [XmlElement("StartBalance", DataType = "duration")]
        [DefaultValue("PT0M")]
        public string XmlStartBalance
        {
            get { return XmlConvert.ToString(StartBalance); }
            set { StartBalance = XmlConvert.ToTimeSpan(value); }
        }

        public List<VacationDay> VacationDays = new List<VacationDay>();
        public DateTime TargetDate;

        public static bool Migrate(VacationData data) {
            var migrationSummary = new List<string>();
            migrationSummary.Add(String.Format("Upgrading from file format {0}:\n", data.FileFormat));

            switch (data.FileFormat.ToString())
            {
                case "1.0":
                    data.BoostDate = new DateTime(Math.Max(data.StartDate.AddYears(3).Ticks, new DateTime(2013, 1, 1).Ticks));
                    migrationSummary.Add(String.Format("Vacation boost start date set to {0}.", data.BoostDate.ToString(frmMain.sLongDateFormat)));
                    break;
            }

            data.FileFormat = frmMain.fileFormat;
            data.fDirty = true;

            MessageBox.Show(String.Join("\n", migrationSummary.ToArray()), "Upgrade successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }
    }
}
