using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.GetRoom;

public interface IGetRoom: IRequestHandler<GetRoomInput, GetRoomOutput>;