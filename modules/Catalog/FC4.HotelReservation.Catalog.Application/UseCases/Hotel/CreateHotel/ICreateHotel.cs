using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Hotel.CreateHotel;

public interface ICreateHotel : IRequestHandler<CreateHotelInput, CreateHotelOutput>;