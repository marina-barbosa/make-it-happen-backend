using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace make_it_happen.Migrations
{
  /// <inheritdoc />
  public partial class SeedCategory : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Name" },
            values: new object[,]
            {
                    { "Arte e Cultura" },
                    { "Tecnologia e Inovação" },
                    { "Educação" },
                    { "Saúde e Bem-estar" },
                    { "Meio Ambiente" },
                    { "Empreendedorismo" },
                    { "Design e Moda" },
                    { "Música" },
                    { "Esportes" },
                    { "Gastronomia" }
            }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DELETE FROM Categories WHERE Name IN ('Arte e Cultura', 'Tecnologia e Inovação', 'Educação', 'Saúde e Bem-estar', 'Meio Ambiente', 'Empreendedorismo', 'Design e Moda', 'Música', 'Esportes', 'Gastronomia')");
    }
  }
}
