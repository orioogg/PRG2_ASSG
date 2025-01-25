using System;
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

namespace PRG2_ASSG
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
        public Airline GetAirline(Flight flight)
        {
            
            if (flight != null)
            {
                string flightcode;
                string s = flight.FlightNumber;
                string[] arr = s.Trim().Split(' ');
                flightcode = arr[0];
                foreach (var kvp in Airlines)
                {
                    if (kvp.Value.Code == flightcode)
                    {
                        Airline airline1 = kvp.Value;
                        return airline1;
                    }
                }
            }
            return null;
            
        }

        //public void PrintAirlineFees()
        //{

        //}
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
