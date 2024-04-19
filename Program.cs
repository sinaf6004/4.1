using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_managing
{
    class Program
    {
        static void Main(string[] args)
        {
            Airport airport;
            try
            {
                airport = Airport.ReadAirport();
                try
                {
                    Ticket.ReadTickets();
                }
                catch { }
                try
                {
                    Flight.ReadFlights();
                }
                catch { }
            }
            catch
            {
                airport = new Airport();

            }



            while (true)
            {
                Console.WriteLine("1. Add airplane, 2. Show air plane, 3. Add flight, 4. Show Flights, 5. Delete Flight, 6. Sell Ticket, 7. Refund Ticket, 8. Edit Ticket");
                int choice = 0;

                try
                {
                    choice = int.Parse(Console.ReadLine());

                }

                catch
                {
                    Console.WriteLine("Enter a number");
                }
                if (choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            airport.AddAirPlane();
                            break;
                        case 2:
                            airport.ShowAirplane();
                            break;
                        case 3:
                            airport.AddFlight();
                            break;
                        case 4:
                            airport.ShowFLights();
                            break;
                        case 5:
                            Console.Write("Id: ");
                            airport.DeleteFlight(Airport.Parser());
                            break;
                        case 6:
                            airport.SellTicket();
                            break;
                        case 7:
                            airport.RefundTicket();
                            break;
                        case 8:
                            airport.EditTicket();
                            break;

                    }
                }
                //try
                //{
                Flight.SaveFlights();

                //}
                //catch
                //{

                //}
                //try
                //{
                Ticket.SaveTickets();

                //}
                //catch
                //{

                //}
                //try
                //{
                airport.SaveAirport();

                //}
                //catch
                //{

                //}
            }
        }
    }
}
