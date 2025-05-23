﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace курсачь_Олег_важно.ViewModel
{
    public class BaseVM
    {
        protected void Signal([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
