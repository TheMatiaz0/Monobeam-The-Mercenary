using System;

namespace TheMatiaz0_MonobeamTheMercenary.Input
{
    public struct Option
    {
        public string Text { get; }
        public ConsoleColor Color { get; }
        public Action EventToLoad { get; }

        public Option(string text, Action eventToLoad) : this(text, ConsoleColor.White, eventToLoad) { }

        public Option(string text, ConsoleColor color, Action eventToLoad)
        {
            Text = text;
            Color = color;
            EventToLoad = eventToLoad;
        }
    }
}