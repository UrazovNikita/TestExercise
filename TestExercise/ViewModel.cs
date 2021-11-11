using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
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
using System.Windows;

namespace TestExercise
{
    public class ViewModel : INotifyPropertyChanged, IDialogService
    {
        private PersonDataExport _selectedPerson = new PersonDataExport();
        public PersonDataExport SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
                ChartUpdateNotify();

            }
        }        
        public string FilePath { get; set; }
       
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
        public void WriteToDisk()
        {
            SaveFileDialog();
            File.WriteAllText(@FilePath+".json", JsonConvert.SerializeObject(SelectedPerson));
            using (StreamWriter file = File.CreateText(@FilePath + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, SelectedPerson);
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
                tempCollection[i].StepsCollection.Add(tempCollection[i].Steps);
                for (int j = i + 1; j < tempCollection.Count - 1; j++)
                {
                    if (tempCollection[i].Steps != 0)
                    {

                        if (tempCollection[i].User == tempCollection[j].User)
                        {
                            tempCollection[i].MaxStepsResult = tempCollection[i].Steps;
                            tempCollection[i].MinStepsResult = tempCollection[i].Steps;

                            tempCollection[i].StepsCollection.Add(tempCollection[j].Steps);

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
            foreach (var person in tempCollection)
            {
                if (((person.AverageStepsResult * 0.2) > person.MaxStepsResult) || ((person.AverageStepsResult * 0.2) < person.MinStepsResult))
                {
                    person.DiffrenceBetweenAverage = true;
                }
            }
            _personsData = tempCollection;
        }

        delegate void SelectedPersonHandler();
        event SelectedPersonHandler ChartUpdateNotify;
        public ViewModel()
        {
            ChartUpdateNotify += UpdateChart;
            SetCollection();
        }

        public void UpdateChart()
        {
            SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Values = new ChartValues<int> (SelectedPerson.StepsCollection)

                    }
                };
            //Labels = new[] {"0","1","2","3","4", "5" };
            Formatter = value => value.ToString("N");
        }
        //public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get
            {
                return _seriesCollection;
            }
            set
            {

                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }
        private Command _writeCommand;
        public Command WriteCommand
        {
            get
            {
                return _writeCommand ??
                    (_writeCommand = new Command(obj =>
                    {
                        WriteToDisk();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

