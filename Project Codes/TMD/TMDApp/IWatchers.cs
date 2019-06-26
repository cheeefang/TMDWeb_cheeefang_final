using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDApp
{
    public interface IWatchers
    {
        Watcher[] getCurrentWatchers();

        bool start();
        bool terminate();

        Exception lastError();
    }
}
