//==========================================================
// Student Number : S10269334
// Student Name : Janice Oh Shi Ting
// Partner Name : Murray Wong Kah Weng
//==========================================================
using PRG2_ASSG;

Terminal terminal = new Terminal("Terminal 5");
LoadAirlines();
LoadBoardingGate();
LoadFlights();
Console.WriteLine($"Loading Airlines...\r\n{terminal.Airlines.Count} Airlines Loaded!\r\nLoading Boarding Gates...\r\n{terminal.BoardingGates.Count} Boarding Gates Loaded!\r\nLoading Flights...\r\n{terminal.Flights.Count} Flights Loaded!\r\n");
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();



while (true)
{
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit\r\nPlease select your option:");
    int option = Convert.ToInt32(Console.ReadLine());
    if (option == 1)
    {
        DisplayFlights();
    }
    else if (option == 2)
    {
        printboardinggate();
    }
    else if (option == 3) 
    {
        AssignBoardingGate();
    }
    else if (option == 4)
    {
        
    }
    else if (option == 5)
    {
        displayspecificflight();
    }
    else if (option == 6)
    {
       modifyflights();
    }
    else if (option == 7)
    {
       
    }
    else if (option == 0)
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid Option");

    }
}

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
                    string code = airlineDetails[1];
                    string name = airlineDetails[0];

                    // Check for duplicates before adding
                   if(terminal.Airlines.ContainsKey(code))
                    {
                        Console.WriteLine($"Duplicate airline code: {code}");
                    }
                    else
                    {
                        Airline airline = new Airline(name, code);
                        terminal.Airlines.Add(code, airline); // Add to the Dictionary Terminal
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
                    bool supportsCFFT = Convert.ToBoolean(details[1]);
                    bool supportsDDJB = Convert.ToBoolean(details[2]);
                    bool supportsLWTT = Convert.ToBoolean(details[3]);

                    // Create BoardingGate object
                    BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);

                    terminal.AddBoardingGate(gate);// Add to the Dictionary Terminal

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



void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? s = sr.ReadLine(); //read heading
        while ((s = sr.ReadLine()) != null)
        {
            string[] flights = s.Split(",");

            string flightNumber = flights[0];
            string origin = flights[1];
            string destination = flights[2];
            DateTime expectedTime = Convert.ToDateTime(flights[3]);

            if (flights.Length == 5)   //check if there is a special request code
            {
                if (flights[4] == "DDJB")
                {
                    Flight flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                    terminal.Flights.Add(flightNumber, flight);
                }
                else if (flights[4] == "CFFT")
                {
                    Flight flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                    terminal.Flights.Add(flightNumber, flight);
                }
                else if (flights[4] == "LWTT")
                {
                    Flight flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                    terminal.Flights.Add(flightNumber, flight);
                }
            }
            else  //if there is no special request code
            {
                Flight flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                terminal.Flights.Add(flightNumber, flight);
            }

        }
    }
}

void DisplayFlights()
{
    Console.WriteLine("=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================\r\n");
    Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
    {
        Airline airline1 = terminal.GetAirline(flight.Value);
        Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-20}");
    }
}

void printboardinggate()
{
    Console.WriteLine("=============================================\r\nList of Boarding Gates for Changi Airport Terminal 5\r\n=============================================\r\n");
    Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");
    foreach (KeyValuePair<string, BoardingGate> boardinggate in terminal.BoardingGates)
    {
        Console.WriteLine($"{boardinggate.Value.GateName,-16}{boardinggate.Value.SupportDDJB,-23}{boardinggate.Value.SupportCFFT,-23}{boardinggate.Value.SupportLWTT,-23}");
    }
}



void AssignBoardingGate()
{
    Console.WriteLine("=============================================\r\n" +
        "Assign a Boarding Gate to a Flight\r\n=============================================\r\n");

    //prompt user for flight number
    Console.Write("Enter Flight Number: ");
    string? flightNumber = Console.ReadLine();
    //prompt user for boarding gate name
    Console.Write("Enter Boarding Gate Name:");
    string? boardingName = Console.ReadLine();
    string? specialCode = "code";
    
    foreach (var flight in terminal.Flights.Values)
    {
        int running = 0;
        if (flight is DDJBFlight)
        {
            running = 1;
            specialCode = "DDJB";
        }
        else if (flight is CFFTFlight)
        {
            specialCode = "CFFT";
            running = 1;
        }
        else if (flight is LWTTFlight)
        {
            specialCode = "LWTT";
            running = 1;
        }
        else if (flight is NORMFlight)     
        {
            specialCode = "None";
            running = 1;
        }
        else if (running == 1)
        {
            break;
        }
        //check if the flightNumber is in the dict
        if (flight.FlightNumber == flightNumber)
        {
            Console.WriteLine($"Flight Number: {flight.FlightNumber} \r\n Origin: {flight.Origin}" +
                              $"\r\n Destination: {flight.Destination}" +
                              $"\r\n  Expected Time: {flight.ExpectedTime}" +
                              $"\r\n Special Request Code: {specialCode}");
            //check if boarding gate entered is in the dictionary
            foreach (var boardingGate in terminal.BoardingGates.Values)
            {
                if (boardingGate.Flight.FlightNumber == flightNumber)
                {
                    Console.WriteLine("This boarding gate is already assigned to another flight.");
                }
                else if (boardingName == boardingGate.GateName)
                {
                    Console.WriteLine($"Boarding Gate Name: {boardingGate.GateName}" +
                                      $"\r\n Supports DDJB: {boardingGate.SupportDDJB}" +
                                      $"\r\n Supports CFFT: {boardingGate.SupportCFFT} \r\nSupports LWTT: {boardingGate.SupportLWTT}");
                    boardingGate.Flight = flight;
                }
            }
        }
        else
        {
            Console.WriteLine("Flight could not be found. Please try again.");
            return;
        }
        
        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        string? input = Console.ReadLine();
        if (input != null)
        {
            if (input == "Y")
            {
                Console.WriteLine("1. Delayed\r\n2. Boarding\r\n3. On Time\r\n");
                Console.WriteLine("Please select the new status of the flight:");
                string? option = Console.ReadLine();
                if (option == "1")
                {
                    flight.Status = "Delayed";
                    break;
                }
                else if (option == "2")
                {
                    flight.Status = "Boarding";
                    break;
                }
                else if (option == "3")
                {
                    flight.Status = "On Time";
                    break;
                }
            }
            else if (input == "N")
            {
                return;
            }
            else
            {
                Console.WriteLine("Input is not valid. Please try again.");
            }
        }
    }
    Console.WriteLine($"Flight {flightNumber} has been assigned to Boarding Gate{boardingName}!");
}
    


void displayspecificflight()
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================\r\nAirline Code    Airline Name\r\nSQ              Singapore Airlines\r\nMH              Malaysia Airlines\r\nJL              Japan Airlines\r\nCX              Cathay Pacific\r\nQF              Qantas Airways\r\nTR              AirAsia\r\nEK              Emirates\r\nBA              British Airways\r\nEnter Airline Code:\r\n");
    string? airlineCode = Console.ReadLine();
    if(airlineCode != "SQ" && airlineCode != "MH" && airlineCode != "JL" && airlineCode != "CX" && airlineCode != "QF" && airlineCode != "TR" && airlineCode != "EK" && airlineCode != "BA")
    {
        Console.WriteLine("Invalid Airline Code");
        return;
    }
    Dictionary <string, Flight> airlineFlights = new Dictionary<string, Flight>(terminal.Flights);
    Console.WriteLine("=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================\r\n");
    Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> flight in airlineFlights)
    {
        Airline airline1 = terminal.GetAirline(flight.Value);
        if (airline1.Code == airlineCode)
        {
            Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-20}");
        }
    }
}
void modifyflights()
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================\r\nAirline Code    Airline Name\r\nSQ              Singapore Airlines\r\nMH              Malaysia Airlines\r\nJL              Japan Airlines\r\nCX              Cathay Pacific\r\nQF              Qantas Airways\r\nTR              AirAsia\r\nEK              Emirates\r\nBA              British Airways\r\nEnter Airline Code: ");
    string? airlineCode = Console.ReadLine();
    if (airlineCode != "SQ" && airlineCode != "MH" && airlineCode != "JL" && airlineCode != "CX" && airlineCode != "QF" && airlineCode != "TR" && airlineCode != "EK" && airlineCode != "BA")
    {
        Console.WriteLine("Invalid Airline Code");
        return;
    }
    Dictionary<string, Flight> airlineFlights = new Dictionary<string, Flight>(terminal.Flights);
    Console.WriteLine("=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================\r\n");
    Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");
    string? airlinename=null;
    foreach (KeyValuePair<string, Flight> flight in airlineFlights)
    {
        Airline airline1 = terminal.GetAirline(flight.Value);
        if (airline1.Code == airlineCode)
        {
            Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-20}");
        }
        airlinename = airline1.Name;

    }
    Console.WriteLine("Choose an existing Flight to modify or delete:");
    string? flightNumber = Console.ReadLine();
    string? specialCode = "code";

    foreach (var flight in terminal.Flights.Values)
    {
        int running = 0;
        if (flight is DDJBFlight)
        {
            specialCode = "DDJB";
            running = 1;
        }
        else if (flight is CFFTFlight)
        {
            specialCode = "CFFT";
            running = 1;
        }
        else if (flight is LWTTFlight)
        {
            specialCode = "LWTT";
            running = 1;
        }
        else if (flight is NORMFlight)
        {
            specialCode = "None";
            running = 1;
        }
        else if (running == 1)
        {
            break;
        }
    }
    
    foreach (var flight in airlineFlights) 
    {
        if (flight.Key == flightNumber)
        {
            Console.WriteLine("1.Modify Flight\n2.Delete Flight\nChoose an option:");
            string? option = Console.ReadLine();
            if (option == "1")
            {
                modifyingoptions();
                string ?option1 = Console.ReadLine();
                if (option1 == "1")
                {   
                    Console.Write("Enter new Origin: ");
                    string? newOrigin = Console.ReadLine();
                    Console.Write("Enter new Destination: ");
                    string? newDestination = Console.ReadLine();
                    Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                    DateTime newExpectedTime = Convert.ToDateTime(Console.ReadLine());
                    Flight flight1 = terminal.Flights[flightNumber];
                    flight1.Origin = newOrigin;
                    flight1.Destination = newDestination;
                    flight1.ExpectedTime = newExpectedTime;
                    
                    
                    Console.WriteLine("Flight updated!!");
                    
                    Console.WriteLine($"Flight Number: {flightNumber}\r\nAirline Name: {airlinename}\r\nOrigin: {newOrigin}\r\nDestination: {newDestination}\r\nExpected Departure/Arrival Time: {newExpectedTime}\r\nStatus: {flight1.Status}\r\nSpecial Request Code: {specialCode}\r\nBoarding Gate: Unassigned\r\n");
                }
            }
            
            
            
        }
    }
}
void modifyingoptions()
{
    Console.WriteLine("1. Modify Basic Information\r\n2. Modify Status\r\n3. Modify Special Request Code\r\n4. Modify Boarding Gate\r\n");
}
