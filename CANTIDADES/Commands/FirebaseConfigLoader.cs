//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FireSharp.Config;
//using FireSharp.Interfaces;

//update


//namespace CANTIDADES.Commands
//{
//    public class FirebaseConfigLoader
//    {
//        public static IFirebaseConfig GetConfig()
//        {
//            string authSecret = Environment.GetEnvironmentVariable("FIREBASE_AUTH_SECRET");
//            string basePath = Environment.GetEnvironmentVariable("FIREBASE_URL");
            
//            if (string.IsNullOrEmpty(authSecret) || string.IsNullOrEmpty(basePath))
//            {
//                throw new Exception("Firebase configuration is not set in environment variables.");
//            }
//            return new FirebaseConfig
//            {
//                AuthSecret = authSecret,
//                BasePath = basePath
//            };
//        }


//    }
//}
