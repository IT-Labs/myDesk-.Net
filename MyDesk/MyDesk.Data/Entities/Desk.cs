﻿#nullable disable

namespace MyDesk.Data.Entities
{
    public partial class Desk
    {
        public Desk()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Categories { get; set; }
        public bool? IsDeleted { get; set; }
        public int? IndexForOffice { get; set; }
        public int OfficeId { get; set; }
        public int? CategorieId { get; set; }

        public virtual Category Categorie { get; set; }
        public virtual Office Office { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}