using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Parse;

namespace QuotesApp
{
    public class RoomListViewBinding
    {
        public string RoomName { get; set; }
        public SolidColorBrush RoomColor { get; set; }
        public ParseObject parseObject;

        public RoomListViewBinding(string roomName, SolidColorBrush roomColor, ParseObject parseObject)
        {
            this.RoomName = roomName;
            this.RoomColor = roomColor;
            this.parseObject = parseObject;
        }
    }
}
