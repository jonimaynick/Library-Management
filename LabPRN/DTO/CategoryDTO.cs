using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPRN
{
    public class CategoryDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public CategoryDTO(string iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public CategoryDTO()
        {
        }
    }
}
