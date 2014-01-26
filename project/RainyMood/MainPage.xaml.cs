using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.BackgroundAudio;
using System.IO.IsolatedStorage;
using System.Windows.Resources;

namespace RainyMood
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly AudioTrack audioTrack;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            CopyTrackToIsoStorage();

            // initialize audio track
            audioTrack = new AudioTrack(new Uri("RainyMood.mp3", UriKind.Relative), 
                "Rainy mood",
                null,
                null,
                null,
                null,
                EnabledPlayerControls.Pause);

            // BackgroundAudioPlayer.Instance.Track = audioTrack;
        }

        /// <summary>
        /// copyies track into isolated storage, where it can be accessed by Background Audio Agent
        /// </summary>
        private void CopyTrackToIsoStorage()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storage.FileExists("RainyMood.mp3"))
                {
                    StreamResourceInfo resource = Application.GetResourceStream(new Uri("Sounds/RainyMood.mp3", UriKind.Relative));

                    using (IsolatedStorageFileStream file = storage.CreateFile("RainyMood.mp3"))
                    {
                        int chunkSize = 4096;
                        byte[] bytes = new byte[chunkSize];
                        int byteCount;

                        while((byteCount = resource.Stream.Read(bytes,0,chunkSize)) > 0)
                        {
                            file.Write(bytes, 0, byteCount);
                        }
                    }
                }
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            BackgroundAudioPlayer.Instance.Track = audioTrack;
            BackgroundAudioPlayer.Instance.Play();
        }
    }
}