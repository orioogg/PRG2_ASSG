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
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        //constructor 
        public LWTTFlight(string flightNumber, string origin, string destination,DateTime expectedTime,string status) : base(flightNumber, origin, destination,expectedTime, status)
        {
            RequestFee = 500;
        }

        //public override double CalculateFees()
        //{

        //}
        public override string ToString()
        {
            return base.ToString() + "Request Fee: " + RequestFee;
        }
    }
}
