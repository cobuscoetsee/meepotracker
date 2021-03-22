using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MeepoRunner.Core;
using static MeepoRunner.Core.Util;
using MeepoRunner.GamePlay;
using Serilog;

namespace MeepoRunner
{
    public class Worker : BackgroundService
    {
        public readonly ILogger<Worker> _logger;
   

        public Worker(ILogger<Worker> logger)
        {
            Setup.Start(Util.HOMEBASE.TOP);
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Setup.SearchMeposPositions(_logger);
                await Board.Run(_logger);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
