﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory_game
{
    class MemoryCard
    {
        private ImageSource backSource;
        private ImageSource frontSource;

        public int id;
        public int value;

        public bool beenClicked;

        public MemoryCard(ImageSource frontSource, int id)
        {
            this.backSource = new BitmapImage(new Uri("Images/turkey.png", UriKind.Relative));
            this.frontSource = frontSource;
            this.id = id;
        }

        public ImageSource GetBackSource()
        {
            return backSource;
        }

        public ImageSource GetFrontSource()
        {
            return frontSource;
        }
    }
}