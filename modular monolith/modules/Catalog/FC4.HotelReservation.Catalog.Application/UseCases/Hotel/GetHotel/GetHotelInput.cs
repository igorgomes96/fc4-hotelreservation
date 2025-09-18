using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Hotel.GetHotel;

public record GetHotelInput(Guid HotelId) : IRequest<GetHotelOutput>;