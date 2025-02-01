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
namespace S10269334_PRG2Assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }
        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            Flights[flight.FlightNumber] = flight;
            return true;
        }
        public double CalculateFees() 
        {
            int counter = Flights.Count / 3;
            double totalFee = 0;
            TimeSpan startTime = new TimeSpan(11, 0, 0);
            TimeSpan endTime = new TimeSpan(21,0,0);
            foreach (var flight in Flights.Values)
            {
                totalFee = flight.CalculateFees();
                if (Flights.Count > 5)
                {
                    double discount = 0.03 * totalFee;
                    totalFee -= discount;
                }
                else if (counter != 0)
                {
                    totalFee -= 350 * counter;
                }
                else if (flight.ExpectedTime.TimeOfDay >= startTime && flight.ExpectedTime.TimeOfDay <= endTime)
                {
                    totalFee -= 110;
                }
                else if (flight.Origin == "Bangkok (BKK)" || flight.Origin == "Dubai (DXB)" || flight.Origin == "Tokyo (NRT)")
                {
                    totalFee -= 25;
                }
                else if (flight is NORMFlight)
                {
                    totalFee -= 50;
                }
            }
            return totalFee;
        }
        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name: {Name} | Code: {Code}";
        }
    }
}

