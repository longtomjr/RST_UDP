using PCars2UDP;
using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;


namespace UDP_Example
{
    class Program
    {
        static void Main(string[] args)
        {

            PCars2Listener listener = new PCars2Listener();
               
            PCars2UDPReader uDP = new PCars2UDPReader(listener);             //Create an UDP object that will retrieve telemetry values from in game.

            while (true)
            {
                uDP.ReadPackets();                      //Read Packets ever loop iteration
                                                        //Console.WriteLine(uDP.ParticipantInfo[uDP.ViewedParticipantIndex, 15]);
                // NOTE: JUST FOR DEBUG PURPOSES
                //XmlSerializer x = new XmlSerializer(uDP.GetType());
                //x.Serialize(Console.Out, uDP);
                //Console.WriteLine();

                //Write to console what our current speed is.

                //For Wheel Arrays 0 = Front Left, 1 = Front Right, 2 = Rear Left, 3 = Rear Right.
            }

            
        }
    }
}
