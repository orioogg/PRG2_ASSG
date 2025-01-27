//==========================================================
// Student Number : S10269334
// Student Name : Janice Oh Shi Ting
// Partner Name : Murray Wong Kah Weng
// Partner Number : S10270448
//==========================================================
using S10269334_PRG2Assignment;
using System;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

Terminal terminal = new Terminal("Terminal 5");
LoadAirlines();
LoadBoardingGate();
LoadFlights();
Console.WriteLine($"Loading Airlines...\r\n{terminal.Airlines.Count} Airlines Loaded!\r\nLoading Boarding Gates...\r\n{terminal.BoardingGates.Count} Boarding Gates Loaded!\r\nLoading Flights...\r\n{terminal.Flights.Count} Flights Loaded!\r\n");
print4spaces();

void print4spaces()
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
}

while (true)
{
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit\r\nPlease select your option:");
    string option = Console.ReadLine();
    if (option == "1")
    {
        DisplayFlights();
        print4spaces();
    }
    else if (option == "2")
    {
        printboardinggate();
        print4spaces();
    }
    else if (option == "3") 
    {
        AssignBoardingGate();
        print4spaces();
    }
    else if (option == "4")
    {
        CreateFlight();
        print4spaces();
    }
    else if (option == "5")
    {
        displayspecificflight();
        print4spaces();
    }
    else if (option == "6")
    {
       modifyflights();
       print4spaces();
    }
    else if (option == "7")
    {
       
    }
    else if (option == "0")
    {
        break;
    }
    else
    {
        Console.WriteLine("Please enter only options 0 to 7.");
        print4spaces();

    }
}
//feature 1
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
//feature 1
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


//feature 2
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

            if (flights.Length == 5)   //check if there is a special request code or not
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
                else  //if there is no special request code
                {
                    Flight flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                    terminal.Flights.Add(flightNumber, flight);
                }
            }
        }
    }
}
//feature 3
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
//feature 4
void printboardinggate()
{
    Console.WriteLine("=============================================\r\nList of Boarding Gates for Changi Airport Terminal 5\r\n=============================================\r\n");
    Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");
    foreach (KeyValuePair<string, BoardingGate> boardinggate in terminal.BoardingGates)
    {
        Console.WriteLine($"{boardinggate.Value.GateName,-16}{boardinggate.Value.SupportDDJB,-23}{boardinggate.Value.SupportCFFT,-23}{boardinggate.Value.SupportLWTT,-23}");
    }
}


//feature 5
void AssignBoardingGate()
{
    Console.WriteLine("=============================================\r\n" +
        "Assign a Boarding Gate to a Flight\r\n=============================================\r\n");

    Console.Write("Enter Flight Number: ");        //prompt user for flight number
    string? flightNumber = Console.ReadLine();
    Console.Write("Enter Boarding Gate Name: ");    //prompt user for boarding gate name
    string? boardingName = Console.ReadLine();
    string? specialCode = "code";

    bool flightFound = false;  //flag to check if flight was found
    bool gateFound = false; //flag to check if gate was found
    foreach (var flight in terminal.Flights.Values)
    {
        //check if the flightNumber is in the dict
        if (flight.FlightNumber == flightNumber)
        {
            flightFound = true;
            if (flight is DDJBFlight)
            {
                specialCode = "DDJB";
            }
            else if (flight is CFFTFlight)
            {
                specialCode = "CFFT";
            }
            else if (flight is LWTTFlight)
            {
                specialCode = "LWTT";
            }
            else if (flight is NORMFlight)
            {
                specialCode = "None";
            }
            Console.WriteLine($"Flight Number: {flight.FlightNumber} \r\nOrigin: {flight.Origin}" +
                              $"\r\nDestination: {flight.Destination}" +
                              $"\r\nExpected Time: {flight.ExpectedTime}" +
                              $"\r\nSpecial Request Code: {specialCode}");
            foreach (var boardingGate in terminal.BoardingGates.Values)
            {
                
                
                    if (boardingName == boardingGate.GateName)    //check if boarding gate entered is in the dictionary
                    {
                        gateFound = true;
                        if (boardingGate.Flight == null)          //checks if the boarding gate is assigned already
                        {
                            Console.WriteLine($"Boarding Gate Name: {boardingGate.GateName}" +
                                          $"\r\nSupports DDJB: {boardingGate.SupportDDJB}" +
                                          $"\r\nSupports CFFT: {boardingGate.SupportCFFT} \r\nSupports LWTT: {boardingGate.SupportLWTT}");
                            boardingGate.Flight = flight;
                        }
                        else
                        {
                            Console.WriteLine("This boarding gate has already been assigned to another flight. Please try again.");
                        return;
                        }
                    }
                
            }
            if (!gateFound)
            {
                Console.WriteLine("Boarding gate could not be found. Please try again.");
                return;
            }
            Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
            string? input = Console.ReadLine().ToUpper() ;
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

                    }
                    else if (option == "2")
                    {
                        flight.Status = "Boarding";

                    }
                    else if (option == "3")
                    {
                        flight.Status = "On Time";

                    }
                }
                else if (input == "N")
                {
                    return;
                }
                
            }
            else
            {
                Console.WriteLine("Input is not valid. Please try again.");
            }
        }
    }
    if (!flightFound)
    {
        Console.WriteLine("Flight could not be found. Please try again.");
    }
    else
    {
        Console.WriteLine($"Flight {flightNumber} has been assigned to Boarding Gate {boardingName}!");
    }
}

//feature 6
void CreateFlight()
{
    string option = "o";
    string flightNumber = "w";
    do
    {
        Console.Write("Enter Flight Number: ");
        flightNumber = Console.ReadLine();

        Console.Write("Enter Origin: ");
        string? origin = Console.ReadLine();

        Console.Write("Enter Destination: ");
        string? destination = Console.ReadLine();

        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        DateTime expectedTime = Convert.ToDateTime(Console.ReadLine());

        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string? specialCode = Console.ReadLine();

        if (specialCode == "DDJB")
        {
            Flight flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
            terminal.Flights.Add(flightNumber, flight);
        }
        else if (specialCode == "CFFT")
        {
            Flight flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
            terminal.Flights.Add(flightNumber, flight);
        }
        else if (specialCode == "LWTT")
        {
            Flight flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
            terminal.Flights.Add(flightNumber, flight);
        }
        else if (specialCode == "None") //if there is no special request code
        {
            Flight flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
            terminal.Flights.Add(flightNumber, flight);
        }

        using (StreamWriter sw = new StreamWriter("flights.csv", true))
        {
            if (specialCode == "None")
            {
                string data = flightNumber + "," + origin + "," + destination + "," + expectedTime;
                sw.WriteLine(data);
            }
            else
            {
                string data = flightNumber + "," + origin + "," + destination + "," + expectedTime + "," + specialCode;
                sw.WriteLine(data);
            }
            
        }
        Console.WriteLine("Would you like to add another flight? (Y/N)");
        option = Console.ReadLine();

    } while (option != "N");
    Console.WriteLine($"Flight {flightNumber} has been added!");
}

//feature 7
void displayspecificflight()
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================\r\nAirline Code    Airline Name\r\nSQ              Singapore Airlines\r\nMH              Malaysia Airlines\r\nJL              Japan Airlines\r\nCX              Cathay Pacific\r\nQF              Qantas Airways\r\nTR              AirAsia\r\nEK              Emirates\r\nBA              British Airways\r\nEnter Airline Code:\r");
    string? airlineCode = Console.ReadLine().ToUpper();
    if(airlineCode != "SQ" && airlineCode != "MH" && airlineCode != "JL" && airlineCode != "CX" && airlineCode != "QF" && airlineCode != "TR" && airlineCode != "EK" && airlineCode != "BA")
    {
        Console.WriteLine("Invalid Airline Code");
        return;
    }
    Dictionary <string, Flight> airlineFlights = new Dictionary<string, Flight>(terminal.Flights);
    Console.WriteLine("=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================");
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
//feature 8
void modifyflights()
{ 
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================\r\nAirline Code    Airline Name\r\nSQ              Singapore Airlines\r\nMH              Malaysia Airlines\r\nJL              Japan Airlines\r\nCX              Cathay Pacific\r\nQF              Qantas Airways\r\nTR              AirAsia\r\nEK              Emirates\r\nBA              British Airways\r\nEnter Airline Code: ");
    string? airlineCode = Console.ReadLine().ToUpper();
    if (airlineCode != "SQ" && airlineCode != "MH" && airlineCode != "JL" && airlineCode != "CX" && airlineCode != "QF" && airlineCode != "TR" && airlineCode != "EK" && airlineCode != "BA")
    {
        Console.WriteLine("Invalid Airline Code");
        return;
    }
    Dictionary<string, Flight> airlineFlights = new Dictionary<string, Flight>(terminal.Flights);
    Console.WriteLine("=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================");
    Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");
    string? airlinename = null;
    foreach (KeyValuePair<string, Flight> flight in airlineFlights)
    {
        Airline airline1 = terminal.GetAirline(flight.Value);
        if (airline1.Code == airlineCode)
        {
            Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-20}");
        }
        airlinename = airline1.Name;
    }
    Console.WriteLine("Choose an existing Flight to modify or delete: ");
    string? flightNumber = Console.ReadLine();
    string? specialCode = "code";
    if (terminal.Flights.ContainsKey(flightNumber))
    {
        Flight flight1 = terminal.Flights[flightNumber];

        if (flight1 is DDJBFlight)
        {
            specialCode = "DDJB";
        }
        else if (flight1 is CFFTFlight)
        {
            specialCode = "CFFT";
        }
        else if (flight1 is LWTTFlight)
        {
            specialCode = "LWTT";
        }
        else if (flight1 is NORMFlight)
        {
            specialCode = "None";
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
                string? option1 = Console.ReadLine();
                if (option1 == "1")
                {

                    Console.Write("Enter new Origin: ");
                    string? newOrigin = Console.ReadLine();
                    if (newOrigin != "Singapore (SIN)" && newOrigin != "Tokyo (NRT)" && newOrigin != "Kuala Lumpur (KUL)" && newOrigin != "Bangkok (BKK)" && newOrigin != "Dubai (DXB)" && newOrigin != "Manila (MNL)" && newOrigin != "London (LHR)" && newOrigin != "Hong Kong (HKD)" && newOrigin != "Sydney (SYD)" && newOrigin != "Jakarta (CGK)" && newOrigin != "Melbourne (MEL)")
                    {
                        Console.WriteLine("Invalid Origin");
                        return;
                    }
                    Console.Write("Enter new Destination: ");
                    string? newDestination = Console.ReadLine();
                    if (newDestination != "Singapore (SIN)" && newDestination != "Tokyo (NRT)" && newDestination != "Kuala Lumpur (KUL)" && newDestination != "Bangkok (BKK)" && newDestination != "Dubai (DXB)" && newDestination != "Manila (MNL)" && newDestination != "London (LHR)" && newDestination != "Hong Kong (HKD)" && newDestination != "Sydney (SYD)" && newDestination != "Jakarta (CGK)" && newDestination != "Melbourne (MEL)")
                    {
                        Console.WriteLine("Invalid Destination");
                        return;
                    }
                    DateTime newExpectedTime = DateTime.Now;
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");

                            newExpectedTime = Convert.ToDateTime(Console.ReadLine());
                            break;
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Flight flight1 = terminal.Flights[flightNumber];
                    flight1.Origin = newOrigin;
                    flight1.Destination = newDestination;
                    flight1.ExpectedTime = newExpectedTime;


                    Console.WriteLine("Flight updated!");
                    
                    Console.Write($"Flight Number: {flightNumber}\r\nAirline Name: {airlinename}\r\nOrigin: {newOrigin}\r\nDestination: {newDestination}\r\nExpected Departure/Arrival Time: {newExpectedTime}\r\nStatus: {flight1.Status}\r\nSpecial Request Code: {specialCode}\n");
                    bool gateassigned = false;
                    foreach (var boardingGate in terminal.BoardingGates.Values)
                    {
                        if (boardingGate.Flight != null)
                        {
                            Console.WriteLine($"Boarding Gate: {boardingGate.GateName}");
                            gateassigned = true;
                            break;
                        }
                    }
                    if (gateassigned == false)
                    {
                        Console.WriteLine("Boarding Gate: Unssigned");
                    }
                }
                else if (option1 == "2")
                {
                    Console.WriteLine("Select new Status");
                    Console.WriteLine("1. Delayed\r\n2. Boarding\r\n3. On Time\r\nPlease select the new status of the flight:");
                    string? status = Console.ReadLine();
                    if (status == "1")
                    {
                        terminal.Flights[flightNumber].Status = "Delayed";
                    }
                    else if (status == "2")
                    {
                        terminal.Flights[flightNumber].Status = "Boarding";
                    }
                    else if (status == "3")
                    {
                        terminal.Flights[flightNumber].Status = "On Time";
                    }
                    else
                    {
                        Console.WriteLine("Please enter only options 1 to 3");
                    }
                    Console.WriteLine("Flight Status Updated!");
                   
                }
                else if (option1 == "3")
                {
                    
                    Console.Write("Enter new Special Request Code (CFFT/DDJB/LWTT/None): ");
                    string? newspecialcode = Console.ReadLine().ToUpper();
                    if (newspecialcode != "CFFT" && newspecialcode != "DDJB" && newspecialcode != "LWTT" && newspecialcode != "NONE")
                    {
                        Console.WriteLine("Invalid Special Request Code");
                        return;
                    }
                    if (newspecialcode == "NONE") 
                    {
                        newspecialcode = "None";
                    }
                    Flight flight1 = terminal.Flights[flightNumber];
                    terminal.Flights.Remove(flightNumber);
                    
                    if (newspecialcode == "DDJB")
                    {
                        flight1 = new DDJBFlight(flightNumber,flight1.Origin, flight1.Destination, flight1.ExpectedTime);
                        terminal.Flights.Add(flightNumber, flight1);
                    }
                    else if (newspecialcode == "CFFT")
                    {
                        flight1 = new CFFTFlight(flightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime);
                        terminal.Flights.Add(flightNumber, flight1);
                    }
                    else if (newspecialcode == "LWTT")
                    {
                        flight1 = new LWTTFlight(flightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime);
                        terminal.Flights.Add(flightNumber, flight1);
                    }
                    else if (newspecialcode == "None")
                    {
                         flight1 = new NORMFlight(flightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime);
                        terminal.Flights.Add(flightNumber, flight1);
                    }
                    Console.WriteLine("Special Request Code Updated!");

                }
                else if (option1 == "4")
                {

                    Console.Write("Enter new Boarding Gate: ");
                    string? newboardinggate = Console.ReadLine();
                    bool gateassigned = false;
                    
                    
                    

                    if (terminal.BoardingGates.ContainsKey(newboardinggate))
                    {
                        if (terminal.BoardingGates[newboardinggate].Flight == null)
                        {
                            terminal.BoardingGates[newboardinggate].Flight = terminal.Flights[flightNumber];
                            Console.WriteLine($"Flight {flightNumber} has been assigned to Boarding Gate {newboardinggate}!");
                            gateassigned = true;
                        }
                        else
                        {
                            Console.WriteLine("This boarding gate has already been assigned to another flight");

                            if (gateassigned == false)
                            {
                                Console.WriteLine("Boarding Gate: Unassigned");
                                return;
                            }
                            return;
                        }

                    }

                    else
                    {
                        Console.WriteLine("Invalid Boarding Gate");
                    }

                }
                else if (option == "2")
                {
                    terminal.Flights.Remove(flightNumber);
                    Console.WriteLine("Flight Deleted!");
                }

            }
        }
     }
}     
//feature 8    
void modifyingoptions()
{
    Console.WriteLine("1. Modify Basic Information\r\n2. Modify Status\r\n3. Modify Special Request Code\r\n4. Modify Boarding Gate");
    Console.WriteLine("Choose an option:");
}
