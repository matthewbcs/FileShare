using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Entities.Transport
{
    // TransportText
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.2.0")]
    public class TransportTextConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TransportText>
    {
        public TransportTextConfiguration()
            : this("dbo")
        {
        }

        public TransportTextConfiguration(string schema)
        {
            ToTable("TransportText", schema);
            HasKey(x => x.TransportTextId);

            Property(x => x.TransportTextId).HasColumnName(@"TransportTextId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.TransportTextData).HasColumnName(@"TransportTextData").HasColumnType("varchar(max)").IsOptional().IsUnicode(false);
            Property(x => x.TransportTextDataEncypted).HasColumnName(@"TransportTextDataEncypted").HasColumnType("varchar(max)").IsOptional().IsUnicode(false);
            Property(x => x.DateAddedOnUtc).HasColumnName(@"DateAddedOnUtc").HasColumnType("datetime2").IsRequired();
            Property(x => x.DateToDeleteByUtc).HasColumnName(@"DateToDeleteByUtc").HasColumnType("datetime2").IsRequired();
            Property(x => x.AccessCode).HasColumnName(@"AccessCode").HasColumnType("int").IsRequired();
        }
    }
}
