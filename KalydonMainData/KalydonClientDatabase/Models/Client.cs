using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalydonClientDatabase.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"[A-Z]{1}[a-z]+\s+[A-Z]{1}[a-z]+\s*", ErrorMessage = "All first and second names are needed and must have capital letters.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [MaxLength(320)]
        [EmailAddress]
        public string EmailAddr { get; set; }
        //public virtual ICollection<Project> Projects { get; set; }
        public Client()
        {

        }

        public Client(int _Id, string _Name, string _PhoneNumber, string _EmailAddr) 
        { 
            Id = _Id;
            Name = _Name;
            PhoneNumber = _PhoneNumber;
            EmailAddr = _EmailAddr;
            //Projects = _Projects;
        }
    }
}
