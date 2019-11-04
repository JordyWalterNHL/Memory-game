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
    /// <summary>
    /// Card used to store all the important information
    /// </summary>
    [Serializable]
    public class MemoryCard
    {
        public string back;     ///String with the path to the backside image
        public string front;    ///String with the path to the frontside image

        public int id;          ///Number of the place in the GameCards list
        public int value;       ///Value of the card, used to compare if you have a pair

        public bool beenClicked;    ///Bool used to check if you've pressed it
        public bool beenUsed;       ///Bool to check if you've got a pair of the cards

        /// <summary>
        /// Sets the background of the image, exept when you've already got a pair of it, then it returns null
        /// </summary>
        public ImageSource GetBackSource()
        {
            if (beenUsed)
                return null;

            return new BitmapImage(new Uri(back, UriKind.Relative)); ;
        }

        /// <summary>
        /// Sets the front of the image
        /// </summary>
        public ImageSource GetFrontSource()
        {
            return new BitmapImage(new Uri(front, UriKind.Relative)); ;
        }
    }
}
