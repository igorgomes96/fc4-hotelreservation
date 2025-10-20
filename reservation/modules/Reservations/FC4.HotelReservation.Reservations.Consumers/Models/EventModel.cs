namespace FC4.HotelReservation.Reservations.Consumers.Models;

public class EventModel<T> where T : class
{
    public T Payload { get; set; }
}