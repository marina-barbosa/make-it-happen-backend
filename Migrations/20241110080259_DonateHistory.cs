using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace make_it_happen.Migrations
{
  /// <inheritdoc />
  public partial class DonateHistory : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
                table: "DonateHistories",
                columns: new[] { "Amount", "CampaignId", "DonationDate", "PaymentMethod", "ReceiptSent", "Status", "UserId" },
                values: new object[,]
                {
                    { 150m, 1, DateTime.Now.AddDays(-20), "Cartão de Crédito", true, "Concluído", 1 },
                    { 200m, 2, DateTime.Now.AddDays(-15), "Transferência Bancária", false, "Pendente", 2 },
                    { 75m, 3, DateTime.Now.AddDays(-10), "Pix", true, "Concluído", 3 },
                    { 50m, 1, DateTime.Now.AddDays(-5), "Boleto", true, "Concluído", 2 },
                    { 300m, 2, DateTime.Now.AddDays(-3), "Cartão de Crédito", false, "Pendente", 4 },
                });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DELETE FROM DonateHistories WHERE UserId IN (1, 2, 3, 4)");
    }
  }
}
