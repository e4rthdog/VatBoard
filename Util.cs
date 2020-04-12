using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace VatBoardCons
{
    public static class Util
    {
        public const string ascVATSIM = @"██╗   ██╗ █████╗ ████████╗██████╗  ██████╗  █████╗ ██████╗ ██████╗ 
██║   ██║██╔══██╗╚══██╔══╝██╔══██╗██╔═══██╗██╔══██╗██╔══██╗██╔══██╗
██║   ██║███████║   ██║   ██████╔╝██║   ██║███████║██████╔╝██║  ██║
╚██╗ ██╔╝██╔══██║   ██║   ██╔══██╗██║   ██║██╔══██║██╔══██╗██║  ██║
 ╚████╔╝ ██║  ██║   ██║   ██████╔╝╚██████╔╝██║  ██║██║  ██║██████╔╝
  ╚═══╝  ╚═╝  ╚═╝   ╚═╝   ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝                                                                    
";

        public const string ascARRIVALS = @" █████╗ ██████╗ ██████╗ ██╗██╗   ██╗ █████╗ ██╗     ███████╗
██╔══██╗██╔══██╗██╔══██╗██║██║   ██║██╔══██╗██║     ██╔════╝
███████║██████╔╝██████╔╝██║██║   ██║███████║██║     ███████╗
██╔══██║██╔══██╗██╔══██╗██║╚██╗ ██╔╝██╔══██║██║     ╚════██║
██║  ██║██║  ██║██║  ██║██║ ╚████╔╝ ██║  ██║███████╗███████║
╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝  ╚═╝  ╚═╝╚══════╝╚══════╝
"

;

        public const string ascDEPARTURES = @"██████╗ ███████╗██████╗  █████╗ ██████╗ ████████╗██╗   ██╗██████╗ ███████╗███████╗
██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██║   ██║██╔══██╗██╔════╝██╔════╝
██║  ██║█████╗  ██████╔╝███████║██████╔╝   ██║   ██║   ██║██████╔╝█████╗  ███████╗
██║  ██║██╔══╝  ██╔═══╝ ██╔══██║██╔══██╗   ██║   ██║   ██║██╔══██╗██╔══╝  ╚════██║
██████╔╝███████╗██║     ██║  ██║██║  ██║   ██║   ╚██████╔╝██║  ██║███████╗███████║
╚═════╝ ╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝╚══════╝
";
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles (default)                         :::
        //:::                  'K' is kilometers                                      :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::
        //:::  Worldwide cities and other features databases with latitude longitude  :::
        //:::  are available at https://www.geodatasource.com                         :::
        //:::                                                                         :::
        //:::  For enquiries, please contact sales@geodatasource.com                  :::
        //:::                                                                         :::
        //:::  Official Web site: https://www.geodatasource.com                       :::
        //:::                                                                         :::
        //:::           GeoDataSource.com (C) All Rights Reserved 2018                :::
        //:::                                                                         :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        public static double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        public static void typeWriter(string _str, int speed, ConsoleColor _col = ConsoleColor.White)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = _col;
            for (int i = 0; i < _str.Length; i++)
            {
                Console.Write(_str[i]);
                System.Threading.Thread.Sleep(speed);
                if (Console.CursorTop == Console.WindowHeight - 1 && Console.CursorLeft == 0)
                {
                    System.Threading.Thread.Sleep(4000);
                    for (int _i = 6; _i <= Console.WindowHeight - 1; _i++)
                    {
                        Console.SetCursorPosition(0, _i);
                        Console.Write(new string(' ', Console.WindowWidth - 2));
                    }
                    Console.SetCursorPosition(0, 6);
                }
            }
            Console.ForegroundColor = currentForeground;
        }

        public static List<Airport> LoadAirports()
        {
            var _ret = new List<Airport>();
            string[] _data = File.ReadAllLines("Airports.txt");
            foreach (string _d in _data)
            {
                string[] col;
                string[] col1 =
                col = _d.Split(":");
                if (col[1].Contains(","))
                {
                    col1 = col[1].Split(",");
                }
                else
                {
                    col1[0] = "0.0";
                    col1[1] = "0.0";
                }
                _ret.Add(new Airport()
                {
                    code = col[0],
                    lat = col1[1],
                    lon = col1[0]
                });
            }
            return _ret;
        }

        public static void WriteLn(string _str, ConsoleColor _background, ConsoleColor _foreground, bool _lf = true)
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.BackgroundColor = _background;
            Console.ForegroundColor = _foreground;
            if (_lf)
            {
                Console.WriteLine(_str);
            }
            else
            {
                Console.Write(_str);
            }
            
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        public static List<VatLine> DownloadVatsimData(string _uri, string _filename)
        {
            var Airports = new List<Airport>();
            List<VatLine> dataList = new List<VatLine>();
            DateTime lastDownload = File.GetLastWriteTime(_filename);
            TimeSpan span = DateTime.Now.Subtract(lastDownload);
            Airports = LoadAirports();
            if (span.TotalMinutes > 3)
            {
                WriteLn("\nDownloading VATSIM data ...",ConsoleColor.Black,ConsoleColor.Yellow,false);
                try
                {
                    WebClient wc = new WebClient();
                    wc.DownloadFile(_uri, _filename);
                    WriteLn("DONE!", ConsoleColor.Black, ConsoleColor.Yellow);
                }
                catch (WebException wex)
                {
                    WriteLn(String.Format("\nERROR!! => [{0}]]", wex.Message), ConsoleColor.Red, ConsoleColor.White);
                    WriteLn(String.Format("Press any key to exit.", wex.Message), ConsoleColor.Red, ConsoleColor.White);
                    Console.ReadKey();
                    System.Environment.Exit(-1);
                }
                string[] dataLines = File.ReadAllLines(_filename);
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
            }
            return dataList;
        }

        public static string GetVersion()
        {
            string gitVersion = String.Empty;
            using (Stream stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("VatBoard.version.txt"))
            using (StreamReader reader = new StreamReader(stream))
            {
                gitVersion = reader.ReadLine();
            }
            return gitVersion;
        }

    }
}
