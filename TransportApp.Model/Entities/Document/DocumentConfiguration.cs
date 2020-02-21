using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Entities.Document
{
    // Document
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.2.0")]
    public class DocumentConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
            : this("dbo")
        {
        }

        public DocumentConfiguration(string schema)
        {
            ToTable("Document", schema);
            HasKey(x => x.DocumentId);

            Property(x => x.DocumentId).HasColumnName(@"DocumentId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.DocumentName).HasColumnName(@"DocumentName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(200);
            Property(x => x.DocumentData).HasColumnName(@"DocumentData").HasColumnType("varbinary(max)").IsOptional();
            Property(x => x.DocumentType).HasColumnName(@"DocumentType").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.DateAddedOnUtc).HasColumnName(@"DateAddedOnUtc").HasColumnType("datetime2").IsRequired();
            Property(x => x.DateToDeleteByUtc).HasColumnName(@"DateToDeleteByUtc").HasColumnType("datetime2").IsRequired();
            Property(x => x.AccessCode).HasColumnName(@"AccessCode").HasColumnType("int").IsRequired();
        }
    }
}
