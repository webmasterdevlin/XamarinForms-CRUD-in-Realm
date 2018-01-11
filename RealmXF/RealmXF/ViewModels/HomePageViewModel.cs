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
    internal class HomePageViewModel : ViewModelBase
    {
        private readonly Realm _realmDb;
        private ObservableCollection<Recipe> _observableRecipes;

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<Recipe> Recipes
        {
            get => _observableRecipes;
            set
            {
                _observableRecipes = value;
                OnPropertyChanged();
            }
        }

        public HomePageViewModel()
        {
            AddCommand = new Command(Add);
            UpdateCommand = new Command(Update);
            DeleteCommand = new Command(Delete);
            _realmDb = Realm.GetInstance();
            List<Recipe> recipes = _realmDb.All<Recipe>().ToList();
            Recipes = new ObservableCollection<Recipe>(recipes);
        }

        private void Add()
        {
            Recipe recipe = new Recipe
            {
                Name = $"Recipe {DateTime.UtcNow.Ticks}",
                Id = _realmDb.All<Recipe>().Count() + 1
            };
            _realmDb.Write(() => { _realmDb.Add(recipe); });

            Recipes.Add(recipe);
        }

        private void Update()
        {
            int i = _realmDb.All<Recipe>().Count() - 1;
            Recipe recipe = Recipes[i];
            _realmDb.Write(() => { _realmDb.Add(recipe, true).Name += " [update]"; });
        }

        private void Delete()
        {
            int i = _realmDb.All<Recipe>().Count() - 1;
            Recipe recipe = Recipes[i];
            using (Transaction transact = _realmDb.BeginWrite())
            {
                _realmDb.Remove(recipe);
                transact.Commit();
            }
            Recipes.Remove(recipe);
        }
    }
}