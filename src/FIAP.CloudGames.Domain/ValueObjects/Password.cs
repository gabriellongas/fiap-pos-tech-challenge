using System.Text.RegularExpressions;

namespace FIAP.CloudGames.Domain.ValueObjects;

public sealed class Password : IEquatable<Password>
{
    public string Hash { get; private set; }

    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password FromPlainText(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("A Senha não pode estar vazia.", nameof(password));

        if (password.Length < 8)
            throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.", nameof(password));

        if (!Regex.IsMatch(password, @"[a-z]"))
            throw new ArgumentException("A senha deve conter pelo menos uma letra minúscula.", nameof(password));

        if (!Regex.IsMatch(password, @"[A-Z]"))
            throw new ArgumentException("A senha deve conter pelo menos uma letra maiúscula.", nameof(password));

        if (!Regex.IsMatch(password, @"[0-9]"))
            throw new ArgumentException("A senha deve conter pelo menos um número.", nameof(password));

        if (!Regex.IsMatch(password, @"[\W_]"))
            throw new ArgumentException("A senha deve conter pelo menos um caractere especial.", nameof(password));

        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        return new Password(hash);
    }

    public static Password FromHashed(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash inválido.", nameof(hash));

        return new Password(hash);
    }

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


