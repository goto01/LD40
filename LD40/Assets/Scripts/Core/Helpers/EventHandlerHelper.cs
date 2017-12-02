using System;

namespace Core.Helpers
{
    public static class EventHandlerHelper
    {
        public static void Raise(this EventHandler handler, object sender)
        {
            if (handler != null) handler(sender, EventArgs.Empty);
        }
    }
}
