namespace FindMyPG.Models
{
    public class PGRoomModel
    {
        public int RoomType { get; set; } // AC/NONAC
        public int Capacity { get; set; }
        public int Occupied { get; set; }
        public int Price { get; set; }
        public int Floor { get; set; }
    }
}
