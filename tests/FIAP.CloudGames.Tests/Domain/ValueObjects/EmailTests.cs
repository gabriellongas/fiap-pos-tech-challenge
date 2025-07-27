using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("usuario@exemplo.com")]
        [InlineData("teste123@email.net")]
        [InlineData("nome.sobrenome@dominio.org")]
        public void Create_ValidEmail_ShouldCreateSuccessfully(string validEmail)
        {
            var email = Email.Create(validEmail);

            Assert.NotNull(email);
            Assert.Equal(validEmail, email.Address);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_EmptyOrNullEmail_ShouldThrowArgumentException(string? invalidEmail)
        {
            var ex = Assert.Throws<ArgumentException>(() => Email.Create(invalidEmail!));
            Assert.Equal("O Email não pode estar vazio.", ex.Message);
        }

        [Theory]
        [InlineData("sem-arroba.com")]
        [InlineData("email@")]
        [InlineData("@dominio.com")]
        [InlineData("email@dominio")]
        [InlineData("email@.com")]
        public void Create_InvalidFormatEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            var ex = Assert.Throws<ArgumentException>(() => Email.Create(invalidEmail));
            Assert.Equal("Formato de Email inválido.", ex.Message);
        }

        [Fact]
        public void Equals_SameEmail_ShouldBeEqual()
        {
            var email1 = Email.Create("teste@exemplo.com");
            var email2 = Email.Create("teste@exemplo.com");

            Assert.True(email1.Equals(email2));
            Assert.Equal(email1, email2);
        }

        [Fact]
        public void Equals_DifferentEmails_ShouldNotBeEqual()
        {
            var email1 = Email.Create("teste1@exemplo.com");
            var email2 = Email.Create("teste2@exemplo.com");

            Assert.False(email1.Equals(email2));
        }
    }
}
