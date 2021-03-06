﻿using osu.Framework;
using osu.Framework.Platform;

namespace kyoseki.UI.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost("visual-tests"))
            using (var game = new KyosekiUITestBrowser())
                host.Run(game);
        }
    }
}
