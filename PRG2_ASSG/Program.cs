//==========================================================
// Student Number : S10269334
// Student Name : Janice Oh Shi Ting
// Partner Name : Murray Wong Kah Weng
//==========================================================
using PRG2_ASSG;
Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGate = new Dictionary<string, BoardingGate>();
LoadAirlines();
LoadBoardingGate();
Console.WriteLine($"Loading Airlines...\r\n{airlineDictionary.Count} Airlines Loaded!\r\nLoading Boarding Gates...\r\n{boardingGate.Count} Boarding Gates Loaded!\r\nLoading Flights...\r\n30 Flights Loaded!\r\n");
Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit\r\n\r\nPlease select your option:\r\n");
void LoadAirlines()
{
    try
    {
        using (StreamReader reader = new StreamReader("airlines.csv"))
        {
            string? line = reader.ReadLine(); // Read the header line 

            while ((line = reader.ReadLine()) != null) // Read subsequent lines
            {
                string[] airlineDetails = line.Split(',');

                
                if (airlineDetails.Length >= 2) // Check if there are at least two columns
                {
                    string code = airlineDetails[0];
                    string name = airlineDetails[1];

                    // Check for duplicates before adding
                    if (!airlineDictionary.ContainsKey(code))
                    {
                        Airline airline = new Airline(name, code);
                        airlineDictionary[code] = airline; // Add to dictionary
                    }
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
    try
    {
        using (StreamReader sr = new StreamReader("boardinggates.csv"))
        {
            string? line = sr.ReadLine(); // Read the header line 
           

            while ((line = sr.ReadLine()) != null)
            {
                

                string[] details = line.Split(',');

                // Validate the row format
                if (details.Length != 4)
                {
                    Console.WriteLine($"Invalid line: {line}");
                    continue;
                }

                try
                {
                    // Parse gate details
                    string gateName = details[0];
                    bool supportsDDJB = Convert.ToBoolean(details[1]);
                    bool supportsCFFT = Convert.ToBoolean(details[2]);
                    bool supportsLWTT = Convert.ToBoolean(details[3]);

                    // Create BoardingGate object
                    BoardingGate gate = new BoardingGate(gateName, supportsDDJB, supportsCFFT, supportsLWTT);

                    boardingGate[gateName] = gate;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line}. Details: {ex.Message}");
                }
            }
        }

       
       
    }
    catch (Exception e)
    {
        Console.WriteLine($"An error occurred while loading boarding gates: {e.Message}");
    }
}

Dictionary<string, Flight> flightsDictionary = new Dictionary<string, Flight>();

void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] flights = s.Split(",");

            string flightNumber = flights[0];
            string origin = flights[1];
            string destination = flights[2];
            DateTime expectedTime = Convert.ToDateTime(flights[3]);

        }


    }
}

