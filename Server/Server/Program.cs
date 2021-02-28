using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using MySql.Data.MySqlClient;

//MySqlClient reference: https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html

namespace Server
{
    public class File
    {
        public File() { }
        public enum AccessType
        {
            PUBLIC = 0,
            PRIVATE = 1
        }
        public File(string fileName, string filePath, string owner, int counter, AccessType type, DateTime uploadDateTime)
        {
            this.FileName = fileName;
            this.FilePath = filePath;
            this.Owner = owner;
            this.Counter = counter;
            this.FileAccessType = type;
            this.UploadDateTime = uploadDateTime;
        }
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Owner { get; set; }
        public int Counter { get; set; }
        public AccessType FileAccessType { get; set; }
        public DateTime UploadDateTime { get; set; }
    }

    public static class FileUtils
    {
        public static File.AccessType AccessTypeConverter(string val)
        {
            switch (val)
            {
                case "PUBLIC":
                    return File.AccessType.PUBLIC;

                case "PRIVATE":
                    return File.AccessType.PRIVATE;

                default:
                    throw new Exception("INVALID ACCESS TYPE INPUTTED");

            }
        }

        public static File.AccessType AccessTypeConverter(int key)
        {
            switch (key)
            {
                case 0:
                    return File.AccessType.PUBLIC;

                case 1:
                    return File.AccessType.PRIVATE;

                default:
                    throw new Exception("INVALID ACCESS TYPE INPUTTED");

            }
        }
    }


    public static class FileDB
    {
        public class PrimaryKey
        { 
            public String FileName { get; }
            public String Owner { get; }
            public int IncCount { get; }
            public PrimaryKey(String fileName, String owner)
            {
                FileName = fileName;
                Owner = owner;
                IncCount = GetNextIncCount(fileName, owner);
            }   
            public PrimaryKey(String fileName, String owner, int incCount)
            {
                FileName = fileName;
                Owner = owner;

                if (CheckIncCount(fileName, owner, incCount))
                {//VALID 
                    IncCount = incCount;
                }
                else
                {
                    IncCount = GetNextIncCount(fileName, owner);
                }
            }
            public override string ToString() => $"{Owner}.{IncCount}.{FileName}";
        }

        //Singleton (Lazy Initilization)
        private static MySqlConnection mySqlConnection = null;
        private const string CONNECTION_STRING = @"server=remotemysql.com;userid=ioI0xzbThf;password=VGITbQxEEa;database=ioI0xzbThf";

        //QUERIES
        private const string INSERT_SQL =
            "INSERT INTO FILES (fileName, filePath, owner, incCount, accessType) VALUES(@fileName, @filePath, @owner, @incCount, @accessType)";

        private const string GET_NEXT_INCCOUNT = 
            "SELECT MAX(incCount) FROM FILES WHERE fileName = @fileName AND owner = @owner";

        private const string CHECK_INCCOUNT =
            "SELECT COUNT(*) FROM FILES WHERE fileName = @fileName AND owner = @owner AND incCount = @incCount";

        private const string UPDATE_ACCESS_TYPE_SQL = 
            "UPDATE FILES SET accessType = @newAccessType WHERE fileName = @fileName AND owner = @owner AND incCount = @incCount";

        private const string DELETE_FILE_BY_KEY_SQL = 
            "DELETE FROM FILES WHERE fileName = @fileName AND owner = @owner AND incCount = @incCount";

        private const string GET_FILE_BY_KEY_SQL =
            "SELECT * FROM FILES WHERE fileName = @fileName AND owner = @owner AND incCount = @incCount";

        private const string GET_ALL_FILES_SQL = "SELECT * FROM FILES";
        private const string GET_FILES_BY_OWNER_SQL = "SELECT * FROM FILES WHERE owner = @owner";
        private const string GET_FILES_BY_ACCESS_TYPE_SQL = "SELECT * FROM FILES WHERE accessType = @accessType";
        private const string GET_ALL_FILE_NAMES_SQL = "SELECT fileName FROM FILES";
        private const string GET_PUBLIC_FILE_NAMES_SQL = "SELECT fileName FROM FILES WHERE  accessType = 'PUBLIC'";
        private const string GET_FILES_BY_NAME_SQL = "SELECT * FROM FILES WHERE fileName = @fileName";
        
        //Singleton init function
        public static MySqlConnection GetMySqlConnection()
        {
            try
            {
                if (mySqlConnection == null)
                {
                    mySqlConnection = new MySqlConnection(CONNECTION_STRING);
                    mySqlConnection.Open();
                }
                else if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
                {
                    mySqlConnection.Open();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return mySqlConnection;

        }
        private static int GetNextIncCount(String fileName, String owner)
        {
            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_NEXT_INCCOUNT, conn);
                cmd.Parameters.AddWithValue("@fileName", fileName);
                cmd.Parameters.AddWithValue("@owner", owner);
                object result = cmd.ExecuteScalar();

                if (!DBNull.Value.Equals(result))
                {
                    return Convert.ToInt32(result) + 1;
                }

            }
            catch (Exception e)
            {
                //TO-DO                
                Console.WriteLine("Error: Database Connection Error");
                //throw e;

            }

            //If no such file exist returns 0
            return 0;
        }
        private static bool CheckIncCount(String fileName, String owner, int incCount)
        {
            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(CHECK_INCCOUNT, conn);
                cmd.Parameters.AddWithValue("@fileName", fileName);
                cmd.Parameters.AddWithValue("@owner", owner);
                cmd.Parameters.AddWithValue("@incCount", incCount);

                object result = cmd.ExecuteScalar();

                if (Convert.ToInt32(result) == 1)
                {
                    return true;
                }
                else if (Convert.ToInt32(result) == 0)
                {
                    return false;
                }
                else
                {
                    throw new Exception("PRIMARY KEY CONSTRAINT VIOLATION");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
                throw e;
            }
        }
        public static void DumbDB()
        {
            MySqlConnection conn = GetMySqlConnection();

            string query = "SELECT * FROM FILES;";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "table_name");
            DataTable dt = ds.Tables["table_name"];

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.Write(row[col] + "\t");
                }

                Console.Write("\n");
            }
        }
        public static List<File> GetAllFiles()
        {
            List<File> fileList = new List<File>();

            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_ALL_FILES_SQL, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    File newFile = new File(rdr.GetString(1), 
                                            rdr.GetString(2), rdr.GetString(3), 
                                            rdr.GetInt16(4), FileUtils.AccessTypeConverter(rdr.GetString(5)),
                                            rdr.GetDateTime(6));
                    fileList.Add(newFile);
                }

                rdr.Close();
            }
            catch (Exception e)
            {
                //TO-DO
            }

            return fileList;
        }
        public static List<String> GetAllFileNames()
        {
            List<String> fileNames = new List<String>();
            MySqlDataReader rdr = null;

            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_ALL_FILE_NAMES_SQL, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    String fileName = rdr.GetString(0);
                    fileNames.Add(fileName);
                }

            }
            catch (Exception e)
            {
                //TO-DO
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }

            return fileNames;
        }
        public static List<String> GetPublicFileNames()
        {
            List<String> fileNames = new List<String>();
            MySqlDataReader rdr = null;

            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_PUBLIC_FILE_NAMES_SQL, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    String fileName = rdr.GetString(0);
                    fileNames.Add(fileName);
                }
            }
            catch (Exception e)
            {
                //TO-DO
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }

            return fileNames;
        }
        public static List<File> GetFilesByOwner(String owner)
        {
            List<File> fileList = new List<File>();

            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_FILES_BY_OWNER_SQL, conn);
                cmd.Parameters.AddWithValue("@owner", owner);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    File newFile = new File(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), 
                                            rdr.GetInt16(4), FileUtils.AccessTypeConverter(rdr.GetString(5)), rdr.GetDateTime(6));
                    fileList.Add(newFile);
                }

                rdr.Close();
            }
            catch (Exception e)
            {
                //TO-DO
            }

            return fileList;
        }
        
        //By default get all public files
        public static List<File> GetFilesByAccessType(File.AccessType accessType = File.AccessType.PUBLIC)
        {
            List<File> fileList = new List<File>();

            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_FILES_BY_ACCESS_TYPE_SQL, conn);
                cmd.Parameters.AddWithValue("@accessType", accessType.ToString());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    File newFile = new File(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), 
                                            rdr.GetInt16(4), FileUtils.AccessTypeConverter(rdr.GetString(5)), rdr.GetDateTime(6));
                    fileList.Add(newFile);
                }

                rdr.Close();
            }
            catch (Exception e)
            {
                //TO-DO
            }

            return fileList;
        }
        public static File GetFileByKey(PrimaryKey key)
        {
            MySqlDataReader rdr = null;
            File newFile = null;

            try
            {
                MySqlConnection conn = GetMySqlConnection();
                MySqlCommand cmd = new MySqlCommand(GET_FILE_BY_KEY_SQL, conn);
                cmd.Parameters.AddWithValue("@fileName", key.FileName);
                cmd.Parameters.AddWithValue("@owner", key.Owner);
                cmd.Parameters.AddWithValue("@incCount", key.IncCount);

                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Read();
                    newFile = new File(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt16(4), FileUtils.AccessTypeConverter(rdr.GetString(5)), rdr.GetDateTime(6));
                }

            }
            catch (Exception e)
            {
                //TO-DO
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();

                }
            }
            
            //if there is no such file in the database returns null
            return newFile;

        }
        public static void InsertFile(PrimaryKey pk, String filePath, File.AccessType accessType = File.AccessType.PRIVATE)
        {
            try
            {
                MySqlConnection conn = GetMySqlConnection();

                MySqlCommand cmd = new MySqlCommand(INSERT_SQL, conn);
                cmd.Parameters.AddWithValue("@fileName", pk.FileName);
                cmd.Parameters.AddWithValue("@owner", pk.Owner);
                cmd.Parameters.AddWithValue("@incCount", pk.IncCount);
                cmd.Parameters.AddWithValue("@filePath", filePath);
                cmd.Parameters.AddWithValue("@accessType", accessType.ToString());

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }

        }
        public static void DeleteFile(PrimaryKey pk)
        {
            try
            {
                MySqlConnection conn = GetMySqlConnection();

                MySqlCommand cmd = new MySqlCommand(DELETE_FILE_BY_KEY_SQL, conn);
                cmd.Parameters.AddWithValue("@fileName", pk.FileName);
                cmd.Parameters.AddWithValue("@owner", pk.Owner);
                cmd.Parameters.AddWithValue("@incCount", pk.IncCount);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }
        public static void UpdateAccessType(PrimaryKey pk, File.AccessType newAccessType)
        {
            try
            {
                MySqlConnection conn = GetMySqlConnection();

                MySqlCommand cmd = new MySqlCommand(UPDATE_ACCESS_TYPE_SQL, conn);
                cmd.Parameters.AddWithValue("@fileName", pk.FileName);
                cmd.Parameters.AddWithValue("@owner", pk.Owner);
                cmd.Parameters.AddWithValue("@incCount", pk.IncCount);
                cmd.Parameters.AddWithValue("@newAccessType", newAccessType.ToString());

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }
    }


    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }

}
