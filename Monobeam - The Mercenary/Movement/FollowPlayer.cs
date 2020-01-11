namespace TheMatiaz0_MonobeamTheMercenary.Movement
{
    public interface IFollowPlayer
    {
        int DiffX { set; get; }
        int DiffY { set; get; }

        int SightFar { get; }

        int MovementSpeed { get; }

        void Calculate();

        void Move();
    }
}
