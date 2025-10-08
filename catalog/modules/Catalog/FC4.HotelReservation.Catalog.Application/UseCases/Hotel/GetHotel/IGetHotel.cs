using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Hotel.GetHotel;

public interface IGetHotel : IRequestHandler<GetHotelInput, GetHotelOutput>;