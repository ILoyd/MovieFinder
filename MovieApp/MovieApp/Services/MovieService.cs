using MovieApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services

{
    public class Response<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class MovieService
    {
        private readonly Uri baseUri = new Uri("https://api.themoviedb.org/3/");
        private readonly string apiKey = "9eb2d44baf8dbc9f56d9c87307a29c42";

        /// <summary>
        /// Aszinkron kéréseket rak össze és küldi el a megadott urira.
        /// </summary>
        /// <param name="uri">Az api alap uri-ja.</param>
        /// <param name="param">A kérés paraméterei.</param>
        /// <returns>A kérés eredménye.</returns>
        private async Task<Response<T>> GetAsync<T>(Uri uri, string param = "")
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                HttpStatusCode statusCode = response.StatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    return new Response<T> { Data = default(T), StatusCode = statusCode };
                }
                var json = await response.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = null;
                if (param != "details")
                    settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        Converters = new List<JsonConverter> { new ShowHeaderConverter() }
                    };
                T result = JsonConvert.DeserializeObject<T>(json, settings);
                
                return new Response<T> { Data = result, StatusCode = statusCode };
            }
        }

        /// <summary>
        /// Aszinkron módon elkéri a trendingen lévő filmeket/tv műsorokat.
        /// </summary>
        /// <param name="ph">A trending adatait: a műsor típusa és a trending ideje.</param>
        /// <returns>A filmek/tv műsorok listája.</returns>
        public async Task<Response<Page>> GetTrendingShowsAsync(PageHeader ph)
        {
            return await GetAsync<Page>(new Uri(baseUri, $"trending/{ph.MediaType}/{ph.DayOrWeek}?api_key={apiKey}"));
        }


        /// <summary>
        /// Aszinkron módon elkéri a filmeket népszerűségi sorrendben.
        /// </summary>
        /// <param name="pageNumber">Az oldalszám.</param>
        /// <returns>A filmek listája.</returns>
        public async Task<Response<Page>> GetPopularMoviesAsync(int pageNumber = 1)
        {
            return await GetAsync<Page>(new Uri(baseUri, $"movie/popular?api_key={apiKey}&language=en-US&page={pageNumber}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri a tv műsorokat népszerűségi sorrendben.
        /// </summary>
        /// <param name="pageNumber">Az oldalszám.</param>
        /// <returns>A tv műsorok listája.</returns>
        public async Task<Response<Page>> GetPopularSeriesAsync(int pageNumber = 1)
        {
            return await GetAsync<Page>(new Uri(baseUri, $"tv/popular?api_key={apiKey}&language=en-US&page={pageNumber}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri egy film részletesebb adatait.
        /// </summary>
        /// <param name="showId">A tv műsor egyedi azonosítója.</param>
        /// <returns>A film adatai.</returns>
        public async Task<Response<Movie>> GetMovieDetailsAsync(int showId)
        {
            return await GetAsync<Movie>(new Uri(baseUri, $"movie/{showId}?api_key={apiKey}"), "details");
        }

        /// <summary>
        /// Aszinkron módon elkéri egy tv műsor részletesebb adatait.
        /// </summary>
        /// <param name="showId">A tv műsor egyedi azonosítója.</param>
        /// <returns>A tv műsor adatai.</returns>
        public async Task<Response<Series>> GetSeriesDetailsAsync(int showId)
        {
            return await GetAsync<Series>(new Uri(baseUri, $"tv/{showId}?api_key={apiKey}"), "details");       
        }

        /// <summary>
        /// Aszinkron módon elkéri keresett filmeket.
        /// </summary>
        /// <param name="ph">A keresés paraméterei: keresés kulcsa és az oldalszám.</param>
        /// <returns>A keresés paramétereire illeszkedő filmek litája.</returns>
        public async Task<Response<Page>> GetSearchedMoviesAsync(PageHeader ph)
        {
            return await GetAsync<Page>(new Uri(baseUri, $"search/movie?api_key={apiKey}&query={ph.SearchKey}&page={ph.PageNumber}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri keresett tv műsorokat.
        /// </summary>
        /// <param name="ph">A keresés paraméterei: keresés kulcsa és az oldalszám.</param>
        /// <returns>A keresés paramétereire illeszkedő tv műsorok litája.</returns>
        public async Task<Response<Page>> GetSearchedSeriesAsync(PageHeader ph)
        {
            return await GetAsync<Page>(new Uri(baseUri, $"search/tv?api_key={apiKey}&query={ph.SearchKey}&page={ph.PageNumber}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri egy film szereplőinek listáját.
        /// </summary>
        /// <param name="showId">A film egyedi azonosítója.</param>
        /// <returns>A szereplők litája.</returns>
        public async Task<Response<ShowCredits>> GetCastAsync(int showId)
        {
            return await GetAsync<ShowCredits>(new Uri(baseUri, $"movie/{showId}/credits?api_key={apiKey}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri egy személy adatait.
        /// </summary>
        /// <param name="personId">A személy egyedi azonosítója.</param>
        /// <returns>A személy adatai.</returns>
        public async Task<Response<Person>> GetPersonAsync(int personId)
        {
            return await GetAsync<Person>(new Uri(baseUri, $"person/{personId}?api_key={apiKey}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri egy személy híresebb szerepeit.
        /// </summary>
        /// <param name="personId">A személy egyedi azonosítója.</param>
        /// <returns>A személy híresebb szerepei.</returns>
        public async Task<Response<ShowCredits>> GetNotableCharactersAsync(int personId)
        {
            return await GetAsync<ShowCredits>(new Uri(baseUri, $"person/{personId}/combined_credits?api_key={apiKey}"));
        }

        /// <summary>
        /// Aszinkron módon elkéri egy tv műsor adott évadának részeit.
        /// </summary>
        /// <param name="showId">Az tv műsor egyedi azonosítója.</param>
        /// <param name="seasonNumber">Az évad sorszáma.</param>
        /// <returns>Egy adott évad epizódjainak listája.</returns>
        public async Task<Response<SeasonDetails>> GetEpisodesAsync(int showId, int seasonNumber)
        {
            return await GetAsync<SeasonDetails>(new Uri(baseUri, $"tv/{showId}/season/{seasonNumber}?api_key={apiKey}"));
        }

    }
}
