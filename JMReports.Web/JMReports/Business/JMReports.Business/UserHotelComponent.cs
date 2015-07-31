
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JMReports.Entities;
using JMReports.Data;

namespace JMReports.Business
{
    public partial class UserHotelComponent
    {
        private UserHotelDAL userhotelDAC;
        public UserHotelComponent()
        {
            userhotelDAC = new UserHotelDAL();
        }
        public List<Hotel> getUserHotel(int userId)
        {

            return userhotelDAC.getUserHotel(userId);
        }


        public int setUserHotel(int userId, List<Hotel> hotelList)
        {
            int returnVal = 0;
            //Delete all hotels by userid
            returnVal = userhotelDAC.deleteUserHotel(userId);

            foreach (var item in hotelList)
            {
                returnVal &= userhotelDAC.insertUserHotel(userId, item.HotelId);
            }

            return returnVal;
        }
    }
}
