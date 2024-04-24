using CommunityToolkit.Maui.Storage;
using System;
using System.Text;

namespace MauiApp1.src
{
    internal class SaveManager
    {
        
        public SaveManager() { }
        private string CreateData(Board board, int size)
        {
            string data = size.ToString() + " ";
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(board.cells[i, j].getIsAlive())
                    {
                        data += "1 ";
                    }
                    else
                    {
                        data += "0 ";
                    }
                }
            }
            return data;
        }
        public async void Save(Board board, int size) 
        {
            
            //string dirPath = @"..\..\..\src\savedStates\";
            string now = DateTime.Now.ToString();
            now = now.Replace(":", "_");
            now = now.Replace(" ", "_");
            now = now.Replace(".", "_");
            string data = CreateData(board, size);
            string name = now + ".txt";
            /*
            string fullPath = dirPath + now + ".txt";
            FileStream fs=  File.Create(fullPath);
            byte[] info = new UTF8Encoding(true).GetBytes(data);
            fs.Write(info, 0, info.Length);
            fs.Close();
            */


            //string message = "Current state saved in: " + now + ".txt";

            // MessageBox.Show(message);

            using var stream = new MemoryStream(Encoding.Default.GetBytes(data));
            var fileSaverResult = await FileSaver.Default.SaveAsync(name, stream, CancellationToken.None);
                
        }

    }
}
