using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using ApiLibrary;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using StarWarsApiApp.Models.Data;

namespace StarWarsApiApp.ViewModels {
    public class HistoryViewModel : BindableBase {
        private CollectionViewSource swPeople;
        public ICommand WindowLoaded { get; set; }
        public CollectionViewSource SWPeople {
            get => swPeople;
            set => SetProperty(ref swPeople, value);
        }
        public HistoryViewModel() {
            SWPeople = new CollectionViewSource();
            WindowLoaded = new DelegateCommand(OnLoad,()=>true);
        }
        private async void OnLoad() {
            await LoadDataDb();
        }
        private async Task LoadDataDb() { 
            await using var dbContext = new StarWarsDBContext();
            SWPeople.Source = dbContext.People.ToList();
        }
    }
}