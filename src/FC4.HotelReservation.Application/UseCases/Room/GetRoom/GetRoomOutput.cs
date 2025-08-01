namespace FC4.HotelReservation.Application.UseCases.Room.GetRoom;

public record GetRoomOutput(
    Guid Id,
    Guid RoomTypeId,
    int Floor,
    string Number,
    Guid HotelId,
    bool IsAvailable)
{
    public static GetRoomOutput FromRoom(Domain.Entities.Room room)
    {
        return new GetRoomOutput(
            room.Id,
            room.RoomTypeId,
            room.Floor,
            room.Number,
            room.HotelId,
            room.IsAvailable
        );
    }
}