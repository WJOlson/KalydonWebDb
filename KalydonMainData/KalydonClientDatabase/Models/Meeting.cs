using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KalydonClientDatabase.Models
{
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Every meeting requires a reason")]
        [StringLength(140)]
        public string MeetingReason { get; set; }
        [Required(ErrorMessage = "Requires a schedule time")]
        [DataType(DataType.Time)]
        public DateTime MeetingSchedule { get; set; }
        [Required(ErrorMessage = "Requires a date to be set")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime MeetingDate { get; set; }
        public bool MeetingAttended { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public Meeting()
        {
            
        }

        public Meeting(int _Id, string _MeetingReason, DateTime _MeetingSchedule, DateTime _MeetingDate, bool _MeetingAttended,
            int _ClientId, Client _Client)
        {
            Id = _Id;
            MeetingReason = _MeetingReason;
            MeetingSchedule = _MeetingSchedule;
            MeetingDate = _MeetingDate;
            MeetingAttended = _MeetingAttended;
            ClientId = _ClientId;
            Client = _Client;

        }
    }
}
