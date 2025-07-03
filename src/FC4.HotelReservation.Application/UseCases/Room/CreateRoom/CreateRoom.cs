using Ardalis.GuardClauses;
using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Room.CreateRoom;

public class CreateRoom(IRoomRepository roomRepository)
{
    private readonly IRoomRepository _roomRepository = Guard.Against.Null(roomRepository, nameof(roomRepository));

    public async Task<CreateRoomOutput> Handle(CreateRoomInput request, CancellationToken cancellationToken)
    {
        var room = request.ToRoom();
        await _roomRepository.CreateAsync(room, cancellationToken);
        return new CreateRoomOutput(room.Id);
    }
}