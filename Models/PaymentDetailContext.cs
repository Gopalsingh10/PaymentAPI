﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PaymentAPI.Models
{
    public class PaymentDetailContext : DbContext
    {
        public PaymentDetailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
