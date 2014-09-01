using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuotesApp
{
    public class RoomListViewBinding
    {
        public string RoomName { get; set; }
        public SolidColorBrush RoomColor { get; set; }

        public RoomListViewBinding(string roomName, SolidColorBrush roomColor)
        {
            this.RoomName = roomName;
            this.RoomColor = roomColor;
        }
    }
}
