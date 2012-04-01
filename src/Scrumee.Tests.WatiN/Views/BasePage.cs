using Scrumee.Tests.WatiN.Helpers;
using WatiN.Core;

namespace Scrumee.Tests.WatiN.Views
{
    /// <summary>
    /// The base page to inherit for all Automated UI Tests
    /// </summary>
    /// <remarks>
    /// This class is intended to provide generic methods available to
    /// all Page mappings
    /// </remarks>
    public abstract class BasePage : Page
    {
        private string _partialUrl;
        private string _baseUrl;
        private string _url;

        public string PartialUrl
        {
            get { return _partialUrl ?? ( _partialUrl = GetType().ToString().Replace( "Scrumee.Tests.WatiN.Views", "" ).Replace( ".", "/" ).Replace( "Page", "" ) ); }
            set { _partialUrl = value; }
        }

        public string BaseUrl
        {
            get { return _baseUrl ?? Constants.BaseUrl; }
            set { _baseUrl = value; }
        }

        /// <summary>
        /// Returns the URL to this page using the established conventions
        /// </summary>
        public string Url
        {
            get { return _url ?? BaseUrl + PartialUrl; }
            set { _url = value; }
        }
    }
}
