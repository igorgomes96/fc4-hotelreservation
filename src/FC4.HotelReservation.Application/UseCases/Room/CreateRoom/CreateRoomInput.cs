using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Room.CreateRoom;

public record CreateRoomInput(
    Guid RoomTypeId,
    int Floor,
    string Number,
    Guid HotelId,
    string Name,
    bool IsAvailable
) : IRequest<CreateRoomOutput>
{
    public Domain.Entities.Room ToRoom()
    {
        return new Domain.Entities.Room(RoomTypeId, Floor, Number, HotelId, Name, IsAvailable);
    }
}