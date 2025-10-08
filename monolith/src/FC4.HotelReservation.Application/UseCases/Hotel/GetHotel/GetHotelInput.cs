using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Hotel.GetHotel;

public record GetHotelInput(Guid HotelId) : IRequest<GetHotelOutput>;