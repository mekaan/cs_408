using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example

namespace Server
{

    public partial class Form1 : Form
    {
        
        int portNum;
        int MAX_CLIENT = 128;
        int MAX_BUF = (2 << 22);
        bool listening;
        string fileDirectory = "";
        Socket server;
        //public static ManualResetEvent allDone = new ManualResetEvent(false);
        IPAddress ipAddress;
        List<Socket> clientSocketList = new List<Socket>() ;
        List<string> usernameList = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            logBox.AppendText("Server IP: " + ip.AddressList[1].ToString()+"\n");
            ipAddress = ip.AddressList[1];
            server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //clientSocketList = new List<Socket>();
            //usernameList = new List<string>();

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            bool input_check = true;
            string port = portBox.Text;
            portBox.Enabled = false;
            try
            {
                portNum = Int32.Parse(port);
            }
            catch (FormatException except)
            {
                logBox.AppendText($"ERROR: port number is not a valid number\n");
                input_check = false;
            }

            if(fileDirectory== "")
            {
                input_check = false;
            }
            if (input_check && (!listening))
            {
                try
                {
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, portNum);
                    server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    server.Bind(localEndPoint);
                    server.Listen(MAX_CLIENT);
                    listening = true;

                    logBox.AppendText($"Server STARTED at port: {portNum} \n");

                    Thread acceptThread = new Thread(Accept);
                    acceptThread.IsBackground = true;
                    acceptThread.Start();


                }
                catch (Exception except)
                {

                    logBox.AppendText($"ERROR: server socket stopped\n");
                    server.Close();
                    listening = false;
                    portBox.Text = "";
                    fileBox.Text = "";
                    portBox.Enabled = true;
                    fileBox.Enabled = true;

                }
            }
            else if(listening)
            {
                logBox.AppendText($"Server is already listening.\n");
            }
            else
            {
                logBox.AppendText($"Invalid or Empty Input\n");
                portBox.Enabled = true;
            }

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                listening = false;
                server.Close();
                logBox.AppendText($"Server STOPPED \n");
                foreach (Socket socket in clientSocketList)
                {
                    try
                    {
                        socket.Close();
                    }
                    catch
                    {
                        logBox.AppendText("ERROR: Closing client socket failed\n");
                    }

                }
                portBox.Text = "";
                fileBox.Text = "";
                fileDirectory = "";
                portBox.Enabled = true;
                fileBox.Enabled = true;
            }
            catch (Exception)
            {
                logBox.AppendText($"ERROR: Server cannot stop properly \n");
                listening = false;
                logBox.AppendText($"Server STOPPED \n");
                portBox.Text = "";
                fileBox.Text = "";
                portBox.Enabled = true;
                fileBox.Enabled = true;
            }            
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = server.Accept();
                    clientSocketList.Add(newClient);
                    logBox.AppendText("A client is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    
                }
                catch (Exception e)
                {

                    //logBox.AppendText("The server socket stopped.\n");
                    server.Close();
                    listening = false;
                    portBox.Enabled = true;
                    fileBox.Enabled = true;
                }
            }
        }

        private void Receive(Socket thisClient) // updated
        {
            string username = "";
            try
            {
                Byte[] buffer = new Byte[64];
                thisClient.Receive(buffer);
                username = Encoding.Default.GetString(buffer);
                username = username.Substring(0, username.IndexOf("\0"));
                logBox.AppendText("Client tries to connect: " + username + "\n");

                //incomingmessage = "giray"
                //string username = incomingMessage.Split(' ').ToList()[1];
            }
            catch
            {
                thisClient.Close();
                clientSocketList.Remove(thisClient);
                listening = false;
                return;
            }
            bool result = CheckUsername(username);
            if (result)
            {
                usernameList.Add(username);
                logBox.AppendText("Client is Accepted: HI " + username + "\n");
                //logBox.AppendText(usernameList.ToString()+"\n");
                HandleClient(thisClient, username);
            }
            else
            {
                RejectClient(thisClient, username);
                thisClient.Shutdown(SocketShutdown.Both);
                thisClient.Close();
                logBox.AppendText("Client is Rejected: " + username + "\n");
                clientSocketList.Remove(thisClient);
            }
        }

        private bool CheckUsername(string username)
        {
            bool result = true;
            
            if (usernameList.Contains(username))
            {
                result = false;
            }
            return result;
        }

        private void HandleClient(Socket client, string username)
        {
            try
            {
                string hello_message = "Hi from server";
                Byte[] buffer = Encoding.Default.GetBytes(hello_message);
                client.Send(buffer);
            }
            catch
            {
                logBox.AppendText($"ERROR: hi message from server to client {username} could not sent!!\n");
            }
            
            while(CheckConnection(client))
            {
                string commandMessage = "";
                string command = "";
                string filename = "";
                try
                {
                    Byte[] commandBuffer = new Byte[64];
                    commandMessage = "";
                    int temp = client.Receive(commandBuffer);
                    commandMessage = Encoding.Default.GetString(commandBuffer);
                    commandMessage = commandMessage.TrimEnd('\0');
                    /*
                    UPLOAD <filename>
                    DOWNLOAD <filename>
                    COPY <filename>
                    GETFILE ME
                    GETFILE PUBLIC
                    DELETE <filename>
                    CH_ACCESS <filename>

                    send command ACK|ERR -> DO COMMAND -> get client ACK
                    */
                    command = "";
                    filename = "";
                      
                    command = commandMessage.Split()[0];
                    filename = commandMessage.Split()[1];
                }
                catch
                {
                    //logBox.AppendText("Client is disconnected !!\n");
                    break;
                }

                logBox.AppendText("Client: " + commandMessage + "\n");

                if (command == "UPLOAD")
                {
                    bool result = UploadCommand(client, commandMessage, username, filename);
                    if(! result)
                    {
                        break;
                    }                   
                }
                else if(command == "DOWNLOAD")
                {
                    bool result = DownloadCommand(client, commandMessage, username, filename);
                    if (!result)
                    {
                        break;
                    }
                }
                else if(command == "DELETE")
                {
                    bool result = DeleteCommand(client, commandMessage, username, filename);
                    if (!result)
                    {
                        break;
                    }
                }
                else if(command == "COPY")
                {
                    bool result = CopyCommand(client, commandMessage, username, filename);
                    if (!result)
                    {
                        break;
                    }
                }
                else if (command == "GETFILE")
                {
                    bool result = GetFileCommand(client, commandMessage, username, filename);
                    if (!result)
                    {
                        break;
                    }
                }
                else if (command == "CH_ACCESS")
                {
                    bool result = ChangeAccessCommand(client, commandMessage, username, filename);
                    if (!result)
                    {
                        break;
                    }
                }
                else
                {
                    logBox.AppendText($"Unknown Command: {command}\n");
                }
            }
            logBox.AppendText($"User: {username} disconnected\n");
            usernameList.Remove(username);
            clientSocketList.Remove(client);
        }

        private bool UploadCommand(Socket client, string commandMessage, string username, string filename)
        {
            bool fileUploadError = false;
            string ackMessage = "ACK " + commandMessage;
            SendClientMessage(client, ackMessage);
            //TODO fix naming
            FileDB.PrimaryKey tempPk = new FileDB.PrimaryKey(filename, username);
            string directoryFileName = Path.Combine(fileDirectory, tempPk.ToString());
            FileStream uploadFile = System.IO.File.Create(directoryFileName);
            Byte[] uploadFileBuffer = new Byte[MAX_BUF];

            Byte[] fileSizeBuffer = new Byte[64];
            try
            {
                int temp = client.Receive(fileSizeBuffer);
            }
            catch
            {
                logBox.AppendText("ERROR: During File Upload\n");
                fileUploadError = true;
                uploadFile.Close();
                return false;
            }

            ulong fileSize = BitConverter.ToUInt64(fileSizeBuffer, 0);
            ulong numBytesRead = 0;

            //sendClientMessage(client, "ACK");
            while (fileSize > numBytesRead)
            {
                try
                {

                    int numBytes = client.Receive(uploadFileBuffer);
                    numBytesRead += (ulong)numBytes;

                    int index = Array.FindIndex(uploadFileBuffer, CheckEnd);

                    if (index > -1)
                    {
                        uploadFile.Write(uploadFileBuffer, 0, index);
                        break;
                    }
                    else
                    {

                        uploadFile.Write(uploadFileBuffer, 0, numBytes);
                    }
                }
                catch
                {
                    logBox.AppendText("ERROR: During File Upload\n");
                    uploadFile.Close();
                    fileUploadError = false;
                    break;
                }
            }
            uploadFile.Close();

            if (!fileUploadError)
            {
                FileDB.PrimaryKey newPK = new FileDB.PrimaryKey(filename, username);
                FileDB.InsertFile(newPK, Path.Combine(fileDirectory, (newPK.ToString())));
                logBox.AppendText($"File {newPK.ToString()} UPLOADED\n");
                string message = newPK.IncCount + "." + newPK.FileName + " UPLOADED";
                SendClientMessage(client, message);
            }
            return true;
        }

        private int GetCopyIdFromFileName(String fullname)
        {

            if (int.TryParse(fullname.Split('.')[1], out int n))
            {
                return n;
            }

            return  -1;
        }

        private string GetOriginalFileName(string fullname)
        {
            try
            {
                int index = fullname.IndexOf('.');
                index = fullname.IndexOf('.', index+1);
                string filename = fullname.Substring(index + 1);
                return filename;
            }
            catch
            {
                return "";
            }
        }

        private string GetUsernameFromFilename(string fullname)
        {
            try
            {
                int index = fullname.IndexOf('.');
                string username = fullname.Substring(0, index);
                return username;
            }
            catch
            {
                return "";
            }
        }

        private bool DownloadCommand(Socket client, string commandMessage, string username, string filename)
        {
            //command - filename
            //receive file -> receive client ACK
            //0_filename
            string ackMessage;
            bool checkFileValid = CheckOwnerFileValidity(GetUsernameFromFilename(filename),GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            string directoryFileName = GetDirectoryFilename(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            bool checkFileExist = System.IO.File.Exists(directoryFileName);
            if (checkFileValid && checkFileExist)
            {
                

                ackMessage = "ACK " + commandMessage;
                SendClientMessage(client, ackMessage);

                Byte[] downloadBuffer = new Byte[MAX_BUF];
                try
                {

                    //clientSocket.SendFile(filepath);
                    // StreamReader sr = new StreamReader("TestFile.txt")
                    using (FileStream fsSource = new FileStream(directoryFileName, FileMode.Open, FileAccess.Read))
                    {
                        int n, temp;

                        while (true)
                        {
                            // Read may return anything from 0 to numBytesToRead.
                            n = fsSource.Read(downloadBuffer, 0, MAX_BUF);

                            temp = client.Send(downloadBuffer, n, SocketFlags.None);
                            // Break when the end of the file is reached.

                            if (n == 0)
                                break;

                            //numBytesRead += (ulong)n;
                            //numBytesToRead -= (ulong)n;

                        }
                    }
                    logBox.AppendText("Server: File Sending Finished\n");
                    Byte[] clientAckBuffer = new Byte[64];
                    int clientAckN = client.Receive(clientAckBuffer);
                    string clientAckMessage = Encoding.Default.GetString(clientAckBuffer);
                    clientAckMessage = clientAckMessage.TrimEnd('\0');
                    logBox.AppendText("Client: "+ clientAckMessage + "\n");
                }
                catch (Exception except)
                {
                    logBox.AppendText("Error: during file sending.\n");
                    logBox.AppendText("Connection STOPPED \n");

                    client.Close();
                    return false;

                }
            }
            else
            {   
                if(!checkFileValid)
                {
                    ackMessage = "ERR " + filename + " does not exist in database!";
                }
                else
                {
                    ackMessage = "ERR " + filename + " does not exist in directory!";
                }
                
                SendClientMessage(client, ackMessage);
                logBox.AppendText("Server: " + ackMessage +"\n");
            }
            return true;
        }

        private bool DeleteCommand(Socket client, string commandMessage, string username, string filename)
        {
            string ackMessage;
            bool checkFileValid = CheckOwnerFileValidity(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            string directoryFileName = GetDirectoryFilename(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            bool checkFileExist = System.IO.File.Exists(directoryFileName);
            if (checkFileValid && checkFileExist && username == GetUsernameFromFilename(filename))
            {
                ackMessage = "ACK " + commandMessage;
                SendClientMessage(client, ackMessage);              
                try
                {

                    System.IO.File.Delete(directoryFileName);
                    FileDB.PrimaryKey pk = new FileDB.PrimaryKey(GetOriginalFileName(filename), username, GetCopyIdFromFileName(filename));
                    FileDB.DeleteFile(pk);

                    string outMessage = "File Delete Finished";
                    logBox.AppendText("Server: "+outMessage+"\n");
                    SendClientMessage(client, outMessage);
                }
                catch (Exception except)
                {
                    logBox.AppendText("Error: during file delete operation.\n");
                    logBox.AppendText("Connection Stopped\n");
                    client.Close();
                    return false;
                }
            }
            else
            {
                if (!checkFileValid)
                {
                    ackMessage = "ERR " + filename + " does not exist in database!";
                }
                else if(!checkFileExist)
                {
                    ackMessage = "ERR " + filename + " does not exist in directory!";
                }
                else
                {
                    ackMessage = "ERR " + filename + " belonged to someone else!";
                }

                SendClientMessage(client, ackMessage);
                logBox.AppendText("Server: " + ackMessage + "\n");
            }
            return true;
        }

        private bool CopyCommand(Socket client, string commandMessage, string username, string filename)
        {
            string ackMessage;
            bool checkFileValid = CheckOwnerFileValidity(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            string directoryFileName = GetDirectoryFilename(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            bool checkFileExist = System.IO.File.Exists(directoryFileName);
            if (checkFileValid && checkFileExist && username == GetUsernameFromFilename(filename))
            {
                ackMessage = "ACK " + commandMessage;
                SendClientMessage(client, ackMessage);
                try
                {
                    FileDB.PrimaryKey originalPk = new FileDB.PrimaryKey(GetOriginalFileName(filename), username, GetCopyIdFromFileName(filename));
                    FileDB.PrimaryKey copyPk = new FileDB.PrimaryKey(GetOriginalFileName(filename), username);
                    File originalFile = FileDB.GetFileByKey(originalPk);
                    string originalFileName = Path.Combine(fileDirectory, originalPk.ToString());
                    string copyFileName = Path.Combine(fileDirectory, copyPk.ToString());
                    System.IO.File.Copy(originalFileName, copyFileName);
                    FileDB.InsertFile(copyPk, Path.Combine(fileDirectory, copyFileName), originalFile.FileAccessType);


                    string outMessage = "File Copy Finished";
                    logBox.AppendText("Server: " + outMessage + "\n");
                    SendClientMessage(client, outMessage);
                }
                catch (Exception except)
                {
                    logBox.AppendText("Error: during file copy operation.\n");
                    logBox.AppendText("Connection Stopped\n");
                    client.Close();
                    return false;
                }
            }
            else
            {
                if (!checkFileValid)
                {
                    ackMessage = "ERR " + filename + " does not exist in database!";
                }
                else if(!checkFileExist)
                {
                    ackMessage = "ERR " + filename + " does not exist in directory!";
                }
                else
                {
                    ackMessage = "ERR " + filename + " belonged to someone else!";
                }

                SendClientMessage(client, ackMessage);
                logBox.AppendText("Server: " + ackMessage + "\n");
            }
            return true;
        }

        private bool ChangeAccessCommand(Socket client, string commandMessage, string username, string filename)
        {
            string ackMessage;
            bool checkFileValid = CheckOwnerFileValidity(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            string directoryFileName = GetDirectoryFilename(GetUsernameFromFilename(filename), GetOriginalFileName(filename), GetCopyIdFromFileName(filename));
            bool checkFileExist = System.IO.File.Exists(directoryFileName);
            if (checkFileValid && checkFileExist && username == GetUsernameFromFilename(filename))
            {
                ackMessage = "ACK " + commandMessage;
                SendClientMessage(client, ackMessage);
                try
                {
                    FileDB.PrimaryKey tempPk = new FileDB.PrimaryKey(GetOriginalFileName(filename), username, GetCopyIdFromFileName(filename));
                    FileDB.UpdateAccessType(tempPk, File.AccessType.PUBLIC);

                    string outMessage = "File Change Access Finished";
                    logBox.AppendText("Server: " + outMessage + "\n");
                    SendClientMessage(client, outMessage);
                }
                catch (Exception except)
                {
                    logBox.AppendText("Error: during change access operation.\n");
                    logBox.AppendText("Connection Stopped\n");
                    client.Close();
                    return false;
                }
            }
            else
            {
                if (!checkFileValid)
                {
                    ackMessage = "ERR " + filename + " does not exist in database!";
                }
                else if(!checkFileExist)
                {
                    ackMessage = "ERR " + filename + " does not exist in directory!";
                }
                else
                {
                    ackMessage = "ERR " + filename + " belonged to someone else!";
                }

                SendClientMessage(client, ackMessage);
                logBox.AppendText("Server: " + ackMessage + "\n");
            }
            return true;
        }

        private bool GetFileCommand(Socket client, string commandMessage, string username, string filename)
        {

            string ackMessage = "ACK " + commandMessage;
            SendClientMessage(client, ackMessage);

            List<File> fileList;
            if(filename == "ME")
            {
                fileList = FileDB.GetFilesByOwner(username);
            }
            else
            {
                fileList = FileDB.GetFilesByAccessType();
            }

            try
            {
                int n, temp = 0;
                int fileCount = fileList.Count();
                for(int i = 0; i<fileCount; i++)
                {
                    string fileDirectoryName = fileList[i].FilePath;
                    if (!System.IO.File.Exists(fileDirectoryName))
                    {
                        temp += 1;
                    }
                }
                n = fileCount - temp;
                Byte[] fileCountBuffer = Encoding.Default.GetBytes(n.ToString());
                n = client.Send(fileCountBuffer);

                Byte[] clientAckBuffer = new Byte [64];
                n = client.Receive(clientAckBuffer);
                string clientAck = Encoding.Default.GetString(clientAckBuffer);
                clientAck = clientAck.TrimEnd('\0');
                string outTemp = "Client: " + clientAck + "\n";
                logBox.AppendText(outTemp);

                while (temp < fileCount)
                {                                   
                    FileInfo info = new FileInfo(fileList[temp].FilePath);
                    long length = info.Length;

                    string tempFilename ="Owner: " + fileList[temp].Owner + " | Name: " + fileList[temp].Counter +"."+ fileList[temp].FileName + " | Size: " + length.ToString() + " | Time:" +fileList[temp].UploadDateTime.ToString() + " | Acces: "+ fileList[temp].FileAccessType.ToString() +"\n";
                    Byte[] getFileBuffer = Encoding.Default.GetBytes(tempFilename);
                    n = client.Send(getFileBuffer);
                    temp += 1;
                }

                logBox.AppendText("Server: File List Sending Finished\n");
                //Byte[] clientAckBuffer = new Byte[64];
                Array.Clear(clientAckBuffer, 0, 64);
                int clientAckN = client.Receive(clientAckBuffer);
                string clientAckMessage = Encoding.Default.GetString(clientAckBuffer);
                clientAckMessage = clientAckMessage.TrimEnd('\0');
                logBox.AppendText("Client: " + clientAckMessage + "\n");
            }
            catch (Exception except)
            {
                logBox.AppendText("Error: during file list sending.\n");
                logBox.AppendText("Connection Stopped \n");
                client.Close();
                return false;
            }
            return true;
        }

        private bool CheckOwnerFileValidity(string username, string filename, int incCount)
        {
            //Database functionality
            FileDB.PrimaryKey pk = new FileDB.PrimaryKey(filename, username, incCount);
            if (FileDB.GetFileByKey(pk) == null)//FileDB.GetFileByKey(pk).Equals(null)
            {
                return false;
            }
            
            return true;
        }

        private string GetDirectoryFilename(string username, string filename, int copyID)
        {
            //TODO: Database Functionality
            string absFilename = Path.Combine(fileDirectory,(username + "." + copyID + "." + filename));
            return absFilename;
        }

        private void SendClientMessage(Socket client, string message)
        {
            try
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                client.Send(buffer);
            }     
            catch
            {
                logBox.AppendText($"ERROR: message to client: {message} not sent\n");
            }
        }

        private void RejectClient(Socket client, string username)
        {
            try
            {
                string hello_message = "REJECT";
                Byte[] buffer = Encoding.Default.GetBytes(hello_message);
                client.Send(buffer);
            }
            catch
            {
                logBox.AppendText($"ERROR: REJECT message from server to client {username} could not sent!!\n");
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            string temp = "";
            fileBox.Enabled = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fileBox.Text = folderBrowserDialog1.SelectedPath;
                temp = fileBox.Text;
            }
            if(Directory.Exists(temp))
            {
                fileDirectory = temp;
                logBox.AppendText($"DOWNLOAD DIRECTORY: {fileDirectory} \n");
                fileBox.Enabled = false;
            }
            else
            {
                logBox.AppendText("ERROR: path does not exist \n");
                fileDirectory = "";
            }
        }

        private bool CheckConnection(Socket socket)
        {
            if (listening)
            {
                bool blockingState = socket.Blocking;
                try
                {
                    byte[] tmp = new byte[1];

                    socket.Blocking = false;
                    socket.Send(tmp, 0, 0);
                    Console.WriteLine("Connected!");
                }
                catch (SocketException e)
                {
                    // 10035 == WSAEWOULDBLOCK
                    if (e.NativeErrorCode.Equals(10035))
                    {
                        //Console.WriteLine("Still Connected, but the Send would block");
                    }
                    else
                    {
                        //Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
                    }
                    logBox.AppendText("ERROR: Connection Check is a failed !!\n");
                }
                finally
                {
                    socket.Blocking = blockingState;
                }
                return socket.Connected;
            }
            else
            {
                return false;
            }
        }

        private bool CheckEnd(Byte b)
        {
            if(b=='\0')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
