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
    class Ticket
    {
        static List<Ticket> tickets = new List<Ticket>();
        int TicketId;//distinct
        int FLightId;
        int Ch_FirstClass;
        int Ch_Businus;
        int Ch_Premium;
        int Ch_Economy;
        double Cost;
        int TicketBaseCost;
        public Ticket(int FLightId)
        {
            this.FLightId = FLightId;
            //this.TicketBaseCost = TicketBasecost;
            this.TicketId = IdGiver();
            this.TicketBaseCost = Flight.flightGiver(this.FLightId).BaseCostGiver();

        }
        static int IdGiver()
        {
            int max = 0;
            foreach (Ticket ticket in tickets)
            {
                if (ticket.TicketId > max)
                {
                    max = ticket.TicketId;
                }
            }
            return max + 1;
        }
        //Methods
        public static void Sell()
        {
            int Id;
            while (true)
            {
                Console.Write("Flihgt Id: ");
                try
                {
                    Id = int.Parse(Console.ReadLine());
                    if (Flight.Exist(Id))
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid");
                }
            }
            //while (true)
            //{
            //    Console.Write("Base cost: ");
            //    try
            //    {
            //        cost = int.Parse(Console.ReadLine());
            //        break;
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Invalid");
            //    }
            //}
            Ticket ticket = new Ticket(Id);
            ticket.Sell(Id);
            tickets.Add(ticket);




        }
        public void Sell(int Id)
        {

            double[] array = new double[5];
            if (Flight.Exist(Id))
            {
                array = Flight.BuySeat(Id);
                Ch_FirstClass = (int)array[0];
                Ch_Businus = (int)array[1];
                Ch_Premium = (int)array[2];
                Ch_Economy = (int)array[3];
                Cost = TicketBaseCost * array[4];

            }
            else
            {
                Console.WriteLine("Couldn't find the Id");
            }

        }
        public double Refund()
        {
            DateTime dateTime = DateTime.Now;
            int day = (int)dateTime.Day;
            int hour = (int)dateTime.Hour;
            int minutes = (int)dateTime.Minute;

            Flight flight = Flight.flightGiver(this.FLightId);

            Flight.SeatFree(FLightId, Ch_FirstClass, Ch_Businus, Ch_Premium, Ch_Economy);
            tickets.Remove(this);
            if (day > flight.day)
            {
                Console.WriteLine("The flight has been finished and we can give all you money back");
                return (Cost) * 0;
            }
            else if (day == flight.day)
            {
                if (hour >= flight.time)
                {
                    Console.WriteLine("The flight has been finished and we can give all you money back");
                    return (Cost) * 0;
                }
                else if (flight.time - hour == 1)
                {
                    return (Cost) * 0.9;
                }
                else
                {
                    return (Cost) * 0.5;
                }
            }
            else
            {
                return (Cost) * 0.1;
            }
        }

        public static void Edit()
        {
            int t = 0;
            int Id;
            while (true)
            {
                Console.Write("Id:");
                try
                {
                    Id = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter a number");
                }
            }
            foreach (Ticket ticket in tickets)
            {
                if (ticket.TicketId == Id)
                {
                    Flight.SeatFree(ticket.FLightId, ticket.Ch_FirstClass, ticket.Ch_Businus, ticket.Ch_Premium, ticket.Ch_Economy);
                    ticket.Sell(ticket.FLightId);
                    t = 1;
                }
            }
            if (t == 0)
            {
                Console.WriteLine("Could not find such an Id for a ticket");
            }
        }




        public static double DeleteFlightTickets(int Id)
        {
            //int t = 0;
            double cost = 0;
            foreach (Ticket ticket in tickets)
            {
                if (ticket.FLightId == Id)
                {
                    Flight.SeatFree(ticket.FLightId, ticket.Ch_FirstClass,ticket.Ch_Businus, ticket.Ch_Premium, ticket.Ch_Economy);
                    cost += ticket.Cost;
                    tickets.Remove(ticket);
                    //t = 1;
                }
            }
            //if(t == 1)
            //{
            //    Console.WriteLine("Tickets deleted successfully");
            //}
            //else
            //{
            //    Console.WriteLine("could not find the Id");
            //}
            return cost;
        }
        public static Ticket TicketReturner(int Id)
        {
            foreach(Ticket ticket in tickets)
            {
                if(ticket.TicketId == Id)
                {
                    return ticket;
                }
            }
            return null;
        }
        public static void SaveTickets()
        {
            FileStream fs = new FileStream(@"D:\elmos\AP\homeworks\tamrin 4\4.1\first try\Transport managing\Transport managing\bin\Debug\Tickets\Tickets.txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, tickets);
            fs.Close();
        }
        public static void ReadTickets()
        {
            FileStream fs = File.Open(@"D:\elmos\AP\homeworks\tamrin 4\4.1\first try\Transport managing\Transport managing\bin\Debug\Flights\Tickets.txt", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try { tickets = (List<Ticket>)formatter.Deserialize(fs); }
            catch
            {

            }
            finally { fs.Close(); }
        }
    }
}
