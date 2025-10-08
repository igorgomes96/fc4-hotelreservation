using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Room.GetRoom;

public interface IGetRoom: IRequestHandler<GetRoomInput, GetRoomOutput>;