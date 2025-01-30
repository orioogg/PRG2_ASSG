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
    abstract class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime ExpectedTime { get; set; }

        public string Status { get; set; }= "Scheduled";

        //constructor

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
          
        }

        //public abstract double CalculateFees();

        //to compare date time
        public DateTime.Days CompareTo(Flight t)
        {
            return ExpectedTime.CompareTo(t.ExpectedTime);
        }

        public override string ToString()
        {
            return "Flight Number: " + FlightNumber + 
                   "Origin: " + Origin + 
                   "Destination: " + Destination + 
                   "Status: " + Status + 
                   "Expected Time: " + ExpectedTime;
        }
    }
}
