namespace TheMatiaz0_MonobeamTheMercenary.Movement
{
    public interface IPositionEntity
    {
        CollisionTag CollisionTag { get; }

        Point Position { get; set; }
    }
}
