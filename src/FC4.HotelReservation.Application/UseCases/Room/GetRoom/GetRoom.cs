using FC4.HotelReservation.Application.Exceptions;
using FC4.HotelReservation.Catalog.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Room.GetRoom;

public class GetRoom(IRoomRepository roomRepository) : IGetRoom
{
    public async Task<GetRoomOutput> Handle(GetRoomInput request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(request.RoomId, cancellationToken)
                   ?? throw new NotFoundException("Room not found");

        return GetRoomOutput.FromRoom(room);
    }
}