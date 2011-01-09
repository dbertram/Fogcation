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

            cboPercentage.SelectedIndex = (int)Percentage.FullDay;
        }
        
        public VacationDay VacationDay
        {
            get
            {
                var day = new VacationDay();
                day.Dt = dt.Value;
                day.DayType = (Percentage)cboPercentage.SelectedIndex;
                return day;
            }
        }

        internal Percentage Percentage
        {
            get
            {
                return (Percentage)cboPercentage.SelectedIndex;
            }

            set
            {
                cboPercentage.SelectedIndex = (int)value;
            }
        }
    }
}
