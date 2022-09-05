using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPRN
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string userName { get; set; }
        public int quantity { get; set; }
        public DateTime releaseDate { get; set; }

        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        public HistoryDTO(int id, string Title, string userName,int quantity, DateTime releaseDate, DateTime dueDate, string status)
        {
            this.quantity = quantity;
            Id = id;
            this.Title = Title;
            this.userName = userName;
            this.releaseDate = releaseDate;
            DueDate = dueDate;
            Status = status;
        }

        public HistoryDTO()
        {
        }
    }
}
