
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;

namespace Infra.EntityConfiguration.Mapping
{
    public class TelefoneMap : IEntityTypeConfiguration<Telefone>
     {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {         
                 {
                    builder.ToTable("Telefone");   
                        
                 }
    
        }
        
    }
}
