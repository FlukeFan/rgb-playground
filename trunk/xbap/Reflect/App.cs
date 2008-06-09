
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

using Atlanta.Application.Domain.Lender;

namespace Xbap
{

    public class MediaDto : Media
    {
/*        public MediaDto(Media media)
        {
            Type = media.Type;
            Name = media.Name;
            Description = media.Description;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }*/
        
        public static void SetName(Media media, string newName)
        {
            media.Name = newName;
        }
    }

    public class App : Application
    {

        // Entry point method
        [STAThread]
        public static void Main()
        {
            Media media = Media.InstantiateOrphanedMedia(MediaType.Dvd, "test name", "test description");

            //media.SetProtectedProperty("Name", "reflect test");
            //Type type = Type.GetType("Atlanta.Application.Domain.Lender.Media, Atlanta.Application.Domain");
            //PropertyInfo pi = type.GetProperty("Name");

            //pi.SetValue(media, "reflected test", null);

            //Media mediaDto = new MediaDto(media);
            //((MediaDto)mediaDto).SetName("modified name");
            MediaDto.SetName(media, "sneakily modified name");

            MessageBox.Show("Reflect test: " + media.Name);
            App app = new App();
            //app.Run();
        }

    }
}