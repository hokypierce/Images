using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSJ2K;
using Jpeg2000Filetype;
using ImageMagick;
using System.Diagnostics;
using System.IO;

namespace Images
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool showSaveDialog = true;

        /// <summary>
        /// filename of tested image
        /// </summary>
        String fileName;

        int fileSize;

        Stopwatch sw = new Stopwatch();

        String saveFileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show details about loaded image
        /// </summary>
        private void setPodrobnosti()
        {
            using (MagickImage image = new MagickImage(fileName))
            {
                lbPodrobnosti.Content = "Podrobnosti o obrázku: \n";
                lbPodrobnosti.Content += "Vyska: " + image.BaseHeight + "\n";
                lbPodrobnosti.Content += "Sirka: " + image.BaseWidth + "\n";
                lbPodrobnosti.Content += "Hloubka: " + image.Depth + "\n";
                lbPodrobnosti.Content += "Kvalita: " + image.Quality + "\n";
                lbPodrobnosti.Content += "Pocet barev: " + image.TotalColors + "\n";
                lbPodrobnosti.Content += "Velikost souboru: " + image.FileSize + "\n";
            }
        }

        /// <summary>
        /// Open image for test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenImage(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            // dlg.DefaultExt = ".txt";
            //dlg.Filter = "Images (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                fileName = dlg.FileName;

                img.Source = new BitmapImage(new Uri(fileName));
                setPodrobnosti();
                lbVyzva.Visibility = Visibility.Hidden;
                enableButtons();
            }
        }

        private void Quality_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbKvalita2.Content = Slider.Value;
        }

        private void enableButtons()
        {
            btnBMP.IsEnabled = true;
            btnGIF.IsEnabled = true;
            btnJPEG.IsEnabled = true;
            btnJPEG2000.IsEnabled = true;
            btnPCX.IsEnabled = true;
            btnPNG.IsEnabled = true;
            btnTIFF.IsEnabled = true;
            btnWBMP.IsEnabled = true;
            btnWEBP.IsEnabled = true;
            btnXCM.IsEnabled = true;
            btnAll.IsEnabled = true;
        }

        private void saveDialog(string fileType)
        {
            // Create OpenFileDialog
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = fileType;


            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                saveFileName = dlg.FileName;
            }
        }

        #region SAVING METHODS
        /// <summary>
        /// Saving image as JPEG200
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveJPEG2000(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("JPEG2000|*.JP2");

            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Format = MagickFormat.J2k;
                image.Quality = Convert.ToInt32(Slider.Value);
                image.CompressionMethod = CompressionMethod.JPEG2000;
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        /// <summary>
        /// Saving image as GIF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveGIF(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("GIF|*.GIF");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }

            sw.Stop();
            lbUkladani.Content = "Čas Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        /// <summary>
        /// Saving image as PNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePNG(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("PNG|*.PNG");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas Čas ukládání: " + sw.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Saving image as JPEG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveJPEG(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("JPEG|*.JPG");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }

            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        private void SaveBMP(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("BMP|*.BMP");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        private void SaveWEBP(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("WEBP|*.WEBP");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        private void SaveTIFF(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("TIFF|*.TIFF");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        private void SavePCX(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("PCX|*.PCX");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        private void SavePCM(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("PCM|*.PCM");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName);
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        private void SaveWPBMP(object sender, RoutedEventArgs e)
        {
            if (showSaveDialog) saveDialog("WPBMP|*.WPBMP");
            sw.Restart();
            using (MagickImage image = new MagickImage(fileName))
            {
                image.Quality = Convert.ToInt32(Slider.Value);
                image.Write(saveFileName); 
                fileSize = image.FileSize;
            }
            sw.Stop();
            lbUkladani.Content = "Čas ukládání: " + sw.Elapsed.TotalMilliseconds + " ms";
        }

        /// <summary>
        /// Save image into all availible formats and create report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAll(object sender, RoutedEventArgs e)
        {
            showSaveDialog = false;

            // Create OpenFileDialog
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {

            }

            string newLine;
            var csv = new StringBuilder();

            newLine = string.Format("Typ,Velikost souboru,Čas ukládání,{0}", Environment.NewLine);
            csv.Append(newLine);

            var path = System.IO.Path.GetDirectoryName(dlg.FileName);

            saveFileName = System.IO.Path.Combine(path, "output.jp2");
            SaveJPEG2000(sender, e);
            newLine = string.Format("JPEG2000,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.png");
            SavePNG(sender, e);
            newLine = string.Format("PNG,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.jpg");
            SaveJPEG(sender, e);
            newLine = string.Format("JPEG,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.jp2");
            SaveGIF(sender, e);
            newLine = string.Format("GIF,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.bmp");
            SaveBMP(sender, e);
            newLine = string.Format("BMP,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.webp");
            SaveWEBP(sender, e);
            newLine = string.Format("WEBP,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.wpbmp");
            SaveWPBMP(sender, e);
            newLine = string.Format("WPBMP,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.tiff");
            SaveTIFF(sender, e);
            newLine = string.Format("TIFF,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.pcm");
            SavePCM(sender, e);
            newLine = string.Format("PCM,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            saveFileName = System.IO.Path.Combine(path, "output.pcx");
            SavePCX(sender, e);
            newLine = string.Format("PCX,{0},{1}{2}", fileSize, sw.ElapsedMilliseconds, Environment.NewLine);
            csv.Append(newLine);

            File.WriteAllText(System.IO.Path.Combine(path, "report.csv"), csv.ToString());
            showSaveDialog = true;
        }
        #endregion
    }
}
