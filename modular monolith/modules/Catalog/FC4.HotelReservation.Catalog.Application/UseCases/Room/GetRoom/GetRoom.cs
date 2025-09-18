using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Shared.Application.Exceptions;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.GetRoom;

public class GetRoom(IRoomRepository roomRepository) : IGetRoom
{
    public async Task<GetRoomOutput> Handle(GetRoomInput request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(request.RoomId, cancellationToken)
                   ?? throw new NotFoundException("Room not found");

        return GetRoomOutput.FromRoom(room);
    }
}