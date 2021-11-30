using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessLibrary
{
    public class Logger
    {
        private string _path;

        public Logger(string path)
        {
            _path = path;
        }

        public string Path { get => _path; }

        /// <summary>
        /// Log incoming message to file
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            using(StreamWriter sw = new StreamWriter(_path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(message);
            }
        }

        /// <summary>
        /// Deletes all the information from file
        /// </summary>
        public void StartLog()
        {
            File.Create(_path).Close();
        }
    }
}
