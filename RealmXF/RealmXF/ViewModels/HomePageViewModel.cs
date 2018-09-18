using Realms;
using RealmXF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RealmXF.ViewModels
{
    internal class HomePageViewModel : ViewModelBase
    {
        private readonly Realm _realmDb;
        private ObservableCollection<Recipe> _observableRecipes;

        public ICommand AddCommand => new Command(Add); // Commands are the one you bind in your Xaml
        public ICommand UpdateCommand => new Command(Update);
        public ICommand DeleteCommand => new Command(Delete);

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
            _realmDb = Realm.GetInstance();
            List<Recipe> recipes = _realmDb.All<Recipe>().ToList();
            Recipes = new ObservableCollection<Recipe>(recipes);
        }

        private void Add()
        {
            Recipe recipe = new Recipe
            {
                Name = $"Recipe {DateTime.UtcNow.Ticks}",
                Id = Guid.NewGuid().ToString()
            };
            _realmDb.Write(() => { _realmDb.Add(recipe); });

            Recipes.Add(recipe);
        }

        private void Update()
        {
            if (Recipes.Count == 0)
            {
                return;
            }

            int i = _realmDb.All<Recipe>().Count() - 1;
            Recipe recipe = Recipes[i];
            _realmDb.Write(() => { _realmDb.Add(recipe, true).Name += " [update]"; });
        }

        private void Delete()
        {
            if (Recipes.Count == 0)
            {
                return;
            }

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