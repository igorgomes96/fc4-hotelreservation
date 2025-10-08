using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Application.Exceptions;
using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Room.CreateRoom;

public class CreateRoom(
    IRoomRepository roomRepository,
    IRoomTypeRepository roomTypeRepository,
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : ICreateRoom
{
    public async Task<CreateRoomOutput> Handle(CreateRoomInput request, CancellationToken cancellationToken)
    {
        _ = await roomTypeRepository.GetByIdAsync(request.RoomTypeId, cancellationToken)
            ?? throw new NotFoundException("Room type not found");
        
        _ = await hotelRepository.GetByIdAsync(request.HotelId, cancellationToken)
            ?? throw new NotFoundException("Hotel not found");
        
        var room = request.ToRoom();
        await roomRepository.CreateAsync(room, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateRoomOutput(room.Id);
    }
}