using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace make_it_happen.Migrations
{
  /// <inheritdoc />
  public partial class SeedUser : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
               table: "Users",
               columns: new[] { "Name", "Email", "Password", "AvatarUrl", "Bio", "Contact", "CreationDate", "Status", "EmailVerified" },
               values: new object[,]
               {
                    // Hortência Flores (psicóloga famosa e diretora do Asylum)
                    {
                        "Hortência Flores",
                        "hortencia.flores@asylum.com",
                        "hashed_password_1",
                        "https://example.com/avatars/hortencia.jpg",
                        "Psicóloga especializada em trauma, diretora do Asylum. Apaixonada por desbravar a mente humana e oferecer apoio para aqueles que enfrentam os maiores desafios. Estudo e experiência em ambientes de recuperação mental.",
                        "(11) 99876-5432",
                        DateTime.Now.AddYears(-15),
                        "Ativa",
                        true
                    },

                    // Isaac Bezerra (trabalhador comum do Velho Oeste, casado com Hortência)
                    {
                        "Isaac Bezerra",
                        "isaac.bezerra@velhoeste.com",
                        "hashed_password_2",
                        "https://example.com/avatars/isaac.jpg",
                        "Homem simples, originário do Velho Oeste. Trabalha como fazendeiro e mecânico. Casado com Hortência Flores. Apaixonado pela vida rural e por manter as tradições familiares vivas.",
                        "(12) 98765-4321",
                        DateTime.Now.AddYears(-10),
                        "Ativo",
                        true
                    },

                    // Edna Moda (designer de moda famosa)
                    {
                        "Edna Moda",
                        "edna.moda@designers.com",
                        "hashed_password_3",
                        "https://example.com/avatars/edna.jpg",
                        "Designer de moda famosa, criadora de trajes inovadores e referência em estilo e elegância. Uma personalidade excêntrica que se destaca por sua visão única da moda, além de ser uma defensora do empoderamento feminino.",
                        "(13) 97654-3210",
                        DateTime.Now.AddYears(-20),
                        "Ativa",
                        true
                    },

                    // Luís Oliveira (empreendedor no ramo de tecnologia)
                    {
                        "Luís Oliveira",
                        "luis.oliveira@techbiz.com",
                        "hashed_password_4",
                        "https://example.com/avatars/luis.jpg",
                        "Empreendedor no ramo de tecnologia, apaixonado por inovação e soluções digitais. Fundador de uma startup de IA que já está transformando diversos setores.",
                        "(21) 12345-6789",
                        DateTime.Now.AddYears(-5),
                        "Ativo",
                        true
                    },

                    // Luciana Ribeiro (médica e ativista ambiental)
                    {
                        "Luciana Ribeiro",
                        "luciana.ribeiro@saudeambiental.com",
                        "hashed_password_5",
                        "https://example.com/avatars/luciana.jpg",
                        "Médica com especialização em saúde pública e ativista ambiental. Trabalha incansavelmente para promover a saúde integral em comunidades carentes e para combater as mudanças climáticas.",
                        "(19) 34567-8901",
                        DateTime.Now.AddYears(-12),
                        "Ativa",
                        true
                    }
               }
           );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DELETE FROM Users WHERE Name IN ('Hortência Flores', 'Isaac Bezerra', 'Edna Moda', 'Luís Oliveira', 'Luciana Ribeiro')");
    }
  }
}
