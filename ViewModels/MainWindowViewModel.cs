using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ApiLibrary;
using Prism.Commands;
using Prism.Mvvm;
using StarWarsApiApp.Models.Data;
using StarWarsApiApp.Views;

namespace StarWarsApiApp.ViewModels {
    public class MainWindowViewModel : BindableBase {
        private ObservableCollection<People> swPeople;
        public ObservableCollection<People> SWPeople {
            get => swPeople;
            set => SetProperty(ref swPeople, value);
        }
        public ICommand FindCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        private string currentPath;
        public string CurrentPath {
            get => currentPath;
            set => SetProperty(ref currentPath, value);
        }
        public MainWindowViewModel() {
            ApiHelper.InitializeClient();
            SWPeople = new ObservableCollection<People>();
            FindCommand = new DelegateCommand(Find,()=>true);
            HistoryCommand = new DelegateCommand(OpenHistory,()=>true);
            SaveCommand = new DelegateCommand(SaveData, () => true);

        }

        private async void SaveData() {
            if (SWPeople.Count == 0) {
                return;
            }
            await using var dbContext = new StarWarsDBContext();
            await dbContext.People.AddRangeAsync(SWPeople);
            await dbContext.SaveChangesAsync();
            MessageBox.Show("Data are successfully saved");
        }

        private static void OpenHistory() {
            var historyView = new HistoryView();
            historyView.Show();
        }

        private async void Find() {
            await LoadData(CurrentPath);
        }
        private async Task LoadData(string path) {
            SWPeople.Clear();
            var starWarsPerson = await StarWarsPersonProcessor.LoadPersonInformation(path);
            foreach (var people in starWarsPerson) {
                people.CurrentDate = DateTime.Now;
            }
            SWPeople.AddRange(starWarsPerson);
        }
    }
}