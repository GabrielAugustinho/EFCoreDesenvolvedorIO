using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Data.Configurations
{
    class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(80);
            builder.Property(p => p.Telefone).HasMaxLength(11);
            builder.Property(p => p.CEP).HasMaxLength(8);
            builder.Property(p => p.Estado).HasMaxLength(2);
            builder.Property(p => p.Cidade).HasMaxLength(60);

            builder.HasIndex(i => i.Telefone);
        }
    }
}
