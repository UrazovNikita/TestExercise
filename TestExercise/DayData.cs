using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestExercise
{
    class DayData : INotifyPropertyChanged
    {
        public DayData()
        {
            ReadFiles();
        }
        public DayData(int number)
        {
            dayNumber = number;
        }
        public int dayNumber { get; private set; }

        private ObservableCollection<PersonData> _personDataDayList;

        private static ObservableCollection<DayData> _allDaysList = new ObservableCollection<DayData>();
        public static ObservableCollection<DayData> AllDaysList
        {
            get
            {
                return _allDaysList;
            }
            private set
            {

            }
        }

        public ObservableCollection<PersonData> PersonDataDayList
        {
            get
            {
                return _personDataDayList;
            }
            private set
            {

            }
        }


        public static void ReadFiles(string path = "TestData")
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo[] allFiles = directory.GetFiles();
                var decimalOrder = allFiles.OrderBy(n => Regex.Replace(n.Name, @"\d+", n => n.Value.PadLeft(4, '0'))); //getting files in "human order" by name
                foreach (FileInfo fileInfo in decimalOrder)
                {
                    string fileName = fileInfo.Name;
                    string[] digits = Regex.Split(fileName, @"\D+");
                    foreach (string value in digits)
                    {
                        if (int.TryParse(value, out int number))
                        {
                            DayData temp = new DayData(number);
                            _allDaysList.Add(temp);
                            string json = File.ReadAllText(fileInfo.FullName);
                            temp._personDataDayList = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<PersonData>>(json);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

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
