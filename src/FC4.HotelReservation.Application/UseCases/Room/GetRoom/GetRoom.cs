using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Room.GetRoom;

public class GetRoom(IRoomRepository roomRepository)
{
    public async Task<GetRoomOutput> Handle(GetRoomInput request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(request.RoomId, cancellationToken)
                   ?? throw new InvalidOperationException("Room not found");

        return GetRoomOutput.FromRoom(room);
    }
}