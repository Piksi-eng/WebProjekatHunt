using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models 
{
        public class Loadout
        {
            [Key]
            public int ID { get; set; }

            [Required]
            [MaxLength(50)]
            public string LName { get; set; }

            public virtual SecondGun SecondGun { get; set; }

            public virtual Tools Tool1 { get; set; }
            public virtual Tools Tool2 { get; set; }
            public virtual Tools Tool3 { get; set; }
            public virtual Tools Tool4 { get; set; }

            [JsonIgnore]
            public virtual GunsVariant GunsVariant { get; set; }
            

        }
}