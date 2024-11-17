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

using Newtonsoft.Json;
using System;
using System.Linq;

namespace VatsimData
{
    public class Atis
    {
        [JsonProperty("facility")]
        int facilityNum;
        [JsonProperty("rating")]
        int ratingNum;
        [JsonProperty("server")]
        string server;
        [JsonProperty("cid")]
        public int CID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("callsign")]
        public string Callsign { get; set; }
        [JsonProperty("frequency")]
        public string Frequency { get; set; }
        [JsonProperty("visual_range")]
        public int VisualRange { get; set; }
        [JsonProperty("atis_code")]
        public string AtisCode { get; set; }
        [JsonProperty("text_atis")]
        public string[] TextAtis { get; set; }
        [JsonProperty("logon_time")]
        private string logon_time;
        public DateTime LogonTime { get => DateTime.Parse(logon_time); }
        [JsonProperty("last_updated")]
        private string last_updated;
        public DateTime Last_Updated { get => DateTime.Parse(last_updated); }

        public Facility Facility
        {
            get
            {
                return VatsimData.Data.Facilities.Where(x => x.ID == facilityNum).FirstOrDefault();
            }
            set
            {
                facilityNum = value.ID;
            }
        }

        public Rating Rating
        {
            get
            {
                return VatsimData.Data.Ratings.Where(x => x.ID == ratingNum).FirstOrDefault();
            }
            set
            {
                ratingNum = value.ID;
            }
        }

        public Server Server
        {
            get
            {
                return VatsimData.Data.Servers.Where(x => x.Ident == server).FirstOrDefault();
            }
            set
            {
                server = value.Ident;
            }
        }
        public override string ToString()
        {
            return Callsign;
        }
        public TimeSpan TimeOnline => DateTime.Now - LogonTime;
    }
}
