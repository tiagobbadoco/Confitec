using Confitec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Infrastructure.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<Escolaridade>()
                .HasData(
                    new Escolaridade { Descricao = "Infantil", Id = 1 },
                    new Escolaridade { Descricao = "Fundamental", Id = 2},
                    new Escolaridade { Descricao = "Médio", Id = 3},
                    new Escolaridade { Descricao = "Superior", Id = 4}
                );


            return builder;
        }
    }
}
