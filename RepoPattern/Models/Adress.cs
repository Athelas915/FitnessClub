﻿namespace FC.Data.Models
{
    class Adress : BaseEntity
    {
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
