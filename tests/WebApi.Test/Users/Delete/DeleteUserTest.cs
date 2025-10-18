using FluentAssertions;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest : CashFlowClassFixture
{
    private const string METHOD = "api/User";
    private readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }
}