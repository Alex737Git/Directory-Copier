using Microsoft.VisualBasic.FileIO;
using Shop_dbfirst.Infranstructure;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryCopier.Models
{
    public class DirectoryCopierViewModel
    {
        public RelayCommand ChooseFolder { get; set; }


        public DirectoryCopierViewModel()
        {
            ChooseFolder = new RelayCommand(o => ChooseFolderFileDialog());
        }




        private async void ChooseFolderFileDialog()
        {

            string sourceDir = "";
            string destinationDir = "";

            using (var sourceFolder = new FolderBrowserDialog())
            {
                DialogResult result = sourceFolder.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(sourceFolder.SelectedPath))
                {
                    string[] files = Directory.GetFiles(sourceFolder.SelectedPath);
                    sourceDir = sourceFolder.SelectedPath;
                }
            }

            if (sourceDir == "")
                return;

            using (var destinationFolder = new FolderBrowserDialog())
            {
                DialogResult result = destinationFolder.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(destinationFolder.SelectedPath))
                {
                    string[] files = Directory.GetFiles(destinationFolder.SelectedPath);
                    destinationDir = destinationFolder.SelectedPath;

                    await Task.Run(() =>
                    {
                        try
                        {
                            FileSystem.CopyDirectory(sourceDir, destinationDir,
                            UIOption.AllDialogs);
                        }
                        catch (System.Exception) { }
                    });
                }
            }
        }



    }
}
