using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemReader.Models
{
    internal class GenshinItem {
        public Bitmap ItemImage { get; set; }
        public string ItemName { get; set; }
        public Bitmap AmountImage { get; set; }
        public int Amount { get; set; }

        public GenshinItem() {}

        public GenshinItem(Bitmap _ItemImage, Bitmap _AmountImage, string _ItemName, int _Amount)
        {
            ItemImage = _ItemImage;
            ItemName = _ItemName;
            AmountImage = _AmountImage;
            Amount = _Amount;
        }
    }
}
