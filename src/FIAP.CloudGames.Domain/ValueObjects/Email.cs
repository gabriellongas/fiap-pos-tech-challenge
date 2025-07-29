using System.Text.RegularExpressions;

namespace FIAP.CloudGames.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O Email não pode estar vazio.", nameof(email));

        if (!EmailRegex.IsMatch(email))
            throw new ArgumentException("Formato de Email inválido.", nameof(email));

        return new Email(email.Trim());
    }

    public override bool Equals(object? obj)
       => obj is Email other && Address == other.Address;

    public bool Equals(Email? other)
        => other is not null && Address == other.Address;

    public override int GetHashCode() => Address.ToLowerInvariant().GetHashCode();

    public override string ToString() => Address;

    // Implicit conversion
    public static implicit operator string(Email email) => email.Address;
    public static explicit operator Email(string email) => Create(email);
}

