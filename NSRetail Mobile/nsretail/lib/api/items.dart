class Items {
  String itemCode;
  int itemCodeId;
  int itemId;
  String itemName;
  int itemPriceId;
  double MRP;
  double salesPrice;
  bool isOpenItem;
  Items(
      {this.itemCodeId,
      this.itemCode,
      this.itemId,
      this.itemName,
      this.itemPriceId,
      this.MRP,
      this.salesPrice,
      this.isOpenItem});
  factory Items.fromJson(Map<String, dynamic> parsed) {
    return Items(
        itemCodeId: parsed["itemcodeid"],
        itemCode: parsed["itemcodE1"] as String,
        itemId: parsed["itemid"],
        itemName: parsed["itemname"],
        itemPriceId: parsed["itempriceid"],
        MRP: parsed["mrp"] as double,
        salesPrice: parsed["saleprice"] as double,
        isOpenItem: parsed["isopenitem"]);
  }
}
