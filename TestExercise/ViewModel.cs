using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestExercise
{
    public class ViewModel : INotifyPropertyChanged
    {
        private PersonData selectedPerson;

        public ObservableCollection<PersonData> PersonDatas { get; set; }
        public PersonData SelectedPerson
        {
            get { return selectedPerson;}
            set
            {
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public ViewModel()
        {
           
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

