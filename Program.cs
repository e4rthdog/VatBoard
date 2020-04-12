using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ConsoleTable;
using System.Linq;

namespace VatBoardCons
{
    class Program
    {
        static void Main(string[] args)
        {
            string remoteUri = "http://cluster.data.vatsim.net/";
            string fileName = "vatsim-data.txt", myStringWebResource = null;
            myStringWebResource = remoteUri + fileName;
            string lookFor;
            int refreshInterval;
            var tableArrivals = new Table();
            var tableDepartures = new Table();
            var Airports = new List<Airport>();

            Airports = Util.LoadAirports();

            Console.CursorSize = 100;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Util.typeWriter(Util.ascVATSIM, 4, ConsoleColor.DarkGray);
            Console.ForegroundColor = ConsoleColor.White;

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Util.typeWriter("\n(c) 2020 - Elias Stassinos  - More Info: http://www.estassinos.com/vatboard - v1", 4, ConsoleColor.White);
            Console.BackgroundColor = ConsoleColor.Black;

            lookFor = ReadLine.Read("\n\nAirport ICAO:", "").ToUpper();
            refreshInterval = Convert.ToInt32(ReadLine.Read("\nRefresh Interval (default 10sec):", "10"));

            do
            {
                DateTime lastDownload = File.GetLastWriteTime(@"vatsim-data.txt");
                TimeSpan span = DateTime.Now.Subtract(lastDownload);
                if (span.TotalMinutes > 3)
                {
                    Console.Write("\nDownloading VATSIM data ...");
                    WebClient wc = new WebClient();
                    wc.DownloadFile(myStringWebResource, fileName);
                    Console.WriteLine("DONE!");
                }

                string[] dataLines = File.ReadAllLines(fileName);
                List<VatLine> dataList = new List<VatLine>();
                bool isPilot = false;
                foreach (string dataLine in dataLines)
                {
                    if (isPilot && dataLine != "!SERVERS:")
                    {
                        string[] col = dataLine.Split(":");
                        if (col[3] == "PILOT")
                        {
                            string user_lat_dep = "0.0";
                            string user_lon_dep = "0.0";
                            string user_lat_dest = "0.0";
                            string user_lon_dest = "0.0";
                            if (Airports.Any(x => x.code == col[11]))
                            {
                                user_lat_dep = Airports.Find(x => x.code == col[11]).lat;
                                user_lon_dep = Airports.Find(x => x.code == col[11]).lon;
                            }
                            if (Airports.Any(x => x.code == col[13]))
                            {
                                user_lat_dest = Airports.Find(x => x.code == col[13]).lat;
                                user_lon_dest = Airports.FirstOrDefault(x => x.code == col[13]).lon;
                            }
                            dataList.Add(new VatLine
                            {
                                callsign = col[0],
                                planned_depairport = col[11],
                                planned_destairport = col[13],
                                planned_aircraft = col[9],
                                planned_tascruise = col[10],
                                altitude = col[7],
                                lat = col[5],
                                lon = col[6],
                                DistanceTo = Util.distance(
                                Convert.ToDouble(user_lat_dest.Replace(".", ",")),
                                Convert.ToDouble(user_lon_dest.Replace(".", ",")),
                                Convert.ToDouble(col[5].Replace(".", ",")),
                                Convert.ToDouble(col[6].Replace(".", ",")), 'N'),
                                DistanceFrom = Util.distance(
                                Convert.ToDouble(user_lat_dep.Replace(".", ",")),
                                Convert.ToDouble(user_lon_dep.Replace(".", ",")),
                                Convert.ToDouble(col[5].Replace(".", ",")),
                                Convert.ToDouble(col[6].Replace(".", ",")), 'N'),
                            });
                        }
                    }

                    if (dataLine == "!CLIENTS:")
                    {
                        isPilot = true;
                    }
                    if (dataLine == "!SERVERS:")
                    {
                        isPilot = false;
                    }
                }

                tableArrivals.SetHeaders("Callsign", "Aircraft", "Departure", "Arrival", "TAS", "Altitude", "Distance To");
                dataList.Where(d => d.planned_destairport == lookFor).ToList().ForEach(d =>
                {
                    tableArrivals.AddRow(
                        d.callsign,
                        d.planned_aircraft,
                        d.planned_depairport,
                        d.planned_destairport,
                        d.planned_tascruise,
                        d.altitude,
                        d.DistanceTo.ToString("N0"));
                });

                tableDepartures.SetHeaders("Callsign", "Aircraft", "Departure", "Arrival", "TAS", "Altitude", "Distance From");
                dataList.Where(d => d.planned_depairport == lookFor).ToList().ForEach(d =>
                {
                    tableDepartures.AddRow(
                          d.callsign,
                          d.planned_aircraft,
                          d.planned_depairport,
                          d.planned_destairport,
                          d.planned_tascruise,
                          d.altitude,
                          d.DistanceFrom.ToString("N0"));
                });

                Console.Clear();
                Util.typeWriter(Util.ascARRIVALS, 4, ConsoleColor.DarkGray);
                Util.typeWriter(tableArrivals.ToString(), 4);
                System.Threading.Thread.Sleep(5000);

                Console.Clear();
                Util.typeWriter(Util.ascDEPARTURES, 4, ConsoleColor.DarkGray);
                Util.typeWriter(tableDepartures.ToString(), 4);
                System.Threading.Thread.Sleep(refreshInterval * 1000);
            } while (true);
        }
    }
}
