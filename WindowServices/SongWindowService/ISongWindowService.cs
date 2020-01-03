using AudioPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.WindowServices
{
    interface ISongWindowService : IWindowService
    {
        Song Song { get; set; }
    }
}
