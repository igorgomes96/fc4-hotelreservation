using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;

public interface ICreateRoom: IRequestHandler<CreateRoomInput, CreateRoomOutput>;