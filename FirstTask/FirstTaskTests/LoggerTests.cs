using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;
using System.IO;


namespace FirstTaskTests
{
    [TestClass]
    public class LoggerTests
    {
        private Logger logger;

        [TestInitialize]
        public void TestInitialize()
        {
            logger = new Logger(@"C:\Users\User\source\repos\FirstTask\FirstTask\Logs.txt");
        }


        /// <summary>
        /// Tests method Log
        /// </summary>
        [TestMethod]
        public void Log_ShouldWriteMessageToTheFile()
        {
            // Arrange
            string expected = "Xif";
            logger.StartLog();

            // Act
            logger.Log(expected);
            string actual;
            using (StreamReader sr = new StreamReader(logger.Path, System.Text.Encoding.Default))
            {
                 actual = sr.ReadLine();
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method StartLog
        /// </summary>
        [TestMethod]
        public void StartLog_ShouldClearAllInformationFromFile()
        {
            // Arrange
            bool expected = true;
            logger.StartLog();

            // Act
            logger.Log("Random Text");
            bool actual = false;
            string fromFile;
            logger.StartLog();
            using (StreamReader sr = new StreamReader(logger.Path, System.Text.Encoding.Default))
            {
                fromFile = sr.ReadToEnd();
            }

            if (fromFile.Replace(" ", "").Length == 0)
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }
            
    }

}
