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
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        //constructor
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            RequestFee = 150;
        }

        public override double CalculateFees()
        {
            double fee = RequestFee + base.CalculateFees();
            return fee;
        }
        public override string ToString()
        {
            return base.ToString() + "Request Fee: " + RequestFee;
        }
    }
}
