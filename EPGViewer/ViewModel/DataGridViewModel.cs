using EPGViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGViewer.ViewModel
{
    class DataGridViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ShowItem> showList;
        public ObservableCollection<ShowItem> ShowList
        {
            get => showList;
            set
            {
                showList = value;
                OnPropertyChanged("ShowList"); //动态刷新GUI
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
