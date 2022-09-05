using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPRN
{
    public class UserDTO
    {
        public string userID { get; set; }
        public string password { get; set; }
        public string roleName { get; set; }
        public string name { get; set; }
        public DateTime birthday { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool status { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(string userID, string password, string roleName, string name, DateTime birthday, string address, string phone, string email, bool status)
        {
            this.userID = userID;
            this.password = password;
            this.roleName = roleName;
            this.name = name;
            this.birthday = birthday;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.status = status;
        }
    }
}
