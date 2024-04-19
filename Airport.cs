using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Transport_managing
{
    [Serializable]
    public struct AirPlane
    {
        public enum AirLine { IranAir = 34, Caspian = 21, Mahan = 18, kish = 45 };
        public enum AirPlaneModel { Airbus = 33, Boeing = 31, Fokker = 44, Douglas = 38 }
        AirLine airLine;
        AirPlaneModel airPlaneModel;
        string Name;
        int Ch_FirstClass;
        int Ch_Businus;
        int Ch_Premium;
        int Ch_Economy;
        //constructor
        public void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}, ariline: {airLine}, airplane model: {airPlaneModel}, firstclass seats: {Ch_FirstClass}, Businus seats: {Ch_Businus}, Premium seats: {Ch_Premium}, Economy seats: {Ch_Economy}");
        }
        public AirPlane(int airline, int airplanemodel, string name, int ch_first, int ch_businus, int ch_premium, int ch_economy)
        {
            if (check(ch_first, ch_businus, ch_premium, ch_economy))//for checking if all of the seats are zero or not
            {

                throw new Exception("every seats can not be zero!!");
            }
            airLine = (AirLine)(airline);
            airPlaneModel = (AirPlaneModel)(airplanemodel);
            Name = name;
            Ch_FirstClass = ch_first;
            Ch_Businus = ch_businus;
            Ch_Premium = ch_premium;
            Ch_Economy = ch_economy;

        }
        static bool check(params int[] numbers)//for checking if all of the seats are zero or not
        {
            int x = 0;
            foreach (int num in numbers)
            {
                if (num == 0)
                {
                    x++;
                }
            }
            if (x >= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static AirPlaneModel? ModelFinder(int x)
        {
            switch (x)
            {
                case 1:
                    return AirPlaneModel.Airbus;
                    break;
                case 2:
                    return AirPlaneModel.Boeing;
                    break;
                case 3:
                    return AirPlaneModel.Fokker;
                    break;
                case 4:
                    return AirPlaneModel.Douglas;
                    break;
                default:
                    return null;
            }

        }
        public static AirLine? airLineFinder(int x)
        {
            switch (x)
            {
                case 1:
                    return AirLine.IranAir;
                    break;
                case 2:
                    return AirLine.Caspian;
                    break;

                case 3:
                    return AirLine.Mahan;
                    break;

                case 4:
                    return AirLine.kish;
                    break;
                default:
                    return null;
                    break;

            }
        }
        public static AirLine? choseAirline()
        {
            while (true)
            {
                string x;
                int y;
                Console.Write("AriLine(1.IranAir, 2.Caspian, 3.Mahan, 4.kish) : ");
                x = Console.ReadLine();
                //if (x == "")
                //{
                //    break;
                //}
                try
                {
                    int t = 1;
                    y = int.Parse(x);
                    AirLine? airLine1;
                    airLine1 = airLineFinder(y);
                    if (airLine1 != null)
                    {
                        return (AirLine)airLine1;
                    }
                    else
                    {
                        t = 0;
                        Console.WriteLine("Please enter a correct number");
                    }
                    if (t == 1)
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a number as an input");
                }
            }
            return null;
        }
        public void Edit()
        {
            string x;
            int y;
            airLine = (AirLine)choseAirline();
            Console.Write("Name: ");
            x = Console.ReadLine();
            if (x != "")
            {
                Name = x;
            }
            int Sum = Ch_Premium + Ch_Economy;
            while (true)
            {
                Console.WriteLine("Premium seats: ");
                x = Console.ReadLine();
                if (x == "")
                {
                    break;
                }
                else
                {


                    try
                    {
                        y = int.Parse(x);
                        if (y > Sum)
                        {
                            Console.WriteLine("Please enter a correct number");
                        }
                        else
                        {
                            Ch_Premium = y;//for being the same value as it was previously
                            Ch_Economy = Sum - y;
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a number as an input");
                    }
                }
            }
            while (true)
            {
                Console.WriteLine("Economy seats: ");
                x = Console.ReadLine();
                if (x == "")
                {
                    break;
                }
                else
                {


                    try
                    {
                        y = int.Parse(x);
                        Ch_Economy = y;//for being the same value as it was previously
                        Ch_Premium = Sum - y;
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a number as an input");
                    }
                }
            }
        }




    }
    [Serializable]
    class Airport
    {
        public string Name = null;
        private List<Flight> flights = new List<Flight>();
        private List<AirPlane> airPlanes = new List<AirPlane>();
        public Airport()
        {
            Console.Write("Name: ");
            Name = Console.ReadLine();
            //List<Flight> flights = new List<Flight>();
            //List<AirPlane> airPlanes = new List<AirPlane>();
            Console.WriteLine("The airPort has been built successfully");
        }

        public void AddAirPlane()
        {
            string x;
            int y;
            Console.Write("Name: ");
            string name = Console.ReadLine();
            int airline = (int)AirPlane.choseAirline();
            int kind;
            while (true)
            {
                Console.Write("Airplane Model (1. Airbus, 2. Boeing, 3. Fokker, 4. Douglas): ");
                x = Console.ReadLine();
                try
                {
                    y = int.Parse(x);
                    if (AirPlane.ModelFinder(y) != null)
                    {
                        kind = (int)(AirPlane.ModelFinder(y));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a correct number");
                    }

                }
                catch
                {
                    Console.WriteLine("Please enter a number");
                }

            }
            int EcoSeats;
            while (true)
            {
                Console.WriteLine("Economy seats: ");
                x = Console.ReadLine();
                if (parser(x) != null)
                {
                    EcoSeats = (int)parser(x);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a correct number");
                }

            }
            int PreSeats;
            while (true)
            {
                Console.WriteLine("Premium seats: ");
                x = Console.ReadLine();
                if (parser(x) != null)
                {
                    PreSeats = (int)parser(x);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a correct number");
                }

            }
            int BusSeats;
            while (true)
            {
                Console.WriteLine("Businus seats: ");
                x = Console.ReadLine();
                if (parser(x) != null)
                {
                    BusSeats = (int)parser(x);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a correct number");
                }

            }
            int FirstSeats;
            while (true)
            {
                Console.WriteLine("Firstclass seats: ");
                x = Console.ReadLine();
                if (parser(x) != null)
                {
                    FirstSeats = (int)parser(x);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a correct number");
                }

            }
            try
            {
                airPlanes.Add(new AirPlane(airline, kind, name, FirstSeats, BusSeats, PreSeats, EcoSeats));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AddFlight()
        {
            int Id;//Distinct
            string StartLocation;
            string Destintion;
            int Day;
            int Time;
            int Duration_hours;
            int Cost;//not zero
                     //Empty seats
            int Ch_FirstClass;
            int Ch_Businus;
            int Ch_Premium;
            int Ch_Economy;
            Console.Write("Id:");
            Id = Flight.IdGiver();
            Console.Write("Start location:");
            StartLocation = Console.ReadLine();
            Console.Write("Destination:");
            Destintion = Console.ReadLine();
            Console.Write("day:");
            Day = Parser();
            Console.Write("Time:");
            Time = Parser();
            Console.Write("Duration:");
            Duration_hours = Parser();
            Console.Write("cost:");
            Cost = Parser();
            Console.Write("First class seats:");
            Ch_FirstClass = Parser();
            Console.Write("Businus seats:");
            Ch_Businus = Parser();
            Console.Write("Premium seats:");
            Ch_Premium = Parser();
            Console.Write("Economy seats:");
            Ch_Economy = Parser();
            Flight flight = new Flight(Id, StartLocation, Destintion, Day, Time, Duration_hours, Cost, Ch_FirstClass, Ch_Businus, Ch_Premium, Ch_Economy);
            flights.Add(flight);
        }

        public void ShowFLights()
        {
            foreach (Flight flight in flights)
            {
                flight.ShowFlightInfo();
            }
        }

        public void DeleteFlight(int Id)
        {
            int t = 0;
            foreach (Flight flight in flights)
            {
                if (flight.ReturnFLightId() == Id)
                {
                    t = 1;
                    flight.ShowFlightInfo();
                    Console.WriteLine("do you want to cancel the flight?(y/n)");
                    string x = Console.ReadLine();
                    switch (x)
                    {
                        case "y":
                            flights.Remove(flight);
                            break;
                        case "n":
                            break;
                        default:
                            Console.WriteLine("Please enter a correct charachter");
                            break;
                    }
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Could not fing such an Id for a flight");
            }
            Ticket.DeleteFlightTickets(Id);
        }
        public void SellTicket()
        {
            Ticket.Sell();
        }



        public void RefundTicket()
        {
            Console.Write("Ticket Id: ");
            int Id = Parser();
            Ticket ticket = Ticket.TicketReturner(Id);
            if (ticket != null)
            {
                ticket.Refund();
            }
            else
            {
                Console.WriteLine("could not find the user name");
            }

        }


        public void EditTicket()
        {
            Ticket.Edit();
        }
        public static int Parser()
        {
            int y = 0;

            while (true)
            {
                int t = 0;
                string x = Console.ReadLine();
                try
                {
                    y = int.Parse(x);
                    t = 1;
                }
                catch
                {
                    Console.WriteLine("Enter a number");
                }
                if (t == 1)
                {
                    break;
                }
            }
            return y;
        }
        public static int? parser(string x)
        {
            try
            {
                int y = int.Parse(x);
                return y;
            }
            catch
            {
                return null;
            }
        }
        public void ShowAirplane()
        {
            foreach (AirPlane airPlane in airPlanes)
            {
                airPlane.ShowInfo();
            }
        }
        public void SaveAirport()
        {
            FileStream fs = new FileStream(@"D:\elmos\AP\homeworks\tamrin 4\4.1\first try\Transport managing\Transport managing\bin\Debug\Airport\Airport.txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
            fs.Close();
        }
        public static Airport ReadAirport()
        {
            FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 4\4.1\first try\Transport managing\Transport managing\bin\Debug\Airport\Airport.txt", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Airport airport = (Airport)formatter.Deserialize(fs);
            fs.Close();
            return airport;
        }




    }
}
