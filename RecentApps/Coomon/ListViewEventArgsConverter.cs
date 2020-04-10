using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.UI;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace RecentApps
{
    public class ListViewEventArgsConverter : EventArgsConverterBase<MouseEventArgs>
    {
        public ListViewEventArgsConverter() { }
        protected override object Convert(object sender, MouseEventArgs args)
        {
            ListView parentElement = (ListView)sender;
            DependencyObject clickedElement = (DependencyObject)args.OriginalSource;
            ListViewItem clickedListBoxItem =
                LayoutTreeHelper.GetVisualParents(child: clickedElement, stopNode: parentElement)
                .OfType<ListViewItem>()
                .FirstOrDefault();
            if (clickedListBoxItem != null)
                return (MenuIDHistory)clickedListBoxItem.DataContext;
            return null;

        }
    }
}
