using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
    }
}
