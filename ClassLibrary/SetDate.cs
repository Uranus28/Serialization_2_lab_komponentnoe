using PluginInterface;
using System;
using System.Drawing;
using System.Globalization;
using Windows.Devices.Geolocation;
using System.Device.Location;

namespace ClassLibrary
{
    [Version(1, 0)]
    class SetDate : IPlugin
    {
        public GeoCoordinateWatcher Watcher = null;
        string shirDolgot;
        public string Name
        {
            get { return "Добавить дату на изображение"; }
        }

        public string Author
        {
            get { return "Me"; }
        }
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {

            // Display the latitude and longitude.
            if (Watcher.Position.Location.IsUnknown)
            {
                shirDolgot = "\n Cannot find location data";
            }
            else
            {
                shirDolgot = "\nshirota: " + Watcher.Position.Location.Latitude.ToString() + '\n' + "" +
                "dolgota: " + Watcher.Position.Location.Longitude.ToString();
            }
        }
        public void Transform(IBitMap map)
        {
            var bitmap = map.Image;
            Watcher = new GeoCoordinateWatcher();
            //Watcher.StatusChanged += Watcher_StatusChanged;
            Watcher.Start();
            if (Watcher.Position.Location.IsUnknown)
            {
                shirDolgot = "\n Cannot find location data";
            }
            else
            {
                shirDolgot = "\nshirota: " + Watcher.Position.Location.Latitude.ToString() + '\n' + "" +
                "dolgota: " + Watcher.Position.Location.Longitude.ToString();
            }
            Watcher.Stop();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            timestamp += shirDolgot;
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawString(timestamp, new Font("Arial", 20), //текст на картинке, шрифт и его размер
            new SolidBrush(Color.Black), bitmap.Width - 350, bitmap.Height - 120); //цвет и расположение текста на изображении
            map.Image = bitmap;
        }
    }
}

