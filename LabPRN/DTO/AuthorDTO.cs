using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPRN
{
    public class AuthorDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime birth { get; set; }
        public string place { get; set; }

        public AuthorDTO(string id, string name, DateTime birth, string place)
        {
            this.id = id;
            this.name = name;
            this.birth = birth;
            this.place = place;
        }

        public AuthorDTO()
        {
        }
    }
}
