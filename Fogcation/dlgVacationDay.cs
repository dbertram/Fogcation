using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fogcation
{
    public partial class dlgVacationDay : Form
    {
        public dlgVacationDay()
        {
            InitializeComponent();

            cboDayType.SelectedIndex = (int)DayType.FullDay;
        }
        
        public VacationDay VacationDay
        {
            get
            {
                var day = new VacationDay();
                day.Dt = dt.Value;
                day.DayType = (DayType)cboDayType.SelectedIndex;
                return day;
            }
        }

        internal DayType DayType
        {
            get
            {
                return (DayType)cboDayType.SelectedIndex;
            }

            set
            {
                cboDayType.SelectedIndex = (int)value;
            }
        }
    }
}
