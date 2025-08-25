using MediatR;

namespace FC4.HotelReservation.Guests.Application.UseCases.Guest.CreateGuest;

public interface ICreateGuest : IRequestHandler<CreateGuestInput, CreateGuestOutput>;