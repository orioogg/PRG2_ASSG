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
    
        class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination,DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            RequestFee = 300;
        }

        //public override double CalculateFee()
        public override string ToString()
        {
            return base.ToString() + " " + "Request Fee: " + RequestFee;
        }
    
}
}
