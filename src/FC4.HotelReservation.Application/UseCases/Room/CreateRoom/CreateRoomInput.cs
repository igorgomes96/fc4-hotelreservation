using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Room.CreateRoom;

public record CreateRoomInput(
    Guid RoomTypeId,
    int Floor,
    string Number,
    Guid HotelId,
    bool IsAvailable
) : IRequest<CreateRoomOutput>
{
    public Domain.Entities.Room ToRoom()
    {
        return Domain.Entities.Room.Create(RoomTypeId, Floor, Number, HotelId, IsAvailable);
    }
}