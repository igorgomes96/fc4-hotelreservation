using Bogus;
using FC4.HotelReservation.Catalog.Domain.Entities;

namespace FC4.HotelReservation.IntegrationTests.DataBuilders;

public class RoomTypeBuilder
{
    private readonly Faker _faker = new();
    private Guid _id = Guid.NewGuid();
    private string _description;
    
    public RoomTypeBuilder()
    {
        _description = _faker.Commerce.ProductDescription();
    }
    
    public static RoomTypeBuilder ARoomType() => new();
    
    public RoomTypeBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public RoomTypeBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }
    
    public RoomType Build()
    {
        return new RoomType(_id, _description);
    }
}