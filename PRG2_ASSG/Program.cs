using PRG2_ASSG;
Terminal terminal = new Terminal("Changi Airport Terminal 5");
Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
LoadAirlines();
LoadBoardingGate();
void LoadAirlines()
{
    try
    {
        using (StreamReader sr = new StreamReader("airlines.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] airlineDetails = line.Split(',');

                // Validate the line
                if (airlineDetails.Length != 2)
                {
                    Console.WriteLine($"Invalid line: {line}");
                    continue;
                }

                string code = airlineDetails[0];
                string name = airlineDetails[1];

                // Create Airline object
                Airline airline = new Airline(name, code);

                // Add to terminal and dictionary
                terminal.AddAirline(airline);

                if (!airlineDictionary.ContainsKey(code))
                {
                    airlineDictionary[code] = airline;
                }
                else
                {
                    Console.WriteLine($"Duplicate airline code found: {code}");
                }
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
void LoadBoardingGate()
{
    {
        using (StreamReader sr = new StreamReader("boardinggates.csv"))
        {
            string line;
            bool isFirstLine = true;

            while ((line = sr.ReadLine()) != null)
            {
                // Skip the header row
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                string[] details = line.Split(',');

                // Validate the row format
                if (details.Length != 4)
                {
                    Console.WriteLine($"Invalid line: {line}");
                    continue;
                }

                // Parse gate details
                string gateName = details[0];
                bool supportsDDJB = Convert.ToBoolean(details[1]);
                bool supportsCFFT = Convert.ToBoolean(details[2]);
                bool supportsLWTT = Convert.ToBoolean(details[3]);

                // Create BoardingGate object
                BoardingGate gate = new BoardingGate(gateName, supportsDDJB, supportsCFFT, supportsLWTT);

                // Add to terminal
                terminal.AddBoardingGate(gate);
            }
        }
       
    }
}
    


