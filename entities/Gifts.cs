using entity.goods;

namespace entity.gifts {
class Gifts {
    private List<Goods> gift;
    private decimal volumeGift;
    private decimal priceGift;

    public Gifts(List<Goods> gift){
        this.gift = sortListGoods(gift);
        this.volumeGift = encountVolumeGift(this.gift);
        this.priceGift = encountPriceGift(this.gift);
    }
    

    public decimal VolumeGift { get => volumeGift; set => volumeGift = value; }
    public decimal PriceGift { get => priceGift; set => priceGift = value; }
    public List<Goods> Gift { get => gift; set => gift = value; }

    private decimal encountVolumeGift(List<Goods> giftList){
        decimal volume = 0m;
        foreach (Goods good in giftList){
            volume = volume + good.PresentVolume;
        }
        return volume;
    }


    private decimal encountPriceGift(List<Goods> giftList){
        decimal price = 0m;
        foreach (Goods good in giftList){
            price = price + good.PresentPrice;
        }
        return price;
    }


    private List<Goods> sortListGoods(List<Goods> unsortedList){
        unsortedList.Sort((x, y) => x.IdGood.CompareTo(y.IdGood));
        return unsortedList;
    }


    public override bool Equals(object? gift){
        Gifts giftNew;
        if (this.GetHashCode() == gift?.GetHashCode()){
            return true;
        }
        if (gift is Gifts){
            giftNew = (Gifts)gift;

            if (giftNew.Gift.Count == this.Gift.Count) {
                List<Goods> thisGoods = this.Gift;
                List<Goods> giftGoods = giftNew.Gift;
                for (int i = 0; i < thisGoods.Count; i++){
                    if (!thisGoods[i].Equals(giftGoods[i])){
                        return false;
                    }
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
        return true;
    }


    public override int GetHashCode(){
        return base.GetHashCode();
    }


    public override string? ToString(){
        string strOut = "";
        foreach (Goods good in this.gift){
            strOut = strOut + good.IdGood + " " + good.GoodName + " " + good.PresentPrice + "\n";
        }
        return strOut + "/n price gift = " + priceGift + "\n volume gift = " + volumeGift;
    }


}
}