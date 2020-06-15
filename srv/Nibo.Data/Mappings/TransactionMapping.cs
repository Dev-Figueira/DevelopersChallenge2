using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nibo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Business.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(cr => cr.Id)
                .HasName("ACCTID");

            builder.Property(x => x.DTPOSTED)
                .HasColumnName("DatePosted")
                .IsRequired();

            builder.Property(x => x.TRNAMT)
                .HasColumnName("Amount")
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(x => x.MEMO)
                .HasColumnName("Memo")
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(x => x.TRNTYPE)
                .HasColumnName("Type")
                .IsRequired();
        }
    }
}
