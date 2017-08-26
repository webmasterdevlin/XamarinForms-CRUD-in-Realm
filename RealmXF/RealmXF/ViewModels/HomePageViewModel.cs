using Realms;
using RealmXF.Annotations;
using RealmXF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealmXF.ViewModels
{
    internal class HomePageViewModel : INotifyPropertyChanged
    {
        private readonly Realm _realmDb;
        private List<Recipe> recipes;
        public ObservableCollection<Recipe> _recipes;
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        
        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HomePageViewModel()
        {
            AddCommand = new Command(Add);
            UpdateCommand = new Command(Update);
            DeleteCommand = new Command(Delete);
            _realmDb = Realm.GetInstance();
            recipes = _realmDb.All<Recipe>().ToList();
            Recipes = new ObservableCollection<Recipe>(recipes);
        }

        private void Add()
        {
            var recipe = new Recipe
            {
                Name = $"Recipe {DateTime.UtcNow.Ticks}",
                Id = _realmDb.All<Recipe>().Count() + 1
            };
            _realmDb.Write(() => { _realmDb.Add(recipe); });

            Recipes.Add(recipe);
        }

        private void Update()
        {
            var i = _realmDb.All<Recipe>().Count() - 1;
            var recipe = Recipes[i];
            _realmDb.Write(()=> { _realmDb.Add(recipe, true).Name += $" [update]" ; });
        }

        private void Delete()
        {
            var i = _realmDb.All<Recipe>().Count() - 1;
            var recipe = Recipes[i];
            using (var transact = _realmDb.BeginWrite())
            {
                _realmDb.Remove(recipe);
                transact.Commit();
            }
            Recipes.Remove(recipe);
        }
    }
}