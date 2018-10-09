using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace http
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _path;
        List<string> _hrefList;


        public MainWindow()
        {
            InitializeComponent();
            _hrefList = new List<string>();
        }

        private void SelectPath(object sender, RoutedEventArgs e)
        {
            SaveFileDialog win = new SaveFileDialog();
            win.FileName = "html_file";
            win.Filter = "(*.html)|*.html";
            if (win.ShowDialog() == true)
            {
                Path.Text = win.FileName;
                _path = Path.Text.Substring(0, Path.Text.LastIndexOf("."));
            }
        }

        private void DownloadPage()
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string data;
                Dispatcher.Invoke(() => { request = (HttpWebRequest)WebRequest.Create(URL.Text); });
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                        data = streamReader.ReadToEnd();

                    FileStream fs = null;
                    Dispatcher.Invoke(() =>
                    {
                        fs = new FileStream(Path.Text,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);
                    });

                    using (StreamWriter sw = new StreamWriter(fs))
                        sw.Write(data);
                    fs.Close();
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        private void DownloadPage(string path, string url, int fileNumber)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string data = null;
                string filePath = null;

                Dispatcher.Invoke(() => { request = (HttpWebRequest)WebRequest.Create(URL.Text); });
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                        data = streamReader.ReadToEnd();

                    if (fileNumber != 0)
                        filePath = $"{path}_{fileNumber}.html";
                    else if (fileNumber == 0)
                        Dispatcher.Invoke(() => { filePath = Path.Text; });

                    Interlocked.Increment(ref fileNumber);

                    FileStream fs = null;
                    Dispatcher.Invoke(() =>
                    {
                        fs = new FileStream(filePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);
                    });

                    using (StreamWriter sw = new StreamWriter(fs))
                        sw.Write(data);
                    fs.Close();

                    GetLinks(path ,filePath, url, fileNumber);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        bool IsContain(string link)
        {
            foreach (var item in _hrefList)
            {
                if (item == link)
                    return false;
            }
            return true;
        }

        private void GetLinks(string path, string filePath, string url, int fileNumber)
        {
            try
            {
                Dispatcher.Invoke(() => { url = URL.Text; });

                Regex protocols = new Regex("(http|https|ftp|file|mailto):");

                var doc = new HtmlDocument();
                doc.Load(filePath);
                var list = doc.DocumentNode.Descendants("a").AsParallel()
                                      .Select(a => a.GetAttributeValue("href", null))
                                      .Where(href => !string.IsNullOrEmpty(href) && !href.Equals("#") && !href.Equals("/"));
                foreach (var item in list)
                {
                    if (protocols.IsMatch(item))
                    {
                        if (item.Contains(url))
                        {
                            if (IsContain(item))
                            {
                                Dispatcher.Invoke(() => { _hrefList.Add(item); });
                                DownloadPage(path, item, fileNumber);
                            }
                        }
                    }
                    else
                    {
                        string newHref = null;
                        Dispatcher.Invoke(() => { newHref = $"{url}{item}"; });
                        if (IsContain(item))
                        {
                            Dispatcher.Invoke(() => { _hrefList.Add(item); });
                            DownloadPage(path, newHref, fileNumber);
                        }
                    }
                }

            }
            catch (WebException ex)
            {
                HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Error 404: not found");
                    return;
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Recursion.IsChecked.Equals(false))
                    Task.Run(() => DownloadPage());
                else
                {
                    string url = URL.Text;
                    Dispatcher.Invoke(() => { Task.Run(() => DownloadPage(_path, url, 0)); });
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
