using AppControle.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.DTO.Request
{
    public class MonthlyFeeRequestCreate
    {
        public DateTime DueDate { get; set; }
        public DateTime Reference { get; set; }
        public List<MonthlyFeeCreateDTO> lMonthliFee { get; set; }


    }
}
