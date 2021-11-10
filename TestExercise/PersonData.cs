using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestExercise
{
    public class PersonData : INotifyPropertyChanged
    {
        private int _rank;
        private string _user;
        private string _status;
        private int _steps;

        public int Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
                OnPropertyChanged("Rank");
            }
        }
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        public int Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                OnPropertyChanged("Steps");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
