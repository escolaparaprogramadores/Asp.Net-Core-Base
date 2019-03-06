
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;

namespace Infra.EntityConfiguration.Mapping
{
    public class UsuarioTelefoneMap : IEntityTypeConfiguration<UsuarioTelefone>
     {
        public void Configure(EntityTypeBuilder<UsuarioTelefone> builder)
        {         
                 {
                    builder.ToTable("UsuarioTelefones"); 
                    builder.HasKey(ut => new { ut.UsuarioId, ut.TelefoneId } );
                 }
    
        }
        
    }
}
