﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfGasStations;

namespace Cosinka.Viewmodel
{
    public interface IGetImageOfCard
    {
        public Image GetImage(Card card);
    }
}
