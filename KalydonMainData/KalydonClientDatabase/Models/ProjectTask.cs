using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KalydonClientDatabase.Models
{
    public class ProjectTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CompletionTime { get; set; }
        public bool IsCompleted { get; set; }
        
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public ProjectTask()
        {

        }
        public ProjectTask(int _Id, string _Name, DateTime _CompletionTime, bool _IsCompleted, int _ProjectId, Project _Project)
        {
            Id = _Id;
            Name = _Name;  
            CompletionTime = _CompletionTime;
            IsCompleted = _IsCompleted;
            ProjectId = _ProjectId;
            Project = _Project;

        }
    }
}
