using Bogus;
using FC4.HotelReservation.Catalog.Domain.ValueObjects;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class HotelBuilder
{
    private readonly Faker _faker = new();
    private Guid _id = Guid.NewGuid();
    private string _name;
    private Address _address;

    public HotelBuilder()
    {
        _name = _faker.Company.CompanyName();
        _address = new Address(
            _faker.Address.StreetAddress(),
            _faker.Address.City(),
            _faker.Address.State(),
            _faker.Address.Country(),
            _faker.Address.ZipCode()
        );
    }

    public static HotelBuilder AHotel() => new();

    public HotelBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public HotelBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public HotelBuilder WithAddress(Address address)
    {
        _address = address;
        return this;
    }

    public Catalog.Domain.Entities.Hotel Build()
        => new(_id, _name, _address);
}