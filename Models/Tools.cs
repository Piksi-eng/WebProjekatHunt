using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models 
{

    [Table("Tools")]
    public class Tools
    {
        
         [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ToolName { get; set; }

        public string ToolDescription { get; set; }

        public int ToolPrice { get; set; }

    }
}