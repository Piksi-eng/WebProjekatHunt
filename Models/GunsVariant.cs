using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class GunsVariant
    {
        [Key]
        public int ID { get; set; }

        //public virtual Loadout Loadout { get; set ;}

        public int GunID { get; set; }

        public int VariantID { get; set; }

        [JsonIgnore]
        public virtual Guns Gun { get; set; }

        [JsonIgnore]
        public virtual Variant Variant { get; set; }


    }
}