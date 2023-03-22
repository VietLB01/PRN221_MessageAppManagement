using MessageApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.ViewModels
{
    public class ViewModel
    {
        public ObservableCollection<StatusDataModel> statusThumbsCollection;
        public ViewModel()
        {
            statusThumbsCollection = new ObservableCollection<StatusDataModel>
            {
                new StatusDataModel
                {
                    IsMeAddStatus= true
                },
                new StatusDataModel
                {
                    ContactName = "Mike",
                    ContactPhoto = new Uri("/asserts/1.png", UriKind.RelativeOrAbsolute),
                    StatusImage =new Uri("/asserts/5.png", UriKind.RelativeOrAbsolute),
                    IsMeAddStatus= false,

                },
                 new StatusDataModel
                {
                    ContactName = "Steve",
                    ContactPhoto = new Uri("/asserts/2.png", UriKind.RelativeOrAbsolute),
                    StatusImage =new Uri("/asserts/8.png", UriKind.RelativeOrAbsolute),
                    IsMeAddStatus= false,

                },
                  new StatusDataModel
                {
                    ContactName = "Will",
                    ContactPhoto = new Uri("/asserts/3.png", UriKind.RelativeOrAbsolute),
                    StatusImage =new Uri("/asserts/5.png", UriKind.RelativeOrAbsolute),
                    IsMeAddStatus= false,

                },
            };
        }
    }
}

