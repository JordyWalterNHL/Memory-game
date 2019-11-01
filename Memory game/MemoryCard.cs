using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Memory_game
{
    [Serializable]
    public class MemoryCard
    {
        public string back;
        public string front;

        public int id;
        public int value;

        public bool beenClicked;
        public bool beenUsed;

        public ImageSource GetBackSource()
        {
            if (beenUsed)
                return null;

            return new BitmapImage(new Uri(back, UriKind.Relative)); ;
        }

        public ImageSource GetFrontSource()
        {
            return new BitmapImage(new Uri(front, UriKind.Relative)); ;
        }
    }
}
