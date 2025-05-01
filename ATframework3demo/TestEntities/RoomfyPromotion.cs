using System.Xml.Linq;

namespace ATframework3demo.TestEntities
{
    public class RoomfyPromotion
    {

        public RoomfyPromotion() { }
        public RoomfyPromotion(string link, string date, string path)
        {
            Link = link;
            Date = date;
            Path = path;

        }

        public string Link { get; set; }
        public string Date { get; set; }
        public string Path { get; set; }

    }
}
