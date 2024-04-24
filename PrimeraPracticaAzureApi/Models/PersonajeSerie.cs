using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeraPracticaAzureApi.Models
{
    [Table("PERSONAJESSERIES")]
    public class PersonajeSerie
    {
        [Key]
        [Column("IDPERSONAJE")]
        public int IdPersonaje { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("SERIE")]
        public string Serie { get; set; }
    }
}
