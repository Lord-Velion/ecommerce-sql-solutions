using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSqlSolutions.Models;

[Table("GoodProperties")]
public class GoodProperty
{
    [Key]
    public int Id { get; set; }

    public int GoodId { get; set; }
    public int ColorId { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Bdate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Edate { get; set; }

    public Good Good { get; set; }
    public Color Color { get; set; }
}
