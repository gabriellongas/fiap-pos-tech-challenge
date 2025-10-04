using FIAP.CloudGames.Domain.ValueObjects;
using Xunit;

namespace FIAP.CloudGames.Tests.Domain.ValueObjects;
public class PasswordTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void FromPlainText_ShouldThrowException_WhenPasswordIsEmpty(string senhaInvalida)
    {
        var ex = Assert.Throws<ArgumentException>(() => Password.FromPlainText(senhaInvalida));
        Assert.Equal("A Senha não pode estar vazia. (Parameter 'password')", ex.Message);
    }

    [Fact]
    public void FromPlainText_ShouldThrowException_WhenPasswordIsTooShort()
    {
        var ex = Assert.Throws<ArgumentException>(() => Password.FromPlainText("Ab1$e"));
        Assert.Equal("A senha deve ter pelo menos 8 caracteres. (Parameter 'password')", ex.Message);
    }

    [Fact]
    public void FromPlainText_ShouldThrowException_WhenMissingLowercaseLetter()
    {
        var ex = Assert.Throws<ArgumentException>(() => Password.FromPlainText("ABC123$#"));
        Assert.Equal("A senha deve conter pelo menos uma letra minúscula. (Parameter 'password')", ex.Message);
    }

    [Fact]
    public void FromPlainText_ShouldThrowException_WhenMissingUppercaseLetter()
    {
        var ex = Assert.Throws<ArgumentException>(() => Password.FromPlainText("abc123$#"));
        Assert.Equal("A senha deve conter pelo menos uma letra maiúscula. (Parameter 'password')", ex.Message);
    }

    [Fact]
    public void FromPlainText_ShouldThrowException_WhenMissingNumber()
    {
        var ex = Assert.Throws<ArgumentException>(() => Password.FromPlainText("Abcdef$#"));
        Assert.Equal("A senha deve conter pelo menos um número. (Parameter 'password')", ex.Message);
    }

    [Fact]
    public void FromPlainText_ShouldThrowException_WhenMissingSpecialCharacter()
    {
        var ex = Assert.Throws<ArgumentException>(() => Password.FromPlainText("Abcdef12"));
        Assert.Equal("A senha deve conter pelo menos um caractere especial. (Parameter 'password')", ex.Message);
    }

    [Fact]
    public void FromPlainText_ShouldCreateValidPassword_WhenCorrectPasswordProvided()
    {
        var senha = "Abcdef1!";
        var password = Password.FromPlainText(senha);

        Assert.NotNull(password);
        Assert.False(string.IsNullOrWhiteSpace(password.Hash));
        Assert.True(password.Verify(senha));
    }

    [Fact]
    public void Verify_ShouldReturnFalse_WhenPasswordDoesNotMatch()
    {
        var password = Password.FromPlainText("SenhaForte123!");

        var resultado = password.Verify("SenhaErrada456@");

        Assert.False(resultado);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForMatchingHashes()
    {
        var senha = "SenhaForte123!";
        var original = Password.FromPlainText(senha);
        var copia = Password.FromHashed(original.Hash);

        Assert.True(original.Equals(copia));
        Assert.True(original.Equals((object)copia));
        Assert.Equal(original.GetHashCode(), copia.GetHashCode());
    }

    [Fact]
    public void ToString_ShouldReturnProtectedText()
    {
        var senha = Password.FromPlainText("SenhaForte123!");
        Assert.Equal("[PROTECTED]", senha.ToString());
    }
}
