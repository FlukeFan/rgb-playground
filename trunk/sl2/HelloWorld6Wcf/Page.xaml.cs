using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;

namespace Sl2
{

    [ServiceContract()]
    public interface IMediaService
    {
        [OperationContract]
        IList<Media> GetMediaList(  User            user,
                                    DomainCriteria  mediaCriteria);
        [OperationContract]
        string TempMethodToTestWebServices();
    }

    public class MediaClient : ClientBase<IMediaService>
    {

        private static EndpointAddress GetEa()
        {
            return new EndpointAddress("http://localhost/atlanta/web/services/MediaService.svc");
        }

        private static BasicHttpBinding GetB()
        {
            return new BasicHttpBinding();
        }

        public MediaClient() : base(GetB(),GetEa())
        {
        }

        override protected IMediaService CreateChannel()
        {
            return ChannelFactory.CreateChannel(GetEa());
        }

        public IMediaService Service
        {
            get { return CreateChannel(); }
        }

    }

    public class Page : UserControl
    {

        public Page()
        {
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            IMediaService mediaService = null;
            MediaClient cf = new MediaClient();
            mediaService = cf.Service;
            string v = mediaService.TempMethodToTestWebServices();
            TextBlock tb = (TextBlock)FindName("textResponse");
            tb.Text = v;
        }

    }
}