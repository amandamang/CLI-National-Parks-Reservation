using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CampGround
    {
        private Dictionary<int, string> _cgNum = new Dictionary<int, string>
        {
            {1, "Blackwoods" },
            {2, "Seawall" },
            {3, "Schoodic Woods" },
            {4, "Devil's Garden" },
            {5, "Canyon Wren Group Site" },
            {6, "Juniper Group Site" },
            {7, "The Unnamed Primitive Campsites" },
        };


        private Dictionary<int, string> _months = new Dictionary<int, string>
        {
            {1, "January" },
            {2, "February" },
            {3, "March" },
            {4, "April" },
            {5, "May" },
            {6, "June" },
            {7, "July" },
            {8, "August" },
            {9, "September" },
            {10, "October" },
            {11, "November" },
            {12, "December" },
        };

        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenFromMm { get; set; }
        public int OpenToMm { get; set; }
        public double DailyFee { get; set; }

        public string OpenMonth
        {
            get
            {
                return _months[OpenFromMm];
            }
        }
        public string CloseMonth
        {
            get
            {
                return _months[OpenToMm];


            }
        }
    }
}
