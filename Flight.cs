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
    class Flight
    {
        static List<Flight> flights = new List<Flight>();
        AirPlane airPlane;
        static List<AirPlane> airPlanes = new List<AirPlane>();
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
        public int time { get => Time; }
        public int day{ get => Day; }

        public Flight(int id, string start, string destination, int day, int Time, int duration, int cost, int first, int businus, int premium, int economy)
        {
            Id = id;
            StartLocation = start;
            Destintion = destination;
            Day = day;
            this.Time = Time;
            Duration_hours = duration;
            Cost = cost;//equal to the base cost of the tickets
            Ch_FirstClass = first;
            Ch_Businus = businus;
            Ch_Premium = premium;
            Ch_Economy = economy;
            flights.Add(this);
        }


        //Methods
        public static int IdGiver()
        {
            int max = 0;
            foreach (Flight flight1 in flights)
            {
                if (flight1.Id > max)
                {
                    max = flight1.Id;
                }
            }
            return max + 1;
        }
        public int BaseCostGiver()
        {
            return Cost;
        }
        public void ShowFlightInfo()
        {
            Console.WriteLine($"Id: {Id} StartLoction: {StartLocation}, Destination: {Destintion}\nFlight day: {Day}, flight time: {Time}, flight duration: {Duration_hours} hours, ticket cost: {Cost} ");
            Console.WriteLine($"Firstclass seats: {Ch_FirstClass}, Businus seats: {Ch_Businus}, Premium seats: {Ch_Premium}, Economy seats: {Ch_Economy}");
        }
        public void EditFlight(string startlocation, string destination, int day, int time, int durarion)
        {
            StartLocation = startlocation;
            Destintion = destination;
            if (Day > day)
            {
                throw new Exception("The time is before the last edition");

            }
            else if (day == Day)
            {
                if (Time > time)
                {
                    throw new Exception("The time is before the last edition");
                }
                else
                {
                    Time = time;
                }
            }
            else
            {
                Day = day;
                Time = time;
            }
            if (durarion > 0)
            {
                Duration_hours = durarion;
            }
        }
        public int ReturnFLightId()
        {
            return Id;
        }

        public int ReturnBasicTicket()
        {
            return Cost;
        }

        public bool CheckSeatcount(int i)
        {
            switch (i)
            {
                case 1:
                    if (Ch_FirstClass == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 2:
                    if (Ch_Businus == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 3:
                    if (Ch_Premium == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 4:
                    if (Ch_Economy == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
            }
            return false;
        }
        public void ReserveSeat()
        {
            while (true)
            {
                Console.WriteLine("1. First class, 2. Businus, 3. Premium, 4. Economy");
                string x = Console.ReadLine();
                int t = 0;
                int y;
                try
                {
                    y = int.Parse(x);
                    switch (y)
                    {
                        case 1:
                            if ((Ch_FirstClass - y) >= 0)
                            {
                                Ch_FirstClass -= y;
                                t = 1;
                            }
                            else
                            {
                                Console.WriteLine("There is no enough empty seats to reserve");
                            }
                            break;
                        case 2:
                            if ((Ch_Businus - y) >= 0)
                            {
                                Ch_Businus -= y;
                                t = 1;
                            }
                            else
                            {
                                Console.WriteLine("There is no enough empty seats to reserve");
                            }
                            break;
                        case 3:
                            if ((Ch_Premium - y) >= 0)
                            {
                                Ch_Premium -= y;
                                t = 1;
                            }
                            else
                            {
                                Console.WriteLine("There is no enough empty seats to reserve");
                            }
                            break;
                        case 4:
                            if ((Ch_Economy - y) >= 0)
                            {
                                Ch_Economy -= y;
                                t = 1;
                            }
                            else
                            {
                                Console.WriteLine("There is no enough empty seats to reserve");
                            }
                            break;
                        default:
                            Console.WriteLine("Please enter a correct number");
                            break;
                    }
                    if (t == 1)
                    {
                        Console.WriteLine("Successfully reserved");
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Please Enter a number as an input");
                }
            }
        }
        public static void SeatFree(int Id, int first, int businus, int premium, int economy)
        {
            foreach (Flight flight in flights)
            {
                if (flight.Id == Id)
                {
                    flight.Ch_FirstClass += first;
                    flight.Ch_Businus += businus;
                    flight.Ch_Premium += premium;
                    flight.Ch_Economy += economy;
                }
            }
        }
        static int addSeat()
        {
            while (true)
            {
                Console.Write("Seats number: ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice > 0)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a positive number");
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a number");
                }
            }
        }
        public static double[] BuySeat(int id)
        {
            double[] array = new double[5];
            double cost = 0;
            foreach (Flight flight in flights)
            {
                if (flight.Id == id)
                {
                    Console.WriteLine("firstclass seats: " + flight.Ch_FirstClass);
                    int y;
                    try
                    {
                        y = int.Parse(Console.ReadLine());
                        if ((flight.Ch_FirstClass - y) > 0)
                        {
                            flight.Ch_FirstClass -= y;
                            cost += 2 * y;
                            array[0] = y;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("you did not enter a number means you dont want first class seat");
                    }
                    Console.WriteLine("businus seats: " + flight.Ch_Businus);
                    y = 0;
                    try
                    {
                        y = int.Parse(Console.ReadLine());
                        if ((flight.Ch_Businus - y) > 0)
                        {
                            flight.Ch_Businus -= y;
                            cost += 1.5 * y;
                            array[1] = y;

                        }
                    }
                    catch
                    {
                        Console.WriteLine("you did not enter a number means you dont want first class seat");
                    }

                    y = 0;
                    Console.WriteLine("premium seats: " + flight.Ch_Premium);
                    try
                    {
                        y = int.Parse(Console.ReadLine());
                        if ((flight.Ch_Premium - y) > 0)
                        {
                            flight.Ch_Premium -= y;
                            cost += 1 * y;
                            array[2] = y;

                        }
                    }
                    catch
                    {
                        Console.WriteLine("you did not enter a number means you dont want first class seat");
                    }
                    Console.WriteLine("economy seats: " + flight.Ch_Economy);
                    y = 0;
                    try
                    {
                        y = int.Parse(Console.ReadLine());
                        if ((flight.Ch_Economy - y) > 0)
                        {
                            flight.Ch_Economy -= y;
                            cost += 0.8 * y;
                            array[3] = y;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("you did not enter a number means you dont want first class seat");
                    }
                    array[4] = cost;
                    return array;

                }
            }
            return null;

        }

        public static bool Exist(int id)
        {
            int t = 0;
            foreach (Flight flight in flights)
            {
                if (flight.Id == id)
                {
                    t = 1;
                }
            }
            if (t == 0)
            {
                return false;
            }
            else { return true; }

        }

        public static Flight flightGiver(int id)
        {
            foreach(Flight flight in flights)
            {
                if (flight.Id == id)
                {
                    return flight;
                }
            }
            return null;
        }

        public static void SaveFlights()
        {
            FileStream fs = new FileStream(@"D:\elmos\AP\homeworks\tamrin 4\4.1\first try\Transport managing\Transport managing\bin\Debug\Flights\Flights.txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, flights);
            fs.Close();
        }
        public static void ReadFlights()
        {
            FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 4\4.1\first try\Transport managing\Transport managing\bin\Debug\Flights\Flights.txt", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try { flights = (List<Flight>)formatter.Deserialize(fs); }
            catch
            {

            }
            finally { fs.Close(); }
        }
    }
}


