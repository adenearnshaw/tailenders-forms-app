//MIT License

//Copyright(c) 2017 Robin-Manuel Thiel

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

using System;

namespace Tailenders.Controls.Swipeview
{
    public class SwipedEventArgs : EventArgs
    {
        public readonly object Item;
        public readonly SwipeDirection Direction;

        public SwipedEventArgs(object item, SwipeDirection direction)
        {
            this.Item = item;
            this.Direction = direction;
        }
    }

    public enum SwipeDirection
    {
        Left, Right
    }
}
