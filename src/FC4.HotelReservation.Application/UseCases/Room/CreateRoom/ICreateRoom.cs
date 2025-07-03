using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Room.CreateRoom;

public interface ICreateRoom: IRequestHandler<CreateRoomInput, CreateRoomOutput>;