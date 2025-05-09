using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotenv.net;

namespace CANTIDADES.Commands
{
   public static class DotEnvLoader
    {
        public static void Load()
        {
            DotEnv.Load();
        }
    }
}
