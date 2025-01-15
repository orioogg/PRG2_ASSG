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
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        //constructor
        public Flight(string flightNumber, string origin, string destination, string status, DateTime expectedTime,double requestFee) : base (flightNumber, origin, destination, status, expectedTime)
        {
            RequestFee = requestFee;
        }

        public override string ToString()
        {
            return base.ToString() + "Request Fee: " + RequestFee;
        }
    }
}
