using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.CLI
{
    public class CampGroundsCLI
    {
        private string _connectionString;

        public CampGroundsCLI(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CampgroundDisplay(Park park)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                CampgroundsDAL campgrounds = new CampgroundsDAL(_connectionString);
                List<CampGround> cgList = new List<CampGround>();
                cgList = campgrounds.GetCampGrounds(park);
                Dictionary<int, CampGround> cgDictionary = new Dictionary<int, CampGround>();

                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("                                Park Campgrounds                                ");
                Console.WriteLine($"                                 {park.Name}                                   ");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine(String.Format("{0, -5} {1,-32} | {2,-15} | {3,-15} | {4,-15}", " ", "Name", "Open", "Close", "Daily Fee"));

                for (int i = 1; i <= cgList.Count; i++)
                {
                    CampGround campground = cgList[i - 1];
                    Console.WriteLine(String.Format("{0, -5} {1,-32} | {2,-15} | {3,-15} | {4,-15}", $"#{i}", campground.Name, campground.OpenMonth, campground.CloseMonth, campground.DailyFee.ToString("c")));
                    cgDictionary.Add(i, campground);

                    
                    //Console.ReadKey();
                }


                Console.WriteLine();
                Console.WriteLine("Select a Command");
                Console.WriteLine("(1) Search for Available Reservation");
                Console.WriteLine("(2) Return to Previous Screen");

                char selCampground = Console.ReadKey().KeyChar;

                if (selCampground == '2')
                {
                    exit = true;
                }
                else
                {
                    ReservationCLI resCLI = new ReservationCLI(_connectionString);
                    resCLI.ReservationDisplay(park);
                }

            }
        }
    }
}
