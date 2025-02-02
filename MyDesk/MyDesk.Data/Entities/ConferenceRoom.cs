﻿#nullable disable

namespace MyDesk.Data.Entities
{
    public partial class ConferenceRoom
    {
        public ConferenceRoom()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int Capacity { get; set; }
        public bool? IsDeleted { get; set; }
        public int? IndexForOffice { get; set; }
        public int OfficeId { get; set; }

        public virtual Office Office { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}