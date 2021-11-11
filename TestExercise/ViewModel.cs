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
        private PersonDataExport _selectedPerson;
        public PersonDataExport SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        private DayData _dayData = new DayData();

        private ObservableCollection<PersonDataExport> _personsData;
        public ObservableCollection<PersonDataExport> PersonsData           
        {
           get
            {
                return _personsData;
            }
            private set
            {

            }
        }

        public void SetCollection()
        {
            var sourceCollection = DayData.AllDaysList.SelectMany(p => p.PersonDataDayList).ToList();

            ObservableCollection<PersonDataExport> tempCollection = new ObservableCollection<PersonDataExport>();
            foreach (var item in sourceCollection)
            {
                tempCollection.Add((PersonDataExport)item);
            }

            for (int i = 0; i < tempCollection.Count; i++)
            {

                for (int j = i + 1; j < tempCollection.Count - 1; j++)
                {
                    if (tempCollection[i].Steps != 0)
                    {
                        if (tempCollection[i].User == tempCollection[j].User)
                        {
                            tempCollection[i].MaxStepsResult = tempCollection[i].Steps;
                            tempCollection[i].MinStepsResult = tempCollection[i].Steps;

                            if (tempCollection[j].Rank > tempCollection[i].Rank)
                            {
                                tempCollection[i].Rank = tempCollection[j].Rank;
                            }
                            if (tempCollection[j].Steps > tempCollection[i].MaxStepsResult)
                            {
                                tempCollection[i].MaxStepsResult = tempCollection[j].Steps;
                            }
                            if (tempCollection[j].Steps < tempCollection[i].MinStepsResult)
                            {
                                tempCollection[i].MinStepsResult = tempCollection[j].Steps;
                            }
                            tempCollection[i].Steps += tempCollection[j].Steps;
                            tempCollection[i].AverageStepsResult = tempCollection[i].Steps / 2;
                            tempCollection[j].Steps = 0;
                        }
                    }
                }
            }
            tempCollection.Remove(x => x.Steps == 0);
            _personsData = tempCollection;
        }

        
        public ViewModel()
        {

            SetCollection();

            ////таким нехитрым способом мы пробрасываем изменившиеся свойства модели во View
            //_model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            //AddCommand = new DelegateCommand<string>(str => {
            //    //проверка на валидность ввода - обязанность VM
            //    int ival;
            //    if (int.TryParse(str, out ival)) _model.AddValue(ival);
            //});
        }

        //public DelegateCommand<string> AddCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

