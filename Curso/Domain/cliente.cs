using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Domain
{
    [Table("Clientes")]
    public class Cliente{
        [Key]
        public int Id {get; set;}
        [Required]
        public string Nome {get; set;}
        [Column("idx_cliente_telefone")]
        public string Telefone {get; set;}
        [Required]
        public string CEP {get; set;}
        [Required]
        public string Estado {get; set;}
        [Required]
        public string Cidade {get; set;}
    }
}