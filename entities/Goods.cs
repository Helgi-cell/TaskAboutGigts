namespace entity.goods {
class Goods
{
    private long idGood;
    private string goodName;
    private decimal presentVolume;
    private decimal presentPrice;
    private int quantity;

    public Goods()
    {
        this.goodName = "";
    }

    public Goods(long idGood, string goodName, decimal presentVolume
                   , decimal presentPrice, int quantity)
    {
        this.idGood = idGood;
        this.goodName = goodName;
        this.presentVolume = presentVolume;
        this.presentPrice = presentPrice;
        this.quantity = quantity;
    }

    public int Quantity { get => quantity; set => quantity = value; }
    public long IdGood { get => idGood; set => idGood = value; }
    public string GoodName { get => goodName; set => goodName = value; }
    public decimal PresentVolume { get => presentVolume; set => presentVolume = value; }
    public decimal PresentPrice { get => presentPrice; set => presentPrice = value; }

    public override string ToString()
    {
        return "Goods{\n" +
                "idGood =" + this.idGood +
                ", goodName ='" + this.goodName + ' ' +
                ", presentVolume =" + this.presentVolume +
                ", presentPrice =" + this.presentPrice +
                ", quantity =" + this.quantity +
                '}' + "\n----------------------------------\n"; ;
    }

    public override bool Equals(object? obj)
    {
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Goods))
            {
                return false;
            }
            return (this.goodName == ((Goods)obj).goodName)
                && (this.presentPrice == ((Goods)obj).presentPrice)
                && (this.presentVolume == ((Goods)obj).presentVolume)
                && (this.quantity == ((Goods)obj).quantity)
                && (this.idGood == ((Goods)obj).idGood);
        }
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
}