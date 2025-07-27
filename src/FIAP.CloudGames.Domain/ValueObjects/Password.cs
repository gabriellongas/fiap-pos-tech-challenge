using System.Text.RegularExpressions;

namespace FIAP.CloudGames.Domain.ValueObjects;

public sealed class Password : IEquatable<Password>
{
    public string Hash { get; private set; }

    private Password(string hash)
    {
        Hash = hash;
    }

    //Creates a new Password
    public static Password FromPlainText(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("A Senha não pode estar vazia.");

        if (password.Length < 8)
            throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.");

        if (!Regex.IsMatch(password, @"[a-z]"))
            throw new ArgumentException("A senha deve conter pelo menos uma letra minúscula.");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            throw new ArgumentException("A senha deve conter pelo menos uma letra maiúscula.");

        if (!Regex.IsMatch(password, @"[0-9]"))
            throw new ArgumentException("A senha deve conter pelo menos um número.");

        if (!Regex.IsMatch(password, @"[\W_]"))
            throw new ArgumentException("A senha deve conter pelo menos um caractere especial.");

        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        return new Password(hash);
    }

    //Verifies if the provided raw password matches the hashed password
    public bool Verify(string rawPassword)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword, Hash);
    }

    public bool Equals(Password? other)
    {
        if (other is null) return false;
        return Hash == other.Hash;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Password);

    public override int GetHashCode() => Hash.GetHashCode();

    public override string ToString() => "[PROTECTED]";
}


