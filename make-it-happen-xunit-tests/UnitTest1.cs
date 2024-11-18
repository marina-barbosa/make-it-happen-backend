using FluentAssertions;

namespace make_it_happen_xunit_tests;

public class UnitTest1
{
  [Fact]
  public void Test1()
  {

  }
  [Fact]
  public void TesteDevePassar()
  {
    // Arrange
    int resultado = 2 + 2;

    // Act & Assert
    resultado.Should().Be(4); // Usando FluentAssertions
  }

  [Fact]
  public void TesteDeveFalhar()
  {
    // Arrange
    string nome = "João";

    // Act & Assert
    nome.Should().StartWith("J").And.EndWith("o"); // Verificando que começa com 'J' e termina com 'o'
  }
}