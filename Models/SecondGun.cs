using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models 
{

    [Table("SecondGun")]
    public class SecondGun
    {
        
         [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string SGunName { get; set; }

        [Range(0,360)]
        public int SGunDmg { get; set; }

        public int SGunRange { get; set; }

        public int SGunSpeed { get; set; }

        public int SGunPrice { get; set; }

    }
}