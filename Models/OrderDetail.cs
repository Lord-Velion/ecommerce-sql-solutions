using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSqlSolutions.Models;

[Table("OrderDetails")]
public class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public int GoodId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "GoodCount must be greater than 0")]
    public int GoodCount { get; set; }

    public Order Order { get; set; }
    public Good Good { get; set; }
}
