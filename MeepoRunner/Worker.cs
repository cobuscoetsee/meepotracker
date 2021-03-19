using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeepoRunner.Core;
using static MeepoRunner.Core.Util;
using MeepoRunner.GamePlay;

namespace MeepoRunner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        Util.HOMEBASE PLAYSTYLE = Util.HOMEBASE.BOTTOM; // NEED TO SETUP BEFORE WE RUN THE APP;

        public Worker(ILogger<Worker> logger)
        {
            Setup.Start(Util.HOMEBASE.BOTTOM);
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Board.Run();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
