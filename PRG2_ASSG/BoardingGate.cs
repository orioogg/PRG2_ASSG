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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportCFFT { get; set; }
        public bool SupportDDJB { get; set; }
        public bool SupportLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate(string gateName, bool supportCFFT, bool supportDDJB, bool supportLWTT)
        {
            GateName = gateName;
            SupportCFFT = supportCFFT;
            SupportDDJB = supportDDJB;
            SupportLWTT = supportLWTT;
            Flight = null;
        }


        //public double CalculateFees() 
        //{
        //    double baseFee = 300; // Base fee for the gate
        //    if (Flight == null) return 0; // No flight, no fee

        //    // Calculate fees based on gate capabilities (not the flight)
        //    if (SupportCFFT) baseFee += 150;
        //    if (SupportDDJB) baseFee += 300;
        //    if (SupportLWTT) baseFee += 500;

        //    return baseFee;
        //}


        public override string ToString()
        {
            return $"{GateName}: DDJB={SupportDDJB}, CFFT={SupportCFFT}, LWTT={SupportLWTT}";

        }
    }
}
