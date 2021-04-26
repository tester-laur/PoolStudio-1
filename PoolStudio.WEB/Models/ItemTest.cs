using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PoolStudio.WEB.Models
{
    public class ItemTest
    {
        [Key]
        public int ItemTestId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Límite de caracteres excedido")]
        [Display(Name = "Nombre")]
        public string ItemName { get; set; }

        [StringLength(100, ErrorMessage = "Límite de caracteres excedido")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [StringLength(100, ErrorMessage = "Límite de caracteres excedido")]
        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [MaxLength(1000, ErrorMessage = "Límite de caracteres excedido.")]
        [Display(Name = "Descripción")]
        public string Comment { get; set; }
    }
}
