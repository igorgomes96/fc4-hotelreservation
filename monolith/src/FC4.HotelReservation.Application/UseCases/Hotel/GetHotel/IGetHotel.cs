using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Hotel.GetHotel;

public interface IGetHotel : IRequestHandler<GetHotelInput, GetHotelOutput>;