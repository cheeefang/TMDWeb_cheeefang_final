using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using System.IO;
using System.Security;
using System.Threading;
using System.Collections.Concurrent;

namespace TMDApp
{
    public class QuividiProcessor:IWatchers
    {
        // Different event that can be received
        private const byte CFG_WATCHER_MODE = 0x01;
        private const byte CFG_TRACKED_MODE = 0x02;
        private const byte CFG_MOTION_MODE = 0x03;

        //Quividi server information
        private string ServerHost = null; // Quividi server IP
        private int ServerPort = 0;       // Quividi server Port
        private byte ConfigMode = CFG_MOTION_MODE;   // The event type that should be handled, currently set to periodic event mode

        // Different usefull values of Quividi protocol header
        private const ushort MSG_MAGIC_WORD = (ushort)(0xCAFE);
        private static readonly byte[] MSG_SPLIT_MAGIC_WORD = { 0xFE, 0xCA }; // Quividi messages are in little-endian
        private const byte MSG_VERSION = 0x02; // Version of the protocol 
        private const short MSG_RESERVED = 0x0000;

        // Message types and payload sizes
        private const byte WATCHER_TYPE = 0x30;
        private const short WATCHER_PAYLOADSIZE = 0x0020;
        private const byte MOTION_TYPE = 0x40;
        private const short MOTION_PAYLOADSIZE = 0x0017;
        private const byte ACK_TYPE = 0x06;
        private const byte NACK_TYPE = 0x07;
        private const short ACK_PAYLOADSIZE = 0x0000;

        // Different usefull alues of config message to send to server
        private const byte CFG_TYPE = 0x20;
        private const short CFG_PAYLOADSIZE = 0x0001;

        // Parameters for socket connection retry
        private const int MAX_NB_TRY = 5;
        private const int RETRY_DELAY = 3;

        // Socket variables
        private Socket vidiSocket = null;

        //watcher status flag
        private const int DEAD_FLAG = 0x00000001;
        private const int VALIDATED_FLAG = 0x00000002;
        private const int SELECTED_FLAG = 0x00000010;

        private bool _errFlag = false;
        private Exception _err = null;
        private Logger lg = new Logger("Quividi.log");
        private bool simMode = false;

        //thread safe implementation of dictionary
        private ConcurrentDictionary<uint, Watcher> _watchers = new ConcurrentDictionary<uint, Watcher>();
        public Watcher[] getCurrentWatchers()
        {
            return simMode ? randomizeWatchers() : _watchers.Values.ToArray();
        }

        //dummy method to get random watchers
        private Watcher[] randomizeWatchers()
        {
            Random r = new Random();
            int noPax = r.Next(1, 11);
            Watcher[] lst = new Watcher[noPax];

            Watcher w;
            for (int i = 0; i < noPax; i++)
            {
                w = new Watcher();

                w.age = (uint)r.Next(1, 101);
                if (w.age <= 12) w.ageGroup = AgeBracket.Child;
                else if (w.age <= 21) w.ageGroup = AgeBracket.YoungAdult;
                else if (w.age <= 50) w.ageGroup = AgeBracket.Adult;
                else w.ageGroup = AgeBracket.Senior;

                w.gender = r.Next(0, 2) == 0 ? Gender.Male : Gender.Female;

                int m = r.Next(0, 6);
                switch (m)
                {
                    case 1:
                        w.mood = Mood.VeryUnhappy;
                        break;
                    case 2:
                        w.mood = Mood.Unhappy;
                        break;
                    case 3:
                        w.mood = Mood.Neutral;
                        break;
                    case 4:
                        w.mood = Mood.Happy;
                        break;
                    case 5:
                        w.mood = Mood.VeryHappy;
                        break;
                    default:
                        w.mood = Mood.Undetermined;
                        break;
                }

                lst[i] = w;
            }

            return lst;
        }

        private Thread thread = null;
        public bool start()
        {
            if (!simMode)
            {
                if (ServerHost == null || ServerHost == "" || ServerPort == 0)
                {
                    _errFlag = true;
                    _err = new ArgumentNullException("Server host or port cannot be empty.");
                    stop = true;
                    return false;
                }

                openConnection();
                if (_errFlag)
                {
                    stop = true;
                    return false;
                }

                thread = new Thread(new ThreadStart(this.run));
                thread.Start();
            }
            return true;
        }

        public bool terminate()
        {
            stop = true;
            if (thread != null) thread.Join();

            return true;
        }

        public bool stop { get; set; }

        public bool hasError()
        {
            return _errFlag;
        }

        public Exception lastError()
        {
            return _err;
        }

        //if in simulation mode, pass in true to inSimMode input argument.
        //in simulation mode, no socket connection is made, instead when getCurrentWatchers method is called, random values will be generated
        public QuividiProcessor(string ServerHost, int ServerPort, bool inSimMode = false)
        {
            this.ServerHost = ServerHost;
            this.ServerPort = ServerPort;
            this.simMode = inSimMode;
        }

        private void openConnection()
        {
            int nbTry = 0;
            _errFlag = true;

            do
            {
                try
                {
                    // Establish the remote endpoint for the socket.   
                    IPAddress ipAddress = IPAddress.Parse(ServerHost);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, ServerPort);

                    // Create a TCP/IP  socket.  
                    vidiSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    Console.WriteLine("Socket created, waiting to connect....");

                    // Connect the socket to the remote endpoint
                    vidiSocket.Connect(remoteEP);

                    _errFlag = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred. " + e.Message + ". Trying to reconnect...");
                    lg.log(e);
                    _errFlag = true;
                    _err = e;
                }
                finally
                {
                    if (_errFlag)
                    {
                        try
                        {
                            Thread.Sleep(RETRY_DELAY * 1000);
                        }
                        catch (Exception ie)
                        {
                            Console.WriteLine("Wait Error: " + ie);
                        }
                    }
                }
            } while (_errFlag && nbTry < MAX_NB_TRY);
        }

        public void run()
        {
            try
            {
                stop = false;
                _errFlag = false;
                _err = null;

                Console.WriteLine("Connected, sending config msg...");
                lg.log("Connected");

                #region send config msg
                vidiSocket.Send(BitConverter.GetBytes(MSG_MAGIC_WORD));
                vidiSocket.Send(new byte[] { MSG_VERSION });
                vidiSocket.Send(new byte[] { CFG_TYPE });
                vidiSocket.Send(BitConverter.GetBytes(MSG_RESERVED));
                vidiSocket.Send(BitConverter.GetBytes(CFG_PAYLOADSIZE));
                vidiSocket.Send(new byte[] { ConfigMode });
                #endregion

                byte[] readByte = new byte[1];
                byte[] readShort = new byte[2];
                bool alreadyReadByte = false;
                byte[] readAlreadyReadByte = new byte[1];
                bool badSynchroMsg = false;
                bool isHeaderStart = false;

                //msg info
                int rcvVersion, rcvType, rcvReserved, rcvPayloadSize;

                Console.WriteLine("Reading from socket...");
                //read and process from socket
                while (!stop)
                {
                    if (!alreadyReadByte)
                        vidiSocket.Receive(readByte);
                    else
                    {
                        readByte = readAlreadyReadByte;
                        alreadyReadByte = false;
                    }

                    #region find magic word aka start of msg              
                    if (readByte[0] == MSG_SPLIT_MAGIC_WORD[0])
                    {
                        vidiSocket.Receive(readByte);
                        if (readByte[0] == MSG_SPLIT_MAGIC_WORD[1])
                        {
                            isHeaderStart = true;
                        }
                        else
                        {
                            alreadyReadByte = true;
                            readAlreadyReadByte = readByte;
                        }
                    }
                    else
                    {
                        //when bad sync, just let it read until it auto sync back
                        if (!badSynchroMsg) Console.WriteLine("Bad Sync.. trying to re-sync...");
                        badSynchroMsg = true;
                    }
                    #endregion

                    #region decode msg
                    //check if start of msg
                    if (isHeaderStart)
                    {
                        badSynchroMsg = false;

                        // read header
                        //FIELD         SIZE(byte)      Value
                        //MagicWord     2               0xCAFE         
                        //Version       1               0x02
                        //Type          1               variable
                        //Reserved      2               variable
                        //PayloadSize   2               variable
                        vidiSocket.Receive(readByte);
                        rcvVersion = (int)readByte[0];

                        vidiSocket.Receive(readByte);
                        rcvType = (int)readByte[0];

                        vidiSocket.Receive(readShort);
                        rcvReserved = (int)readByte[0];

                        vidiSocket.Receive(readShort);
                        rcvPayloadSize = BitConverter.ToInt16(readShort, 0);

                        // If Quividi Motion message
                        if (rcvVersion <= MSG_VERSION && rcvType == MOTION_TYPE && rcvPayloadSize == MOTION_PAYLOADSIZE)
                        {
                            //motion msg are send per picture frame so they are very fast
                            //Console.WriteLine("< Quividi motion message found");

                            processMotionMsg();
                        }
                        else if (rcvType == ACK_TYPE && rcvPayloadSize == ACK_PAYLOADSIZE)
                        {
                            Console.WriteLine("< Quividi ack message found");
                        }
                        else if (rcvType == NACK_TYPE && rcvPayloadSize == ACK_PAYLOADSIZE)
                        {
                            Console.WriteLine("< Quividi nack message found");
                        }
                        else // not a valid quividi message or message is not being used
                        {
                            if (rcvVersion > MSG_VERSION)
                                Console.WriteLine("< Quividi wrong protocol version, msg is dropped");

                            // drop payload
                            if (rcvPayloadSize > 0)
                            {
                                byte[] payload = new byte[rcvPayloadSize];
                                vidiSocket.Receive(payload);
                            }
                        }
                    }
                    #endregion

                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                lg.log(ane);
                _errFlag = true;
                _err = ane;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                lg.log(se);
                _errFlag = true;
                _err = se;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                lg.log(e);
                _errFlag = true;
                _err = e;
            }
            finally
            {
                if (vidiSocket != null)
                {
                    // Release the socket.  
                    vidiSocket.Shutdown(SocketShutdown.Both);
                    vidiSocket.Close();
                }

                stop = true;
                lg.log("Stopped");
            }
        }

        //FIELD             SIZE(byte)
        //X_Position        2
        //Y_Position        2
        //Width             2
        //Height            2
        //Est Dist          2
        //WatcherID         4
        //Status            4
        //Gender            1
        //Age Bracket       1
        //Age value         1
        //Extra features    2
        private void processMotionMsg()
        {
            byte[] readByte = new byte[1];
            byte[] readShort = new byte[2];
            byte[] readInt = new byte[4];

            //watcher info
            uint status, watcherID, age;
            AgeBracket ageBracket;
            Gender gender;
            Watcher watcher;
            ushort m;

            vidiSocket.Receive(readShort); //x position not needed
            vidiSocket.Receive(readShort); //y position not needed
            vidiSocket.Receive(readShort); //width not needed
            vidiSocket.Receive(readShort); //height not needed
            vidiSocket.Receive(readShort); //est dist not needed

            vidiSocket.Receive(readInt);
            watcherID = BitConverter.ToUInt32(readInt, 0);

            vidiSocket.Receive(readInt);
            status = BitConverter.ToUInt32(readInt, 0);

            vidiSocket.Receive(readByte);
            if (readByte[0] == 0x01) gender = Gender.Male;
            else if (readByte[0] == 0x02) gender = Gender.Female;
            else gender = Gender.Unknown;

            vidiSocket.Receive(readByte);
            if (readByte[0] == 0x01) ageBracket = AgeBracket.Child;
            else if (readByte[0] == 0x02) ageBracket = AgeBracket.YoungAdult;
            else if (readByte[0] == 0x03) ageBracket = AgeBracket.Adult;
            else if (readByte[0] == 0x04) ageBracket = AgeBracket.Senior;
            else ageBracket = AgeBracket.Unknown;

            vidiSocket.Receive(readByte);
            age = readByte[0];

            //extra features
            vidiSocket.Receive(readShort);
            m = BitConverter.ToUInt16(readShort, 0);
            Mood mood = Mood.Undetermined;
            //shift the bits to extract the mood portion
            //if do not cast to short, c# by default uses int 
            switch (((ushort)(m << 5)) >> 13)
            {
                case 1:
                    mood = Mood.VeryUnhappy;
                    break;
                case 2:
                    mood = Mood.Unhappy;
                    break;
                case 3:
                    mood = Mood.Neutral;
                    break;
                case 4:
                    mood = Mood.Happy;
                    break;
                case 5:
                    mood = Mood.VeryHappy;
                    break;
            }

            //Console.WriteLine("Watcher " + watcherID + " status: " + status);

            if ((status & DEAD_FLAG) == DEAD_FLAG)
            {
                Console.WriteLine("Watcher " + watcherID + " exited..");

                //watcher exited, remove from list
                _watchers.TryRemove(watcherID, out watcher);
            }
            else
            {
                //Console.WriteLine("Watcher " + watcherID + " validated..");

                watcher = new Watcher() { age = age, ageGroup = ageBracket, gender = gender, mood = mood };

                _watchers.AddOrUpdate(watcherID, watcher, (key, existWatcher) => {
                    //watcher exist, update values
                    existWatcher.age = watcher.age;
                    existWatcher.ageGroup = watcher.ageGroup;
                    existWatcher.gender = watcher.gender;
                    existWatcher.mood = watcher.mood;
                    return existWatcher;
                });
            }
        }
    }
}
