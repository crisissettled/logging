using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbLogging {   
    internal record MongoDbLoggingEntity(string Environment, string ServiceName,string LogLevel, string CategoryName, string Message, DateTime LogDate);
    internal record ServiceInfo(string Environment, string ServiceName);
}
