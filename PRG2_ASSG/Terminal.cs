using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10269334
// Student Name : Janice Oh Shi Ting
// Partner Name : Murray Wong Kah Weng
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
        public Terminal(string terminalName,Dictionary<string, Airline>airlines = null,Dictionary<string, Flight> flights = null,Dictionary<string, BoardingGate> boardingGates = null,Dictionary<string, double> gateFees = null)
        {
            TerminalName = terminalName;
            Airlines = airlines=new Dictionary<string,Airline>();
            Flights = flights;
            BoardingGates = new Dictionary<string,BoardingGate>();
            GateFees = gateFees;
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
        public Airline GetAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code))
            {
                return Airlines[airline.Code];
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
