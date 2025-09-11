using Bogus;
using FC4.HotelReservation.Guests.Domain.ValueObjects;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class GuestBuilder
{
    private readonly Faker _faker = new();
    private Guid _id = Guid.NewGuid();
    private string _firstName;
    private string _lastName;
    private string? _email;
    
    public GuestBuilder()
    {
        _firstName = _faker.Name.FirstName();
        _lastName = _faker.Name.LastName();
        _email = _faker.Internet.Email(_firstName, _lastName);
    }
    
    public static GuestBuilder AGuest() => new();
    
    public GuestBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public GuestBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }
    
    public GuestBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }
    
    public GuestBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }
    
    public Guests.Domain.Entities.Guest Build() 
        => new(_id, _firstName, _lastName, new Email(_email!));
}