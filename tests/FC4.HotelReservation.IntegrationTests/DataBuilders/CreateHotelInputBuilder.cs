using Bogus;
using FC4.HotelReservation.Catalog.Application.UseCases.Hotel.CreateHotel;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class CreateHotelInputBuilder
{
    private readonly Faker _faker = new();
    private string _name;
    private string _street;
    private string _city;
    private string _state;
    private string _country;
    private string _zipCode;

    public CreateHotelInputBuilder()
    {
        _name = _faker.Company.CompanyName();
        _street = _faker.Address.StreetAddress();
        _city = _faker.Address.City();
        _state = _faker.Address.State();
        _country = _faker.Address.Country();
        _zipCode = _faker.Address.ZipCode();
    }
    
    public static CreateHotelInput ACreateHotelInput() => new CreateHotelInputBuilder().Build();

    public CreateHotelInputBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CreateHotelInputBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }

    public CreateHotelInputBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public CreateHotelInputBuilder WithState(string state)
    {
        _state = state;
        return this;
    }

    public CreateHotelInputBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }

    public CreateHotelInputBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public CreateHotelInput Build() => new(
        Name: _name,
        Street: _street,
        City: _city,
        State: _state,
        Country: _country,
        ZipCode: _zipCode
    );
}