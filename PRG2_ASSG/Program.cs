//==========================================================
// Student Number : S10269334
// Student Name : Janice Oh Shi Ting
// Partner Name : Murray Wong Kah Weng
// Partner Number : S10270448
//==========================================================
using S10269334_PRG2Assignment;
using System;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;
List<string> Locations = new List<string>();
Terminal terminal = new Terminal("Terminal 5");
LoadAirlines();
LoadBoardingGate();
LoadFlights();
Console.WriteLine($"Loading Airlines...\r\n{terminal.Airlines.Count} Airlines Loaded!\r\nLoading Boarding Gates...\r\n{terminal.BoardingGates.Count} Boarding Gates Loaded!\r\nLoading Flights...\r\n{terminal.Flights.Count} Flights Loaded!\r\n");
print4spaces();
Console.Write("");
void print4spaces()
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
}

while (true)
{
    getthelocations();
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n8. Auto assign boarding gates in bulk\r\n9. Display Fees\r\n0. Exit\r\n\r\nPlease select your option:");
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
        CompareFlights();
        print4spaces();
    }
    else if (option == "9")
    {
        CalculateFees();
        print4spaces();
    }
    else if (option == "0")
    {
        Console.WriteLine("GoodBye!");
        break;
    }
    else if (option == "8")
    {
        //advanced feature (a)
        int assignedcount = 0;
        int alrassigned = 0;
        Queue<Flight> unassignedflights = new Queue<Flight>();
        

        foreach (var flight in terminal.Flights.Values)
        {
            var cflight=getDetails(flight.FlightNumber, flight);
            
            if (cflight.BoardingGateName == "")
            {
                unassignedflights.Enqueue(flight);
            }
            else
            {
                alrassigned++;
            }
            
        }

        int unassignedcount = unassignedflights.Count;
        Console.WriteLine($"There are {unassignedcount} unassigned flights.");
        int count = 0;
        foreach (var gate in terminal.BoardingGates.Values)
        {
            if (gate.Flight == null)
            {
                count++;
            }
        }
        Console.WriteLine($"There are {count} unassigned gates.");
        List<BoardingGate> normlist= new List<BoardingGate>();
        List<BoardingGate> ddjblist = new List<BoardingGate>();
        List<BoardingGate> cfftlist = new List<BoardingGate>();
        List<BoardingGate> lwttlist = new List<BoardingGate>();
        foreach (var gate in terminal.BoardingGates.Values)
        {
            if (gate.SupportsDDJB&&gate.Flight==null)//check if the gate supports DDJB and if it is already assigned
            {
                ddjblist.Add(gate);//add the gate to the list of gates that support DDJB

            }
            if (gate.SupportsCFFT && gate.Flight == null)
            {
                cfftlist.Add(gate);
                
            }
            if (gate.SupportsLWTT && gate.Flight == null)
            {
                lwttlist.Add(gate);
                
            }
            else if (!gate.SupportsLWTT&&!gate.SupportsDDJB&& !gate.SupportsCFFT &&gate.Flight== null)
            {
                normlist.Add(gate);
                
            }
        }

        int assignedflights = 0;
        if (unassignedflights.Count > 0)//check if there are any unassigned flights
        {
            Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time",-35}{"Special Request Code",-23}Boarding Gate");
        }
        while (unassignedflights.Count > 0) //assign the flights to the boarding gates
        {
            
            Flight flight = unassignedflights.Dequeue();
            var cflight = terminal.Flights[flight.FlightNumber];//get the flight from the terminal dictionary
            var details = getDetails(cflight.FlightNumber, cflight);//get the special request code of the flight
            BoardingGate assignedGate = null;//assign the flight to the boarding gate
            if (cflight is NORMFlight && normlist.Count > 0)
            {
                assignedGate = normlist[0];//assign the flight to the first gate in the list
                normlist.RemoveAt(0);//remove the gate from the list
            }
            else if (cflight is DDJBFlight && ddjblist.Count > 0)
            {
                assignedGate = ddjblist[0];
                ddjblist.RemoveAt(0);
            }
            else if (cflight is CFFTFlight && cfftlist.Count > 0)
            {
                assignedGate = cfftlist[0];
                cfftlist.RemoveAt(0);
            }
            else if (cflight is LWTTFlight && lwttlist.Count > 0)
            {
                assignedGate = lwttlist[0];
                lwttlist.RemoveAt(0);
            }
            if (assignedGate != null)//check if the gate is already assigned
            {
                    if (terminal.BoardingGates[assignedGate.GateName].Flight != null)
                    {
                        Console.WriteLine($"Error: Boarding gate {assignedGate.GateName} is already occupied by flight {terminal.BoardingGates[assignedGate.GateName].Flight.FlightNumber}");//check if the gate is already assigned
                        continue;
                    }
                
                
                terminal.BoardingGates[assignedGate.GateName].Flight = cflight;//assign the flight to the gate
                assignedflights++;
                assignedcount++;
                cflight.Status = "On time";

                Console.WriteLine($"{cflight.FlightNumber,-16}{terminal.GetAirlineFromFlight(cflight).Name,-23}{cflight.Origin,-23}{cflight.Destination,-23}{cflight.ExpectedTime,-35}{details.Code,-23}{assignedGate.GateName}");
            }
        }
        
        Console.WriteLine($"There are {assignedflights} flights assigned to boarding gates");
        Console.WriteLine($"There are {assignedcount} boarding gates assigned to flights");
        if(alrassigned==0&&assignedcount!=0)
        {
            Console.WriteLine($"Cant be converted in to a percentage as there were no flights that were already assigned");
        }
        else
        {
            double percentage =( (double)assignedcount / (double)alrassigned) * 100;
            Console.WriteLine($"Percentage of flights assigned to boarding gates: {percentage.ToString("0.00")}%");
        }
        print4spaces();
    }
    else
    {
        Console.WriteLine("Please enter only options 0 to 9.");
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
    try
    {
        using (StreamReader sr = new StreamReader("flights.csv"))
        {
            string? s = sr.ReadLine(); //read heading
            while ((s = sr.ReadLine()) != null)
            {
                string[] flights = s.Split(",");

                if (flights.Length < 4 || flights.Length > 5) //checks if there are extra info
                {
                    Console.WriteLine("Skipping invalid row.");
                    continue;
                }
                string flightNumber = flights[0];
                string origin = flights[1];
                string destination = flights[2];
                DateTime expectedTime = Convert.ToDateTime(flights[3]);

                if(terminal.Flights.ContainsKey(flightNumber))  //checks for duplicates
                {
                    Console.WriteLine("Skipping duplicate flight.");
                }


                if (flights.Length == 5)   //check if there is a special request code or not
                {
                    if (flights[4] == "DDJB")
                    {
                        Flight flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);

                        //add flight into airline dictionary
                        Airline airline = terminal.GetAirlineFromFlight(flight);
                        airline.AddFlight(flight);

                        //add flight into terminal dictionary
                        terminal.Flights.Add(flightNumber, flight);
                    }
                    else if (flights[4] == "CFFT")
                    {
                        Flight flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                        Airline airline = terminal.GetAirlineFromFlight(flight);
                        airline.AddFlight(flight);
                        terminal.Flights.Add(flightNumber, flight);
                    }
                    else if (flights[4] == "LWTT")
                    {
                        Flight flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                        Airline airline = terminal.GetAirlineFromFlight(flight);
                        airline.AddFlight(flight);
                        terminal.Flights.Add(flightNumber, flight);
                    }
                    else  //if there is no special request code
                    {
                        Flight flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                        Airline airline = terminal.GetAirlineFromFlight(flight);
                        airline.AddFlight(flight);
                        terminal.Flights.Add(flightNumber, flight);
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
//feature 3
void DisplayFlights()
{
    Console.WriteLine("=============================================\r\nList of Flights for Changi Airport Terminal 5\r\n=============================================");
    Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");
    if (terminal.Flights == null)
    {
        Console.WriteLine("There are no flights.");
    }
    try
    {
        foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
        {
            Airline airline1 = terminal.GetAirlineFromFlight(flight.Value);   //get airline from method
            Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-20}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
//feature 4
void printboardinggate()
{
    Console.WriteLine("=============================================\r\nList of Boarding Gates for Changi Airport Terminal 5\r\n=============================================");
    Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");
    foreach (KeyValuePair<string, BoardingGate> boardinggate in terminal.BoardingGates)
    {
        Console.WriteLine($"{boardinggate.Value.GateName,-16}{boardinggate.Value.SupportsDDJB,-23}{boardinggate.Value.SupportsCFFT,-23}{boardinggate.Value.SupportsLWTT,-23}");
    }
}


//feature 5
void AssignBoardingGate()
{
    Console.WriteLine("=============================================\r\n" +
        "Assign a Boarding Gate to a Flight\r\n=============================================");
    try
    {
        Flight? flight;
        while (true)
        {
            Console.WriteLine("Enter Flight Number: ");        //prompt user for flight number
            string? flightNumber = Console.ReadLine().ToUpper();
            flight = FindFlight(flightNumber);     //calls method
            if (flight != null)    //if flight is found
            {
                break;
            }
            Console.WriteLine("Flight could not be found. Please try again.");
        }

        BoardingGate? boardingGate;
        while (true)
        {
            Console.WriteLine("Enter Boarding Gate Name: ");   //prompt user for boarding gate name
            string? boardingName = Console.ReadLine().ToUpper();

            boardingGate = FindBoardingGate(boardingName);
            if (boardingGate == null) //if boarding gate is not found
            {
                Console.WriteLine("Boarding gate could not be found. Please try again.");
                continue;
            }
           
            if (boardingGate.Flight != null)   //to check if the boarding gate is already assigned to a flight
            {
                Console.WriteLine("This boarding gate has already been assigned to another flight. Please try again.");
                continue;
            }
            boardingGate.Flight = flight;  
            break;
        }
        string code = getDetails(boardingGate.GateName,flight).Code;
        //print out flight and boarding gate details 
        Console.WriteLine($"Flight Number: {flight.FlightNumber} \r\nOrigin: {flight.Origin}" +
                                  $"\r\nDestination: {flight.Destination}" +
                                  $"\r\nExpected Time: {flight.ExpectedTime}" +
                                  $"\r\nSpecial Request Code: {code}");
        Console.WriteLine($"Boarding Gate Name: {boardingGate.GateName}" +
                          $"\r\nSupports DDJB: {boardingGate.SupportsDDJB}" +
                          $"\r\nSupports CFFT: {boardingGate.SupportsCFFT} \r\nSupports LWTT: {boardingGate.SupportsLWTT}");
        flight.Status = "On Time";

        while (true)
        { 
        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        string? input = Console.ReadLine().ToUpper();
            if (input != null)
            {
                if (input == "Y")
                {
                    Console.WriteLine("1. Delayed\r\n2. Boarding\r\n3. On Time");
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
                    else   //if user enters something other than the options given
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                        continue;
                    }
                }
                else if (input == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
            }
        }
    Console.WriteLine($"Flight {flight.FlightNumber} has been assigned to Boarding Gate {boardingGate.GateName}!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}   

//method to find flight 
Flight? FindFlight(string flightNumber)
{
    foreach (var flight in terminal.Flights.Values)
    {
        //check if the flightNumber is in the dict
        if (flight.FlightNumber == flightNumber)
        {
            return flight;
        }
    }
    //if flight not found
    return null;
}
BoardingGate? FindBoardingGate(string boardingName)
{
    foreach (var boardingGate in terminal.BoardingGates.Values)
    {
        if (boardingName == boardingGate.GateName)    //check if boarding gate entered is in the dictionary
        {
            return boardingGate;
        }
    }
    //if boarding gate is not found
    return null;
}


//feature 6
void CreateFlight()
{
    string? option = "o";
    do
    {
        try
        {
            Console.Write("Enter Flight Number: ");    //prompts user for flight number
            string? flightNumber = Console.ReadLine().ToUpper();
            if (string.IsNullOrWhiteSpace(flightNumber))   //if empty
            {
                Console.WriteLine("Flight number cannot be empty. Please try again.");
                continue;
            }
            if (flightNumber.Length != 6) 
            {
                Console.WriteLine("Please enter a valid Flight number.");
                continue;
            }
            string[] flightNumberArray = flightNumber.Split(" ");
            if (flightNumberArray.Length != 2)
            {
                Console.WriteLine("Please enter a valid Flight number.");
                continue;
            }
            if (flightNumberArray[0].Length != 2 || flightNumberArray[1].Length != 3)
            {
                Console.WriteLine("Please enter a valid Flight number.");
                continue;
            }
            int num = Convert.ToInt32(flightNumberArray[1]);
            if (num < 1 || num > 999)
            {
                Console.WriteLine("Please enter a valid Flight number.");
                continue;
            }
            if (!terminal.Airlines.ContainsKey(flightNumberArray[0]))  //check if airline code entered is valid
            {
                Console.WriteLine("Invalid Airline Code. Please enter a valid airline code.");
                continue;
            }
            //check if flight number entered is already in the dict
            if (terminal.Flights.ContainsKey(flightNumber))
            {
                Console.WriteLine($"Flight {flightNumber} already exists. Please enter a unique flight number.");
                continue;
            }
            Console.WriteLine("Enter Origin: ");
            string origin = Console.ReadLine();
            if(!Locations.Contains(origin))
            {
                Console.WriteLine("Invalid Origin. Please enter a valid location.");
                continue;
            }
            Console.WriteLine("Enter Destination: ");
            string destination = Console.ReadLine();
            if (!Locations.Contains(destination))
            {
                Console.WriteLine("Invalid Destination. Please enter a valid location.");
                continue;
            }
            if (origin == destination)
            {
                Console.WriteLine("Origin and Destination cannot be the same.");
                continue;
            }
            
            Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            DateTime expectedTime = Convert.ToDateTime(Console.ReadLine());



            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string? specialCode = Console.ReadLine().ToUpper();

            if (specialCode == "NONE")  //if there is no special request code
            {
                specialCode = "None";
                Flight flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                terminal.Flights.Add(flightNumber, flight);
            }

            else if (specialCode == "DDJB")
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
            else
            {
                Console.WriteLine("Invalid special request code. Please enter CFFT, DDJB, LWTT, or None.");
                continue;
            }

            using (StreamWriter sw = new StreamWriter("flights.csv", true))
            {
                if (specialCode == "None")   //if there is no special request code
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
            Console.WriteLine($"Flight {flightNumber} has been added!");
            Console.WriteLine("Would you like to add another flight? (Y/N)");
            option = Console.ReadLine().ToUpper();
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    } while (option != "N");
}



//feature 7
void displayspecificflight()
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================\r\nAirline Code    Airline Name");
    foreach (var flight in terminal.Airlines)
    {
        Console.WriteLine($"{flight.Value.Code}              {flight.Value.Name}");
    }
    Console.Write("Enter Airline Code:");
    string? airlineCode = Console.ReadLine().ToUpper();

    if (terminal.Airlines.ContainsKey(airlineCode)) 
    {
        Console.WriteLine($"=============================================\nList of Flights for {terminal.Airlines[airlineCode].Name}\n=============================================");
        Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");

        foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
        {
            Airline airline1 = terminal.GetAirlineFromFlight(flight.Value);
            if (airline1.Code == airlineCode)
            {
                Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-23}");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code");
    }
}
//feature 8
void modifyflights()
{
    Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================\r\nAirline Code    Airline Name");
    foreach (var flight in terminal.Airlines) 
    {
        Console.WriteLine($"{flight.Value.Code}              {flight.Value.Name}");
    }
    Console.WriteLine("Enter Airline Code:");
    string? airlineCode = Console.ReadLine().ToUpper();
    Dictionary<string, Flight> airlineFlights = new Dictionary<string, Flight>();
    
    
    if (terminal.Airlines.ContainsKey(airlineCode))
    {

        Console.WriteLine($"=============================================\nList of Flights for {terminal.Airlines[airlineCode].Name}\n=============================================");//print the airline name
        Console.WriteLine("Flight Number   Airline Name               Origin                 Destination            Expected Departure/Arrival Time");

        foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
        {
            Airline airline1 = terminal.GetAirlineFromFlight(flight.Value);
            if (airline1.Code == airlineCode)
            {
                Console.WriteLine($"{flight.Value.FlightNumber,-16}{airline1.Name,-27}{flight.Value.Origin,-23}{flight.Value.Destination,-23}{flight.Value.ExpectedTime,-23}");
                airlineFlights.Add(flight.Key, flight.Value);
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code");
    }
    Console.WriteLine("Choose an existing Flight to modify or delete: ");
    string? flightNumber = Console.ReadLine().ToUpper();
    string? specialCode = "code";
    if (terminal.Flights.ContainsKey(flightNumber))// check which special request code the flight has
    {
        Flight flight1 = terminal.Flights[flightNumber];//store the flight details in a variable

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
    else 
    {
        Console.WriteLine("Invalid Flight Number");
        return;
    }

        foreach (var flight in airlineFlights)
        {
            if (flight.Key == flightNumber)//check if the flight number entered is in the dictionary
        {
            Console.WriteLine("1.Modify Flight\n2.Delete Flight\nChoose an option:");
            string? option = Console.ReadLine();
            if (option == "1")
            {
                modifyingoptions();
                string? option1 = Console.ReadLine();
                if (option1 == "1")
                {

                    Console.Write("Enter new Origin (Country (XXX)): ");
                    string? newOrigin = Console.ReadLine();
                    if(!Locations.Contains(newOrigin))
                    {
                        Console.WriteLine("Invalid Origin. Please enter a valid location.");
                        return;
                    }
                    Console.Write("Enter new Destination (Country (XXX)): ");
                    string? newDestination = Console.ReadLine();
                    if (!Locations.Contains(newDestination))
                    {
                        Console.WriteLine("Invalid Destination. Please enter a valid location.");
                        return;
                    }
                    if (newOrigin == newDestination)
                    {
                        Console.WriteLine("Origin and Destination cannot be the same.");
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
                    Flight flight1 = terminal.Flights[flightNumber];//save the current flight details before updating them with the new ones
                    flight1.Origin = newOrigin;
                    flight1.Destination = newDestination;
                    flight1.ExpectedTime = newExpectedTime;
                    string airlinename=terminal.GetAirlineFromFlight(flight1).Name;

                    Console.WriteLine("Flight updated!");
                    
                    Console.Write($"Flight Number: {flightNumber}\r\nAirline Name: {airlinename}\r\nOrigin: {newOrigin}\r\nDestination: {newDestination}\r\nExpected Departure/Arrival Time: {newExpectedTime}\r\nStatus: {flight1.Status}\r\nSpecial Request Code: {specialCode}\n");
                    bool gateassigned = false;
                    foreach (var boardingGate in terminal.BoardingGates.Values)
                    {
                        if (boardingGate.Flight != null)// check if the flight is already assigned to a boarding gate
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
                    Flight flight1 = terminal.Flights[flightNumber];//save the flight details before removing it
                    terminal.Flights.Remove(flightNumber);
                    
                    if (newspecialcode == "DDJB")
                    {
                        flight1 = new DDJBFlight(flightNumber,flight1.Origin, flight1.Destination, flight1.ExpectedTime);
                        terminal.Flights.Add(flightNumber, flight1);//add the flight back to the dictionary
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
                    bool gateassigned = false; // flag to check if gate is assigned
                    if (terminal.BoardingGates.ContainsKey(newboardinggate))
                    {
                        if (terminal.BoardingGates[newboardinggate].Flight == null)
                        {
                            terminal.BoardingGates[newboardinggate].Flight = terminal.Flights[flightNumber]; // check if the flight is already assigned to a boarding gate
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
    
                }
            else if (option == "2")
            {
                //Flight flight1 = terminal.Flights[flightNumber];
                //if (terminal.BoardingGates.ContainsKey(flightNumber))
                //{
                //    BoardingGate gate = terminal.BoardingGates[flightNumber];
                //    gate.Flight = null;
                //}
                Flight flight1 = terminal.Flights[flightNumber];

                if(flight1.Status != "Scheduled")
                {
                    foreach (var boardingGate in terminal.BoardingGates.Values)
                    {
                        if (boardingGate.Flight == flight1)
                        {
                            boardingGate.Flight = null;
                        }
                    }
                }
                if (terminal.Flights.ContainsKey(flightNumber))
                {
                    terminal.Flights.Remove(flightNumber);
                }


                Console.WriteLine("Flight Deleted!");
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

//feature 9
void CompareFlights()
{
    List<Flight> sortedFlights = new List<Flight>(terminal.Flights.Values);  //store flights dict in list
    sortedFlights.Sort();     //sort flights
    if (terminal.Flights == null || terminal.Flights.Count == 0)
    {
        Console.WriteLine("No flights available for comparison.");
        return;
    }
    string? specialCode = "";
    Console.WriteLine("=============================================\r\nFlight Schedule for Changi Airport Terminal 5\r\n=============================================\r\n");
    Console.WriteLine("{0,-15} {1,-22} {2,-22} {3,-22} {4,-34} {5,-15} {6,-13}", "Flight Number", "Airline Name", "Origin", "Destination",
                          "ExpectedDeparture/Arrival Time", "Status", "Boarding Gate");
    foreach (var flight in sortedFlights)
    {
        
        string BoardingGateName = getDetails("Unassigned", flight).BoardingGateName;
        if (BoardingGateName == "")  //if boarding gate name is not assigned
        {
            BoardingGateName = "Unassigned";
        }
        Airline airline = terminal.GetAirlineFromFlight(flight);
        if (airline == null)
        {
            Console.WriteLine("No airlines found.");
        }
        Console.WriteLine("{0,-15} {1,-22} {2,-22} {3,-22} {4,-34} {5,-15} {6,-13}", flight.FlightNumber, airline.Name, flight.Origin, flight.Destination,
                            flight.ExpectedTime, flight.Status, BoardingGateName);
    }
}

(string Code, string BoardingGateName) getDetails(string BoardingGate, Flight flight)
{
    string Code = "";
    if (flight is DDJBFlight)
    {
        Code = "DDJB";
    }
    else if (flight is CFFTFlight)
    {
        Code = "CFFT";
    }
    else if (flight is LWTTFlight)
    {
        Code = "LWTT";
    }
    else
    {
        Code = "None";
    }
    string BoardingGateName = "";
    foreach(var boardingGate in terminal.BoardingGates.Values)
    {
        if (boardingGate.Flight == flight)
        {
            BoardingGateName = boardingGate.GateName;
        }
    }
    return (Code,BoardingGateName);
}


//advanced feature b
void CalculateFees()
{
    double? totalFeesForTerminal = 0;
    double? totalDiscount = 0;
    foreach (var airline in terminal.Airlines.Values)
    {
        double? airlineFee = 0;
        double? flightFee = 0;
        double? airlineDiscount = 0;
        foreach (var flight in airline.Flights.Values)
        {
            if (terminal.Airlines.Values.Count == 0)
            {
                Console.WriteLine("No airlines found.");
     
            }
            string BoardingGateName = getDetails("Unassigned", flight).BoardingGateName;
            if (BoardingGateName == "")  //ensures that boarding gate has been assigned
            {
                Console.WriteLine("Please ensure that you have assigned boarding gates for each of the flights.");
                return;
            }
            //checks if there is special request code and calls the calculate fees method
            if (flight is NORMFlight)
            {
                flightFee = flight.CalculateFees();
            }
            else if (flight is CFFTFlight)
            {
                flightFee = flight.CalculateFees();
            }
            else if (flight is LWTTFlight)
            {
                flightFee = flight.CalculateFees();
            }
            else if (flight is DDJBFlight)
            {
                flightFee = flight.CalculateFees();
            }
            flightFee += 300;   //adds boarding gate fee

            airlineFee += flightFee;  
        }
        airlineDiscount = airline.CalculateFees();  //call airline method to get discount
        totalFeesForTerminal += airlineFee;
        totalDiscount += airlineDiscount;
        //print fees for each airline
        Console.WriteLine($"==================== Total Fees for {airline.Name} ====================");
        Console.WriteLine($"Total Discount: {airlineDiscount}");
        Console.WriteLine($"Final Total Fees to be Collected: {airlineFee}");
        Console.WriteLine($"Discount Percentage: {(airlineDiscount / airlineFee * 100):F2}%");

    }
    Console.WriteLine("\n==================== Total for Terminal =====================");
    Console.WriteLine($"Total Fees for Terminal: {totalFeesForTerminal}");
    Console.WriteLine($"Total Discounts to be Deducted: {totalDiscount}");
    Console.WriteLine($"Final Total Fees to be Collected: {totalFeesForTerminal}");
    Console.WriteLine($"Discount Percentage: {(totalDiscount / totalFeesForTerminal * 100):F2}%");
}
void getthelocations()
{
    foreach (var flight in terminal.Flights.Values)
    {
        if (!Locations.Contains(flight.Origin))
        {
            Locations.Add(flight.Origin);
        }
        if (!Locations.Contains(flight.Destination))
        {
            Locations.Add(flight.Destination);
        }
    }
}
