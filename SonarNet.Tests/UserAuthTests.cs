using SonarNet;

namespace SonarNet.Tests;

public class UserAuthTests
{
    [Fact]
    public void Register_NewUser_ReturnsTrue()
    {
        var auth = new UserAuth();
        Assert.True(auth.Register("nikita", "secret123"));
    }

    [Fact]
    public void Register_DuplicateUser_ReturnsFalse()
    {
        var auth = new UserAuth();
        auth.Register("nikita", "secret123");
        Assert.False(auth.Register("NIKITA", "other"));
    }

    [Fact]
    public void Login_CorrectPassword_ReturnsTrue()
    {
        var auth = new UserAuth();
        auth.Register("nikita", "secret123");
        Assert.True(auth.Login("nikita", "secret123"));
    }

    [Theory]
    [InlineData("nikita", "wrong")]
    [InlineData("unknown", "secret123")]
    [InlineData("", "secret123")]
    public void Login_InvalidCredentials_ReturnsFalse(string user, string password)
    {
        var auth = new UserAuth();
        auth.Register("nikita", "secret123");
        Assert.False(auth.Login(user, password));
    }
}
