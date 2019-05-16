using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WordSuggestion
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public Trie t;


        public MainPage()
        {
            InitializeComponent();
            t = new Trie();
            List<String> s = new List<string>();

            var assembly = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            System.IO.Stream stream = assembly.GetManifestResourceStream("WordSuggestion.CommonWords.txt");
            using (var reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    s.Add(reader.ReadLine());
                }
            }
            t.generateFromList(s);
            t.generateSuggestions("ab");

            foreach(String ss in t.Suggestions)
            {
                Console.WriteLine(ss);
            }

        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            (sender as Entry).Text = "";
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            String text = (sender as Entry).Text;
            t.generateSuggestions(text);
            filesView.ItemsSource = t.Suggestions;
        }
    }
}
