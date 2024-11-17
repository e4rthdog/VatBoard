//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<https://www.gnu.org/licenses/>.
//
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace VatsimData
{
    public class VatsimData
    {
        [JsonProperty("general")]
        public General General;

        [JsonProperty("pilots")]
        public Pilot[] Pilots;

        [JsonProperty("controllers")]
        public Controller[] Controllers;

        [JsonProperty("atis")]
        public Atis[] Atis;

        [JsonProperty("servers")]
        public Server[] Servers;

        [JsonProperty("prefiles")]
        public Prefile[] Prefiles;

        [JsonProperty("facilities")]
        public Facility[] Facilities;

        [JsonProperty("ratings")]
        public Rating[] Ratings;

        [JsonProperty("pilot_ratings")]
        public PilotRating[] PilotRatings;

        public static VatsimData Data = new VatsimData();

        public static void GetData(string url)
        {
            var webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/json";
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var jsonData = sr.ReadToEnd();
                    Data = JsonConvert.DeserializeObject<VatsimData>(jsonData);
                }
            }
        }
    }

    public struct General
    {
        [JsonProperty("version")] 
        public int Version { get; set; }
        [JsonProperty("reload")]
        public int Reload { get; set; }
        [JsonProperty("update")]
        public string Update { get; set; }
        [JsonProperty("update_timestamp")]
        private string update_timestamp;
        public DateTime UpdateTimestamp { get=> DateTime.Parse(update_timestamp); }
        [JsonProperty("connected_clients")]
        public int ConnectedClients { get; set; }
        [JsonProperty("unique_users")]
        public int UniqueUsers { get; set; }
    }
}
