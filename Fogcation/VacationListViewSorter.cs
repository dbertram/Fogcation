using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Fogcation
{
    public class VacationListViewSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var listviewX = (ListViewItem)x;
            var listviewY = (ListViewItem)y;

            var dayX = listviewX.Tag as VacationDay;
            var dayY = listviewY.Tag as VacationDay;

            // Compare the two items
            return dayX.Dt.CompareTo(dayY.Dt);
        }
    }
}
