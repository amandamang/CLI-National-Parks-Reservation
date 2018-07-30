
using System;
using System.Collections.Generic;
using Capstone.CLI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.CLI
{
    class WelcomeMenuCLI
    {
        private string _connectionString;

        public WelcomeMenuCLI(string connectionString)
        {
            _connectionString = connectionString;
        }

        
        public void Display()
        {
            bool exit = false;
            while(!exit)
            {
                Console.Clear();
                Console.WriteLine("************ Welcome to the National Park Campsite Reservation Center! ************");
                Console.WriteLine();
                Console.WriteLine("Select a park for more options: ");

                ParksDAL parks = new ParksDAL(_connectionString);
                List<Park> parkList = new List<Park>();
                parkList = parks.GetParks();
                Dictionary<int, Park> parkDictionary = new Dictionary<int, Park>();

                for (int i = 1; i <= parkList.Count; i++)
                {
                    Park park = parkList[i-1];
                    Console.WriteLine($"({i}) {park.Name}");
                    parkDictionary.Add(i, park);
                }
                Console.WriteLine("(q) Quit");

                char selPark = Console.ReadKey().KeyChar;

                
                if (selPark == 'q' || selPark == 'Q')
                {
                    exit = true;
                }
                else
                {
                    int i = int.Parse(selPark.ToString());
                    Park park = parkDictionary[i];
                    DisplayParkDetailMenu(park);
                }
            }
        }

        void DisplayParkDetailMenu(Park park)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine(park.Name);
                Console.WriteLine($"Location: {park.Location}");
                Console.WriteLine($"Established: {park.EstablishDate}");
                Console.WriteLine($"Area: {park.Area.ToString("N0")} sq km");
                Console.WriteLine($"Annual Visitors: {park.Visitors.ToString("N0")}");
                Console.WriteLine();
                Console.WriteLine($"{park.Description}");

                Console.WriteLine();
                Console.WriteLine("Select a Command:");
                Console.WriteLine("(1) View Campgrounds");
                Console.WriteLine("(2) Search for Reservation");
                Console.WriteLine("(3) Return to Previous Screen");

                char selCommand = Console.ReadKey().KeyChar;

                if (selCommand == '1')
                {
                    CampGroundsCLI cgCLI = new CampGroundsCLI(_connectionString);
                    cgCLI.CampgroundDisplay(park);
                }
                else if (selCommand == '2')
                {

                }
                else if (selCommand == '3')
                {
                    exit = true;
                }
            }
        }
    }
}
