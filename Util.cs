﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using VatsimData;

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

        public static void typeWriter(string _str, int speed, ConsoleColor _colFG = ConsoleColor.White, ConsoleColor _colBG = ConsoleColor.Black)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            ConsoleColor currentBackground = Console.BackgroundColor;
            Console.ForegroundColor = _colFG;
            Console.BackgroundColor = _colBG;
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
            Console.BackgroundColor = currentBackground;
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

        public static List<VatLine> DownloadVatsimData(string _uri, string _filename, string _interval)
        {
            var Airports = new List<Airport>();
            List<VatLine> dataList = new List<VatLine>();
            DateTime lastDownload = File.GetLastWriteTime(_filename);
            TimeSpan span = DateTime.Now.Subtract(lastDownload);
            Airports = LoadAirports();
            if (span.TotalMinutes > Convert.ToDouble(_interval))
            {
                WriteLn("\nDownloading VATSIM data ...", ConsoleColor.Black, ConsoleColor.Yellow, false);
                try
                {
                    VatsimData.VatsimData.GetData(_uri);
                    WriteLn("DONE!", ConsoleColor.Black, ConsoleColor.Yellow);
                }
                catch (WebException wex)
                {
                    WriteLn(String.Format("\nERROR!! => [{0}]]", wex.Message), ConsoleColor.Red, ConsoleColor.White);
                    WriteLn(String.Format("Press any key to exit.", wex.Message), ConsoleColor.Red, ConsoleColor.White);
                    Console.ReadKey();
                    System.Environment.Exit(-1);
                }
            }
            var fp = new FlightPlan();
            foreach (var pilot in VatsimData.VatsimData.Data.Pilots)
            {
                string user_lat_dep = "0.0";
                string user_lon_dep = "0.0";
                string user_lat_dest = "0.0";
                string user_lon_dest = "0.0";
                if (pilot.FlightPlan != null)
                {
                    fp = (FlightPlan)pilot.FlightPlan;

                    if (Airports.Any(x => x.code == fp.Departure))
                    {
                        user_lat_dep = Airports.Find(x => x.code == fp.Departure).lat;
                        user_lon_dep = Airports.Find(x => x.code == fp.Departure).lon;
                    }
                    if (Airports.Any(x => x.code == fp.Arrival))
                    {
                        user_lat_dest = Airports.Find(x => x.code == fp.Arrival).lat;
                        user_lon_dest = Airports.FirstOrDefault(x => x.code == fp.Arrival).lon;
                    }
                }

                dataList.Add(new VatLine
                {
                    callsign = pilot.Callsign,
                    planned_depairport = fp.Departure,
                    planned_destairport = fp.Arrival,
                    planned_aircraft = fp.Aircraft,
                    planned_tascruise = fp.CruiseTAS,
                    altitude = fp.Altitude,
                    lat = pilot.Latitude.ToString(),
                    lon = pilot.Longitude.ToString(),
                    DistanceTo = Util.distance(
                                    Convert.ToDouble(user_lat_dest.Replace(".", ",")),
                                    Convert.ToDouble(user_lon_dest.Replace(".", ",")),
                                    Convert.ToDouble(pilot.Latitude.ToString().Replace(".", ",")),
                                    Convert.ToDouble(pilot.Longitude.ToString().Replace(".", ",")), 'N'),
                    DistanceFrom = Util.distance(
                                    Convert.ToDouble(user_lat_dep.Replace(".", ",")),
                                    Convert.ToDouble(user_lon_dep.Replace(".", ",")),
                                    Convert.ToDouble(pilot.Latitude.ToString().Replace(".", ",")),
                                    Convert.ToDouble(pilot.Longitude.ToString().Replace(".", ",")), 'N'),
                });
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
