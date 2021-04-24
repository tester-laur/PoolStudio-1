using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PoolStudio.WEB.Models
{
    public class Clasification
    {
        [Key]
        public int ClasificationId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(30, ErrorMessage = "Limite de caracteres excedido.")]
        [Display(Name = "Categoría")]
        public string ItemType { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
