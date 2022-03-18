using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models 
{


    public class Variant
    {
        
         [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string VariantName { get; set; }
        public int DmgC { get; set; }

        public int RangeC { get; set; }

        public int SpeedC { get; set; }

        public int PriceC { get; set; }
        
        [JsonIgnore]
        public virtual List<GunsVariant> VariantGuns { get; set; }

    }
}