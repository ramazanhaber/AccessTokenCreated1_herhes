﻿using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Windows.Forms;

namespace AccessTokenCreated1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeFirebase();
        }
        private void InitializeFirebase()
        {
            try
            {
                string jsonPath = "adminsdk.json"; // JSON dosyanızın yolu
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

                var credential = GoogleCredential.FromFile(jsonPath).CreateScoped(new[]
                {
         "https://www.googleapis.com/auth/firebase.messaging"
     });

                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = credential,
                    });
                }

                var token = credential.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;

                Clipboard.SetText(token);

                textBox1.Text = token;
                MessageBox.Show("Panoya Kopyalandi -> Access Token: " + token);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Firebase: " + ex.Message);
            }
        }

    }
}
