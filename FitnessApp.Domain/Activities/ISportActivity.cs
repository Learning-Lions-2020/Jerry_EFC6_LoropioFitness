using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Domain.Activities
{
    public class ISportActivity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Distance { get; set; }
        public int UserId { get; set; }
    }
}
