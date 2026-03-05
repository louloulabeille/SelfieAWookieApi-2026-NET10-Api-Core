using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Loggers
{
    public class SelfieLoggerProvider : ILoggerProvider
    {
        #region private Fields
        private ConcurrentDictionary<string, SelfieLogger> _loggerList = new();
        #endregion

        public ILogger CreateLogger(string categoryName)
        {
            this._loggerList.GetOrAdd(categoryName, key=> new SelfieLogger());
            return this._loggerList[categoryName];
        }

        public void Dispose()
        {
            this._loggerList.Clear();
            this.Dispose();
        }
    }
}
