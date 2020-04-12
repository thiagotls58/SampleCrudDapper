using System;
using System.ComponentModel.DataAnnotations;

namespace SampleCrudDapper.Models
{
    public class Produtos
    {
        [Key]
        [Display(Name = "Código")]
        public int ProdutoId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a 1")]
        [Display(Name = "Estoque")]
        public int Estoque { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter entre 1 até 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Preço")]
        public double Preco { get; set; }
    }
}
