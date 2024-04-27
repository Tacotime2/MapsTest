using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Attach event handlers
            this.Load += new System.EventHandler(this.Form1_Load);
            gmapControl.MouseClick += new MouseEventHandler(gmapControl_MouseClick);
        }


        GMapOverlay markersOverlay = new GMapOverlay("markers");


        private void Form1_Load(object sender, EventArgs e)
        {
            //Set the map to Google Satellite view
            gmapControl.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmapControl.Position = new GMap.NET.PointLatLng(43.380240, -95.147795);

            //Configure the zoom settings
            gmapControl.MinZoom = 5;
            gmapControl.MaxZoom = 20;  //Higher zoom level for better satellite details
            gmapControl.Zoom = 10;
            gmapControl.CanDragMap = true;
            gmapControl.DragButton = MouseButtons.Left;

        }

        private void AddMarker(double lat, double lng, string tooltipText)
        {
            var markersOverlay = new GMap.NET.WindowsForms.GMapOverlay("markers");
            var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                new GMap.NET.PointLatLng(lat, lng),
                GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);

            marker.ToolTipText = tooltipText;
            markersOverlay.Markers.Add(marker);
            gmapControl.Overlays.Add(markersOverlay);
        }

        private void gmapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //Checks if the right mouse button was clicked
            {
                var point = gmapControl.FromLocalToLatLng(e.X, e.Y);

                //Coordinates on click
                double lat = point.Lat;
                double lng = point.Lng;

                AddMarker(lat, lng, "New Marker");

                gmapControl.Refresh();

                MessageBox.Show($"Latitude: {lat}, Longitude: {lng}");
            }
        }
    }
}
