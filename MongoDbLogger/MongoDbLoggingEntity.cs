using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbLogging {   
    internal record MongoDbLoggingEntity(string Environment, string ServiceName,string LogLevel, string CategoryName, string Message);
    internal record ServiceInfo(string Environment, string ServiceName);
}
