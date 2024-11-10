using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace make_it_happen.Migrations
{
  /// <inheritdoc />
  public partial class SeedCampaign : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
        table: "Campaigns",
        columns: new[] { "Name", "Description", "Goal", "AmountRaised", "CreationDate", "Deadline", "ImageUrl", "CategoryId", "UserId", "Status", "TermsConditions", "VideoUrl" },
        values: new object[,]
        {
          // Campanha do Enigma do Medo (RPG)
          {
            "Enigma do Medo: RPG Interativo",
            "Ajude a criar o jogo de RPG interativo 'Enigma do Medo', uma experiência imersiva desenvolvida por Cellbit. O jogo promete surpreender com histórias arrepiantes e decisões cruciais para os jogadores.",
            150000m, 
            0m, 
            DateTime.Now, DateTime.Now.AddMonths(2), 
            "https://example.com/enigma-do-medo.jpg", 
            2, 
            4, 
            "Ativo",
            "Termos e condições de participação... (detalhamento sobre como funciona a campanha).",
            "https://example.com/enigma-do-medo-video"
          },
          // Campanha de transporte para campeonato de jiu-jitsu
          {
            "Transporte para Campeonato de Jiu-Jitsu",
            "Contribua para o transporte de atletas de jiu-jitsu para o campeonato nacional. Vamos garantir que todos possam competir sem preocupações com logística!",
            50000m, 
            0m, 
            DateTime.Now, DateTime.Now.AddMonths(1), 
            "https://example.com/jiu-jitsu.jpg", 
            9, 
            2, 
            "Ativo",
            "Termos e condições de participação... (detalhamento sobre como funciona a campanha).",
            "https://example.com/jiu-jitsu-video"
          },
          // Campanha de criação das roupas dos Incríveis
          {
            "Desenvolvimento de Uniformes de Elite para Forças Especiais",
            "Ajude a financiar a criação de uniformes de alta performance para nossos soldados de elite. Cada doação contribui para a melhoria de equipamentos de última geração, que garantirão a proteção e eficiência de nossos soldados em missões de alto risco.",
            200000m, 0m, DateTime.Now, DateTime.Now.AddMonths(4), "https://example.com/uniformes-elite.jpg", 7, 3, "Ativo",
            "Os fundos arrecadados serão usados para o desenvolvimento de uniformes militares inovadores, incluindo materiais de alta resistência, tecnologias de proteção e sistemas de comunicação avançados. A campanha está aberta a contribuições de qualquer valor, com recompensas baseadas no montante doado.",
            "https://example.com/uniformes-elite-video"
          },
          // Campanha para a criação de um centro de reabilitação para animais abandonados
          {
            "Centro de Reabilitação para Animais Abandonados",
            "Ajude a criar um centro especializado para reabilitação de animais abandonados, oferecendo tratamento médico e um novo lar para cães e gatos.",
            80000m, 0m, DateTime.Now, DateTime.Now.AddMonths(4), "https://example.com/rehab-animals.jpg", 5, 5, "Ativo",
            "Termos e condições de participação... (detalhamento sobre como funciona a campanha).",
            "https://example.com/rehab-animals-video"
          },
          // Campanha de apoio à educação de crianças carentes
          {
            "Apoio à Educação de Crianças Carentes",
            "Sua doação irá garantir que crianças de comunidades carentes recebam materiais escolares, uniformes e acesso a atividades extracurriculares.",
            20000m, 0m, DateTime.Now, DateTime.Now.AddMonths(6), "https://example.com/educacao-criancas.jpg", 3, 1, "Ativo",
            "Termos e condições de participação... (detalhamento sobre como funciona a campanha).",
            "https://example.com/educacao-criancas-video"
          }
        });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DELETE FROM Campaigns WHERE Name IN ('Enigma do Medo: RPG Interativo', 'Transporte para Campeonato de Jiu-Jitsu', 'Desenvolvimento de Uniformes de Elite para Forças Especiais', 'Centro de Reabilitação para Animais Abandonados', 'Apoio à Educação de Crianças Carentes')");
    }
  }
}
