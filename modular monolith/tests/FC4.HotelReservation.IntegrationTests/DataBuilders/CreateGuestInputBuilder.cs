using Bogus;
using FC4.HotelReservation.Guests.Application.UseCases.Guest.CreateGuest;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class CreateGuestInputBuilder
{
    private readonly Faker _faker = new();
    private string _firstName;
    private string _lastName;
    private string _email;

    public CreateGuestInputBuilder()
    {
        _firstName = _faker.Name.FirstName();
        _lastName = _faker.Name.LastName();
        _email = _faker.Internet.Email();
    }
    
    public static CreateGuestInput ACreateGuestInput() => new CreateGuestInputBuilder().Build();

    public CreateGuestInputBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CreateGuestInputBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CreateGuestInputBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public CreateGuestInput Build() => new(
        FirstName: _firstName,
        LastName: _lastName,
        Email: _email
    );
}