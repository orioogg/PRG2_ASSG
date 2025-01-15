﻿using System;
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
    abstract class Flight
    {
        public string FlightNumber { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime ExpectedTime { get; set; }

        public string Status { get; set; }

        //constructor

        public Flight(string flightNumber, string origin, string destination, string status, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            Status = status;
            ExpectedTime = DateTime.Now;
        }

        public abstract double CalculateFees();

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
