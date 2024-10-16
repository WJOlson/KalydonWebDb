using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalydonClientDatabase.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime EndDate { get; set; }
        //[Required]
        //public int ClientId { get; set; }
        //[ForeignKey("ClientId")]
        //public virtual Client Client { get; set; }
        
        public Project()
        {

        }
        public Project(int _Id, string _Name, DateTime _StartDate, DateTime _EndDate) 
        { 
            Id = _Id;
            Name = _Name;
            StartDate = _StartDate;
            EndDate = _EndDate;
            //ClientId = _ClientId;
            //Client = _Client;
            

        }
    }
}
