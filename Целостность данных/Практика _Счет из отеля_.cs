using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting;

class AccountingModel : ModelBase
{
    private double price;
    private int nightsCount;
    private double discount;

    public double Price
    {
        get { return price; }
        set
        {
            if (value < 0) throw new ArgumentException();
            price = value;
            Notify(nameof(Price));
            Notify(nameof(Total));
        }
    }

    public int NightsCount
    {
        get { return nightsCount; }
        set
        {
            if (value <= 0) throw new ArgumentException();
            nightsCount = value;
            Notify(nameof(NightsCount));
            Notify(nameof(Total));
        }
    }

    public double Discount
    {
        get { return discount; }
        set
        {
            if (price * nightsCount * (1 - value / 100) < 0) throw new ArgumentException();
            discount = value;
            Notify(nameof(Discount));
            Notify(nameof(Total));
        }
    }

    public double Total
    {
        get
        {
            return price * nightsCount * (1 - discount / 100);
        }
        set
        {
            if (value < 0 || price == 0 || nightsCount == 0) throw new ArgumentException();
            discount = 100 * (1 - value / (price * nightsCount));
            Notify(nameof(Discount));
            Notify(nameof(Total));
        }
    }
}
