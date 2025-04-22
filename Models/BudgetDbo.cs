using GeoSolucoesAPI.Helpers;
using GeoSolucoesAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceManagement.Models
{
    public class BudgetDbo
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal AreaSize { get; set; }
        public EUnitOfMeasure UnitOfMeasure { get; set; }
        public int Confrontations { get; set; } = 0;
        public int IntentionServiceId { get; set; }
        public virtual IntentionServiceDbo IntentionService { get; set; }
        public int ServiceTypeId { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceTypeDbo ServiceType { get; set; }
        public virtual AddressDbo Address { get; set; }
    }
}
