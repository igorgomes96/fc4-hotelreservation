namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.GetRoom;

public record GetRoomOutput(
    Guid Id,
    Guid RoomTypeId,
    int Floor,
    string Number,
    Guid HotelId,
    bool IsAvailable)
{
    public static GetRoomOutput FromRoom(Catalog.Domain.Entities.Room room)
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