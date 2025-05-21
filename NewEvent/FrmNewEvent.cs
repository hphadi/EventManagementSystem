using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace NewEvent
{
    public partial class FrmNewEvent : Form
    {
        public FrmNewEvent()
        {
            InitializeComponent();
        }

        private async void BtnSubmit_Click(object sender, EventArgs e)
        {
            var NewEvent = new Event
            {
                Title = txtTitle.Text,
                Description = rtxtDescription.Text,
                StartDate = dtmPickerStartDate.Value,
                EndDate = dtmPickerEndDate.Value,
                Location = txtLocation.Text,

            };
            var json = JsonConvert.SerializeObject(NewEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 3. Send to ASP.NET API
            using (var client = new HttpClient())
            {
                try
                {
                    string apiUrl = "http://localhost:5144/api/event"; // replace with your actual URL
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Event submitted successfully!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error submitting event: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API call failed: " + ex.Message);
                }
            }

        }
    }
}
