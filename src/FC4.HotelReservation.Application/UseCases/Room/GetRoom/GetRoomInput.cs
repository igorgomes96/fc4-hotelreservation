using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Room.GetRoom;

public record GetRoomInput(Guid RoomId) : IRequest<GetRoomOutput>;