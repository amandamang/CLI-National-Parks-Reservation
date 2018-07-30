using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.CLI
{
    public class ReservationCLI
    {
        private string _connectionString;

        public ReservationCLI(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ReservationDisplay(Park park)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                CampgroundsDAL campgrounds = new CampgroundsDAL(_connectionString);
                List<CampGround> cgList = new List<CampGround>();
                cgList = campgrounds.GetCampGrounds(park);
                Dictionary<int, CampGround> cgDictionary = new Dictionary<int, CampGround>();

                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                      Park Campgrounds                                          ");
                Console.WriteLine($"                                          {park.Name}                                          ");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine(String.Format("{0, -5} {1,-32} | {2,-15} | {3,-15} | {4,-15}", " ", "Name", "Open", "Close", "Daily Fee"));

                CampGround campground = new CampGround();
                for (int i = 1; i <= cgList.Count; i++)
                {
                    campground = cgList[i - 1];
                    Console.WriteLine(String.Format("{0, -5} {1,-32} | {2,-15} | {3,-15} | {4,-15}", $"#{i}", campground.Name, campground.OpenMonth, campground.CloseMonth, campground.DailyFee.ToString("c")));
                    cgDictionary.Add(i, campground);
                }


                Console.WriteLine();
                Console.WriteLine("Which campground (enter 0 to cancel)?");
                string selCampground = Console.ReadLine();

                if (selCampground == "0")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine();
                    ReservationDAL reservations = new ReservationDAL(_connectionString);
                    Reservation reservation = new Reservation();
                    
                    // try catch
                    Console.WriteLine("What is the arrival date? (mm/dd/yyyy)");
                    DateTime arrivalDate = Convert.ToDateTime(Console.ReadLine());
                    reservation.FromDate = arrivalDate;
                    Console.WriteLine("What is the departure date? (mm/dd/yyyy)");
                    DateTime departureDate = Convert.ToDateTime(Console.ReadLine());
                    reservation.ToDate = departureDate;
                    
                    //CampGround campground = new CampGround();
                    int cgId = cgDictionary[int.Parse(selCampground)].CampgroundId;
                    CampGround camp = cgDictionary[int.Parse(selCampground)];
                    arrivalDate = Convert.ToDateTime(arrivalDate);
                    departureDate = Convert.ToDateTime(departureDate);

                    //List<Reservation> resList = new List<Reservation>();

                    var resList = reservations.SearchReservations(reservation);


                    bool toExit = false;
                    while (!toExit)
                    {

                        bool isAvailable = (resList.Count != 0);
                        if (isAvailable)
                        {
                            Console.Clear();
                            Console.WriteLine("Results Matching Your Search Criteria: ");
                            Console.WriteLine();

                            Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,-18} | {3,-15} | {4,-15} | {5,-15}", "Site No.", "Max Occup.", "Accessible?", "Max RV Length", "Utility", "Cost"));

                            TimeSpan totalTime = departureDate - arrivalDate;
                            int totalDays = totalTime.Days;


                            Site site = new Site();
                            SiteDAL sites = new SiteDAL(_connectionString);
                            List<Site> siteList = new List<Site>();
                            siteList = sites.GetSites(camp);
                            Dictionary<int, Site> siteDictionary = new Dictionary<int, Site>();

                            //double totalCost = campground.DailyFee * totalDays;

                            for (int i = 1; i <= siteList.Count; i++)
                            {
                                site = siteList[i - 1];
                                Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,-18} | {3,-15} | {4,-15} | {5,-15}", site.SiteNumber, site.MaxOccupancy, site.Accessible, site.MaxRvLength, site.Utilities, (campground.DailyFee * totalDays).ToString("c")));
                                siteDictionary.Add(i, site);
                            }

                            Console.WriteLine();
                            Console.WriteLine("Select a site to reserve: ");
                            string selSite = Console.ReadLine();
                            site.SiteNumber = int.Parse(selSite);

                            Console.WriteLine("Enter a name for the reservation: ");
                            string resName = Console.ReadLine();
                            //reservations.CreateReservation(reservation);
                            //reservation.Name = resName;
                            //reservation.ToDate = arrivalDate;
                            //reservation.FromDate = departureDate;

                            reservations.CreateReservation(resName, arrivalDate, departureDate, site.SiteNumber);




                            Random random = new Random();
                            int confirmNum = random.Next(000000000, 999999999);
                            Console.WriteLine($"Reservation made. Your confirmation number is: {confirmNum}.");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("The campsite you have chosen is not available for those dates.");
                            Console.WriteLine("(1) Select new dates");
                            Console.WriteLine("(2) Return to previous menu");

                            string selection = Console.ReadLine();
                            
                            if(selection == "2")
                            {
                                toExit = true;
                            }
                            else
                            {
                                Console.ReadKey();
                            }

                        }

                    }
                    // arrivalDate.CompareTo()
                    // or create a SQL statement that where date is before date and date is after date
                    
                }
            }
        }
    }
}
