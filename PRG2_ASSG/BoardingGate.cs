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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; } = null;
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
        }


        public double CalculateFees()
        {
            double baseFee = 300; // Base fee for the gate
            if (Flight == null) return 0; // No flight, no fee

            //// Calculate fees based on gate capabilities (not the flight)
            //if (SupportsCFFT) baseFee += 150;
            //if (SupportsDDJB) baseFee += 300;
            //if (SupportsLWTT) baseFee += 500;
            return baseFee;
        }


        public override string ToString()
        {
            return $"{GateName}: DDJB={SupportsDDJB}, CFFT={SupportsCFFT}, LWTT={SupportsLWTT}";

        }
    }
}
