using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    interface ILoader
    {
        bool loading { get; }

        string loaderText { get; }

        void startLoading();
        void updateText(string text);
        void stopLoading();
    }
}
