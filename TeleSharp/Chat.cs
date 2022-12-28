using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TeleSharp
{
    public enum Status { Offline,Away,Online }
    public class ChatRepository
    {
        BindingList<Chat> chats = new BindingList<Chat>();
        public BindingList<Chat> GetChats() => chats;
    }
    public class Chat
    {
        public ImageSource Source { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public TimeOnly timeLast { get; set; }
        public string Context { get; set; }
        public string Info { get; set; }
        public string Username { get; set; }
    }
}
