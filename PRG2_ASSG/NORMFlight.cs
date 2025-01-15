using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSG
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, string status,DateTime expectedTime) : base(flightNumber, origin, destination, status,expectedTime) { }

        //public override double CalculateFees()
        //{
        //    double fees = 300;
        //    if (Destination == "Singapore")
        //    {
        //        fees += 500;
        //    }
        //    else if (Origin == "Singapore")
        //    {
        //        fees += 800;
        //    }

        //    return fees;
        //}
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
