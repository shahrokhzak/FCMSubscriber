using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCMSubscriber;

namespace FCMSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var topicName = FCMSubscriber.TopicName.All;
            
            var token =
                "c18gz7uLTWm_CB3CNOPAHw:APA91bE_fmDzUowDjtw7afE1t4xsZjMFpYi2pKi01RiMtL6-jxZfH4Ybx68BaRvwIA9ICs4LT1j4lpR4e1eAYVWl-moJ7VkuY5j_ec1vEnoXW7bALUODVM3bOcpEjTMUqCaedq_mGQ-4";

            var TokensList = new List<string>
            {
                
                "c18gz7uLTWm_CB3CNOPAHw:APA91bE_fmDzUowDjtw7afE1t4xsZjMFpYi2pKi01RiMtL6-jxZfH4Ybx68BaRvwIA9ICs4LT1j4lpR4e1eAYVWl-moJ7VkuY5j_ec1vEnoXW7bALUODVM3bOcpEjTMUqCaedq_mGQ-4",
                "cnntxr78SkSR1TqVTV7rAm:APA91bG84B83wMmvidqgN31G6ewF7Y8MdR_KTg6QAzLs9xrL5LG4npaO_uhvJ1FXuYFf5c2DecJ4_nTeHgfUt6ScIq48ibkGxNqLYz5oID0B0cOf9rj8hN-jA1i65-TnDgoBIZ6HOL3E",
                "ekllUeA2TSKY1k2F1CK5z_:APA91bFCxmaeR9kjb3kD_tpo3s6D44IPe0tThm1YOWYC6D-FfWAV81-kZLawxwAYsp5rvDHAnzJDUuq52Lj-qIs1anhtDMtugFM6y3-mv3EXx6-6bp3bo-_TSr6zqGG8qO0bQ_t-oz_I",
                "cu_eDLYjR5G7YfurM4syQ6:APA91bEh5sCXw56a2AxxopMfTKr5e3BtBS0GBKmT9CQ4s7e2uBp7Q2NHLWT_MCwKydhiRA4YXFnvpisd1ve4jU1cpDuxL1AOZtNbuO5WQjOb3hoDVY03u3oCJkPJ0T--l1SAvJAzgilO"
                
            };

            var connection = "Server=10.0.79.36;Database=PNA_SunService;User Id=sa; Password=abc@1234;Trusted_Connection=false;";


            //SAMPLES
            
            //var result = FCMSubscriber.Subscribe("asdasdasdasdasdasd", topicName,@"D:\sun3test-firebase-adminsdk-xaoio-42fe26a1fb.json");
            //var result = FCMSubscriber.Subscribe("asdasdasdasdasdasd", topicName,@"D:\sun3test-firebase-adminsdk-xaoio-42fe26a1fb.json");
            //var result = FCMSubscriber.Subscribe(connection, "customer", "FcmToken", topicName,@"D:\sun3test-firebase-adminsdk-xaoio-42fe26a1fb.json");



            Console.WriteLine(result.Result);
            Console.WriteLine("*********************");
            Console.ReadLine();
        }
    }
}
