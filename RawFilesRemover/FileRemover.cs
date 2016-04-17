using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace RawFilesRemover
{
    class FileRemover
    {
        private string WorkingDirectory;
        public FileRemover()
        {
            WorkingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public List<string> GetFilesToRemove(string checkExtension, string compareExtension)
        {
            List<string> ckeckFiles = GetFilesOfType(checkExtension);
            List<string> compareFiles = GetFilesOfType(compareExtension);

            List<string> deleteFiles = new List<string>();
            for (int deleteIndex = 0; deleteIndex < ckeckFiles.Count; deleteIndex++)
            {
                bool found = false;
                for (int compareIndex = 0; compareIndex < compareFiles.Count; compareIndex++)
                {
                    string deleteFile = DeleteExtension(ckeckFiles[deleteIndex], checkExtension);
                    string compareFile = DeleteExtension(compareFiles[compareIndex], compareExtension);
                    if (deleteFile.Equals(compareFile))
                    {
                        found = true;
                        compareFiles.RemoveAt(compareIndex);
                        break;
                    }
                }
                if (!found)
                {
                    deleteFiles.Add(ckeckFiles[deleteIndex]);
                }
            }
            return deleteFiles;

        }

        internal void DeleteFiles(List<string> deleteFiles)
        {
            for (int index = 0; index < deleteFiles.Count; index++)
            {
                try
                {
                    File.Delete(deleteFiles[index]);
                }
                catch(System.IO.IOException)
                {

                }
            }
        }

        private List<string> GetFilesOfType(string extension)
        {
            return Directory.GetFiles(WorkingDirectory, "*." + extension).ToList();
        }

        private string DeleteExtension(string file, string extension)
        {
            if (file.EndsWith(extension))
            {
                return file.Remove(file.Length - extension.Length - 1);
            }
            else
            {
                return file;
            }
        }
    }
}
