using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.PickList
{
    public partial class PickListDipatchTrayDetailsViewModel : BaseViewModel
    {
        public PickListDipatchTrayDetailsViewModel(PickListTrayModel pickListTrayModel)
        {
            PickListTrayModel = pickListTrayModel;
        }

        public PickListTrayModel PickListTrayModel { get; }
    }
}
