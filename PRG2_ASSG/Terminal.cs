﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10269334
// Student Name : Janice Oh Shi Ting
// Partner Name : Murray Wong Kah Weng
// Partner Number : S10270448
//==========================================================

namespace S10269334_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();
        //constructor
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            
        }
        public bool AddAirline(Airline airline)
        {
            
            if (Airlines.ContainsKey(airline.Code))
            {
                return false;
            }

            Airlines[airline.Code] = airline;
            return true; 
        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName))
            {
                return false;
            }

            BoardingGates[boardingGate.GateName] = boardingGate;
            return true;
        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            
            if (flight != null)
            {
                string[] code = flight.FlightNumber.Split(' ');
                if (Airlines.ContainsKey(code[0]))
                {
                    return Airlines[code[0]];
                }
            }
            return null;
            
        }

        public void PrintAirlineFees()
        {
            foreach (var airline in Airlines.Values)
            {
                double fee = airline.CalculateFees();
                Console.WriteLine(fee);
            }
        }
        //public override string ToString()
        //{
        //    return "Terminal Name: " + TerminalName +
        //           "Airlines: " + Airlines +
        //           "Flights: " + Flights +
        //           "Boarding Gates: " + BoardingGates +
        //           "Gate Fees: " + GateFees;
        //}
    }
}
