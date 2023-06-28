using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels
{
    public class PersonPageViewModel : ViewModelBase
    {
        private Person _person;
        public ObservableCollection<string> NotableCharacters { get; set; } = new ObservableCollection<string>();
        private readonly string imagePath = "https://image.tmdb.org/t/p/w500";

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { Set(ref errorMessage, value); }
        }

        public Person Person 
        {
            get { return _person; }
            set { Set(ref _person, value);}
        }

        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var personId = (int)parameter;
            var service = new MovieService();
            var pesronResponse = await service.GetPersonAsync(personId);
            
            if (pesronResponse.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                Person = pesronResponse.Data;
                ErrorMessage = "";
                FormatPerson();

                var creditsResponse = await service.GetNotableCharactersAsync(personId);
                if (creditsResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var list = creditsResponse.Data.cast;
                    foreach (var item in list.Take(10))
                    {
                        NotableCharacters.Add(item.character);
                    }
                } 
                else
                    ErrorMessage = "Error message: " + creditsResponse.StatusCode;
            }
            else
            {
                Person = null;
                ErrorMessage = "Error message: "+ pesronResponse.StatusCode;
            }
            

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// A személy adatainak formázása, képek helyettesítése placeholderekkel.
        /// </summary>
        private void FormatPerson()
        {
            if (string.IsNullOrEmpty(Person.biography))
                Person.biography = "We do not have a biography for " + $"{Person.name}";
            if (!string.IsNullOrEmpty(Person.profile_path))
                Person.profile_path = imagePath + Person.profile_path;
            else
                Person.profile_path = "https://ongaropc.com/wp-content/uploads/2023/03/avatar-placeholder.jpg";
        }
    }
}
