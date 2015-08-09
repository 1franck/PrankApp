using System;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace PrankApp
{
    class Program
    {

        public static Random _random = new Random();

        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Prank App (Original codes/idea from Jerry(aka. Barnacules)) by 1Franck");
            Console.WriteLine(" ");

            // check for cli arguments
            if(args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            // Create all threads
            Thread mouseThread = new Thread(new ThreadStart(MouseThread));
            Thread keyboardThread = new Thread(new ThreadStart(KeyboardThread));
            Thread soundThread = new Thread(new ThreadStart(SoundThread));
            Thread popupThread = new Thread(new ThreadStart(PopupThread));

            Console.WriteLine("Waiting 10 seconds before starting threads...");

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            // Start all of the threads
            mouseThread.Start();
            keyboardThread.Start();
            soundThread.Start();
            popupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Killing threads...");

            mouseThread.Abort();
            keyboardThread.Abort();
            soundThread.Abort();
            popupThread.Abort();

            
        }

        #region Thread Functions
        /// <summary>
        /// Mouse Thread
        /// </summary>
        public static void MouseThread()
        {
            int moveX = 0;
            int moveY = 0;

            while(true)
            {
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                Cursor.Position = new System.Drawing.Point(Cursor.Position.X - moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Keyboard Thread
        /// </summary>
        public static void KeyboardThread()
        {
            while (true)
            {
                //random capital letter
                char key = (char)(_random.Next(25) + 65);

                if(_random.Next(2) == 0)
                {
                    key = char.ToLower(key);
                }
                SendKeys.SendWait(key.ToString());
                Thread.Sleep(_random.Next(1500));
            }
        }

        /// <summary>
        /// Sound Thread
        /// </summary>
        public static void SoundThread()
        {
            while (true)
            {
                if(_random.Next(100) > 80)
                {
                    switch(_random.Next(5))
                    {
                        case 0: SystemSounds.Asterisk.Play(); break;
                        case 1: SystemSounds.Beep.Play(); break;
                        case 2: SystemSounds.Exclamation.Play(); break;
                        case 3: SystemSounds.Hand.Play(); break;
                        case 4: SystemSounds.Question.Play(); break;
                    }
                } 
                Thread.Sleep(_random.Next(1500));
            }
        }

        /// <summary>
        /// Popup Thread
        /// </summary>
        public static void PopupThread()
        {
            while (true)
            {
                string[] messages = {
                    "System is unstable. Try to reboot!",
                    "Your system is running low on resources",
                    "Explorer.exe - DLL Initialization Failed...",
                    "A resource required for this operation is disabled.",
                    "Secure Boot detected that rollback of protected data has been attempted.",
                    "The drive letter assigned to a system disk on one node conflicted with the drive letter assigned to a disk on another node.",
                };

                MessageBox.Show(
                    messages[_random.Next(6)],
                    "Microsoft Windows",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                Thread.Sleep(2500);
            }
        }
        #endregion

    }
}
