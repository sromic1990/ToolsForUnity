using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Sourav.Utilities.Scripts
{
    public static class FileIO
    {
        private static string STRINGPATH = Application.persistentDataPath + "/gameData.sou";
        
        public static void WriteData(string dataStream)
        {
            File.WriteAllText(STRINGPATH, dataStream);
        }

        public static string ReadData()
        {
            if (FileExists())
            {
                string str = File.ReadAllText(STRINGPATH);
                return str;
            }
            else
            {
                return "";
            }
        }

        public static bool FileExists()
        {
            if(File.Exists(STRINGPATH))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetSavePath()
        {
            return STRINGPATH;
        }
    }
}