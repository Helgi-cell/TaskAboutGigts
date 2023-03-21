using entity.gifts;
using entity.taskcondition;

namespace entity.giftsinbag {

 class GiftsInBag {
    
    #nullable disable
    private List<Gifts> bagWithGifts;

    private decimal deltaToBudget;
    
    #nullable disable
    private TaskConditions taskConditions;



    public GiftsInBag(List<Gifts> bagWithGift, TaskConditions taskCondition )
    {
        this.TaskConditions = taskCondition;
        this.BagWithGifts = bagWithGift;
        this.DeltaToBudget = encountDeltaBudget();
        
    }

    public decimal DeltaToBudget { get => deltaToBudget; set => deltaToBudget = value; }
    public List<Gifts> BagWithGifts { get => bagWithGifts; set => bagWithGifts = value; }
    public TaskConditions TaskConditions { get => taskConditions; set => taskConditions = value; }
   
    public decimal howVolumeBag(List<Gifts> bagGifts){
        decimal volumeBag = 0.0m;
        foreach (Gifts gift in bagGifts) {
            volumeBag = volumeBag + gift.VolumeGift;
        }
        return volumeBag;
    }

    public decimal howPriceBag(List<Gifts> bagGifts){
        decimal priceBag = 0.0m;
        foreach (Gifts gift in bagGifts) {
            priceBag = priceBag + gift.PriceGift;
        }
        return priceBag;
    }

    private Boolean isEncountVolumeBag(List<Gifts> bagGifts) {
        decimal volumeBag = 0.0m;
        foreach (Gifts gift in bagGifts) {
            volumeBag = volumeBag + gift.VolumeGift;
        }
        if (taskConditions.BagVolume >= volumeBag){
            return true;
        } else {
            return false;
        }
    }

    private Boolean isEncountPriceBag(List<Gifts> bagGifts) {
       decimal priceBag = 0.0m;
        foreach (Gifts gift in bagGifts) {
            priceBag = priceBag + gift.PriceGift;
        }
        if (taskConditions.Budget >= priceBag) {
            return true;
        } else {
            return false;
        }
    }

    private decimal encountDeltaBudget() {
        decimal delta = 0.0m;
        if (isEncountPriceBag(this.bagWithGifts) && isEncountVolumeBag(this.bagWithGifts)){

            foreach (Gifts gift in this.bagWithGifts ) {
                delta = delta + gift.PriceGift;
            }
            if (delta > this.taskConditions.Budget){
                
                return this.taskConditions.Budget + 100m;
            } else{
                return this.taskConditions.Budget - delta;
            }
        } else {

            return this.taskConditions.Budget + 100m;
        }

    }



  private GiftsInBag cloneGiftsInBag(GiftsInBag giftsInBag){
        List<Gifts> newListGifts = new List<Gifts>();
        foreach (Gifts gift in giftsInBag.BagWithGifts) {
            newListGifts.Add(gift);
        }
        return new GiftsInBag(newListGifts, this.taskConditions);
    }

    public override bool Equals(object obj)
    {
        GiftsInBag objGifts = (GiftsInBag) obj;
        if (this == obj) return true;
        if (!(obj is GiftsInBag)) return false;
        if (this.bagWithGifts.Count > 0 &&  objGifts.bagWithGifts.Count > 0){
            GiftsInBag externalGiftInBag = cloneGiftsInBag( objGifts);
            GiftsInBag internalGiftInBag = cloneGiftsInBag(this);

            foreach (Gifts internalGift in internalGiftInBag.BagWithGifts) {
                foreach (Gifts externalGift in externalGiftInBag.bagWithGifts){
                      if (internalGift.Equals(externalGift)){
                          internalGiftInBag.bagWithGifts.Remove(internalGift);
                          externalGiftInBag.bagWithGifts.Remove(externalGift);
                          if (externalGiftInBag.bagWithGifts.Count == 0  && internalGiftInBag.bagWithGifts.Count == 0){
                              return true;
                          } else {
                              return internalGiftInBag.Equals(externalGiftInBag);
                          }
                      }
                }
            }
        }

       return false;
    }


    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }



    public override string ToString()
    {

string strOut = "";
        foreach (Gifts gift in this.bagWithGifts){
            strOut = strOut + gift.Gift + " " + gift.PriceGift + " " + gift.VolumeGift + "\n";
        }

        return strOut + "\ndelta = " + this.DeltaToBudget;
    }
}
}