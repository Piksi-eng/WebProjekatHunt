using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models 
{

    [Table("Gun")]
    public class Guns
    {
        
         [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string GunName { get; set; }

        [Range(0,360)]
        public int GunDmg { get; set; }

        public int GunRange { get; set; }

        public int GunSpeed { get; set; }

        public int GunPrice { get; set; }

        [JsonIgnore]
        public virtual List<GunsVariant> GunsVariant { get; set; }

    }
}