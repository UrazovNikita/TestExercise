using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExercise
{
    public class PersonDataExport
    {
        public int count;
        public int _averageStepsResult;
        public int _maxStepsResult;
        public int _minStepsResult;
        private int _rank;
        private string _user;
        private string _status;
        private int _steps;

        public int AverageStepsResult
        {
            get { return _averageStepsResult; }
            set
            {
                _averageStepsResult = value;
                OnPropertyChanged("AverageStepsREsult");
            }
        }
        public int MaxStepsResult
        {
            get { return _maxStepsResult; }
            set
            {
                _maxStepsResult = value;
                OnPropertyChanged("MaxStepsResult");
            }
        }
        public int MinStepsResult
        {
            get { return _minStepsResult; }
            set
            {
                _minStepsResult = value;
                OnPropertyChanged("MinStepsResult");
            }
        }
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

        public static explicit operator PersonDataExport(PersonData param)
        {
            return new PersonDataExport
            {
                _user = param.User,
                _rank = param.Rank,
                _status = param.Status,
                _steps = param.Steps
            };
        }
    }
}

