using EPGViewer.Model;
using EPGViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace EPGViewer
{
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            DateSelector.SelectedDate = DateTime.Now;
        }

        private void SearchBar_OnSearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            string key = e.Info;
            if (!(sender is FrameworkElement searchBar && searchBar.Tag is DataGrid dataGrid))
            {
                return;
            }

            if (string.IsNullOrEmpty(key))
            {
                foreach (ShowItem item in dataGrid.Items)
                {
                    var row = dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    row.Visibility = Visibility.Visible;
                }
            }
            else
            {
                key = key.ToLower();
                foreach (ShowItem item in dataGrid.Items)
                {
                    string name = item.Name.ToLower();
                    var row = dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (name.Contains(key))
                        row.Visibility = Visibility.Visible;
                    else
                        row.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void ComboBox_Channel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataGrid_ShowList == null) return;
            ComboBox_ChannelType.IsEnabled = false;
            ComboBox_Channel.IsEnabled = false;
            DateSelector.IsEnabled = false;
            DataGrid_ShowList.IsEnabled = false;
            try
            {
                if ((ComboBox_ChannelType.SelectedItem as ChannelGroup)?.Name != "广东频道")
                    (DataGrid_ShowList.DataContext as DataGridViewModel).ShowList = await Util.GetShowList(ComboBox_Channel.SelectedItem as ChannelItem, DateSelector.SelectedDate.Value);
                else
                    (DataGrid_ShowList.DataContext as DataGridViewModel).ShowList = await Util.GetGDShowList(ComboBox_Channel.SelectedItem as ChannelItem, DateSelector.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataGrid_ShowList.IsEnabled = true;
            DateSelector.IsEnabled = true;
            ComboBox_Channel.IsEnabled = true;
            ComboBox_ChannelType.IsEnabled = true;
        }

        private void DoForamt(string template)
        {
            var selected = DataGrid_ShowList.SelectedItems.Cast<ShowItem>().ToArray();
            template = Util.FormatItems((ComboBox_Channel.SelectedItem as ChannelItem).Name, template, Convert.ToInt32(MarginMinutes.Value), selected);
            Clipboard.SetText(template);
            MessageBox.Show(template, "已复制");
        }

        private void DataGrid_ShowList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataGrid_ShowList.SelectedItem != null)
            {
                DoForamt(Txt_DoubleClickCopy.Text);
            }
        }

        private void CopyCustom1(object sender, RoutedEventArgs e)
        {
            if (DataGrid_ShowList.SelectedItems != null)
            {
                DoForamt(Txt_DoubleClickCopy.Text);
            }
        }

        private void CopyCustom2(object sender, RoutedEventArgs e)
        {
            if (DataGrid_ShowList.SelectedItems != null)
            {
                DoForamt(Txt_DoubleClickCopy2.Text);
            }
        }
    }
}
