using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone.Classes
{
    public class MainMenu
    {
        //biz class object

        public void Display()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("*********Welcome to the National Park Campsite Reservation*********");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Please choose from the following:");
                Console.WriteLine("(1) View list of available parks");
                Console.WriteLine("(Q) Quit application");

                Console.Write("Which option would you like to select? ");
                char input = Console.ReadKey().KeyChar;

                if (input == '1')
                {
                    exit = true;
                    //DisplayItems();
                }
                
                else if (input == 'Q' || input == 'q')
                {
                    return;
                }
            }
        
        }
        private void AvailableParks(bool ignorePressKey = false)
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.Write("Location \t Item \t $ Price");
                Console.WriteLine("--------------------------");

                _vendingMachine.Display();

                Console.WriteLine();
                Console.WriteLine("(P) Go To Purchase Menu");
                Console.WriteLine("(Q) Quit application");
                Console.WriteLine("Which option would you like to select? ");
                char input = Console.ReadKey().KeyChar;

                if (input == 'p')
                {
                    exit = true;
                    Purchase();
                }
                else if (input == 'q')
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option");
                }
            } while (!exit);
        }

    }
}
