﻿namespace Billing.Api.Models
{
    public class ShipperModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public int TownId { get; set; }
    }
}