using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Hotel.CreateHotel;

public interface ICreateHotel : IRequestHandler<CreateHotelInput, CreateHotelOutput>;