using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSG
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportCFFT { get; set; }
        public bool SupportDDJB { get; set; }
        public bool SupportLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate(string gateName, bool supportCFFT, bool supportDDJB, bool supportLWTT, Flight flight)
        {
            GateName = gateName;
            SupportCFFT = supportCFFT;
            SupportDDJB = supportDDJB;
            SupportLWTT = supportLWTT;
            Flight = flight;
        }
        //public double CalculateFees() { }
        public override string ToString()
        {
            return "Gate Name: " + GateName +
                   "Support CFFT: " + SupportCFFT +
                   "Support DDJB: " + SupportDDJB +
                   "Support LWTT: " + SupportLWTT +
                   "Flight: " + Flight;
        }
    }
}
