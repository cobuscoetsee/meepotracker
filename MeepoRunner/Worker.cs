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

namespace MeepoRunner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        Util.HOMEBASE PLAYSTYLE = Util.HOMEBASE.BOTTOM; // NEED TO SETUP BEFORE WE RUN THE APP;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Point position = Util.GetCursorPosition();

                string xx = "Worker running at: {time}. Position: {pos} Color: {color}";
                
                _logger.LogInformation(xx, DateTimeOffset.Now, position, Util.GetColorAt(position));

                Point p = new Point(position.X+50, position.Y+50);

                Util.SetCursorPos(p.X, p.Y);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
