using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSG
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public Flight(string flightNumber, string origin, string destination, string status, DateTime expectedTime, double requestFee) : base(flightNumber, origin, destination, status, expectedTime)
        {
            RequestFee = requestFee;
        }

        //public override double CalculateFee()
        public override string ToString()
        {
            return base.ToString() + "Request Fee: " + RequestFee;
        }
    }
}
