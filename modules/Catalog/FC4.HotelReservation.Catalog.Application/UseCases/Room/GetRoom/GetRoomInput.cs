using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.GetRoom;

public record GetRoomInput(Guid RoomId) : IRequest<GetRoomOutput>;