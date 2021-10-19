using CursoEFCore.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Domain
{
    [Table("Produtos")]
    public class Produto{
        public int Id {get; set;}
        [Required]
        public string CodigoBarras {get; set;}
        public string Descricao {get; set;}
        public decimal Valor {get; set;}
        public TipoProduto TipoProduto {get; set;}
        public bool Ativo {get; set;}
    }
}