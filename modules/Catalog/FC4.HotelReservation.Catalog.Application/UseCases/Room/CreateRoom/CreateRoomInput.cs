using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;

public record CreateRoomInput(
    Guid RoomTypeId,
    int Floor,
    string Number,
    Guid HotelId,
    string Name,
    bool IsAvailable
) : IRequest<CreateRoomOutput>
{
    public Catalog.Domain.Entities.Room ToRoom()
    {
        return new Catalog.Domain.Entities.Room(RoomTypeId, Floor, Number, HotelId, Name, IsAvailable);
    }
}