﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ConsoleTable;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace VatBoardCons
{
    class Program
    {
        public IConfiguration config;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("VatBoard.json", true, true)
                .Build();

            string remoteUri = config["VATSIMURL"];
            string fileName = config["VATSIMDATAFILE"];
            string myStringWebResource = remoteUri + fileName;
            string lookFor;
            int refreshInterval;
            var VATSIMList = new List<VatLine>();
            var tableArrivals = new Table();
            var tableDepartures = new Table();

            Console.CursorSize = 100;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Util.typeWriter(
                Util.ascVATSIM,
                4,
                (ConsoleColor)Convert.ToInt32(config["ASCIICOLORVATBOARD"]));

            Util.WriteLn(
                "\n(c) 2020 - Elias Stassinos  - More Info: http://www.estassinos.com/vatboard ",
                ConsoleColor.DarkGreen,
                ConsoleColor.White);
            Util.WriteLn(
                string.Format("\n\nVersion: {0} ", Util.GetVersion()),
                ConsoleColor.DarkGreen,
                ConsoleColor.White, false);

            lookFor = ReadLine.Read("\n\nAirport ICAO:", "").ToUpper();
            refreshInterval = Convert.ToInt32(ReadLine.Read("\nRefresh Interval (default 10sec):", "10"));

            do
            {
                tableArrivals.ClearRows();
                tableDepartures.ClearRows();
                VATSIMList = Util.DownloadVatsimData("https://data.vatsim.net/v3/vatsim-data.json", fileName, config["VATSIMINTERVAL"]);
                tableArrivals.SetHeaders("Callsign", "Aircraft", "Departure", "Arrival", "TAS", "Altitude", "Distance To");
                VATSIMList.Where(d => d.planned_destairport == lookFor).OrderBy(o => o.DistanceTo).ToList().ForEach(d =>
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
                VATSIMList.Where(d => d.planned_depairport == lookFor).OrderBy(o => o.DistanceFrom).ToList().ForEach(d =>
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
                Util.typeWriter(Util.ascARRIVALS, 4, (ConsoleColor)Convert.ToInt32(config["ASCIICOLORARRIVALS"]));
                Util.typeWriter(tableArrivals.ToString(), 4);
                System.Threading.Thread.Sleep(5000);

                Console.Clear();
                Util.typeWriter(Util.ascDEPARTURES, 4, (ConsoleColor)Convert.ToInt32(config["ASCIICOLORDEPARTURES"]));
                Util.typeWriter(tableDepartures.ToString(), 4);
                System.Threading.Thread.Sleep(refreshInterval * 1000);
            } while (true);
        }
    }
}
