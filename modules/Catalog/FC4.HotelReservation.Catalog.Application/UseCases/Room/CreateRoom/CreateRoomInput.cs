using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;

public record CreateRoomInput(
    Guid RoomTypeId,
    int Floor,
    string Number,
    Guid HotelId,
    bool IsAvailable
) : IRequest<CreateRoomOutput>
{
    public Catalog.Domain.Entities.Room ToRoom()
    {
        return Catalog.Domain.Entities.Room.Create(RoomTypeId, Floor, Number, HotelId, IsAvailable);
    }
}